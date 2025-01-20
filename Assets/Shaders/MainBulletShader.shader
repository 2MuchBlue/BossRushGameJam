Shader "Unlit/MainBulletShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _MainColor ("Main Color", Color) = (0, 0, 0, 1)
        _LightColor ("Light Color", Color) = (0, 0, 0, 1)
        _LightDirection ("Light Durection", Vector) = (1, 0, 0)
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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _MainColor;
            float4 _LightColor;
            float3 _LightDirection;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.normal = v.normal;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 localNormal = mul(unity_ObjectToWorld, i.normal);
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                float lightMult = dot(normalize(_LightDirection.xyz), normalize(localNormal.xyz)) * 0.5 + 0.5;
                col = (_LightColor * lightMult) + (_MainColor * (1.0 - lightMult));
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
