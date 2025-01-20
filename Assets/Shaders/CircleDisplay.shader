Shader "Unlit/CircleDisplay"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Health Color", Color) = (0.0, 1.0, 0.0, 1.0)
        _BadColor ("Low Health Color", Color) = (1.0, 0.0, 0.0, 1.0)
        _Progress ("Progress", float) = 0
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _BadColor;
            float _Progress;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                
                if(distance(i.uv, float2(0.5, 0.5)) > 0.5){
                    discard;
                }

                float PI = 3.14159265359;
                // sample the texture
                fixed4 col = _Color;

                float angleAdded = 0;

                //float2 targetPoint = float2(cos(_Progress * 2 * PI + angleAdded), sin(_Progress * 2 * PI + angleAdded));

                float proDot = dot(
                    normalize(
                        i.uv - float2(0.5, 0.5)
                    ), 
                    float2(0, 1)
                );

                float angle = acos(
                    proDot
                );

                float t = (angle) / PI;

                if(t < _Progress){
                    discard;
                }

                col = ((1 - _Progress) * _Color + (_Progress * _BadColor));

                return col;
            }
            ENDCG
        }
    }
}
