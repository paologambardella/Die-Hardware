Shader "Hidden/CRTShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _PixelSize;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed3 col;

				float2 cor;
	
				cor.x =  (i.uv.x * _ScreenParams.x)/_PixelSize;
				cor.y = ((i.uv.y * _ScreenParams.y)+_PixelSize*1.5*fmod(floor(cor.x),2.0))/(_PixelSize*3.0);

				float2 ico = floor( cor );
				float2 fco = frac( cor );

				float3 pix; 
				pix.x = step(1.5, fmod( ico.x, 3.0 ));
				pix.y = step(1.5, fmod( 1.0 + ico.x, 3.0 ));
				pix.z = step(1.5, fmod( 2.0 + ico.x, 3.0 ));

				float3 ima = tex2D( _MainTex,_PixelSize*ico*float2(1.0,3.0)/_ScreenParams.xy ).xyz;

				col = pix*dot( pix, ima );


			    col *= step( abs(fco.x-0.5), 0.4 );
			   	col *= step( abs(fco.y-0.5), 0.4 );
				
				col *= 1.2;

				return fixed4(col, 1.0);
			}
			ENDCG
		}
	}
}
