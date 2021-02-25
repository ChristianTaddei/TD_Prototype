Shader "Unlit/ColorShader"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.5

            struct v2f {
                float4 pos : SV_POSITION;
            };

            v2f vert (
                float4 vertex : POSITION
                )
            {
                v2f o;
                o.pos = UnityObjectToClipPos(vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 worldPos = mul(unity_ObjectToWorld, float4(i.pos.xyz, 1.0)).xyz;
                return half4(worldPos.y, 0, 0, 0);
            }
            ENDCG
        }
    }
}