//Shader "Custom/BlurEffect"
//{
//	Properties
//	{
//		_MainTex("Texture", 2D) = "white" {}
//	}
//		SubShader
//	{
//		Tags { "RenderType" = "Opaque" }
//		LOD 100
//
//		Pass
//		{
//			ZTest Off
//			ZWrite Off
//			Cull Back
//
//			CGPROGRAM
//			#pragma fragmentoption ARB_precision_hint_fastest
//			#pragma vertex vert
//			#pragma fragment frag
//			#include "UnityCG.cginc"
//
//			struct appdata
//			{
//				float4 vertex : POSITION;
//				float2 uv : TEXCOORD0;
//			};
//
//			struct v2f
//			{
//				float4 vertex : SV_POSITION;
//				half2 uv : TEXCOORD0;
//			};
//
//			sampler2D _MainTex;
//
//			half4 _Offsets;
//
//			static const int samplingCount = 10;
//			half _Weights[samplingCount];
//
//			v2f vert(appdata v)
//			{
//				v2f o;
//				o.vertex = UnityObjectToClipPos(v.vertex);
//				o.uv = v.uv;
//
//				return o;
//			}
//
//			fixed4 frag(v2f i) : SV_Target
//			{
//				half4 col = 0;
//
//				[unroll]
//				for (int j = samplingCount - 1; j > 0; j--)
//				{
//					col += tex2D(_MainTex, i.uv - (_Offsets.xy * j)) * _Weights[j];
//				}
//
//				[unroll]
//				for (int j = 0; j < samplingCount; j++)
//				{
//					col += tex2D(_MainTex, i.uv + (_Offsets.xy * j)) * _Weights[j];
//				}
//
//				return col;
//			}
//			ENDCG
//		}
//	}
//}

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'






//Shader "Custom/Blur"
//{
//	Properties
//	{
//		_Factor("Factor", Range(0, 5)) = 1.0
//	}
//		SubShader
//	{
//		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
//
//		GrabPass { }
//
//		Pass
//		{
//			CGPROGRAM
//			#pragma vertex vert
//			#pragma fragment frag
//			#pragma fragmentoption ARB_precision_hint_fastest
//
//			#include "UnityCG.cginc"
//
//			struct appdata
//			{
//				float4 vertex : POSITION;
//				float2 uv : TEXCOORD0;
//			};
//
//			struct v2f
//			{
//				float4 pos : SV_POSITION;
//				float4 uv : TEXCOORD0;
//			};
//
//			v2f vert(appdata v)
//			{
//				v2f o;
//				o.pos = UnityObjectToClipPos(v.vertex);
//				o.uv = ComputeGrabScreenPos(o.pos);
//				return o;
//			}
//
//			sampler2D _GrabTexture;
//			float4 _GrabTexture_TexelSize;
//			float _Factor;
//
//			half4 frag(v2f i) : SV_Target
//			{
//
//				half4 pixelCol = half4(0, 0, 0, 0);
//
//				#define ADDPIXEL(weight,kernelX) tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(float4(i.uv.x + _GrabTexture_TexelSize.x * kernelX * _Factor, i.uv.y, i.uv.z, i.uv.w))) * weight
//
//				pixelCol += ADDPIXEL(0.05, 4.0);
//				pixelCol += ADDPIXEL(0.09, 3.0);
//				pixelCol += ADDPIXEL(0.12, 2.0);
//				pixelCol += ADDPIXEL(0.15, 1.0);
//				pixelCol += ADDPIXEL(0.18, 0.0);
//				pixelCol += ADDPIXEL(0.15, -1.0);
//				pixelCol += ADDPIXEL(0.12, -2.0);
//				pixelCol += ADDPIXEL(0.09, -3.0);
//				pixelCol += ADDPIXEL(0.05, -4.0);
//				return pixelCol;
//			}
//			ENDCG
//		}
//
//		GrabPass { }
//
//		Pass
//		{
//			CGPROGRAM
//			#pragma vertex vert
//			#pragma fragment frag
//			#pragma fragmentoption ARB_precision_hint_fastest
//
//			#include "UnityCG.cginc"
//
//			struct appdata
//			{
//				float4 vertex : POSITION;
//				float2 uv : TEXCOORD0;
//			};
//
//			struct v2f
//			{
//				float4 pos : SV_POSITION;
//				float4 uv : TEXCOORD0;
//			};
//
//			v2f vert(appdata v)
//			{
//				v2f o;
//				o.pos = UnityObjectToClipPos(v.vertex);
//				o.uv = ComputeGrabScreenPos(o.pos);
//				return o;
//			}
//
//			sampler2D _GrabTexture;
//			float4 _GrabTexture_TexelSize;
//			float _Factor;
//
//			fixed4 frag(v2f i) : SV_Target
//			{
//
//				fixed4 pixelCol = fixed4(0, 0, 0, 0);
//
//				#define ADDPIXEL(weight,kernelY) tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(float4(i.uv.x, i.uv.y + _GrabTexture_TexelSize.y * kernelY * _Factor, i.uv.z, i.uv.w))) * weight
//
//				pixelCol += ADDPIXEL(0.05, 4.0);
//				pixelCol += ADDPIXEL(0.09, 3.0);
//				pixelCol += ADDPIXEL(0.12, 2.0);
//				pixelCol += ADDPIXEL(0.15, 1.0);
//				pixelCol += ADDPIXEL(0.18, 0.0);
//				pixelCol += ADDPIXEL(0.15, -1.0);
//				pixelCol += ADDPIXEL(0.12, -2.0);
//				pixelCol += ADDPIXEL(0.09, -3.0);
//				pixelCol += ADDPIXEL(0.05, -4.0);
//				return pixelCol;
//			}
//			ENDCG
//		}
//	}
//}





