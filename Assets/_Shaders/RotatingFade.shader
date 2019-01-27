Shader "CustomSprites/RotatingFade"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Main Color", Color) = (1, 1, 1, 1)
		_FadeIntensity("_FadeIntensity", Range(-1, 1)) = 0
		_Angle("Angle of rotation of fade", Range(0, 6.2831)) = 0
		_ColorG("Glow Color", Color) = (1,1,1,1)
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
				float2 uv2 : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST, _Color, _ColorG;
			float _FadeIntensity, _Angle, _Glow;
			
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
				fixed4 c = tex2D(_MainTex, i.uv2);
				c.a *= _FadeIntensity;
				return c * _Color * _ColorG * _Glow;
			}
			ENDCG
		}
	}
}