Shader "Unlit/GroundGrid"
{
    Properties
    {
        _Color ("Color", Color) = (0, 0, 0)
        _Thickness ("Thickness", Range(1, 10)) = 1
        _Fade("Fade", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            fixed4 _Color;
            float _Thickness;
            float _Fade;

            v2f vert (appdata v)
            {
                v2f o;

                o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = _Color;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                float distance = length(i.uv - float2(0.5, 0.5));

                float2 uv = frac(i.worldPos.xz);
                uv -= float2(0.5, 0.5);
                uv *= 2;

                float grid = max(abs(uv.x), abs(uv.y)) > 1 - _Thickness * 0.01;

                float falloff = 1 - (distance * distance) * 4;
                falloff = saturate(falloff);

                grid *= falloff;
                grid *= 1 - _Fade;

                col.a *= grid;

                return col;
            }
            ENDCG
        }
    }
}
