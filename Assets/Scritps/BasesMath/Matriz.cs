using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matriz : MonoBehaviour
{
    // Atributos de la clase
    float[,] mat;      
    public int rows;    
    public int columns; 

    // Constructor: Inicializa una matriz de dimensiones específicas
    public Matriz(int _rows, int _columns)
    {
        rows = _rows;
        columns = _columns;
        mat = new float[rows, columns]; // Crea la matriz con el tamaño dado
    }

    // Método para obtener un valor de la matriz en una posición específica
    public float GetAt(int x, int y)
    {
        return mat[x, y];
    }

    // Método para establecer un valor en una posición específica
    public void SetAt(int x, int y, float v)
    {
        mat[x, y] = v;
    }

    // Sobrecarga del operador + para sumar dos matrices
    public static Matriz operator +(Matriz m1, Matriz m2)
    {
        // Verifica que las matrices tengan las mismas dimensiones
        if (m1.rows == m2.rows && m1.columns == m2.columns)
        {
            // Itera sobre cada posición y realiza la suma
            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m1.columns; j++)
                {
                    m1.SetAt(i, j, m1.GetAt(i, j) + m2.GetAt(i, j));
                }
            }
        }
        return m1; // Devuelve la primera matriz modificada
    }

    // Sobrecarga del operador * para multiplicar dos matrices
    public static Matriz operator *(Matriz m1, Matriz m2)
    {
        // Matriz vacía que se devuelve si las dimensiones no son compatibles
        Matriz mat2 = new Matriz(0, 0);

        // La multiplicación solo es válida si las columnas de m1 son iguales a las filas de m2
        if (m1.columns == m2.rows)
        {
            // Crea una nueva matriz para almacenar el resultado
            Matriz mat3 = new Matriz(m1.rows, m2.columns);

            // Realiza la multiplicación de matrices
            for (int i = 0; i < m1.rows; i++)
            {
                for (int k = 0; k < m2.columns; k++)
                {
                    for (int j = 0; j < m2.rows; j++)
                    {
                        mat3.SetAt(i, k, mat3.GetAt(i, k) + m1.GetAt(i, j) * m2.GetAt(j, k));
                    }
                }
            }
            return mat3; // Devuelve el resultado
        }
        else
        {
            // Error: las dimensiones no son compatibles
            UnityEngine.Debug.LogError("FAIL");
            return mat2;
        }
    }

    // Inicializa la matriz con valores aleatorios entre -100 y 100
    public void RandomInitialize()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                mat[i, j] = UnityEngine.Random.Range(-100f, 100f);
            }
        }
    }

    // Devuelve la transpuesta de la matriz (intercambia filas y columnas)
    public Matriz Transpose()
    {
        Matriz m = new Matriz(columns, rows); // Crea una nueva matriz con dimensiones invertidas
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                m.SetAt(j, i, mat[i, j]); 
            }
        }
        return m;
    }

    // Realiza un cruzamiento de un solo punto entre dos matrices
    public static Matriz SinglePointCross(Matriz m1, Matriz m2)
    {
        Matriz mr = new Matriz(m1.rows, m1.columns); 
        int crosspointC = UnityEngine.Random.Range(0, m1.columns); 
        int crosspointR = UnityEngine.Random.Range(0, m1.rows);    

        // Verifica que las matrices tengan las mismas dimensiones
        if (m1.columns == m2.columns && m1.rows == m2.rows)
        {
            // Llena la nueva matriz según los puntos de cruce
            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m1.columns; j++)
                {
                    if (i < crosspointC || j < crosspointR)
                    {
                        mr.SetAt(i, j, m1.GetAt(i, j)); 
                    }
                    else
                    {
                        mr.SetAt(i, j, m2.GetAt(i, j)); 
                    }
                }
            }
            return mr; // Devuelve la matriz resultante
        }
        UnityEngine.Debug.LogError("BAD SINGLEPOINTCROSS"); // Error si las dimensiones no coinciden
        return null;
    }

    // Mutación: cambia un número aleatorio de valores en la matriz
    public void Mutate(int mut)
    {
        for (int i = 0; i < mut; i++)
        {
            int n1 = UnityEngine.Random.Range(0, rows);    
            int n2 = UnityEngine.Random.Range(0, columns); 
            mat[n1, n2] = mat[n1, n2] + UnityEngine.Random.Range(-100, 100); 
        }
    }

    // Imprime todos los valores de la matriz en la consola
    public void print()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.WriteLine(mat[i, j]); 
            }
        }
    }
}
