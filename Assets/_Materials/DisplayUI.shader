Shader "CustomSprites/DisplayUI"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		[PerRendererData]_Color("Main color", Color) = (1,1,1,1)
		_Width("Outline Width", Range(0,0.05)) = 0.003
		[PerRendererData]_Alpha("General Alpha",  Range (0,1)) = 1
		_Alpha2("Outline Alpha",  Range (0,1)) = 1
		_Glow("Sprite Glow Intensity", Range(0,50)) = 1
		[PerRendererData]_Glow2("Outline Glow Intensity", Range(0,50)) = 1
		_HitGlow("Hit Glow Intensity", Range(0,50)) = 1
		_Blend("Blend 1-2",  Range (0,1)) = 1
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
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float _Alpha, _Alpha2, _Width, _Glow, _Glow2;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 c = tex2D(_MainTex, i.uv) * _Color * _Glow;
				c.rgb *= c.a;

				float spriteLeft = tex2D(_MainTex, i.uv + float2(_Width, 0)).a;
                float spriteRight = tex2D(_MainTex, i.uv - float2(_Width,  0)).a;
                float spriteBottom = tex2D(_MainTex, i.uv + float2( 0 ,_Width)).a;
                float spriteTop = tex2D(_MainTex, i.uv - float2( 0 , _Width)).a;
				float result = saturate((spriteRight + spriteLeft + spriteTop+ spriteBottom));
				result *= (1-c.a);
				result *= _Alpha2;
				float4 outline = result * _Color * _Glow2;

				c += outline;
				c.a *= _Alpha;
				return c;
			}
			ENDCG
		}
	}
}
