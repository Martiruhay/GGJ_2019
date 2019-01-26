Shader "CustomSprites/RotatingFade"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1, 1, 1, 1)
		_FadeMask("_FadeMask", 2D) = "Grey" {}
		_FadeIntensity("_FadeIntensity", Range(-1, 1)) = 0
		[PerRendererData]_Angle("Angle of rotation of fade", Range(-6.2831, 6.2831)) = 0
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
				float2 uv2 : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex, _FadeMask;
			float4 _MainTex_ST, _Color;
			float _FadeIntensity, _Angle;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				float2 uvC = v.uv;
				float cosAngle = cos(_Angle);
                float sinAngle = sin(_Angle);
                float2x2 rot = float2x2(cosAngle, -sinAngle, sinAngle, cosAngle);
				float2 pivot = float2(0.5, 0.5);
				uvC -= pivot; 
				o.uv2 = mul(rot, uvC);
				o.uv2 += pivot;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.uv);
				//c.a *= saturate(tex2D(_FadeMask, i.uv2).r + _FadeIntensity);
				c.a *= saturate(i.uv2.y + _FadeIntensity);
				return c * _Color;
			}
			ENDCG
		}
	}
}