Shader "Custom/Blur"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Blur("Blur", Float) = 10
	}
		SubShader
		{

			Tags{ "Queue" = "Transparent" }

			GrabPass
			{
			}

			Pass
			{
				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					fixed4 color : COLOR;
				};

				struct v2f
				{
					float4 grabPos : TEXCOORD0;
					float4 pos : SV_POSITION;
					float4 vertColor : COLOR;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.grabPos = ComputeGrabScreenPos(o.pos);
					o.vertColor = v.color;
					return o;
				}

				sampler2D _GrabTexture;
				fixed4 _GrabTexture_TexelSize;

				float _Blur;

				half4 frag(v2f i) : SV_Target
				{
					float blur = _Blur;
					blur = max(1, blur);

					fixed4 col = (0, 0, 0, 0);
					float weight_total = 0;

					[loop]
					for (float x = -blur; x <= blur; x += 1)
					{
						float distance_normalized = abs(x / blur);
						float weight = exp(-0.5 * pow(distance_normalized, 2) * 5.0);
						weight_total += weight;
						col += tex2Dproj(_GrabTexture, i.grabPos + float4(x * _GrabTexture_TexelSize.x, 0, 0, 0)) * weight;
					}

					col /= weight_total;
					return col;
				}
				ENDCG
			}
			GrabPass
			{
			}

			Pass
			{
				CGPROGRAM

				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					fixed4 color : COLOR;
				};

				struct v2f
				{
					float4 grabPos : TEXCOORD0;
					float4 pos : SV_POSITION;
					float4 vertColor : COLOR;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.grabPos = ComputeGrabScreenPos(o.pos);
					o.vertColor = v.color;
					return o;
				}

				sampler2D _GrabTexture;
				fixed4 _GrabTexture_TexelSize;

				float _Blur;

				half4 frag(v2f i) : SV_Target
				{
					float blur = _Blur;
					blur = max(1, blur);

					fixed4 col = (0, 0, 0, 0);
					float weight_total = 0;

					[loop]
					for (float y = -blur; y <= blur; y += 1)
					{
						float distance_normalized = abs(y / blur);
						float weight = exp(-0.5 * pow(distance_normalized, 2) * 5.0);
						weight_total += weight;
						col += tex2Dproj(_GrabTexture, i.grabPos + float4(0, y * _GrabTexture_TexelSize.y, 0, 0)) * weight;
					}

					col /= weight_total;
					return col;
				}
				ENDCG
			}

		}
}