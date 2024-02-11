Shader "Custom/Custom Specular"
{
    Properties{
 _Color("Color", Color) = (1.000000,1.000000,1.000000,1.000000)
 _MainTex("Albedo", 2D) = "white" { }
 _Cutoff("Alpha Cutoff", Range(0.000000,1.000000)) = 0.500000
 _Glossiness("Smoothness", Range(0.000000,1.000000)) = 0.500000
 _GlossMapScale("Smoothness Factor", Range(0.000000,1.000000)) = 1.000000
[Enum(Specular Alpha,0,Albedo Alpha,1)]  _SmoothnessTextureChannel("Smoothness texture channel", Float) = 0.000000
 _SpecColor("Specular", Color) = (0.200000,0.200000,0.200000,1.000000)
 _SpecGlossMap("Specular", 2D) = "white" { }
[ToggleOff]  _SpecularHighlights("Specular Highlights", Float) = 1.000000
[ToggleOff]  _GlossyReflections("Glossy Reflections", Float) = 1.000000
 _BumpScale("Scale", Float) = 1.000000
[Normal]  _BumpMap("Normal Map", 2D) = "bump" { }
 _Parallax("Height Scale", Range(0.005000,0.080000)) = 0.020000
 _ParallaxMap("Height Map", 2D) = "black" { }
 _OcclusionStrength("Strength", Range(0.000000,1.000000)) = 1.000000
 _OcclusionMap("Occlusion", 2D) = "white" { }
 _EmissionColor("Color", Color) = (0.000000,0.000000,0.000000,1.000000)
 _EmissionMap("Emission", 2D) = "white" { }
 _DetailMask("Detail Mask", 2D) = "white" { }
 _DetailAlbedoMap("Detail Albedo x2", 2D) = "grey" { }
 _DetailNormalMapScale("Scale", Float) = 1.000000
[Normal]  _DetailNormalMap("Normal Map", 2D) = "bump" { }
[Enum(UV0,0,UV1,1)]  _UVSec("UV Set for secondary textures", Float) = 0.000000
[HideInInspector]  _Mode("__mode", Float) = 0.000000
[HideInInspector]  _SrcBlend("__src", Float) = 1.000000
[HideInInspector]  _DstBlend("__dst", Float) = 0.000000
[HideInInspector]  _ZWrite("__zw", Float) = 1.000000

[Enum(CompareFunction)] _StencilComp("Stencil Comp", Int) = 3
    }

    CGINCLUDE
#define UNITY_SETUP_BRDF_INPUT SpecularSetup
    ENDCG

    SubShader
    {
 LOD 300
 Tags { "RenderType" = "Opaque" "PerformanceChecks" = "False" }

    Stencil
    {
            Ref 1
            Comp[_StencilComp]
    }

     

        CGPROGRAM
    // Physically based Standard lighting model, and enable shadows on all light types
    #pragma surface surf Standard fullforwardshadows

    // Use shader model 3.0 target, to get nicer looking lighting
    #pragma target 3.0

    sampler2D _MainTex;

    struct Input
    {
        float2 uv_MainTex;
    };

    half _Glossiness;
    half _Metallic;
    fixed4 _Color;

    // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
    // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
    // #pragma instancing_options assumeuniformscaling
    UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
    UNITY_INSTANCING_BUFFER_END(Props)

    void surf(Input IN, inout SurfaceOutputStandard o)
    {
        // Albedo comes from a texture tinted by color
        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
        o.Albedo = c.rgb;
        // Metallic and smoothness come from slider variables
        o.Metallic = _Metallic;
        o.Smoothness = _Glossiness;
        o.Alpha = c.a;
    }
    ENDCG
     
    }

    SubShader{
 LOD 150
 Tags { "RenderType" = "Opaque" "PerformanceChecks" = "False" }

     Stencil
    {
            Ref 1
            Comp[_StencilComp]
    }



       CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            #pragma surface surf Standard fullforwardshadows

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _MainTex;

            struct Input
            {
                float2 uv_MainTex;
            };

            half _Glossiness;
            half _Metallic;
            fixed4 _Color;

            // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
            // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
            // #pragma instancing_options assumeuniformscaling
            UNITY_INSTANCING_BUFFER_START(Props)
                // put more per-instance properties here
            UNITY_INSTANCING_BUFFER_END(Props)

            void surf(Input IN, inout SurfaceOutputStandard o)
            {
                // Albedo comes from a texture tinted by color
                fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
                o.Albedo = c.rgb;
                // Metallic and smoothness come from slider variables
                o.Metallic = _Metallic;
                o.Smoothness = _Glossiness;
                o.Alpha = c.a;
            }
            ENDCG
        }


    FallBack "Diffuse"
}
