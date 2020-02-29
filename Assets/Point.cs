using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    bool isMove = true;

    public Vector2 pos;

    public float speed;

    public float delayTime;

    public Vector2 direct;

    public bool  next = false;

    public bool isDelay=false;
    private void Start()
    {
     
    }

    private void Update()
    {
       
    }

    public bool Interable()
    {
        return next;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            next = true;
        }
    }

    

}
