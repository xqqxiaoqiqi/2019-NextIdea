// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "OurGameParticle/CloverShader" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	_Color ("Color", Color) = (1, 1, 1, 2)	
	_Alpha ("Alpha", Float) = 1.0	
	_RotationSpeed ("RotaionSpeed", Float) = 0.0
	_TranslateU ("U", Float) = 0.0
	_TranslateV ("V", Float) = 0.0
	_ScreenMode ("ScreenMode:1,None;2:Alpha;3:Blend;4:All", Float) = 2.0
	_Black ("_Black", Range(1.0, 4.0) ) = 1.0
	_UVTime ("Time", Float) = 0.0
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	LOD 200
	Blend SrcAlpha OneMinusSrcAlpha
	AlphaTest Greater .01
	ColorMask RGB
	Lighting Off ZWrite Off
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex
		Bind "TexCoord", texcoord
	}
	
	// ---- Fragment program cards
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _Color;			
			float _Alpha;			
			float _RotationSpeed;
			float _TranslateU;
			float _TranslateV;
			float _ScreenMode;
			float _Black;
			float _UVTime;
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};
			
			float4 _MainTex_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);				
				o.color = v.color;
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				
				float sinX = sin ( _RotationSpeed * _Time.y);	
	        	float cosX = cos ( _RotationSpeed * _Time.y);	
	        	float sinY = sin ( _RotationSpeed * _Time.y);
	
	        	float2x2 rotationMatrix = float2x2( cosX, -sinX, sinY, cosX);
	        	o.texcoord.xy = mul ( o.texcoord.xy-0.5, rotationMatrix )+0.5;
	        	
	        	o.texcoord.x = o.texcoord.x+_TranslateU*_UVTime;	       	        
	        	o.texcoord.y = o.texcoord.y+_TranslateV*_UVTime;	 
	        
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{				
				fixed4 c = i.color * _Color * tex2D(_MainTex, i.texcoord);
	
				if(_ScreenMode == 1.0)		//none
					c.a = _Alpha;						
				if(_ScreenMode == 2.0)		//alpha
					c.a = c.a*_Alpha;
				if(_ScreenMode == 3.0)	//blend
				{		
					//c *= 2.0;			
					//c.a = (c.r+c.g+c.b)/3.0*0.47*_Alpha;						
					c.a = (c.r+c.g+c.b)/_Black*_Alpha;		
				}
				if(_ScreenMode == 4.0)	//all
				{
					c.a = (c.r+c.g+c.b)/_Black*c.a*_Alpha;						
				}
//				else
//				{
//					c.a = _Alpha;
//				}				
				
				return c;
			}
			ENDCG 
		}
	} 
	
}
FallBack "Diffuse"
}
