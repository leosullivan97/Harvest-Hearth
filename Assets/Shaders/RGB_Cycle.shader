Shader "Sprites/RGB_Cycle"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _Speed ("Cycle Speed", Range(0.1, 5)) = 1.0
        _Intensity ("Color Intensity", Range(0.5, 2)) = 1.0
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [HideInInspector] _AlphaTex ("External Alpha", 2D) = "white" {}
        [HideInInspector] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
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
            #pragma vertex SpriteVert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
            
            float _Speed;
            float _Intensity;
            
            fixed4 frag(v2f IN) : SV_Target
            {
                // Get original sprite color with transparency
                fixed4 c = SampleSpriteTexture(IN.texcoord) * IN.color;
                
                // If transparent pixel, return as-is
                if (c.a == 0) return c;
                
                // Calculate RGB cycle
                float time = _Time.y * _Speed;
                float r = (sin(time) + 1) * 0.5;
                float g = (sin(time + 2.094) + 1) * 0.5; // 120° phase shift
                float b = (sin(time + 4.188) + 1) * 0.5; // 240° phase shift
                
                // Apply RGB cycle while preserving original alpha
                fixed3 rgbCycle = fixed3(r, g, b) * _Intensity;
                c.rgb *= rgbCycle; // Multiply with original color for tint effect
                // Alternative: Replace color completely with: c.rgb = rgbCycle;
                
                // Preserve transparency
                c.rgb *= c.a;
                return c;
            }
            ENDCG
        }
    }
}