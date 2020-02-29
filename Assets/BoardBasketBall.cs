using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBasketBall : MonoBehaviour
{
    public float Move_with_distance;

    public float speed = 1;

    float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {
        MoveWithDistance(0.1f);
    }

    public void MoveWithDistance(float distance)
    {
        count += Time.deltaTime * speed;

        float dis = Mathf.Sin(count) * distance;
        float y = transform.position.y + dis;
        Vector2 pos = new Vector2(transform.position.x,y );
        transform.position = pos;
    }
}
