// Upgrade NOTE: upgraded instancing buffer 'Props' to new syntax.

Shader "Custom/Ice" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_NormalTex ("Normal", 2D) = "bump" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Distortion ("Distortion", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue" = "Transparent" }
		LOD 200

		GrabPass{}

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _GrabTexture;
		sampler2D _NormalTex;

		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		float _Distortion;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)
		
		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.pos = v.vertex;
		}

		float rand(float3 co) {
			return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 56.787))) * 43758.5433);
		}

		float noise(float3 pos) {
			float3 ip = floor(pos);
			float3 fp = smoothstep(0, 1, frac(pos));

			float4 a = float4(
				rand(ip + float3(0, 0, 0)),
				rand(ip + float3(1, 0, 0)),
				rand(ip + float3(0, 1, 0)),
				rand(ip + float3(1, 1, 0)));

		float4 b = float4(
				rand(ip + float3(0, 0, 1)),
				rand(ip + float3(1, 0, 1)),
				rand(ip + float3(0, 1, 1)),
				rand(ip + float3(1, 1, 1)));

			a = lerp(a, b, fp.z);
			a.xy = lerp(a.xy, a.zw, fp.y);
			return lerp(a.x, a.y, fp.x);
		}

		float perlin(float3 pos) {
			return (noise(pos * 16) * 32 +
				noise(pos * 32) * 16 +
				noise(pos * 64) * 8 +
				noise(pos * 128) * 4 +
				noise(pos * 256) * 2 +
				noise(pos * 512)) / 63;
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float2 grabUV = (IN.screenPos.xy / IN.screenPos.w);
			fixed2 normalTex = UnpackNormal(tex2D(_NormalTex, IN.uv_MainTex)).rg;
			grabUV += normalTex * _Distortion;
			fixed3 grab = tex2D(_GrabTexture, grabUV).rgb;
			o.Emission = grab;
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
