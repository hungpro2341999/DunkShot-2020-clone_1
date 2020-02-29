using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Score : MonoBehaviour
{
    public Transform score;

    public Transform level;


    BallPlayer player;
    bool getScore = false;
    int count = 0;
     public  int ScoreCurrent =0;

    public float cooltime = 0;

    float currCoolTimeLevel = 0;
    // Start is called before the first frame update

 



    void Start()
    {
        player = GameObject.Find("Ball").GetComponent<BallPlayer>();

        score.GetComponent<Text>().text = "" + ScoreCurrent;

        cooltime = LevelController.instance.getCoolTimeLevel();
    }

    // Update is called once per frame
    void Update()
    {

        if (!GameController.instance.GameOver && !GameController.instance.GamePause)
        {

            currCoolTimeLevel = LevelController.instance.getCoolTimeLevel();

            if (!GameController.instance.GameOver)
            {

                cooltime = cooltime - Time.deltaTime;

                if (LevelController.instance.UpdateTime)
                {
                    cooltime = LevelController.instance.getCoolTimeLevel();
                    LevelController.instance.UpdateTime = false;
                }
                if (cooltime > 0)
                {
                    GetScore();

                }
                else
                {
                    GameObject.Find("ScreenGameplay").GetComponent<ScreenGamePlay>().GameOverWindow();

                    Debug.Log("Game Over Screen");
                }
            }

        }

        level.GetComponent<Text>().text = "LEVEL :" + LevelController.instance.getLevel();

        score.GetComponent<Text>().text = "SCORE : " + ScoreCurrent;
    }

    public void GetScore()
    {
        if (count == 2)
        {
            ScoreCurrent++;
            count = 0;

            GameObject.FindObjectOfType<Board>().SpawnPefect();

            cooltime = currCoolTimeLevel;

            SpawnerCtrl.instante.chanceBoard = true;

         

            if (ScoreCurrent % 4 == 0)
            {


                LevelController.instance.UpdateLevel();


              

               


            }
        }
      


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "First")
        {
            
           
            count = 1;
        }
        if (collision.gameObject.tag == "Second")
        {
            if (count == 1)
            {
              
                count = 2;


            }
            else
            {
                count = 0;
            }
        }
        
    }

    void SaveInfor()
    {
      

     

    }
}
