Shader "CustomSprites/PaletteSwap"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Emissive("Emissive Texture", 2D) = "black" {}
		_Swap("Swap Map Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1,1,1,1)
		_ColorR("Red Swap Color", Color) = (1,1,1,1)
		_ColorG("Green Swap Main Color", Color) = (1,1,1,1)
		_ColorB("Blue SwapMain Color", Color) = (1,1,1,1)
		_Alpha("General Alpha",  Range (0,1)) = 1
		_Glow("Glow Intensity", Range(0,50)) = 1
	}
	
	SubShader
	{
		Tags {"Queue"="Transparent" "CanUseSpriteAtlas"="True"}
		Blend SrcAlpha OneMinusSrcAlpha
		Cull off
		ZTest Off

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
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex,_Emissive, _Swap;
			float4 _MainTex_ST;
			float4 _Color,_ColorR, _ColorG, _ColorB;
			float _Alpha, _Glow;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.uv) * _Color;
				fixed4 s = tex2D(_Swap, i.uv);
				float3 redSwap = _ColorR * s.r * c.rgb;
				float3 greenwap = _ColorG * s.g * c.rgb;
				float3 blueSwap = _ColorB * s.b * c.rgb;
				
				c.rgb = c.rgb * (1 - s.r - s.g - s.b); //Only show unmasked parts
                c.rgb = c + redSwap + greenwap + blueSwap;
				
				half4 e = tex2D(_Emissive, i.uv) * _Glow;
				c.rgb += e.rgb;
				c.a *= _Alpha;
				return c;
			}
			ENDCG
		}
	}
}
