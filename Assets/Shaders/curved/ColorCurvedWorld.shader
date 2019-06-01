Shader "Unlit/ColorCurvedWorld" {

	Properties{
		//color
		_Color("Color", Color) = (1,1,1,1)
	}
	SubShader{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		LOD 200

		CGPROGRAM
		// Surface shader function is called surf, and vertex preprocessor function is called vert
		// addshadow used to add shadow collector and caster passes following vertex modification
		#pragma surface surf Lambert vertex:vert

		// Access the shaderlab properties
		uniform fixed4 _Color;
		uniform float _CurveStrength;

		// Basic input structure to the shader function
		// requires only a single set of UV texture mapping coordinates
		struct Input {
			float2 uv_MainTex;
		};

		// This is where the curvature is applied
		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);

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
			o.Albedo = _Color.rgb;
			o.Emission = _Color.rgb; // * _Color.a;
			o.Alpha = _Color.a;
		}

		ENDCG
	}
}