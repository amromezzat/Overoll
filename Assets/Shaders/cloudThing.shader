// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33000,y:32628,varname:node_4013,prsc:2|diff-408-OUT;n:type:ShaderForge.SFN_Tex2d,id:5541,x:32279,y:32587,ptovrint:False,ptlb:Cloud Texture,ptin:_CloudTexture,varname:node_5541,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-4471-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7496,x:31189,y:32570,varname:node_7496,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:6924,x:31110,y:32786,varname:node_6924,prsc:2;n:type:ShaderForge.SFN_Panner,id:4471,x:31488,y:32483,varname:node_4471,prsc:2,spu:1,spv:1|UVIN-7496-UVOUT,DIST-1726-OUT;n:type:ShaderForge.SFN_Divide,id:1726,x:31469,y:32707,varname:node_1726,prsc:2|A-6924-TSL,B-4404-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4404,x:31237,y:32993,ptovrint:False,ptlb:Cloud Speed Higher is Slower,ptin:_CloudSpeedHigherisSlower,varname:node_4404,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:6;n:type:ShaderForge.SFN_Tex2d,id:8826,x:32229,y:32878,ptovrint:False,ptlb:Alpha Map,ptin:_AlphaMap,varname:node_8826,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:408,x:32593,y:32804,varname:node_408,prsc:2|A-5541-A,B-8826-A,C-4921-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4921,x:32458,y:33022,ptovrint:False,ptlb:Cloud Alpha,ptin:_CloudAlpha,varname:node_4921,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;proporder:5541-4404-8826-4921;pass:END;sub:END;*/

Shader "Shader Forge/cloudThing" {
    Properties {
        _CloudTexture ("Cloud Texture", 2D) = "white" {}
        _CloudSpeedHigherisSlower ("Cloud Speed Higher is Slower", Float ) = 6
        _AlphaMap ("Alpha Map", 2D) = "white" {}
        _CloudAlpha ("Cloud Alpha", Float ) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 2.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _CloudTexture; uniform float4 _CloudTexture_ST;
            uniform float _CloudSpeedHigherisSlower;
            uniform sampler2D _AlphaMap; uniform float4 _AlphaMap_ST;
            uniform float _CloudAlpha;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_6924 = _Time + _TimeEditor;
                float2 node_4471 = (i.uv0+(node_6924.r/_CloudSpeedHigherisSlower)*float2(1,1));
                float4 _CloudTexture_var = tex2D(_CloudTexture,TRANSFORM_TEX(node_4471, _CloudTexture));
                float4 _AlphaMap_var = tex2D(_AlphaMap,TRANSFORM_TEX(i.uv0, _AlphaMap));
                float node_408 = (_CloudTexture_var.a*_AlphaMap_var.a*_CloudAlpha);
                float3 diffuseColor = float3(node_408,node_408,node_408);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile_fog
            #pragma exclude_renderers metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 2.0
            uniform float4 _LightColor0;
            uniform float4 _TimeEditor;
            uniform sampler2D _CloudTexture; uniform float4 _CloudTexture_ST;
            uniform float _CloudSpeedHigherisSlower;
            uniform sampler2D _AlphaMap; uniform float4 _AlphaMap_ST;
            uniform float _CloudAlpha;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_6924 = _Time + _TimeEditor;
                float2 node_4471 = (i.uv0+(node_6924.r/_CloudSpeedHigherisSlower)*float2(1,1));
                float4 _CloudTexture_var = tex2D(_CloudTexture,TRANSFORM_TEX(node_4471, _CloudTexture));
                float4 _AlphaMap_var = tex2D(_AlphaMap,TRANSFORM_TEX(i.uv0, _AlphaMap));
                float node_408 = (_CloudTexture_var.a*_AlphaMap_var.a*_CloudAlpha);
                float3 diffuseColor = float3(node_408,node_408,node_408);
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
