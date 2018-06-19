Shader "Unlit/CrackableCurvedWorld" {

	Properties{
		//texture
		_MainTex("Base (RGB)", 2D) = "white" {}
		_SecondTex("Cracks (A)", 2D) = "white" {}
		_Cutoff("Alpha Cutoff", Range(0,1)) = 0.5
		_CrackColor("Cracks Color", Color) = (0.5, 0.5, 0.5, 1)
	}

	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Surface shader function is called surf, and vertex preprocessor function is called vert
		// addshadow used to add shadow collector and caster passes following vertex modification
		#pragma surface surf Lambert vertex:vert

		// Access the shaderlab properties
		sampler2D _MainTex;
		sampler2D _SecondTex;
		float _CurveStrength;
		half _Cutoff;
		float3 _CrackColor;

		// Basic input structure to the shader function
		// requires only a single set of UV texture mapping coordinates
		struct Input {
			float2 uv_MainTex;
			float2 uv_SecondTex;
			fixed4 color;
		};

		// This is where the curvature is applied
		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = v.color;

			// Transform the vertex coordinates from model space into world space
			float4 vv = mul(unity_ObjectToWorld, v.vertex);

			// Now adjust the coordinates to be relative to the camera position
			vv.xyz -= _WorldSpaceCameraPos.xyz;

			// Reduce the y coordinate (i.e. lower the "height") of each vertex based
			// on the square of the distance from the camera in the z axis, multiplied
			// by the chosen curvature factor
			vv = float4(0.0f, _CurveStrength * vv.z * vv.z, 0.0f, 0.0f);

			// Now apply the offset back to the vertices in model space
			v.vertex += mul(unity_WorldToObject, vv);
		}

		// This is just a default surface shader
		void surf(Input IN, inout SurfaceOutput o) {
			half4 mainTex = tex2D(_MainTex, IN.uv_MainTex);
			half4 secondTex = tex2D(_SecondTex, IN.uv_SecondTex);
			float crackVisibility = saturate((secondTex.a - _Cutoff) * 10);

			o.Emission = lerp(mainTex.rgb, secondTex.rgb * _CrackColor, crackVisibility) * IN.color;
			o.Alpha = mainTex.a;
		}

		ENDCG
	}
}