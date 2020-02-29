using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewBall : MonoBehaviour
{
    public GameObject Ball;
    public Transform trans;
    public int count = 30;
    bool notSpawn = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }
    public void SetBullet()
    {

    }
    // Update is called once per frame

    private void Update()
    {

        if (transform.Find("Ball_Effect") != null || transform.Find("Ball_Effect(Clone)") != null)
        {

            transform.parent.gameObject.tag = "Net_In";
               Debug.Log("Have");
        }
        else if ( transform.Find("Ball_Effect") == null || transform.Find("Ball_Effect(Clone)") == null)
        {
          
         //   {
           //     for (int i = 0; i < count; i++)
            //    {
                    StartCoroutine(SpawnBall());
                    Debug.Log(" Not Have");
                    notSpawn = true;
          //      }
          //  }
           
            transform.parent.gameObject.tag = "Net_Out";
     
        }






    }
    IEnumerator SpawnBall()
    {
        Transform trs = transform.parent;
        yield return new WaitForSeconds(1);
        var a = Instantiate(Ball,trans.position, Quaternion.identity, transform);
        a.GetComponent<Board_Mode_4>().Board = trs.Find("Net").GetComponent<Board_Mode_1>();
        a.GetComponent<Board_Mode_4>().rotate = trs.Find("Hool").GetComponent<Rotate>();
        a.GetComponent<Board_Mode_4>().Board_1 = trs.Find("Img").GetComponent<Board_Rotation>();
    }
}
