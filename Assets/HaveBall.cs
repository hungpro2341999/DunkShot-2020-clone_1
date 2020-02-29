using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaveBall : MonoBehaviour
{

    public  GameObject Ball;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    private void Update()
    {
       
        if (transform.Find("Ball")!= null|| transform.Find("Ball_Effect") != null)
        {

            transform.parent.gameObject.tag = "Net_In"; 
    //      Debug.Log("Have");
        }
        else if((transform.Find("Ball") == null || transform.Find("Ball_Effect") == null))
        {
            transform.parent.gameObject.tag = "Net_Out";
        }

        
              
            
         

    }
    IEnumerator SpawnBall()
    {
        Transform trs = transform.parent;
        yield return new WaitForSeconds(2);
      var a =    Instantiate(Ball, Vector3.zero, Quaternion.identity, transform);
        a.GetComponent<Board_Mode_4>().Board = trs.Find("Net").GetComponent<Board_Mode_1>();
        a.GetComponent<Board_Mode_4>().rotate = trs.Find("Hool").GetComponent<Rotate>();
        a.GetComponent<Board_Mode_4>().Board_1 = trs.Find("Img").GetComponent<Board_Rotation>();
    }
}
