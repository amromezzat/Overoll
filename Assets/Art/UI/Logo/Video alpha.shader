Shader "Custom/alpha1" { 

     Properties{ 
          _MainTex("Color (RGB)", 2D) = "white" 
          _AlphaTex("Color (RGB)", 2D) = "white" 
     } 


     SubShader{ 
          Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" } 

          Cull off
          ZWrite On
          AlphaToMask On
          ColorMask RGB

          CGPROGRAM 
          #pragma surface surf NoLighting keepalpha  addshadow  

          fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten) { 
               fixed4 c; 
               c.rgb = s.Albedo; 
               c.a = s.Alpha; 
               return c; 
          } 

          struct Input { 
               float2 uv_MainTex; 
               float2 uv_AlphaTex; 
          }; 

          sampler2D _MainTex; 
          sampler2D _AlphaTex; 
           
          void surf(Input IN, inout SurfaceOutput o) { 
               o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb; 
               o.Alpha = tex2D(_AlphaTex, IN.uv_AlphaTex).rgb;
          } 

          ENDCG 
     } 

}