Shader "Unlit/Shader-Gameplay"
{
    Properties
    {
        _Color("Main Color", Color) = (0.0705,0.16,0.2235,1)
        _MaxColor("Max Color", Color) = (0.21,0.48,0.66,1)
        _BorderColor("Border Color", Color) = (3,3,3,1)
        _InaccessibleColor("Inaccessible Color", Color) = (0,0,0,1)
        _HeightMin("Height Min", Float) = 0.5
        _HeightMax("Height Max", Float) = 20
        _BorderSize("BorderSize", Float) = 5
        _EdgeDetect("Edge Detect Instead?", Float) = 0
        _NormalDepth("Normal Depth", Float) = 3.75
        _NormalEdge("Normal Edge", Float) = 0.85
        _EmissiveAtCombinedValues("Emissive Combined Value", Float) = 0
        _EmissiveForced("Emissive forced", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

         CGPROGRAM
         #pragma surface surf Lambert

        fixed4 _Color;
        fixed4 _MaxColor;
        fixed4 _BorderColor;
        float _BorderSize;
        fixed4 _InaccessibleColor;
        //local or world in the future?
        float _HeightMin;
        float _HeightMax;
        float _EdgeDetect;
        float _NormalDepth;
        float _NormalEdge;
        float _EmissiveForced;
        float _EmissiveAtCombinedValues;


        struct Input
        {
            float3 worldPos;
            float4 screenPos;
        };

        inline half CheckSame(half2 centerNormal, float centerDepth, half4 theSample)
        {
            // difference in normals
            // do not bother decoding normals - there's no need here
            half2 diff = abs(centerNormal - theSample.xy) * _NormalEdge;
            int isSameNormal = (diff.x + diff.y) * _NormalEdge < 0.1;
            // difference in depth
            float sampleDepth = DecodeFloatRG(theSample.zw);
            float zdiff = abs(centerDepth - sampleDepth);
            // scale the required threshold by the distance
            int isSameDepth = zdiff * _NormalDepth < 0.09 * centerDepth;

            // return:
            // 1 - if normals and depth are similar enough
            // 0 - otherwise

            return isSameNormal * isSameDepth ? 1.0 : 0.0;
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        //UNITY_INSTANCING_CBUFFER_START(Props)
            // put more per-instance properties here
        //UNITY_INSTANCING_CBUFFER_END

        /*
        void edgecolor(Input IN, SurfaceOutputStandard o, inout fixed4 color)
        {
            // create our three screen UVs where we sample
            float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
            float sampleSizeX = _CameraDepthNormalsTexture_TexelSize.x;
            float sampleSizeY = _CameraDepthNormalsTexture_TexelSize.y;
            float2 _uv2 = screenUV + float2(-sampleSizeX, -sampleSizeY) * _SampleDistance;
            float2 _uv3 = screenUV + float2(+sampleSizeX, -sampleSizeY) * _SampleDistance;

            // get depth and normals from there
            half4 center = tex2D(_CameraDepthNormalsTexture, screenUV);
            half4 sample1 = tex2D(_CameraDepthNormalsTexture, _uv2);
            half4 sample2 = tex2D(_CameraDepthNormalsTexture, _uv3);

            // encoded normal
            half2 centerNormal = center.xy;
            // decoded depth
            float centerDepth = DecodeFloatRG(center.zw);

            // calculate how faded the edge is
            float d = clamp(centerDepth * _Falloff - 0.05, 0.0, 1.0);
            half4 depthFade = half4(d, d, d, 1.0);

            // is it an edge? 0 if yes, 1 if no
            half edge = 1.0;
            edge *= CheckSame(centerNormal, centerDepth, sample1);
            edge *= CheckSame(centerNormal, centerDepth, sample2);

            // calculate this fragment/pixel's color!
            color = edge * color + (1.0 - edge) * depthFade * color;
        }*/


        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 tintColor;
            if (_EdgeDetect < 1)
            {
                float h = clamp(((_HeightMax - IN.worldPos.y) / (_HeightMax - _HeightMin)), 0, 1);

                if (IN.worldPos.y > _HeightMax + _BorderSize)
                {
                    tintColor = _InaccessibleColor;
                }
                else if (IN.worldPos.y > _HeightMax && IN.worldPos.y < _HeightMax + _BorderSize)
                {
                    tintColor = _BorderColor;
                }
                else
                {
                    tintColor = lerp(_MaxColor.rgba, _Color.rgba, h);
                }
            }
            else 
            {
                tintColor = float4(0, 0, 0, 0);
            }
            o.Albedo = tintColor.rgb;
            o.Alpha = tintColor.a;
            float _checkEmis = tintColor.r+ tintColor.g+tintColor.b;

            if ((_EmissiveAtCombinedValues != 0 &&_checkEmis > _EmissiveAtCombinedValues) || _EmissiveForced > 0.0)
                o.Emission = o.Albedo;
        }

        ENDCG
    }
}
