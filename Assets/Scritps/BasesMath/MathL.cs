using System;
using UnityEngine;

public static class MathL 
{
    public static double Sigmoid(double value)
    {
        return 1.0f / (1.0f + (float)Math.Exp(-value)); ;
    }

    public static double HyperbolicTangtent(double x)
    {
        if (x < -45.0) return -1.0;
        else if (x > 45.0) return 1.0;
        else return Math.Tanh(x);
    }

    public static double ReLu(float x)
    {
        return Mathf.Max(0, x);
    }

    public static float ELU(float x)
    {
        float alpha = 1.0f;
        return x > 0 ? x : alpha * (Mathf.Exp(x) - 1);
    }
}

