//===============================================================================
//Copyright (c) 2017 PTC Inc. All Rights Reserved.
//
//Vuforia is a trademark of PTC Inc., registered in the United States and other
//countries.
//===============================================================================

Shader "Vuforia/Character" {
	Properties {
		_GlowColor ("Glow Color", Color) = (1,1,1,1)
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Noise ("Noise", 2D) = "white" {}
		_Occlusion ("Occlusion", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_LocalHeight("Local Height", Float) = 1.0
		_LocalOffset("Local Offset", Float) = 0.0
		_FillRatio ("Fill Ratio", Range(0,1)) = 0
		_PlaneNorm ("Plane Normal", Vector) = (0,-1,0,0)
		_ScanLines("Scan Line", Range(0,1)) = 0
		[Toggle] _ClipModel ("Clip Model", Float) = 1
		[Toggle] _UseCustomTransform("Use Custom Transform", Float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
        {
            Name "InnerFill"
            Cull Front
            CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"
				#include "CharacterHelper.cginc"
 
                struct v2f {
                    float4 pos          : POSITION;
                    float4 localPos    : TEXCOORD0;
                };

                v2f vert (appdata_full v)
                {
                    v2f o;
					float4x4 tmp = unity_ObjectToWorld;
					o.localPos = v.vertex;
					if (_UseCustomTransform) {
						unity_ObjectToWorld = _CustomLocalToWorld;
					}
					o.pos = UnityObjectToClipPos(v.vertex);
					unity_ObjectToWorld = tmp;
                    return o;
                }
 
                fixed4 frag( v2f i ) : COLOR
                {
					if(_ClipModel) {
						float3 planePos = GetPlanePos();
						float dotPlane = dot(i.localPos - planePos, _PlaneNorm);
						clip(dotPlane);
					}
                    return _GlowColor+float4(0.3, 0.3, 0.3, 1);
                }
            ENDCG          
        }

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
		#include "CharacterHelper.cginc"

		sampler2D _MainTex;
		sampler2D _Noise;
		sampler2D _Occlusion;

		struct Input {
			float2 uv_MainTex;
			float3 localPos;
		};

		half _Glossiness;
		half _Metallic;
		float _ScanLines;
		fixed4 _Color;

		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.localPos = v.vertex;

			//multiply by _CustomLocalToWorld then by unity_WorldToObject to nullify unity_ObjectToWorld and replace it with our custom transform
			if (_UseCustomTransform) {
				v.vertex = mul(_CustomLocalToWorld, v.vertex);
				v.vertex = mul(unity_WorldToObject, v.vertex);
				v.normal = mul(_CustomLocalToWorld, float4(v.normal, 0.0));
				v.normal = mul(unity_WorldToObject, float4(v.normal, 0.0));
			}

		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Occlusion = tex2D (_Occlusion, IN.uv_MainTex);

			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			//o.Metallic = _Metallic;
			//o.Smoothness = _Glossiness;

			float4 emission = float4(0, 0, 0,1);
			//Add a glow to the parts close to the target
			if(_ClipModel > 0){
				float3 planePos = GetPlanePos();
				float dotPlane = dot(IN.localPos - planePos, _PlaneNorm);
				float3 projectedPos = dotPlane * _PlaneNorm; //position projected onto the normal of the plane
				clip(dotPlane);

				float GlowHeight = tex2D(_Noise, dotPlane.xx*10).r*0.5+0.5; //Glow height in the range of 0.5-1.0, use dotPlane.xx to add striation along the plane normal
				float4 glow = _GlowColor*saturate(GlowHeight - length(projectedPos) * 10 / _LocalHeight); //gradient should cover 1/10th of the model

				float scale = saturate((length(projectedPos)/_LocalHeight));
				float4 lines = (sin(scale * 500)*0.33 + 0.33) * _GlowColor * _ScanLines;
				emission = lines + glow;
			}

			o.Emission = emission;
			o.Alpha = c.a;
		}

		fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			fixed4 c;
			c.rgb = s.Albedo;
			c.a = s.Alpha;
			return c;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
