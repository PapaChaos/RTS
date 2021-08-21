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
