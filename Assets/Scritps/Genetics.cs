using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Genetics : MonoBehaviour
{
    //Variables
    public TextMeshProUGUI textoEpocas;
    public TextMeshProUGUI textoVelocidad;
    public int epocas = 1;
    public GameObject planePrefab;
    public static int planeAlive;

    public int poblacion = 30;
    public float probMutación = .75f;

    public int mejoresCromosomas = 5;
    public int peoresCromosomas = 4;
    public int cromosomasParaMutar = 20;
    public int mutacionesPorCromosomas = 7;

    private List<GameObject> Planes;
    private List<GameObject> newsPlanes;

    
    void Start()
    {
        NormalSpeed();
        planeAlive = poblacion;
        Planes = new List<GameObject>();
        newsPlanes = new List<GameObject>();

        for(int i = 0; i< poblacion; i++) 
        {
            GameObject newObject = Instantiate(planePrefab) as GameObject;
            Planes.Add(newObject);
        }
    }

    
    void Update()
    {
        textoEpocas.text = $"Época {epocas.ToString()}";
        if (planeAlive <= 0)
        {
            NextEpoch();
            DeletePlane();
            planeAlive = poblacion;
            epocas++;
        }
    }

    void DeletePlane()
    {
        for(int i = 0; i< Planes.Count; i++)
        {
            Destroy(Planes[i]);
        }
        Planes.Clear();
        Planes = newsPlanes;
    }

     void NextEpoch()
    {
        Planes.Sort((x, y) => x.GetComponent<IA>().score.CompareTo(y.GetComponent<IA>().score));
        List<GameObject> PlanesNews;
        PlanesNews = new List<GameObject>();

        for(int i = 0 ; i <mejoresCromosomas; i++)
        {
            PlanesNews.Add(Copy(Planes[poblacion - 1 - i]));
        }

        for (int i = 0; i < peoresCromosomas; i++)
        {
            PlanesNews.Add(Copy(Planes[i]));
        }
        int k = mejoresCromosomas + peoresCromosomas;

        while (k < poblacion)
        {
            int n1 = Random.Range(0, k - 1);
            int n2 = Random.Range(0, k - 1);
            PlanesNews.Add(Cross(PlanesNews[n1], PlanesNews[n2]));
            k++;
        }

        //Mutaciones
        for (int i = 0; i < cromosomasParaMutar; i++)
        {
           
            int n1 = Random.Range(0, poblacion - 1);
            IA iaN = PlanesNews[n1].GetComponent<IA>();

            
            for (int j = 0; j < iaN.biases.Length; j++)
            {
                PlanesNews[n1].GetComponent<IA>().biases[j].Mutate(mutacionesPorCromosomas);
            }
            
            for (int j = 0; j < iaN.pesos.Length; j++)
            {
                PlanesNews[n1].GetComponent<IA>().pesos[j].Mutate(mutacionesPorCromosomas);
            }
        }
        newsPlanes = PlanesNews;
    }

    GameObject Cross(GameObject g1, GameObject g2)
    {

        GameObject newObject = Instantiate(planePrefab) as GameObject;
        GameObject cross = newObject;
        cross.GetComponent<IA>().Initialize();
        IA ia1 = g1.GetComponent<IA>();
        IA ia2 = g2.GetComponent<IA>();

        for (int i = 0; i < ia1.biases.Length; i++)
        {
            cross.GetComponent<IA>().biases[i] = Matriz.SinglePointCross(ia1.biases[i], ia2.biases[i]);
        }

        for (int i = 0; i < ia1.pesos.Length; i++)
        {
            cross.GetComponent<IA>().pesos[i] =  ia1.pesos[i] + ia2.pesos[i];
        }
        return cross;
    }
    

    GameObject Copy(GameObject c)
    {
        GameObject newObject = Instantiate(planePrefab) as GameObject;
        GameObject copy = newObject;
        copy.GetComponent<IA>().Initialize();
        IA ia = c.GetComponent<IA>();

        for (int i = 0; i < ia.biases.Length; i++)
        {
            copy.GetComponent<IA>().biases[i] = ia.biases[i];
        }

        for (int i = 0; i < ia.pesos.Length; i++)
        {
            copy.GetComponent<IA>().pesos[i] = ia.pesos[i];
        }
        return copy;
    }
    
    public void NewGeneration()
    {
        NextEpoch();
        DeletePlane();
        planeAlive = poblacion;
        epocas++;
    }

    public void NormalSpeed()
    {
        Time.timeScale = 1.0f;
        textoVelocidad.text = "1x";
    }

    public void SpeedBoost()
    {
        Time.timeScale = 2.0f;
        textoVelocidad.text = "2x";
    }
}
