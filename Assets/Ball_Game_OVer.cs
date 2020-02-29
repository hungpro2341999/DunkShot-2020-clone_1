using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Game_OVer : MonoBehaviour
{
    public Rigidbody2D body;

    float radius;
    private void Start()
    {
        radius = GetComponent<SpriteRenderer>().size.x/2;
        body = GetComponent<Rigidbody2D>();
    }
    public void StartMove()
    {
        
    }
}
