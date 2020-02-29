using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector2 position;
    public Vector3 point;

    public float size = 5;
    // Start is called before the first frame update
    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {

        Camera.main.orthographicSize = size;

        point = Camera.main.ScreenToWorldPoint(new Vector3(0, 1280, Camera.main.nearClipPlane));

        position = point;

        transform.position = position;
    }
}
