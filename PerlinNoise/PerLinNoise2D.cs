using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// PerLinNoise2D 注释
/// <para>Author: zhaojun jun.zhao@ifreeteam.com</para>
/// <para>Date: 2014/5/30 10:26:12</para>
/// <para>$Id$</para>
/// </summary>

namespace PerlinNoise
{
    class PerLinNoise2D
    {
        private static float noise(int x, int y)
        {
            int n = x + y * 57;
            n = (n << 13) ^ n;
            return (float)( 1.0 - ( (n * (n * n * 15731 + 789221) + 1376312589) & 0x7fffffff) / 1073741824.0); 
        }

        private static float smoothNoise(int x, int y)
        {
            float corners = (noise(x - 1, y - 1) + noise(x - 1, y + 1) + noise(x + 1, y - 1) + noise(x + 1, y + 1)) / 16;
            float sides = (noise(x - 1, y) + noise(x + 1, y) + noise(x, y - 1) + noise(x, y + 1)) / 8;
            float center = noise(x, y) / 4;
            return (corners + sides + center);
        }

        private static float interpolate(float x, float y)
        {
            int xx = (int)x;
            int yy = (int)y;
            float fractional_x = x - xx;
            float fractional_y = y - yy;

            float v1 = smoothNoise(xx, yy);
            float v2 = smoothNoise(xx, yy + 1);
            float v3= smoothNoise(xx + 1, yy);
            float v4 = smoothNoise(xx + 1, yy + 1);

            float i1 = PerlinNoise.PerlinNoise1D.Cosine_Interpolate(v1, v2, fractional_x);
            float i2 = PerlinNoise1D.Cosine_Interpolate(v3, v4, fractional_y);

            return PerlinNoise1D.Cosine_Interpolate(i1, i2, Mathf.Sqrt(fractional_y*fractional_x));

        }

        public static float genPerlinNoise(float x,float y)
        {
            float total = 0.0f;
            float persistence = 0.4f;
            int octaves = 6;
            for (int i = 0; i < octaves; i++)
            {
                float frequency = (1 << i);
                float amplitude = Mathf.Pow(persistence, i);
                total += interpolate(x * frequency,y*frequency) * amplitude;
            }
            return total;
        }

    }

    
}
