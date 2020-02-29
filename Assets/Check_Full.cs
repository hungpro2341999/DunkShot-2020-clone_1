using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Full : MonoBehaviour
{

    public string key;

    public Check_Board Check_Board;
    public CheckInBall Check_in_Ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
          
                Check_in_Ball.ResetKey();
            
        }
    }
    
   
      
  

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13)

        {
          
            Check_Board.isBound = true;
            

           
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 13)
        {
            if (!Check_Board.isGoard())
            {
                Check_Board.Reset_board();
                Check_Board.isBound = false;

            }
           

      
        }

    }
  
}
