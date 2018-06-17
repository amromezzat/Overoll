// Overlays a texture on top of another (e.g. for showing cracks in a material) using the Cutoff property
// Uses optimizations from the Simplified Bumped Specular shader. Those differences (from regular Bumped Specular) are:
// - specular lighting directions are approximated per vertex
// - writes zero to alpha channel
// - Normalmap uses Tiling/Offset of the Base texture
// - no Deferred Lighting support
// - no Lightmap support
// - supports ONLY 1 directional light. Other lights are completely ignored.
 
 
Shader "Mobile/FX/Cracking Bumped Specular" {
Properties {
    _Shininess ("Shininess", Range (0.01, 1)) = 0.078125
    _Color ("Color", Color) = (1, 1, 1, 1)
    _MainTex ("Base (RGB) Gloss (A)", 2D) = "white" {}
    _BumpMap ("Normalmap", 2D) = "bump" {}
    _SecondTex ("Cracks (A)", 2D) = "white" {}
    _CrackColor ("Cracks Color", Color) = (0.5, 0.5, 0.5, 1)
    _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.5
}
 
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 250
   
    CGPROGRAM
    #pragma surface surf MobileBlinnPhong exclude_path:prepass nolightmap noforwardadd halfasview novertexlights
 
    inline fixed4 LightingMobileBlinnPhong (SurfaceOutput s, fixed3 lightDir, fixed3 halfDir, fixed atten)
    {
        fixed diff = max (0, dot (s.Normal, lightDir));
        fixed nh = max (0, dot (s.Normal, halfDir));
        fixed spec = pow (nh, s.Specular*128) * s.Gloss;
       
        fixed4 c;
        c.rgb = (s.Albedo * _LightColor0.rgb * diff + _LightColor0.rgb * spec) * (atten*2);
        c.a = 0.0;
        return c;
    }
   
    half _Shininess;
    sampler2D _MainTex;
    sampler2D _BumpMap;
    sampler2D _SecondTex;
    float3 _Color;
    float3 _CrackColor;
    half _Cutoff;
   
    struct Input {
        float2 uv_MainTex;
        float2 uv_SecondTex;
        float2 uv_BumpMap;
    };
   
    void surf (Input IN, inout SurfaceOutput o) {
        fixed4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
        fixed4 secondTex = tex2D(_SecondTex, IN.uv_SecondTex);
        float crackVisibility = saturate((secondTex.a - _Cutoff) * 10);
        o.Albedo = lerp(mainTex.rgb * _Color, secondTex.rgb * _CrackColor, crackVisibility);
        o.Gloss = mainTex.a;
        o.Specular = _Shininess;
        o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
    }
    ENDCG
}
 
FallBack "Mobile/VertexLit"
}