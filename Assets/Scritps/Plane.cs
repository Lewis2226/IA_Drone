using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    //Variables
    Vector3 pos;
    Vector3 front;
    Vector3 left;
    Vector3 right;
    Vector3 up;
    Vector3 down;

    public float frontDistance;
    public float leftDistance;
    public float rightDistance;
    public float upDistance;
    public float downDistance;


    void Update() //Asigna los valores a los vectores de las direcciones 
    {
        front = transform.TransformDirection(Vector3.forward) * 30;
        left = transform.TransformDirection(new Vector3(.5f, 0, 1)) * 30;
        right = transform.TransformDirection(new Vector3(-.5f, 0, 1)) * 30;
        up = transform.TransformDirection(new Vector3(0f, .5f, 1)) * 30;
        down = transform.TransformDirection(new Vector3(0f, -.5f, 1)) * 30;

        pos = new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z);

        Debug.DrawLine(pos, pos + front, Color.red);
        Debug.DrawLine(pos, pos + left, Color.red);
        Debug.DrawLine(pos, pos + right, Color.red);
        Debug.DrawLine(pos, pos + up, Color.blue);
        Debug.DrawLine(pos, pos + down, Color.blue);
    }

    private void FixedUpdate()//Revisa si hay algo en alguno de las direcciones
    {
        RaycastHit hit;
        RaycastHit hit1;
        RaycastHit hit2;
        RaycastHit hit3;
        RaycastHit hit4;

        Ray rayFront = new Ray(pos,front);
        Ray rayLeft = new Ray(pos,left);
        Ray rayRight = new Ray(pos,right);
        Ray rayUp = new Ray(pos,up);
        Ray rayDown = new Ray(pos,down);

        frontDistance = 1;
        leftDistance = 1;
        rightDistance = 1;
        upDistance = 1;
        downDistance = 1;

        if(Physics.Raycast(pos,front, out hit, 25))
        {
            frontDistance = hit.distance/25;
        }

        if(Physics.Raycast(pos,left, out hit1, 25))
        {
            leftDistance = hit1.distance/25;
        }

        if(Physics.Raycast(pos,right, out hit2, 25))
        {
            rightDistance = hit2.distance/25;
        }

        if(Physics.Raycast(pos,up, out hit3, 25))
        {
            upDistance = hit3.distance/25;
        }

        if(Physics.Raycast(pos,down, out hit4, 25))
        {
            downDistance = hit4.distance/25;
        }

        //Debug.Log($"Distancia al frente {frontDistance}, distancia al la izquierda {leftDistance}, distancia a la derecha {rightDistance}, distancia a arriba {upDistance} y la distancia a abajo{downDistance}");
    }
}
