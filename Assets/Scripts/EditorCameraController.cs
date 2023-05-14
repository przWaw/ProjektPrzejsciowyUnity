 using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class EditorCameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraBasePivot = null;
    [SerializeField] private Transform cameraRotationXPivot = null;
    [SerializeField] private Transform cameraHead = null;
    [SerializeField] private MeshRenderer gridRenderer = null;
    [Header("Speed")]
    [SerializeField] private float movementSpeed = 5;
    [SerializeField] private float verticalMovementSpeed = 10;
    [SerializeField] private float zoomSpeed = 10;
    [SerializeField] private Vector2 rotationSpeed = new Vector2(5, 5);
    [Header("Smoothing")]
    [SerializeField] private float moveSpeedSmoothing = 1;
    [SerializeField] private float timeBeforeGridFadesOut = 3;
    [SerializeField] private float gridFadeOutDuration = 0.5f;
    [SerializeField] private float gridFadeInDuration = 0.5f;
    [Header("Zoom Constraints")]
    [SerializeField] private float maxZoom = -5;

    private Vector3 currentMoveSpeed;
    private Vector3 moveVelocity;

    private bool wasDragging = false;
    private Vector3 lastGrabPosition;
    private Vector2 lastPointerPosition;

    private float fade = 1;
    private bool gridFadedIn = false;
    private float timeSinceMove = 0;
    private Tween gridFadeTween;

    private void Start()
    {
        UpdateFade();
    }

    private void Update()
    {
        var input = ReadInput();

        HandleMoveAndDrag(input);
        HandleZoom(input);
        HandleRotation(input);

        var isNotIdle = CheckIfNotIdle(input);

        if (isNotIdle)
        {
            timeSinceMove = 0;

            if (!gridFadedIn)
            {
                gridFadedIn = true;

                gridFadeTween.Kill();
                gridFadeTween = DOTween.To(() => fade, v => fade = v, 0, gridFadeInDuration)
                    .OnUpdate(UpdateFade);
            }
        }
        else
        {
            timeSinceMove += Time.deltaTime;

            if (timeSinceMove > 3f && gridFadedIn)
            {
                gridFadedIn = false;

                gridFadeTween.Kill();
                gridFadeTween = DOTween.To(() => fade, v => fade = v, 1, gridFadeOutDuration)
                    .OnUpdate(UpdateFade);
            }
        }

        lastPointerPosition = input.PointerPosition;
    }

    private bool CheckIfNotIdle(InputData input)
    {
        bool wantsAnything = input.WantsToDrag || input.WantsToZoom || input.WantsToRotate;
        bool isMoving = input.Move.magnitude > 0.01 || Mathf.Abs(input.VerticalMove) > 0.01;
        bool isMovingMouseWhileWantingANything = (input.PointerPosition - lastPointerPosition).magnitude > 5 && wantsAnything;
        return isMoving || isMovingMouseWhileWantingANything;
    }

    private void HandleRotation(InputData input)
    {
        if (input.WantsToRotate)
        {
            var pointerDelta = input.PointerPosition - lastPointerPosition;
            var angularDelta = Vector2.Scale(pointerDelta, rotationSpeed);

            cameraBasePivot.localRotation *= Quaternion.AngleAxis(angularDelta.x, Vector3.up);
            cameraRotationXPivot.localRotation *= Quaternion.AngleAxis(-angularDelta.y, Vector3.right);
        }
    }

    private void HandleMoveAndDrag(InputData input)
    {
        HandleMovement(input);
        HandleDragging(input, out var grabPositionDelta);

        cameraBasePivot.position += (currentMoveSpeed * Time.deltaTime) + grabPositionDelta;
    }

    private void HandleZoom(InputData input)
    {
        if (input.WantsToZoom)
        {
            var pointerDelta = input.PointerPosition - lastPointerPosition;

            var zoomDelta = pointerDelta.y * zoomSpeed;
            cameraHead.localPosition += Vector3.forward * zoomDelta;
            if (cameraHead.localPosition.z > maxZoom)
            {
                cameraHead.localPosition = new Vector3(0, 0, maxZoom);
            }
        }
    }

    private void HandleDragging(InputData input, out Vector3 positionDelta)
    {
        positionDelta = Vector3.zero;

        if (input.WantsToDrag)
        {
            var direction = cameraBasePivot.position - Camera.main.transform.position;
            var groundPlane = new Plane(direction, cameraBasePivot.position);

            if (!groundPlane.Raycast(input.PointerRay, out float d))
            {
                Debug.LogError("It should never happen.");
                return;
            }

            var grabPosition = input.PointerRay.GetPoint(d);

            if (wasDragging)
            {
                var grabDelta = grabPosition - lastGrabPosition;
                positionDelta = -grabDelta;

                grabPosition -= grabDelta;
            }

            lastGrabPosition = grabPosition;
        }

        wasDragging = input.WantsToDrag;
    }

    private void HandleMovement(InputData input)
    {
        var planeMoveVelocity = movementSpeed * cameraBasePivot.TransformDirection(new Vector3(input.Move.x, 0, input.Move.y));
        var verticalMoveVelocity = verticalMovementSpeed * input.VerticalMove * cameraBasePivot.up;
        if (input.WantsToVertical)
        {
            var pointerDelta = input.PointerPosition - lastPointerPosition;

            var zoomDelta = pointerDelta.y * zoomSpeed;
            verticalMoveVelocity = -verticalMovementSpeed * zoomDelta * cameraBasePivot.up;
        }
        var targetMoveSpeed = planeMoveVelocity + verticalMoveVelocity;
        currentMoveSpeed = Vector3.SmoothDamp(currentMoveSpeed, targetMoveSpeed, ref moveVelocity, moveSpeedSmoothing);
    }

    private void UpdateFade()
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        gridRenderer.GetPropertyBlock(block);

        block.SetFloat("_Fade", fade);

        gridRenderer.SetPropertyBlock(block);
    }

    private InputData ReadInput()
    {
        float moveX =
            (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) -
            (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0);

        float moveZ =
            (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) -
            (Input.GetKey(KeyCode.DownArrow) ? 1 : 0);


        float moveY = Input.mouseScrollDelta.y;


        return new InputData(
            new(moveX, moveZ),
            moveY,
            Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftAlt),
            Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftAlt) || Input.GetMouseButton(2),
            Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.LeftShift),
            Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftAlt),
            Input.mousePosition,
            Camera.main.ScreenPointToRay(Input.mousePosition)
        );
    }

    private readonly struct InputData
    {
        public readonly Vector2 Move;
        public readonly float VerticalMove;
        public readonly bool WantsToRotate;
        public readonly bool WantsToDrag;
        public readonly bool WantsToZoom;
        public readonly bool WantsToVertical;
        public readonly Vector2 PointerPosition;
        public readonly Ray PointerRay;

        public InputData(Vector2 move, float verticalMove, bool wantsToRotate, bool wantsToDrag, bool wantsToZoom, bool wantsToVertical ,Vector2 pointerPosition, Ray pointerRay)
        {
            Move = move;
            VerticalMove = verticalMove;
            WantsToRotate = wantsToRotate;
            WantsToDrag = wantsToDrag;
            WantsToZoom = wantsToZoom;
            WantsToVertical = wantsToVertical;
            PointerPosition = pointerPosition;
            PointerRay = pointerRay;
        }
    }
}
