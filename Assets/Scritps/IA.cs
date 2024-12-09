using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class IA : MonoBehaviour
{
    //Variables
    public int numeroCapas = 2;
    public int numeroNeuronas = 10;
    public Matriz[] pesos;
    public Matriz[] biases;
    Matriz Input;
    float acceleration;
    float rotation;
    public float score;
    bool dead = false;

    //ForFitness
    private Vector3 lastPosition;
    private float distanceTraveled = 0;
    float accelerationPR = 0;
    int accelerationProm = 0;

    // Start is called before the first frame update
    void Start()
    {
        pesos = new Matriz[numeroCapas];
        biases = new Matriz[numeroCapas];
        Input = new Matriz(1, 5);

        for (int i = 0; i < numeroCapas; i++)
        {
            if (i == 0)
            {
                pesos[i] = new Matriz(5, numeroNeuronas);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, 5);
                biases[i].RandomInitialize();
            }
            else if (i == numeroCapas - 1)
            {
                pesos[i] = new Matriz(2, numeroNeuronas);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, 3);
                biases[i].RandomInitialize();
            }
            else
            {
                pesos[i] = new Matriz(numeroNeuronas, numeroNeuronas);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, numeroNeuronas);
                biases[i].RandomInitialize();
            }
        }

    }

    public void Initialize()
    {
        pesos = new Matriz[numeroCapas];
        biases = new Matriz[numeroCapas];
        Input = new Matriz(1, 5);

        for (int i = 0; i < numeroCapas; i++)
        {
            if (i == 0)
            {
                pesos[i] = new Matriz(5, numeroNeuronas);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, 5);
                biases[i].RandomInitialize();
            }
            else if (i == numeroCapas - 1)
            {
                pesos[i] = new Matriz(2, numeroNeuronas);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, 3);
                biases[i].RandomInitialize();
            }
            else
            {
                pesos[i] = new Matriz(numeroNeuronas, numeroNeuronas);
                pesos[i].RandomInitialize();
                biases[i] = new Matriz(1, numeroNeuronas);
                biases[i].RandomInitialize();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            float FD = GetComponent<Plane>().frontDistance;
            float RD = GetComponent<Plane>().rightDistance;
            float LD = GetComponent<Plane>().leftDistance;
            float UD = GetComponent<Plane>().upDistance;
            float DD = GetComponent<Plane>().downDistance;
            Input.SetAt(0, 0, FD);
            Input.SetAt(0, 1, RD);
            Input.SetAt(0, 2, LD);
            Input.SetAt(0, 3, UD);
            Input.SetAt(0, 4, DD);
            resolve();

            transform.Translate(Vector3.forward * acceleration);
            transform.eulerAngles = transform.eulerAngles + new Vector3(0, (rotation * 90) * 0.02f, 0);

            distanceTraveled += Vector3.Distance(transform.position, lastPosition);
            lastPosition = transform.position;
            accelerationPR += acceleration;
            accelerationProm++;
            SetScore();

            //Se deberia tomar en cuenta tanto la alutra que hay libre hacia arriba y la hacia abajo y con eso poder determinar que hacer 
        }

    }

    void resolve()
    {
        Matriz result;
        result = Activation((Input * pesos[0]) + biases[0]);
        for (int i = 1; i < numeroCapas; i++)
        {
            result = (Activation((pesos[i] * result.Transpose()) + biases[i]));
        }
        ActivationLast(result);
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

    void ActivationLast(Matriz m)
    {
        rotation = (float)MathL.HyperbolicTangtent(m.GetAt(0, 0));
        acceleration = MathL.Sigmoid(m.GetAt(1, 0));
    }

    void SetScore()//FitnessFunction
    {
        float FD = GetComponent<Plane>().frontDistance;
        float RD = GetComponent<Plane>().rightDistance;
        float LD = GetComponent<Plane>().leftDistance;
        float UD = GetComponent<Plane>().upDistance;
        float DD = GetComponent<Plane>().downDistance;
        float s = (FD + RD + LD + UD + DD) / 5;
        s += ((distanceTraveled * 8) + (acceleration));
        score += (float)Math.Pow(s, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dead")
        {
            dead = true;
            Genetics.planeAlive--;
        }
    }
}