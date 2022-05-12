Shader "Unlit/DistanceLight"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Density ("Light Density", Range(0.0, 10.0)) = 7
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
                float4 worldPos : TEXCOORD2;

            };

            sampler2D _MainTex;
            float _Density;
            float4 _MainTex_ST;
            float dist; 

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                dist = distance(i.worldPos, _WorldSpaceCameraPos);

                float4 c = col / dist * _Density; 
                col = clamp(c, c, 1.0);
                return col;// / dist * 7;
            }
            ENDCG
        }
    }
}
