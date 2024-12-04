using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matriz : MonoBehaviour
{
    //Variables
    float[,] mat;      
    public int rows;    
    public int columns; 

    
    public Matriz(int _rows, int _columns)//Crea una matriz con el n�mero de columnas y filas dado
    {
        rows = _rows;
        columns = _columns;
        mat = new float[rows, columns]; 
    }

    
    public float GetAt(int x, int y)//Regresa el valor de la posicion dada
    {
        return mat[x, y];
    }

   
    public void SetAt(int x, int y, float v)//Asigna un valor en la posicion dada
    {
        mat[x, y] = v;
    }

  
    public static Matriz operator +(Matriz m1, Matriz m2)//Suma las matrices
    {
        
        if (m1.rows == m2.rows && m1.columns == m2.columns)
        {
           
            for (int i = 0; i < m1.rows; i++)
            {
                for (int j = 0; j < m1.columns; j++)
                {
                    m1.SetAt(i, j, m1.GetAt(i, j) + m2.GetAt(i, j));
                }
            }
        }
        return m1; 
    }

    
    public static Matriz operator *(Matriz m1, Matriz m2)//Multplica las matrices
    {
        
        Matriz mat2 = new Matriz(0, 0);

        
        if (m1.columns == m2.rows)
        {
            
            Matriz mat3 = new Matriz(m1.rows, m2.columns);

            
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
            return mat3; 
        }
        else
        {
           
            UnityEngine.Debug.LogError("FAIL");
            return mat2;
        }
    }

    
    public void RandomInitialize()//Da valores aletorios a la matriz
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                mat[i, j] = UnityEngine.Random.Range(-100f, 100f);
            }
        }
    }

 
    public Matriz Transpose()//Hace la transpuesta de la matriz
    {
        Matriz m = new Matriz(columns, rows); 
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                m.SetAt(j, i, mat[i, j]); 
            }
        }
        return m;
    }


    public static Matriz SinglePointCross(Matriz m1, Matriz m2)//Hace la reproduccion de matrices 
    {
        Matriz mr = new Matriz(m1.rows, m1.columns); 
        int crosspointC = UnityEngine.Random.Range(0, m1.columns); 
        int crosspointR = UnityEngine.Random.Range(0, m1.rows);    


        if (m1.columns == m2.columns && m1.rows == m2.rows)
        {

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
            return mr; 
        }
        UnityEngine.Debug.LogError("BAD SINGLEPOINTCROSS"); 
        return null;
    }

   
    public void Mutate(int mut)//Realiza la mutaci�n de genes
    {
        for (int i = 0; i < mut; i++)
        {
            int n1 = UnityEngine.Random.Range(0, rows);    
            int n2 = UnityEngine.Random.Range(0, columns); 
            mat[n1, n2] = mat[n1, n2] + UnityEngine.Random.Range(-100, 100); 
        }
    }

    public void print()//Imprime los valores que hay en la matriz
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
