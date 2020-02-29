using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 13)
        {
            switch (GameController.instance.Game_Type)
            {
                case GAME_MODE_TYPE.MODE_1:

                    BallPlayer.instance.isGround = true;
                    break;
                case GAME_MODE_TYPE.MODE_2:
                  
                    break;
                case GAME_MODE_TYPE.MODE_3:  //Dunk Shot

                    BoardControl.instance.isWall = true;

                    break;
                case GAME_MODE_TYPE.MODE_4:
                  
                    break;
            }

        }
        
    }
}
