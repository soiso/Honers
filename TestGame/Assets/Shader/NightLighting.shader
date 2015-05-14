Shader "Custom/NightLighting" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Pass{
			Tags { "RenderType"="Opaque" }
			LOD 200
			
			CGPROGRAM

			#pragma vertex Vertex
			#pragma fragment Fragment

			sampler2D _MainTex;
			fixed4 _Color;

			struct VS_INPUT
			{
				float4 Pos		:	POSITION;
				float2 Tex		:	TEXCOORD0;
				float3 Normal	:	NORMAL;
			};

			struct VS_OUTPUT
			{
				float4 Pos		:	SV_POSITION;
				float3 Normal	:	NORMAL; 
				float2 Tex		:	TEXCOORD0;
			};

			VS_OUTPUT Vertex( VS_INPUT In  )
			{
				VS_OUTPUT Out = (VS_OUTPUT)0;

				Out.Pos = mul( UNITY_MATRIX_MVP, In.Pos );
				Out.Tex = In.Tex;
				Out.Normal = In.Normal;
				return Out;
			}

			float4 Fragment( VS_OUTPUT In )	: COLOR0
			{
				float4 Out = tex2D( _MainTex, In.Tex )*_Color;
				return Out; 
			}

			ENDCG
		}
	} 
}
