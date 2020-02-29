using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ScreenGamePlay : MonoBehaviour
{
    public static ScreenGamePlay instance = null;
    public Score Score;

    public Windown WindownRate;

    public Windown WindownPause;

    public Windown WindowAdsToContine;

    public Windown WindownGameOVer;

    public Windown WindownShop;

    public  static int select=2;

    public Transform Canvas;
    public Transform trans;
    // Start is called before the first frame update
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

    public void BlackBackGround()
    {
    
        GameController.instance.GameOver = false;
        GameController.instance.GamePause = false;

        StartCoroutine(BackToScreennStart());
    }

   IEnumerator BackToScreennStart()
    {
        yield return new WaitForSeconds(7.5f);
        trans.gameObject.SetActive(true);
        SceneManager.LoadScene("Start Screen");

    }
        public void UpdateShop()
    {
        GameController.instance.Update_To_Infor();
    }
    void Start()
    {

        trans.gameObject.SetActive(false);
        CloseAll();
        GameController.instance.GamePause = false;
        GameController.instance.GameOver = false;

    

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {


            GameOverWindow();
           
        }
       
        ManagerAds.Ins.ShowBanner();
    }

    public void OpenWindow(Windown window)
    {
        window.OpenWindow();
    }

    public void CloseAll()
    {
        WindownRate.CloseWindow();

        WindownPause.CloseWindow();

         WindownGameOVer.CloseWindow();
        WindowAdsToContine.CloseWindow();
        WindownShop.CloseWindow();
    }
    public void ContinueGame()
    {
        VisibleCanvas(true);

        Time.timeScale = 2;
        GameController.instance.GameOver = false;
        GameController.instance.GamePause = false;
        switch (GameController.instance.Game_Type)
        {
            case GAME_MODE_TYPE.MODE_1:

                Time.timeScale = 2;
                ModeCtrl.instance.MODE_GAME[0].ContineMode();
                break;

            case GAME_MODE_TYPE.MODE_2:
                Time.timeScale = 2;
                ModeCtrl.instance.MODE_GAME[1].ContineMode();
                break;
            case GAME_MODE_TYPE.MODE_3:
                Time.timeScale = 2;
                ModeCtrl.instance.MODE_GAME[2].ContineMode();
                break;
        }

      
        CloseAll();

    }
    public void ContinueGameHaveAds()
    {



     
        if (ManagerAds.Ins.IsRewardVideoAvailable())
        {
            ManagerAds.Ins.ShowRewardedVideo(success =>
            {
                if (success)
                {
                    VisibleCanvas(true);
                    Time.timeScale = 2;
                    GameController.instance.GameOver = false;
                    GameController.instance.GamePause = false;
                    switch (GameController.instance.Game_Type)
                    {
                        case GAME_MODE_TYPE.MODE_1:

                            Time.timeScale = 2;
                            ModeCtrl.instance.MODE_GAME[0].ContineMode();
                            break;

                        case GAME_MODE_TYPE.MODE_2:
                            Time.timeScale = 2;
                            ModeCtrl.instance.MODE_GAME[1].ContineMode();
                            break;
                        case GAME_MODE_TYPE.MODE_3:
                            Time.timeScale = 2;
                            ModeCtrl.instance.MODE_GAME[2].ContineMode();
                            break;
                    }


                    CloseAll();
                }
            });

        }




     
     
    }
    public void RestGame()
    {
        VisibleCanvas(true);
        Time.timeScale = 2;
       
        switch (GameController.instance.Game_Type)
        {
            case GAME_MODE_TYPE.MODE_1:
                
                Time.timeScale = 2;
                ModeCtrl.instance.MODE_GAME[0].RestMode();
                break;

            case GAME_MODE_TYPE.MODE_2:
                Time.timeScale = 2;
                ModeCtrl.instance.MODE_GAME[1].RestMode();
                break;
            case GAME_MODE_TYPE.MODE_3:
                Time.timeScale = 2;
                SceneManager.LoadScene("GamePlay");
                break;
        }
        Save_Game();
        GameController.instance.GameOver = false;
        GameController.instance.GamePause = false;
        WindownGameOVer.CloseWindow();

    }

    public void ContineGameWithPauseWindown()
    {
        ClickPause.Click = false;
        VisibleCanvas(true);
        Time.timeScale = 2;

        Invoke("ResetStatus", 0.1f);
        WindownPause.CloseWindow();

    }

    public void ResetStatus()
    {
        if (GameController.instance.Game_Type != GAME_MODE_TYPE.MODE_3)
        {
            GameController.instance.GamePause = false;
        }
    }


    public void PauseWindown()
    {
        if (GameController.instance.Game_Type != GAME_MODE_TYPE.MODE_3)
        {
            GameController.instance.GamePause = true;
            AutioControl.instance.Stop();
            Time.timeScale = 0;
       

            WindownPause.OpenWindow();
        }
        else
        {
           
            AutioControl.instance.Stop();
            Time.timeScale = 0;
            WindownPause.OpenWindow();

        }
        
       
    }


    public void House()
    {
        ClickPause.Click = false;
        Save_Game();
        GameController.instance.GameOver = true;
        GameController.instance.GamePause = true;
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Screen");
      
     
    }

    public void GameOverWindow()
    {
     
        VisibleCanvas(false);
        if (select%2== 0)
        {
          
            CloseAll();
            if (ManagerAds.Ins.IsRewardVideoAvailable())
            {
                OpenAdsWindown();
            }
            else
            {
                WindownGameOVer.OpenWindow();
            }






        }
        else
        {
            Save_Game();
            ManagerAds.Ins.ShowInterstitial();
            GameController.instance.GamePause = true;
            GameController.instance.GameOver = true;
            WindowAdsToContine.CloseWindow();
            WindownGameOVer.OpenWindow();
            
        }
        AutioControl.instance.GetAudio("Lose").Play();


        select++;
    }

    public void OpenGameOverNoAds()
    {
        Save_Game();
        GameController.instance.GamePause = true;
        GameController.instance.GameOver = true;
        WindowAdsToContine.CloseWindow();
        WindownGameOVer.OpenWindow();
    }
    public void OpenAdsWindown()
    {
        Save_Game();
        VisibleCanvas(false);
        AutioControl.instance.GetAudio("Lose").Play();
        WindowAdsToContine.OpenWindow();
        GameController.instance.GamePause = true;
        GameController.instance.GamePause = true;
    }

    public void Save_Game()
    {
        
        switch (GameController.instance.Game_Type)
        {
            //Tap Tap
            case GAME_MODE_TYPE.MODE_1:
                
                GameController.instance.Save_Star(BallPlayer.instance.getStar());
                GameController.instance.SaveScoreTapTap(BallPlayer.instance.getScore());
                Debug.Log("SAVE :" + GameController.instance.Get_Star());
                break;
                // Shoot
            case GAME_MODE_TYPE.MODE_2:
                GameController.instance.Save_Star(Ball_Player_2.instance.GetStar());
                GameController.instance.SaveScoreShoot(Ball_Player_2.instance.GetScore());
                Debug.Log("SAVE :" + GameController.instance.Get_Star());

                break;
            case GAME_MODE_TYPE.MODE_3:
                GameController.instance.Save_Star((int)BoardControl.instance.GetStar());
                GameController.instance.SaveSCoreModeDropDrap((int)BoardControl.instance.GetScore());
                Debug.Log("SAVE :" + GameController.instance.Get_Star());

                break;
            case GAME_MODE_TYPE.MODE_4:

                GameController.instance.Save_Star((int)Reward_Game.instance.GetStar());


                break;
        }

       

        
    }
    public void Get_Reward_Ads()
    {
        if (ManagerAds.Ins.IsRewardVideoAvailable()){
            ManagerAds.Ins.ShowRewardedVideo(success =>
            {
                if (success)
                {
                    int star = GameController.instance.Get_Star();
                    star += 25;
                    GameController.instance.Save_Star(star);
                }
            });
        }
         
       

        //////////ADS////////
    }
    
    public void Rate_Us()
    {
        ManagerAds.Ins.RateApp();
    }

    public void adsToContinue()
    {
        VisibleCanvas(true);
        if (ManagerAds.Ins.IsRewardVideoAvailable())
        {
            ManagerAds.Ins.ShowRewardedVideo(success =>
            {
                if (success)
                {
                    WindowAdsToContine.CloseWindow();
                    GameController.instance.GameOver = false;
                    GameController.instance.GamePause = false;
                    LevelController.instance.UpdateTime = true;
                }
            });

        }
      
    }
    public void ReviceByEarnStar()
    {
        if(GameController.instance.Get_Star() >=25){
            GameController.instance.earnMoney(25);
            WindowAdsToContine.CloseWindow();
            VisibleCanvas(true);
            GameController.instance.GameOver = false;
            GameController.instance.GamePause = false;
            LevelController.instance.UpdateTime = true;
            switch (GameController.instance.Game_Type)
            {
                case GAME_MODE_TYPE.MODE_1:
                    BallPlayer.instance.Star = GameController.instance.Get_Star();
                    Time.timeScale = 2;
                    ModeCtrl.instance.MODE_GAME[0].ContineMode();
                    break;

                case GAME_MODE_TYPE.MODE_2:
                   Ball_Player_2.instance.Star= GameController.instance.Get_Star();
                    Time.timeScale = 2;
                    ModeCtrl.instance.MODE_GAME[1].ContineMode();
                    break;
                case GAME_MODE_TYPE.MODE_3:
                    BoardControl.instance.Star = GameController.instance.Get_Star();
                    Time.timeScale = 2;
                    ModeCtrl.instance.MODE_GAME[2].ContineMode();
                    break;
            }


            CloseAll();

        }


    }


    public void VisibleCanvas(bool visible)
    {
        Canvas.gameObject.SetActive(visible);

    }
}
