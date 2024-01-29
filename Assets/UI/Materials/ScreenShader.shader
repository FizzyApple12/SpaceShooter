Shader "Unlit/ScreenShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 fixed_uv = floor(i.uv * float2(WIDTH, HEIGHT)) / float2(WIDTH, HEIGHT);

                float2 fract_uv = frac(i.uv * float2(WIDTH, HEIGHT)) - float2(0.5, 0.5);

                // sample the texture
                float4 col = tex2D(_MainTex, fixed_uv);

                return col * clamp((1.0 - sqrt(fract_uv.x * fract_uv.x + fract_uv.y * fract_uv.y)), 0.0, 1.0);
            }
            ENDCG
        }
    }
}
