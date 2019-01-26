Shader "CustomSprites/BurnSprite"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Emissive("Emissive Texture", 2D) = "white" {}
		_Noise("Burn Noise Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_ColorG("Glow Color", Color) = (1,1,1,1)
		_ColorB("Burn Color", Color) = (1,1,1,1)
		_Alpha("General Alpha",  Range (0,1)) = 1
		_Glow("Glow Intensity", Range(0,50)) = 0
		_Glow2("Glow Burn Intensity", Range(0,50)) = 0
		_Burn("Burn Amount", Range(0,1)) = 0
		_BurnLineWidth("Burn Line Width", Range(0,2)) = 0
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

			sampler2D _MainTex,_Emissive, _Noise;
			float4 _MainTex_ST;
			float4 _Color, _ColorG, _ColorB;
			float _Alpha, _Glow, _Burn, _BurnLineWidth, _Glow2;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				_Burn = abs(sin(_Time.y)); //REMOVE ME

				fixed4 c = tex2D(_MainTex, i.uv) * _Color;
				float4 outline = float4(1,1,1,1) * _ColorB * _Glow2;
				outline.a = step(max(0, _Burn - _BurnLineWidth), tex2D(_Noise, i.uv).r) * c.a;

				half4 e = tex2D(_Emissive, i.uv) * _ColorG * _Glow;
				c.rgb += e.rgb;
				c.a *= _Alpha;
				c.a *= step(_Burn, tex2D(_Noise, i.uv).r);

				outline.a -= c.a;

				return lerp(c, outline, outline.a);
			}
			ENDCG
		}
	}
}
