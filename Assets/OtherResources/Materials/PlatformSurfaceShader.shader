Shader "Custom/PlatformSurfaceShader"
{
    Properties
    {
        _SolidColor ("SolidColor", Color) = (1,1,1,1)
		_TransColor ("TransColor", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
		// Surface
        Tags {"Queue" = "Transparent" "RenderType"="Transparent"}
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _SolidColor;
		fixed4 _TransColor;
		uniform fixed3 _ORB_LEFT_POS = (0,0,0);
		uniform float _ORB_LEFT_SIZE = 0.0;
		uniform fixed3 _ORB_RIGHT_POS = (0,0,0);
		uniform float _ORB_RIGHT_SIZE = 0.0;
		uniform float _TICKER = 1.0;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

		float mmod(float x, float m) {
			return ((x % m) + m) % m;
		}

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
			if (sqrt(pow(_ORB_LEFT_POS.x-IN.worldPos.x, 2) + pow(_ORB_LEFT_POS.y-IN.worldPos.y, 2) + pow(_ORB_LEFT_POS.z-IN.worldPos.z, 2)) < _ORB_LEFT_SIZE
			 || sqrt(pow(_ORB_RIGHT_POS.x-IN.worldPos.x, 2) + pow(_ORB_RIGHT_POS.y-IN.worldPos.y, 2) + pow(_ORB_RIGHT_POS.z-IN.worldPos.z, 2)) < _ORB_RIGHT_SIZE)
			{
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _SolidColor;
				o.Albedo = c.rgb;
				o.Alpha = _SolidColor.a;
			} else {
				// Albedo comes from a texture tinted by color
				fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _TransColor;
				o.Albedo = c.rgb;
				o.Alpha = _TransColor.a;
			}
			// Animated stripes
			float t = _TICKER / 2200;
			float intv = 0.02;
			float wid = 0.0125;
			float wmx = mmod(IN.worldPos.x, intv);
			float wmy = mmod(IN.worldPos.y, intv);
			float wmz = mmod(IN.worldPos.z, intv);
			float b1 = mmod(t, intv);
			float b2 = mmod((t + wid), intv);
			if (b2 > b1) {
				if ((wmx >= b1 && wmx <= b2) && (wmy >= b1 && wmy <= b2) && (wmz >= b1 && wmz <= b2))
					o.Alpha = min(1, o.Alpha + 0.15);
			} else {
				if ((wmx >= b1 || wmx <= b2) && (wmy >= b1 || wmy <= b2) && (wmz >= b1 || wmz <= b2))
					o.Alpha = min(1, o.Alpha + 0.15);
			}
        }
        ENDCG
    }
    FallBack "Diffuse"
}
