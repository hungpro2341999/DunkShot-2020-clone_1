using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public bool UpdateSound = true;
    public string key;
    public Check_Board Check_Board;
    public CheckInBall Check_In_Ball;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //13 :::::::::Player


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            
            AutioControl.instance.GetAudio("Wall").Play();
            Check_In_Ball.Check_Ball[key] = 1;

            // Debug.Log("KEY "+key+" "+ Check_Board.instance.Score[key]);

        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 13)
        {
            if (Check_Board != null)
            {
             //   AutioControl.instance.GetAudio("Wall").Play();
                if (Check_Board != null)
                    if (Check_Board.isBound)
                    {

                        {

                            Check_Board.SetValueKey(key, 1);


                        }

                    }

            }


        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 13)
        {
            if (Check_Board != null)
            {
                AutioControl.instance.GetAudio("Wall").Play();
                if (Check_Board != null)
                    if (Check_Board.isBound)
                    {

                        {

                            //Check_Board.SetValueKey(key, 1);


                        }

                    }

            }


        }

        if (gameObject.gameObject.layer == 15)
        {
            AutioControl.instance.GetAudio("Wall2").Play();

        }
       else if (collision.gameObject.layer == 13)
        {
            if (gameObject.name == "BottomBoardObs"&&GameController.instance.Game_Type==GAME_MODE_TYPE.MODE_1)
            {
                if (UpdateSound)
                {
                    AutioControl.instance.GetAudio("ballground").Play();
                 //   Debug.Log("Ground");
                    collision.gameObject.GetComponent<BallPlayer>().isGround = false;
                    UpdateSound = false;
                }
            }
            else if (Check_Board!=null)
            {
                if (Check_Board.isBound)
                {
                    if (gameObject.name == "giado 1")
                    {
                        AutioControl.instance.GetAudio("SlamBoard").Play();
                        Check_Board.SetValueKey(key, 1);
                    }
                 
                    
                      
                    

                }
               

            }
          
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_1 && gameObject.name == "BottomBoardObs")
        {
            UpdateSound = true; 

        }
    }

   
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.layer == 13)
        {

            if (Check_Board.isBound)
            {
               
                {
                  
                    Check_Board.SetValueKey(key, 1);
                    if(GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_2)
                    {
                        Check_Board.DestroyBy(other.GetComponent<Bullet>().indexBullet, key);
                        Debug.Log(other.GetComponent<Bullet>().indexBullet);

                    }
                
                }

            }
           

        }





    }
}






