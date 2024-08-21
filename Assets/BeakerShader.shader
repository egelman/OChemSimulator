Shader "Custom/BeakerShader"
{
    Properties
    {
        _Color ("Top Color", Color) = (1,1,1,1)
        _BaseColor ("Base Color", Color) = (0,0,1,1)
        _TransitionValue ("Transition Value", Range(0,1)) = 0
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldNormal;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed4 _BaseColor;
        half _TransitionValue;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float heightFactor = dot(normalize(IN.worldNormal), float3(0,1,0));
            fixed4 gradColor = lerp(_BaseColor, _Color, heightFactor);
            fixed4 finalColor = lerp(gradColor, _Color, _TransitionValue);
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * finalColor;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG

    }
    FallBack "Diffuse"
}
