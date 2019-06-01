Shader "Unlit/CurvedWorld" {

	Properties{
		//texture
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Color("Color", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Surface shader function is called surf, and vertex preprocessor function is called vert
		// addshadow used to add shadow collector and caster passes following vertex modification
		#pragma surface surf Lambert vertex:vert

		// Access the shaderlab properties
		uniform sampler2D _MainTex;
		fixed4 _Color;
		uniform float _CurveStrength;

		// Basic input structure to the shader function
		// requires only a single set of UV texture mapping coordinates
		struct Input {
			float2 uv_MainTex;
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
			o.Emission = mainTex.rgb * IN.color * _Color;
			o.Alpha = mainTex.a;
		}

		ENDCG
	}
}