using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiiterWindow : MonoBehaviour
{
    bool Update_1 = false;
     public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
      
   
    }

    // Update is called once per frame
    void Update()
    {
        if (!Update_1)
        {
          
            switch (GameController.instance.Game_Type)
            {
                case GAME_MODE_TYPE.MODE_1:

                    transform.Find("Panel/Score").GetComponent<Text>().text = BallPlayer.instance.getScore().ToString();
                    transform.Find("Panel/BestScore").GetComponent<Text>().text = "Best " + GameController.instance.GetHighScoreModeTapTAp();
                    GameController.instance.SaveScoreTapTap(BallPlayer.instance.getScore());
                    break;
                case GAME_MODE_TYPE.MODE_2:


                    transform.Find("Panel/Score").GetComponent<Text>().text = Ball_Player_2.instance.GetScore().ToString();
                    Debug.Log("SCORE : " + Ball_Player_2.instance.GetScore());
                    transform.Find("Panel/BestScore").GetComponent<Text>().text = "Best " + GameController.instance.GetHighScoreModeSHoot();
                    GameController.instance.SaveScoreShoot(Ball_Player_2.instance.GetScore());



                    break;
                case GAME_MODE_TYPE.MODE_3:
                    transform.Find("Panel/Score").GetComponent<Text>().text = BoardControl.instance.GetScore().ToString();
                    transform.Find("Panel/BestScore").GetComponent<Text>().text = "Best " + GameController.instance.GetHighScoreModeDropDrap();
                    GameController.instance.SaveSCoreModeDropDrap((int)BoardControl.instance.GetScore());
                    break;

            }
            transform.Find("Panel/Star/Star (1)").GetComponent<Text>().text = GameController.instance.Get_Star().ToString();
        }
        if (text != null)
        {
            text.text = GameController.instance.Get_Star().ToString();
        }
      
    }
}
