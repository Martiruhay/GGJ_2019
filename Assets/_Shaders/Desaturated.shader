Shader "CustomSprites/Desaturated"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Main color", Color) = (1,1,1,1)
		_Alpha("General Alpha",  Range (0,1)) = 1
		_Glow("Glow Intensity", Range(1,50)) = 1
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

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
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
				fixed4 c = tex2D(_MainTex, i.uv);
				float luminance = 0.3 * c.r + 0.59 * c.g + 0.11 * c.b;
				c.rgb = float3(luminance, luminance, luminance);
				c.a *= _Alpha;
				return c * _Color * _Glow;
			}
			ENDCG
		}
	}
}
