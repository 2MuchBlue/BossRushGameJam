Shader "Screen/Bloom"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BloomRadius ("Bloom Radius", float) = 5
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

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float _BloomRadius;

            float4 BoxBlur(float2 center, float radius, float2 blurAxis = float2(1, 0)){
                //float oneOverRadius = 1.0 / radius;
                float sampleCount = 0;
                float4 collectiveColor = float4(0.0, 0.0, 0.0, 0.0);
                float actualRadius = floor(radius);
                for(float a = -actualRadius; a < actualRadius; a++){
                    collectiveColor += tex2D(_MainTex, center + (float2(a * _MainTex_TexelSize.x, a * _MainTex_TexelSize.y) * blurAxis));
                    sampleCount++;
                }
                
                return (collectiveColor / sampleCount);
            }

            float getBrightnessOfColor(float3 col ){
                return ((col.r * 0.21) + (col.g * 0.72) + (col.b * 0.07));
            }

            float4 BloomBlur(float2 center, float radius){
                //float oneOverRadius = 1.0 / radius;
                float sampleCount = 1;
                float4 collectiveColor = float4(0.0, 0.0, 0.0, 0.0);
                float actualRadius = floor(radius);
                for(float y = -actualRadius; y < actualRadius; y++){
                    for(float x = -actualRadius; x < actualRadius; x++){
                        float4 offsetColor = tex2D(_MainTex, center + (float2(x * _MainTex_TexelSize.x, y * _MainTex_TexelSize.y)));
                        collectiveColor += offsetColor * getBrightnessOfColor(offsetColor);
                        sampleCount++;
                    }
                }
                
                return (collectiveColor / sampleCount);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                col += BloomBlur(i.uv, _BloomRadius);

                return col;
            }
            ENDCG
        }
    }
}
