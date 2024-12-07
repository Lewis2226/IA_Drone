using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    //Variables
    public int capasNum;
    public int neuronasNum;
    public Matriz[] pesos; 
    public Matriz[] biases;
    public float score;
    Matriz inputs;
    float acceleration;
    float height;
    float rotation;
    bool alive = true;

    Vector3 lastPostion;
    float distanceTravel;
    float accelerationPR = 0;
    int accelerationProm = 0;

    void Start()
    {
        pesos = new Matriz[capasNum];
        biases = new Matriz[capasNum];
        inputs = new Matriz(1,5);//Tal vez necesite 5 entradas 

        for(int i = 0; i < capasNum; i++)
        {
            if (i == 0)
            {
                pesos[i] = new Matriz(3, neuronasNum);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, 3);
                biases[i].RandomInitialize();
            }
            else if (i == (capasNum -1)) 
            {
                pesos[i] = new Matriz(2, neuronasNum);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, 2);
                biases[i].RandomInitialize();
            }

            else
            {
                pesos[i] = new Matriz (neuronasNum, neuronasNum);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, neuronasNum);
                biases[i].RandomInitialize();
            }
        }
    }

    public void Initialize()
    {
        pesos = new Matriz[capasNum];
        biases = new Matriz[capasNum];
        inputs = new Matriz(1, 5);

        for (int i = 0; i < capasNum; i++)
        {
            if (i == 0)
            {
                pesos[i] = new Matriz(3, neuronasNum);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, 3);
                biases[i].RandomInitialize();
            }
            else if (i == (capasNum - 1))
            {
                pesos[i] = new Matriz(2, neuronasNum);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, 2);
                biases[i].RandomInitialize();
            }

            else
            {
                pesos[i] = new Matriz(neuronasNum, neuronasNum);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, neuronasNum);
                biases[i].RandomInitialize();
            }
        }
    }

    void Update()
    {
        if (!alive)
        {
            float FD = GetComponent<Plane>().frontDistance;
            float RD = GetComponent<Plane>().rightDistance;
            float LD = GetComponent<Plane>().leftDistance;
            float UD = GetComponent<Plane>().upDistance;
            float DD = GetComponent<Plane>().downDistance;
            inputs.SetAt(0, 0, FD);
            inputs.SetAt(0, 1, RD);
            inputs.SetAt(0, 2, LD);
            inputs.SetAt(0, 3, UD);
            inputs.SetAt(0, 4, DD);
            resolve();

            transform.Translate((Vector3.forward)* acceleration);
            transform.eulerAngles = transform.eulerAngles + new Vector3(0, (rotation * 90) * 0.02f, 0);
            distanceTravel += Vector3.Distance(transform.position, lastPostion);
            lastPostion = transform.position;
            accelerationPR += acceleration;
            accelerationProm++;
        }
    }

    void resolve()
    {
        Matriz result;
        result = Activation((inputs * pesos[0]) + biases[0]);

        for(int i = 1; i < capasNum; i++)
        {
            result = (Activation((pesos[i] * result.Transpose()) + biases[i]));
        }
        ActivationEnd(result);
    }

    Matriz Activation(Matriz m)
    {
        for (int i = 0; i < m.rows; i++)
        {
            for (int j = 0; j < m.columns; j++)
            {
                m.SetAt(i, j, (float)MathL.HyperbolicTangtent(m.GetAt(i, j)));
            }
        }

        return m;
    }

    void ActivationEnd(Matriz m)
    {
        acceleration = (float)MathL.HyperbolicTangtent(m.GetAt(0, 1));
        height = MathL.ReLu(m.GetAt(0,1));
        rotation = MathL.Sigmoid(m.GetAt(1, 0));
    }

    void SetScore()
    {
        float FD = GetComponent<Plane>().frontDistance;
        float RD = GetComponent<Plane>().rightDistance;
        float LD = GetComponent<Plane>().leftDistance;
        float UD = GetComponent<Plane>().upDistance;
        float DD = GetComponent<Plane>().downDistance;

        float s = (FD + RD + LD + UD + DD) / 5;
        s += ((distanceTravel * 8) + (acceleration));
        score += (float)Math.Pow(s, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dead")
        {
            alive = false;
            Genetics.planeAlive--;
        }
    }
}
