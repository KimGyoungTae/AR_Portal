Shader "Custom/PortalMask"
{
    Properties
    {
        
    }
    SubShader
    {
        Tags 
        { 
            
            "RenderType"="Opaque"
            "Queue" = "Geometry"
        }

        LOD 100
        ColorMask 0 // Portal 자체는 마스크 용도이기에, 렌더링 하지 않음 -> 투명색으로 변환
        Zwrite off  // 개체가 앞이든 뒤이든 마스크를 통해서 보여지고 싶을 때 
        Cull off

        Pass
        {
           Stencil
            {
                Ref 1
                Comp Always
                Pass replace
            }

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


             v2f vert(appdata v)
             {
                 v2f o;
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.uv = v.uv;
                 
                 return o;
             }

             fixed4 frag(v2f i) : SV_Target
             {
                 return fixed4(0.0, 0.0, 0.0, 0.0);
             }
        ENDCG
        }
    } 
}
