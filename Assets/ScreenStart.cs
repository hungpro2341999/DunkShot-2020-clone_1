using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public   enum TypeWindown { Shop, Setting, Rate, Start, Chanlegend,Packet,ShopPlay,SettingPlay,Mode};
public class ScreenStart : MonoBehaviour
{
  
    // Start is called before the first frame update
    public static ScreenStart  instance = null;

    public List<Windown> windowns;

    public Windown Shop;

    public Windown Lboard;

    public Windown setting;

    public Windown Rate;

    public Windown Mode_1;
    
    public Windown Mode_2;

    public Windown Mode_3;

    
    

    public Windown Changlegend;
    public Transform bg;
    public int index_mode=1;
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
      foreach(Windown windown in windowns)
        {
            if(windown.type == TypeWindown.Start)
            {
                windown.OpenWindow();
            }
            else
            {
                windown.CloseWindow();
            }
        }
       
    

      

    }


        void Start()
        {
        CloseWIndow();
      
       // CloseAll();
     //   Mode_1.OpenWindow();
       // Mode_2.CloseWindow();
      //  Mode_3.CloseWindow();
       
    }

        // Update is called once per frame
        void Update()
        {
        Time.timeScale = 1;
        bg.GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBackGround();
        }

        public void CloseWindow(Windown windown)
        {
       
            windown.CloseWindow();
        }

        public void OpenWindow(Windown windown)
        {
        
           foreach(Windown w in windowns)
        {
            if(windown.type == w.type)
            {

                w.OpenWindow();
            }
            else
            {
                w.CloseWindow();
            }

        }
          }
     public void CloseWIndow()
    {
        foreach (Windown w in windowns)
        {
            if (w.type == TypeWindown.Start)
            {

                w.OpenWindow();
            }
            else
            {
                w.CloseWindow();
            }

        }
    }

        public void OpenAll()
        {
            Shop.OpenWindow();
            Lboard.OpenWindow();
            setting.OpenWindow();
        Changlegend.OpenWindow();
        Rate.OpenWindow();
        }
        public void CloseAll()
        {
            Shop.CloseWindow();
            Lboard.CloseWindow();
            setting.CloseWindow();
        Changlegend.CloseWindow();
        Rate.CloseWindow();

        }

        public void StartMode_1()
        {



            GameController.instance.GameOver = false;


            GameController.instance.GamePause = false;

        GameController.instance.Set_Mode_Game(GAME_MODE_TYPE.MODE_1);
      //  Debug.Log("gameplay");
            SceneManager.LoadScene("GamePlay");

        }
     public void StartMode_2()
    {
        GameController.instance.GameOver = false;

        Debug.Log("gameplay");
        GameController.instance.GamePause = false;
        GameController.instance.Set_Mode_Game(GAME_MODE_TYPE.MODE_2);

        SceneManager.LoadScene("GamePlay");

     }
    public void StartMode_3()
    {

        Debug.Log("gameplay");

        GameController.instance.GameOver = false;


        GameController.instance.GamePause = false;

        GameController.instance.Set_Mode_Game(GAME_MODE_TYPE.MODE_3);

        SceneManager.LoadScene("GamePlay");

    }
    public void StartMode_4()
    {

        Debug.Log("gameplay");

        GameController.instance.GameOver = false;


        GameController.instance.GamePause = false;

        GameController.instance.Set_Mode_Game(GAME_MODE_TYPE.MODE_4);

        SceneManager.LoadScene("GamePlay");

    }
    public void Next()
    {
        index_mode++;
        if (index_mode > 3)
        {
            index_mode = 1;
        }
      
        SelcetMode(index_mode);

    }
    public void Prev()
    {
        index_mode--;
        if (index_mode <1)
        {
            index_mode = 3;
        }
        SelcetMode(index_mode);
     
    }

    public void SelcetMode(int index)
    {
        AutioControl.instance.GetAudio("change").Play();
        switch (index)
        {
            case 1:
                Mode_1.OpenWindow();
                Mode_2.CloseWindow();
                Mode_3.CloseWindow();

                break;

            case 2:
                Mode_1.CloseWindow();
                Mode_2.OpenWindow();
                Mode_3.CloseWindow();
                break;
            case 3:
                Mode_1.CloseWindow();
                Mode_2.CloseWindow();
                Mode_3.OpenWindow();
                break;
            
        }
    }
    
   public void More_Game()
    {
        ManagerAds.Ins.MoreGame();
    }
    public void Rate_US()
    {
        ManagerAds.Ins.RateApp();

    }
    public void Get_Reward()
    {
        if (ManagerAds.Ins.IsRewardVideoAvailable())
        {
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
    }

}



 