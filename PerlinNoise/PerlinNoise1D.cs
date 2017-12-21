using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// PerlinNoise 注释
/// <para>Author: zhaojun jun.zhao@ifreeteam.com</para>
/// <para>Date: 2014/5/30 9:50:04</para>
/// <para>$Id$</para>
/// </summary>

namespace PerlinNoise
{
    class PerlinNoise1D
    {
        private static float noise(int x)
        {
            x = (x<<13) ^ x;
            return (float)( 1.0 - ( (x * (x * x * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0d); 
        }

        private static float smoothNoise(int x)
        {
            return noise(x) / 2 + noise(x - 1) / 4 + noise(x + 1) / 4;
        }

        public static float Cosine_Interpolate(float a, float b, float x)
        {
            float ft = x * 3.1415927f;
            float f = (1 - Mathf.Cos(ft)) * 0.5f;
            return a * (1 - f) + b * f;
        }

        private static float InterpolateNoise(float x)
        {
            int xx = (int)x;
            float fractional_x = x-xx;
            float v1 = smoothNoise(xx);
            float v2 = smoothNoise(xx+1);
            return Cosine_Interpolate(v1, v2, fractional_x);
        }

        public static float genPerlinNoise(float x)
        {
            float total = 0.0f;
            float persistence = 0.8f;
            int octaves = 6;
            for (int i = 0; i < octaves; i++)
            {
                int frequency = 1 << i;
                float amplitude = Mathf.Pow(persistence, i);
                total += InterpolateNoise(x * frequency) * amplitude;
            }
            return total;
        }

    }
    
}
