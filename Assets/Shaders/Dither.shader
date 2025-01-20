Shader "Screen/Dither"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorList ("Colors", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float mod(float x, float y)
            {
                return x - y * floor(x/y);
            }

            float4 posterizeSeprate(float4 color, float steps){
                return (floor(color * (steps - 1.0) + 0.5) / (steps - 1.0));
            }

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float4 _MainTex_ST;
            float4x4 _DitherMat;

            float ditherPixSize = 1.0;

            float N = 4.0; // the width of the dither matrix (2.0 for mat2, 4.0 for mat4, etc)

            float4x4 ditherMat = { 
                0.0, 12.0, 3.0, 15.0, 
                8.0, 4.0, 11.0, 7.0, 
                2.0, 14.0, 1.0, 13.0, 
                10.0, 6.0, 9.0, 5.0 
            };

            fixed4 frag (v2f i) : SV_Target
            {
                float Nsquared = N * N; // N^2 precomuted so we don't have to keep doing this.

                float ditherAmount = ditherMat[
                    int( mod( i.uv.y * _MainTex_TexelSize.z * 1.0, 4.0 ) )
                ][
                    int( mod( i.uv.x * _MainTex_TexelSize.w * 1.0, 4.0 ) )
                ];

                fixed4 col = float4(posterizeSeprate(
                    tex2D( _MainTex, i.uv ) +
                    1.0 * ((ditherAmount / 16.0) - 0.5), 4.0 
                ).xyz, 1.0);
                return col;
            }
            ENDCG
        }
    }
}
