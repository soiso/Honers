Shader "Custom/Spoil" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_SubTex ("Normalmap", 2D) = "bamp" {}
	}
	SubShader {
Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}		LOD 300
		LOD 300
		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		sampler2D _SubTex;
		fixed4 _Color;

		struct Input {
			float2 uv_MainTex;
			float2 uv_SubTex;
		};

			void surf (Input IN, inout SurfaceOutput o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex)*_Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Normal = UnpackNormal(tex2D(_SubTex,IN.uv_SubTex));
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
