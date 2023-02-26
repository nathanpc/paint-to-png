Shader "Unlit/Drawable"
{
	//#pragma enable_d3d11_debug_symbols

	Properties
	{
		_MainTex ("Texture", 2D) = "black" {}
		_BaseTex ("Texture", 2D) = "black" {}
		_ForcedBlendRange("Float with range", Range(0.0, 1.0)) = 0.2
	}
	SubShader
	{
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		//ZWrite Off
		//Blend SrcAlpha OneMinusSrcAlpha
		//Cull front
		LOD 100


		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv2 : TEXCOORD1;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv2;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			sampler2D _MainTex;
			sampler2D _BaseTex;
			fixed _ForcedBlendRange;

			//sampler2D _BlendTex;
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 paintedTex = tex2D(_MainTex, i.uv);
				fixed4 baseTex = tex2D(_BaseTex, i.uv);

				// Forces the value to have a min range of transparency
				// e.g. _ForcedBlendRange = 0.2, means that at 80% opacity its forced to 1
				//		Otherwise its clamped to max value of 1
				// NOTE: This atm does nothing, as the basic alpha is always 1... rip
				fixed scalar = clamp(paintedTex.a - _ForcedBlendRange, 0, 1);

				fixed4 final = (baseTex * (scalar)) + (paintedTex * 10);

				/*
				if (paintedTex.x > _ForcedBlendRange || paintedTex.y > _ForcedBlendRange || paintedTex.z > _ForcedBlendRange)
				{
					return paintedTex;
				}
				*/

				return final;
				
				//return final;// paintedTex;
			}
			ENDCG
		}
	}
}
