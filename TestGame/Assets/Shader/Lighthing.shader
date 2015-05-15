Shader "Custom/Lighting" {
	Properties {
		_Color ("Color", Color) = (1.0, 0.0, 0.0, 1.0)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Pass{
			Tags { "RenderType"="Opaque" }
			LOD 200
			
			CGPROGRAM
			#include "UnityCG.cginc"

			#pragma vertex Vertex
			#pragma fragment Fragment

			sampler2D _MainTex;
			fixed4 _Color;

			appdata_base Vertex( appdata_base In  )
			{
				appdata_base Out = (appdata_base)0;

				Out.vertex = mul( UNITY_MATRIX_MVP, In.vertex );
				Out.texcoord = In.texcoord;
				Out.normal = In.normal;
				return Out;
			}

			float4 Fragment( appdata_base In )	: COLOR0
			{
				float4 Out = tex2D( _MainTex, In.texcoord.xy )*_Color;
				return Out; 
			}

			ENDCG
		}
	} 
}
