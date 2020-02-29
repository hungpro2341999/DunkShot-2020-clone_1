using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Player_2 : MonoBehaviour
{
    Rigidbody body;
    public static Ball_Player_2 instance;
    public int Score=0;
   public  int Star=0;
    public int countPerfect=1;


    /// <summary>
    /// ///////

    /// </summary>
    // Start is called before the first frame update
    public int CountDoubleKill = 0;
    public int CountTripleKill = 0;
    public bool isWall;
    public int CountPerfect = 1;
    public bool isGround = false;
    public int countGround;
    public bool FullForce = false;
    public int countGolbal = 0;
    public int countFullForce = 0;
    public float timeburn = 0;
    public void Reset()
    {
        CountDoubleKill = 0;
        CountTripleKill = 0;
        isWall = false;
        CountPerfect = 1;
        isGround = false;
        countGround = 0;
        FullForce = false;
        countGolbal = 0;
        countFullForce = 0;
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        MODE_GAME.Reset_Mode_2 += Reset;
        Star = GameController.instance.Get_Star();

        MODE_GAME.Reset_Mode_2 += on_Rest_Mode_2;
        MODE_GAME.Continue_Mode_2 += Continue;
       
       
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
     //   Time.timeScale = 2;
   
       Debug.Log("SCORE_BALL : "+GetScore());
        if (timeburn >=0)
        {
          
            timeburn -= Time.deltaTime;
        }
        else
        {

            Gun.instance.SetParticeBullet(false, false);
        }

    }
    public void Continue()
    {
        LevelController.instance.UpdateTime = true;
        Gun.instance.RestBullet(3);
    }
    
    public void on_Rest_Mode_2()
    {
        Star = GameController.instance.Get_Star();
        Score = 0;
        countPerfect = 1;

    }

   
    public void TakeScore(Paractice_Type type)
    {
        switch (type)
        {
            case Paractice_Type.NORMAL:

                countPerfect = 1;
                AddSCore(1);
                //  TestAction.instance.StartBurnBall_2(10);
                Gun.instance.SetParticeBullet(false, false);
                AutioControl.instance.GetAudio("Perfect_1").Play();
                break;

            case Paractice_Type.PERFECT:
                if (countPerfect > 3)
                {

                    Gun.instance.SetParticeBullet(false, true);
                    timeburn = 10;
                    if (countPerfect > 4)
                    {
                      
                        Gun.instance.SetParticeBullet(true, true);
                    }
                }
                AddSCore(3 * countPerfect);
                countPerfect++;
                if (countPerfect > 2)
                {

                   // TestAction.instance.StartBurnBall_2(10);
                }
                if (CountPerfect > 7)
                {
                    string name = "Perfect_" + 7;
                    AutioControl.instance.GetAudio(name).Play();

                }
                else
                {
                    string name = "Perfect_" + countPerfect + "";
                    AutioControl.instance.GetAudio(name).Play();
                    TestAction.instance.reset = true;
                }
           

                break;
            case Paractice_Type.DOUBLE:
                timeburn = 10;
                Gun.instance.SetParticeBullet(true, true);
                CountDoubleKill++;
                break;
            case Paractice_Type.TRIPLE:
                timeburn = 10;
                Gun.instance.SetParticeBullet(false, true);
                CountTripleKill++;
                break;

        }
        countGolbal++;

    }

   public void AddSCore(int amount)
    {
        Score += amount;
    

    }
     public void addStar(int amout)
    {
        Star += amout;
        GameController.instance.Save_Star(Star);
    }
    public int GetScore()
    {
   
        return Score;
    }

  public int  GetStar()
    {
    
        //    Debug.Log("GET SRAR : " + Star);
        return GameController.instance.Get_Star();

    }


}
