Shader "Custom/EmissiveDiffuse"
{

Properties
{
	_MainTex("Diffuse", 2D) = "white" {}
	_BumpMap("Normal", 2D) = "bump" {}
	_Emissive("Emissive", 2D) = "black" {}
	_EmissivePower("Emissive Power", Range(0,2)) = 1.0
}
	SubShader
	{
		Tags {"RenderType" = "Opaque"}
		CGPROGRAM
		#pragma surface surf BlinnPhong
		sampler2D _MainTex;
		sampler2D _BumpMap;
		sampler2D _Emissive;
		float _EmissivePower;
		
		struct Input
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
			float2 uv_Emissive;
			
			//float4 color : COLOR; //(1.0, 1.0, 1.0, 1.0) R,G,B,A
			
		};
		void surf (Input IN, inout SurfaceOutput o)
		{
			fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
			fixed4 emissiveTex = tex2D(_Emissive, IN.uv_Emissive);
			o.Albedo = tex.rgb;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
			o.Emission = emissiveTex.rgb * _EmissivePower;
		}		
		ENDCG
	}
	Fallback "Diffuse"
}
