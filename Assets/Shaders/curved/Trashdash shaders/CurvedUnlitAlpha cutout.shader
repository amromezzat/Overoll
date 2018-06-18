Shader "Unlit/CurvedUnlitAlpha cutout"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
	{
		Tags{"Queue"="AlphaTest" "RenderType" = "TransparentCutout" "IgnoreProjector"="True" }
		LOD 100


		Pass
		{
		AlphaToMask On

			CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
			// make fog work
	#pragma multi_compile_fog

	#include "CurvedCode.cginc"

			ENDCG
		}
	}
}
