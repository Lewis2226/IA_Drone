using System;
using UnityEngine;

public static class MathL 
{
    public static float Sigmoid(double value)
    {
        return 1.0f / (1.0f + (float)Math.Exp(-value));
    }

    public static double HyperbolicTangtent(double x)
    {
        if (x < -45.0) return -1.0;
        else if (x > 45.0) return 1.0;
        else return Math.Tanh(x);
    }

    public static float ReLu(float y)//Hace la función de activación ReLu
    {
        return Mathf.Max(0, y);
    }

    public static float ELU(float x)//Hace la función de activación ELU
    {
        float alpha = 1.0f;
        return x > 0 ? x : alpha * (Mathf.Exp(x) - 1);
    }
}

