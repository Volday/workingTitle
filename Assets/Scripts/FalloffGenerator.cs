using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FalloffGenerator {

    public static float[,] GenerateFalloffMap(int size, int FalloffMode) {
        float[,] map = new float[size,size];

        for (int i = 0; i < size; i++) {
            for (int j = 0; j < size; j++)
            {
                float x = i / (float)size * 2 - 1;
                float y = j / (float)size * 2 - 1;
                float value = 0;
                // 0 Вокруг, 1 с одной стороны, 2 с двух сторон(внешний угол), 3 кроме стороны, 4 с двух сторон
                if (FalloffMode == 0) {
                    value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                }else if (FalloffMode == 1)
                {
                    value = x;
                }else if (FalloffMode == 2){
                    value = Mathf.Max(x, y);
                }
                else if (FalloffMode == 3)
                {
                    value = Mathf.Max(Mathf.Abs(x), y);
                }
                else if (FalloffMode == 4)
                {
                    value = Mathf.Abs(x);
                }
                map[i, j] = Evaluate(value);
            }
        }

        return map;
    }


    //Функция кривой для краёв карты
    static float Evaluate(float value) {
        float a = 3f;
        float b = 2.2f;

        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
    }
}
