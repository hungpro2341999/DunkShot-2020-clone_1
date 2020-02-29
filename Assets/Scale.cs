 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{

    public Vector2 Pos;
    public Vector2 Pos_1;

    public Transform posLast;
    public Transform posLast_1;
    public Vector2 posScale;
    bool done = true;
    public bool destroy = true;
    Vector3 posReal;
     Vector2 direct0;
    Vector2 direct1;
    
   
    public void Getdirect()
    {
     
       direct1 = posLast.position - posLast.parent.position;
        float angle = Mathf.DeltaAngle(Mathf.Atan2(direct0.y, direct0.x) * Mathf.Rad2Deg,
                               Mathf.Atan2(direct1.y,direct1.x) * Mathf.Rad2Deg);
    
      
      
    //    posReal = Quaternion.Euler(0, 0, angle) * Pos;
        posReal = RotatePointAroundPivot(Pos, posLast.parent.position, new Vector3(0, 0, angle));
    }
    // Start is called before the first frame update

    public bool enableAction = true;
    void Start()
    {
      
        direct0 = posLast.position - posLast.parent.position;
        Pos = posLast.position;
        posScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!destroy)
        {
            Getdirect();
            if (transform.parent.transform.Find("Net").GetComponent<Board_Mode_1>().movePath)
            {
                Pos_1 = posLast_1.transform.position;
                float distance = Pos_1.y - posLast.position.y;
                distance = Mathf.Clamp(distance, -0.6f, 1.65f);
                Vector2 scale = posScale;
                scale.y += distance * 0.6f;

                transform.localScale = scale;
            }
            else if (transform.parent.transform.Find("Net").GetComponent<Board_Mode_1>().Board_type == BOARD_TYPE.ROTAION ||
               transform.parent.transform.Find("Net").GetComponent<Board_Mode_1>().Board_type == BOARD_TYPE.JERK_TOP_DOWN_CROSS ||
                transform.parent.transform.Find("Net").GetComponent<Board_Mode_1>().Board_type == BOARD_TYPE.JERK_RIGHT_LEFT_CROSS

                  )
            {
                Pos_1 = posLast_1.transform.position;
                float distance = Pos_1.y - posLast.position.y;
                distance = Mathf.Clamp(distance, -0.6f, 1.65f);
                Vector2 scale = posScale;
                scale.y += distance;

                transform.localScale = scale;
                //        float distance = Vector2.Distance(posLast.position, Pos);
                //   distance = Mathf.Clamp(distance, -999f, 2.2f);
                //   Vector2 scale = posScale;
                ///   scale.y += distance * 0.5f -0.2f;
                //   transform.localScale = scale;
             
            }

            else
            {
                float distance = Vector2.Distance(posLast.position, posReal);
                distance = Mathf.Clamp(distance, -999f, 5f);
                Vector2 scale = posScale;
                scale.y += distance *2.2f;
                transform.localScale = scale;

            }


   

        }

 

    }
    public float dis = 0;
    private void LateUpdate()
    {
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        if (done)
        {
         //   transform.localScale = new Vector2(0.2453337f, 0.3155432f);
            yield return new WaitForSeconds(0.6f);
            done = false;
        }
       
        
       
    }
    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return RotatePointAroundPivot(point, pivot, Quaternion.Euler(angles));
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        return rotation * (point - pivot) + pivot;
    }


}