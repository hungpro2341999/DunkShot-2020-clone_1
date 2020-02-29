using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector2 PosMousePosition;
    public Transform posButtom;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        MouseMove();
        
    }
    public void Direct()
    {
        float angle = Mathf.Atan2(DirectRotation().y, DirectRotation().x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);

    }

    public Vector2 DirectRotation()
    {
        return ((Vector2)transform.position - PosMousePosition);

    }
    public void MouseMove()
    {
        PosMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);



    }
    
   
}
