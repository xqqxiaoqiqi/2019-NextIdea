// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4013,x:33306,y:32646,varname:node_4013,prsc:2|emission-520-OUT;n:type:ShaderForge.SFN_Tex2d,id:1339,x:31840,y:32927,ptovrint:False,ptlb:diffuse,ptin:_diffuse,varname:node_2568,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:87fcde75fc76b574cb9dc13f0ebbff85,ntxv:0,isnm:False|UVIN-5350-OUT;n:type:ShaderForge.SFN_VertexColor,id:3590,x:31272,y:32441,varname:node_3590,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9261,x:32110,y:32638,varname:node_9261,prsc:2|A-3590-RGB,B-1339-RGB;n:type:ShaderForge.SFN_Multiply,id:5644,x:32173,y:32858,varname:node_5644,prsc:2|A-3590-A,B-1339-A;n:type:ShaderForge.SFN_Multiply,id:2688,x:32672,y:32748,varname:node_2688,prsc:2|A-9261-OUT,B-1666-OUT,C-6366-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6366,x:32486,y:33132,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_5857,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Panner,id:104,x:31310,y:32606,varname:node_104,prsc:2,spu:1,spv:0|UVIN-4499-UVOUT,DIST-6420-OUT;n:type:ShaderForge.SFN_TexCoord,id:4499,x:31094,y:32532,varname:node_4499,prsc:2,uv:0;n:type:ShaderForge.SFN_Tex2d,id:2994,x:32079,y:33117,ptovrint:False,ptlb:mask,ptin:_mask,varname:node_2994,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7600bcb08ac50d842bff11a07095091d,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Panner,id:5443,x:31350,y:32892,varname:node_5443,prsc:2,spu:0,spv:1|UVIN-7164-UVOUT,DIST-8890-OUT;n:type:ShaderForge.SFN_TexCoord,id:7164,x:31095,y:32881,varname:node_7164,prsc:2,uv:0;n:type:ShaderForge.SFN_Time,id:7204,x:30938,y:32720,varname:node_7204,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6420,x:31107,y:32684,varname:node_6420,prsc:2|A-7824-OUT,B-7204-T;n:type:ShaderForge.SFN_Lerp,id:5350,x:31667,y:32927,varname:node_5350,prsc:2|A-104-UVOUT,B-5443-UVOUT,T-9357-OUT;n:type:ShaderForge.SFN_Time,id:8611,x:30752,y:33187,varname:node_8611,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8890,x:30921,y:33151,varname:node_8890,prsc:2|A-5445-OUT,B-8611-T;n:type:ShaderForge.SFN_Vector1,id:9357,x:31492,y:33023,varname:node_9357,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:1666,x:32335,y:32951,varname:node_1666,prsc:2|A-5644-OUT,B-2994-R;n:type:ShaderForge.SFN_Color,id:5053,x:32679,y:32587,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5053,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:520,x:32971,y:32662,varname:node_520,prsc:2|A-5053-RGB,B-5053-A,C-2688-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7824,x:30642,y:32599,ptovrint:False,ptlb:Uspeed,ptin:_Uspeed,varname:node_7824,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:5445,x:30599,y:32909,ptovrint:False,ptlb:Vspeed,ptin:_Vspeed,varname:_Uspeed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;proporder:1339-6366-2994-5053-7824-5445;pass:END;sub:END;*/

Shader "<effect>/AdditiveUV" {
    Properties {
        _diffuse ("diffuse", 2D) = "white" {}
        _Intensity ("Intensity", Float ) = 1
        _mask ("mask", 2D) = "white" {}
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _Uspeed ("Uspeed", Float ) = 0
        _Vspeed ("Vspeed", Float ) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
			Fog { Mode off }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 2.0
            uniform float4 _TimeEditor;
            uniform sampler2D _diffuse; uniform float4 _diffuse_ST;
            uniform float _Intensity;
            uniform sampler2D _mask; uniform float4 _mask_ST;
            uniform float4 _Color;
            uniform float _Uspeed;
            uniform float _Vspeed;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
               // float isFrontFace = ( facing >= 0 ? 1 : 0 );
                //float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_7204 = _Time + _TimeEditor;
                float4 node_8611 = _Time + _TimeEditor;
                float2 node_5350 = lerp((i.uv0+(_Uspeed*node_7204.g)*float2(1,0)),(i.uv0+(_Vspeed*node_8611.g)*float2(0,1)),0.5);
                float4 _diffuse_var = tex2D(_diffuse,TRANSFORM_TEX(node_5350, _diffuse));
                float4 _mask_var = tex2D(_mask,TRANSFORM_TEX(i.uv0, _mask));
                float3 emissive = (_Color.rgb*_Color.a*((i.vertexColor.rgb*_diffuse_var.rgb)*((i.vertexColor.a*_diffuse_var.a)*_mask_var.r)*_Intensity));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    // CustomEditor "ShaderForgeMaterialInspector"
}
