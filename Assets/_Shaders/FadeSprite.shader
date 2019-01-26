Shader "CustomSprites/FadeSprite"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1, 1, 1, 1)
		_FadeMask("_FadeMask", 2D) = "Grey" {}
		_FadeIntensity("_FadeIntensity", Range(-1, 1)) = 0
	}

	SubShader
	{
		Tags {"Queue"="Transparent" "CanUseSpriteAtlas"="True"}
		Blend SrcAlpha OneMinusSrcAlpha
		Cull off

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

			sampler2D _MainTex, _FadeMask;
			float4 _MainTex_ST, _Color;
			float _FadeIntensity;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.uv);
				//Alpha gets value from texture and adds an arbitrary value
				//Saturate clamps between 0 and 1, in case intensity is too low/high
				c.a *= saturate(tex2D(_FadeMask, i.uv).r + _FadeIntensity);
				return c * _Color;
			}
			ENDCG
		}
	}
}