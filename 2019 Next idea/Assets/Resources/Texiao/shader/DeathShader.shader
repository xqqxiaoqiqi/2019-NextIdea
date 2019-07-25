Shader "OurGameParticle/DeathShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_NoiseTex("NoiseTexture",2D) = "white" {}
		_TintColor("Tint Color",Color) = (1,1,1,1)
		_DeathQ ("DeathQ",Float) = 0
		_CenterQ ("CenterQ",Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityCG.cginc"
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _NoiseTex;
		half4 _TintColor;
		half _DeathQ;
		half _CenterQ;

		struct Input {
			float2 uv_MainTex;
			float3 viewDir;
			float3 worldNormal;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			
			half3 viewD = normalize(IN.viewDir);
			half3 wNormal = normalize(IN.worldNormal);
			
			half noiseLum = Luminance(tex2D(_NoiseTex, IN.uv_MainTex).rgb);
			fixed centerLum = lerp(1,dot(viewD,wNormal),_CenterQ);
			half vdw =1-max(0, centerLum*noiseLum)-_DeathQ;
			o.Albedo = c.rgb*_TintColor.rgb;
			o.Alpha = c.a*_TintColor.a;
			clip(vdw);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
