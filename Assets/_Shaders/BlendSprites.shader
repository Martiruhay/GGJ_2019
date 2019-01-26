Shader "CustomSprites/BlendSprites"
{
	Properties
	{
		_MainTex ("Texture 1", 2D) = "white" {}
		_SecondTex ("Texture 2", 2D) = "white" {}
		_ThirdTex ("Texture 2", 2D) = "white" {}
		_Color("Main color", Color) = (1,1,1,1)
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

			sampler2D _MainTex, _SecondTex, _ThirdTex;
			float4 _MainTex_ST;
			float4 _Color;
			float _Blend;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c2 = tex2D(_SecondTex, i.uv);
				c2.rgb *= c2.a;
				fixed4 c3 = tex2D(_ThirdTex, i.uv);
				c3.rgb *= c3.a;
				return lerp(c2,c3,_Blend);
			}
			ENDCG
		}
	}
}
