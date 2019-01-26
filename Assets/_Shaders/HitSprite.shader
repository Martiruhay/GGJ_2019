Shader "CustomSprites/HitSprite"
{
	Properties
	{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Emissive("Emissive Texture", 2D) = "white" {}
		_Color("Color", Color) = (1,1,1,1)
		_ColorG("Glow Color", Color) = (1,1,1,1)
		_ColorHit("Hit Color", Color) = (1,1,1,1)
		_Alpha("General Alpha",  Range (0,1)) = 1
		_Glow("Glow Intensity", Range(0,50)) = 1
		_HitGlow("Hit Glow Intensity", Range(0,50)) = 1
		_Blend("Blend 1-2",  Range (0,1)) = 1
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

			sampler2D _MainTex,_Emissive;
			float4 _MainTex_ST;
			float4 _Color, _ColorG, _ColorHit;
			float _Alpha, _Glow, _Blend, _HitGlow;
			
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

				_Blend = (max(0, sin((_Time.y*2) % (2*3.1416))));
				if(((_Time.y*2) % (2*3.1416)) < 1.5707){
					_Blend = 0;
				}

				half4 e = tex2D(_Emissive, i.uv) * _ColorG * _Glow;
				c.rgb += e.rgb;
				c.rgb = lerp(c.rgb, _ColorHit.rgb * _HitGlow,_Blend);
				c.a *= _Alpha;
				return c;
			}
			ENDCG
		}
	}
}
