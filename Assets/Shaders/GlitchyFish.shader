Shader "Sprites/GlitchyFish"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _GlitchAmount ("Glitch Intensity", Range(0, 1)) = 0.1
        _GlitchSpeed ("Glitch Speed", Range(0.1, 10)) = 1.0
        _PixelSize ("Pixelation", Range(1, 20)) = 1
        [MaterialToggle] _ColorShift ("Color Shift", Float) = 1
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            fixed4 _Color;
            float _GlitchAmount;
            float _GlitchSpeed;
            float _PixelSize;
            float _ColorShift;
            
            // Random noise function
            float rand(float2 co)
            {
                return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
            }
            
            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color;
                
                // Add vertex jitter
                float glitch = rand(float2(_Time.y, _Time.y)) * _GlitchAmount;
                OUT.vertex.x += (rand(float2(OUT.vertex.x, _Time.y)) - 0.5) * glitch * 0.1;
                OUT.vertex.y += (rand(float2(OUT.vertex.y, _Time.y)) - 0.5) * glitch * 0.1;
                
                return OUT;
            }
            
            fixed4 frag(v2f IN) : SV_Target
            {
                // Pixelation
                float2 pixelatedUV = floor(IN.texcoord * _ScreenParams.xy / _PixelSize) * _PixelSize / _ScreenParams.xy;
                
                // Time-based glitch factors
                float time = _Time.y * _GlitchSpeed;
                float glitchFactor = _GlitchAmount * (0.5 + 0.5 * sin(time * 10.0));
                
                // Random UV displacement
                float2 uv = pixelatedUV;
                uv.x += (rand(float2(time, uv.y)) - 0.5) * glitchFactor * 0.1;
                uv.y += (rand(float2(time * 2.0, uv.x)) - 0.5) * glitchFactor * 0.05;
                
                // Color channel offset
                float r = tex2D(_MainTex, uv + float2(glitchFactor * 0.02, 0)).r;
                float g = tex2D(_MainTex, uv + float2(0, glitchFactor * -0.01)).g;
                float b = tex2D(_MainTex, uv + float2(glitchFactor * -0.015, glitchFactor * 0.01)).b;
                
                fixed4 c = fixed4(r, g, b, tex2D(_MainTex, uv).a) * IN.color;
                
                // Random color shift
                if (_ColorShift > 0.5 && rand(float2(time, uv.x + uv.y)) > 0.7)
                {
                    c.rgb = frac(c.rgb * float3(1.2, 0.8, 1.4));
                }
                
                // Random black flashes
                if (rand(float2(time * 0.5, uv.x)) > 0.95)
                {
                    c.rgb = 0;
                }
                
                c.rgb *= c.a; // Preserve transparency
                return c;
            }
            ENDCG
        }
    }
}