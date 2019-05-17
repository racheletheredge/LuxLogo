Shader "Unlit/wavy"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			#define PI 3.14159265358

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
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}


float normY(float pixel) {
    return pixel / 100;
}

float4 mainImage(in float2 fragCoord )
{
	float2 xy = fragCoord.xy / float2(100,1);
	
    float4 color = float4(1.0, 1.0, 1.0, 1.0);
    float buffer = normY(5.0);
    const int amt = 5;
    float amtf = float(amt);
    
    float sinVal;
    bool colored = false;
    
    for(int i = 0; i < amt; i++) {
        sinVal = sin(xy.x * 12.0 * PI - _Time.x) / amtf + float(i) / amtf;

        if(xy.y < sinVal + buffer && xy.y > sinVal - buffer) {
            color.r = xy.y;
            color.g = xy.x;
            colored = true;
            break;
        }
    }
    
    if (!colored && xy.y < sinVal) {
            color.r = xy.x;
            color.g = xy.y;
    }
    
    return color;
}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return mainImage(i.uv);
			}
			ENDCG
		}
	}
}
