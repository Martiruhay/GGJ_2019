Shader "CustomSprites/GlowSprite"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Emissive("Emissive Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "white" {}
		_NoiseAmount("Noise Amount",  Range (0,1)) = 0
		_Color("Color", Color) = (1,1,1,1)
		_ColorG("Glow Color", Color) = (1,1,1,1)
		_Alpha("General Alpha",  Range (0,1)) = 1
		_Glow("Glow Intensity", Range(0,50)) = 0
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

			sampler2D _MainTex,_Emissive, _NoiseTex;
			float4 _MainTex_ST;
			float4 _Color, _ColorG;
			float _Alpha, _Glow, _NoiseAmount;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 distScroll = float2(_Time.x, _Time.x);
				fixed2 dist = (tex2D(_NoiseTex, i.uv + distScroll).rg - 0.5) * 2;

				fixed4 c = tex2D(_MainTex, i.uv + dist * _NoiseAmount)* _Color;

				//fixed4 c = tex2D(_MainTex, i.uv) * _Color;

				half4 e = tex2D(_Emissive, i.uv) * _ColorG * _Glow;
				c.rgb += e.rgb;
				c.a *= _Alpha;
				return c;
			}
			ENDCG
		}
	}
}
