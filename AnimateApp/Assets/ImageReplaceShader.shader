Shader "Custom/ImageReplaceShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _BlendTex ("Blend Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
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
            sampler2D _BlendTex;
            float4 _MainTex_ST;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 blendCol = tex2D(_BlendTex, i.uv);

                // ���բ�����᷹�������ըҡ Blend Texture
                if (col.r > 0.9 && col.g > 0.9 && col.b > 0.9 && col.a > 0.9)
                {
                    col = blendCol;
                }

                // ��Ǩ�ͺ��� Alpha ���ͷ���������
                if (blendCol.a < 0.1)
                {
                    discard;
                }

                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent/Cutout/Diffuse"
}