using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisibleBar : MonoBehaviour
{
    public Transform bar;
    public Text Score;

    public Text Star_text;

    public Transform Level;

    public static VisibleBar instance;

    public Transform barVisible;

    RectTransform rect;
    public Transform image;
   

    float timeCurr;

    float timeLevel;

    float width;
    public float time = 0;

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
        MODE_GAME.Reset_Mode_1 += onRest_Time;
        MODE_GAME.Reset_Mode_2 += onRest_Time;
    }


    // Start is called before the first frame update
    void Start()
    {

        image.GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBackGround();
        if (GameController.instance.Game_Type != GAME_MODE_TYPE.MODE_4 && GameController.instance.Game_Type != GAME_MODE_TYPE.MODE_2)
        {
            rect = barVisible.GetComponent<RectTransform>();

            width = GetComponent<RectTransform>().rect.width;

            time = LevelController.instance.getCoolTimeLevel();

            timeLevel = LevelController.instance.getCoolTimeLevel();
        }
        else
        {
            bar.GetChild(0).GetComponent<Image>().enabled = false;
            bar.GetChild(1).GetComponent<Image>().enabled = false;
        }
            
        if(GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_4)
        {
            Score.text = "";
        }
        //Debug.Log(time + " " + timeLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (GameController.instance.Game_Type)
        {
            case GAME_MODE_TYPE.MODE_1:
                int Star = BallPlayer.instance.getStar();

                int SCore = BallPlayer.instance.GetScore();
                Score.text = SCore.ToString();
                Star_text.text = Star.ToString();



                break;
            case GAME_MODE_TYPE.MODE_2:
                Score.text = Ball_Player_2.instance.GetScore().ToString();
                Star_text.text = Ball_Player_2.instance.GetStar().ToString();

                break;
            case GAME_MODE_TYPE.MODE_3:

                Score.text = BoardControl.instance.GetScore().ToString();
                Star_text.text = BoardControl.instance.GetStar().ToString();
                break;
            case GAME_MODE_TYPE.MODE_4:

                Score.text = "";
                Star_text.text = Reward_Game.instance.GetStar().ToString();
                GameController.instance.Save_Star((int)Reward_Game.instance.GetStar());
                break;
        }

        if (! GameController.instance.GameOver && ! GameController.instance.GamePause )
        {
            if( GameController.instance.Game_Type != GAME_MODE_TYPE.MODE_4 && GameController.instance.Game_Type != GAME_MODE_TYPE.MODE_2)
            {

                time -= Time.deltaTime;
                if (LevelController.instance.UpdateTime)
                {
                    time = LevelController.instance.getCoolTimeLevel();
                    timeLevel = LevelController.instance.Timelevelcurr;
                    LevelController.instance.UpdateTime = false;

                }
                if (time > 0)
                {

                    float ratio = time / timeLevel;
                    if (ratio > 0)
                    {

                        setBar(ratio);
                    }
                    else
                    {
                        setBar(1);
                    }

                }
                else
                {
                    GameObject.Find("ScreenGameplay").GetComponent<ScreenGamePlay>().GameOverWindow();
                }

            }
            else if(GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_4)
            {

                Score.text = "";
                Debug.Log(Reward_Game.instance.GetStar().ToString());
                Star_text.text = Reward_Game.instance.GetStar().ToString();
                GameController.instance.Save_Star((int)Reward_Game.instance.GetStar());
            }
           









        }
        else
        {
        }
    }
    public void onRest_Time()
    {
        time = LevelController.instance.getCoolTimeLevel();
    }

    void setBar(float ratio)
    {
        

          float x =  width* ratio;

        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);

       
    }
}
