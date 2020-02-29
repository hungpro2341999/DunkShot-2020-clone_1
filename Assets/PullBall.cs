using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {

            SpawnEffect.instance.Set_System_Wall(new Vector2(0,0), null, true);
        }
    }
    bool isColl = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
                Debug.Log("Da tuong tac");

            if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_4&&gameObject.layer!=17)
            {
                collision.gameObject.GetComponent<Ball_Effect>().changeDirect();
                AutioControl.instance.GetAudio("Wall2").Play();
            }


            if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_3)
            { 
               if(gameObject.name == "LeftBoard2D")
                {
                    SpawnEffect.instance.Set_System_Wall(collision.gameObject.transform.position, null, true);
                   // Debug.Log("Spawn");
                }
                else if(gameObject.name == "RightBoard2D")
                {
                   SpawnEffect.instance.Set_System_Wall(collision.gameObject.transform.position, null, false);
                   // Debug.Log("Spawn");
                }
                AutioControl.instance.GetAudio("Wall2").Play();
                collision.gameObject.GetComponent<BoardControl>().changeDirect();
                collision.gameObject.GetComponent<BoardControl>().countWall++;
            }
            else
            {
                if (!isColl)
                {
                    if (gameObject.name == "UpBody2D")
                    {
                        AutioControl.instance.GetAudio("Wall2").Play();
                        //   collision.gameObject.GetComponent<Ball_Effect>().changeDirectX();

                    }
                    else
                    {


                    }
                }
               

            }
         
        }


        if (gameObject.layer == 17 && collision.gameObject.layer == 14)
        {
            AutioControl.instance.GetAudio("shootMonster").Play();
            collision.gameObject.GetComponent<Ball_Effect>().changeDirect();

        }
        if (gameObject.name == "UpBody2D" && collision.gameObject.layer == 14)
        {
            collision.gameObject.GetComponent<Ball_Effect>().Body.velocity = Vector2.zero;
        }
        

    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 14)
        {
            isColl = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AutioControl.instance.GetAudio("Wall2").Play();
            collision.gameObject.GetComponent<Bullet>().ChangeDirect();
        }

    }
}
