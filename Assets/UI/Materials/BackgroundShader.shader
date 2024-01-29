Shader "Unlit/ScreenShader"
{
    Properties
    {
        _EnemyHitMult ("Enemy Hit Multiplier", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #define WIDTH 1600
            #define HEIGHT 240

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;

                UNITY_VERTEX_INPUT_INSTANCE_ID 
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;

                UNITY_VERTEX_OUTPUT_STEREO 
            };

            float _EnemyHitMult;

            v2f vert (appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float3 col = 0.5 + 0.5 * cos(_Time.y + i.uv.xyx + float3(0, 2, 4));

                return float4(col * (0.25 + (_EnemyHitMult * 0.75)), 1);
            }
            ENDCG
        }
    }
}
