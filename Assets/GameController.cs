using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum GAME_MODE_TYPE { MODE_1, MODE_2, MODE_3,MODE_4,Dèault};

public enum BOARD_TYPE   { NORMAL, ROTAION, TOP_DOWN,LEFT_RIGHT,JERK_TOP_DOWN,JERK_LEFT_RIGHT,JERK_RIGHT_LEFT_CROSS,JERK_TOP_DOWN_CROSS};

public class GameController : MonoBehaviour
{
    public delegate void OnSound(bool open);

    public event OnSound On_Off_Sound;

    public GAME_MODE_TYPE Game_Type = GAME_MODE_TYPE.MODE_2;

    public List<PlayerInfor> listplayer;

    public List<BallDetail> ListBallDetail;

    public float time;

    public Transform WorldSpace;
    public Card[] card;

    public Card[] backGroundCard;

    public Challenges[] ChallengesCard;
    public bool GameStart = false;
    public bool GamePause = false;
    public bool GameOver = false;
    public Windown windownPause;
    public Windown WindownGameOVer;
    public static GameController instance = null;


    public float width;

    public float height;

    public Vector2 leftbottom;

    public Vector2 rightbottom;

    public Vector2 leftup;

    public Vector2 rightup;

    public float sizeCamera;

    public Vector2 leftBottomReal;
    public Vector2 rightBottomReal;
    public Vector2 leftUpRead;
    public Vector2 rightUpReal;
    public float widthReal;
    public float heightReal;

    public const string Key_Score_Mode_1 = "KEY_SCORE_GAME_1";
    public const string Key_Score_Mode_2 = "KEY_SCORE_GAME_2";
    public const string Key_Score_Mode_3 = "KEY_SCORE_GAME_3";

    public const string key_List_Ball = "KEY_LIST_BALL";

    public const string key_List_Achie = "KEY_LIST_ACHIEVEMENT";

    public const string key_Shop = "KEY_SHOP";

    public const string key_Sound = "KEY_SOUND";

    public const string key_vibration = "KEY_VIBRATION";

    public const string firstTime = "KEY_FIRST_TIME";

    public const string Key_Money = "KEY_MONEY";

    public const string Key_Image = "KEY_IMAGE";

    public const string Key_BackGround = "KEY_BACKGROUND";

    public const string Key_Star = "KEY_STAR_GAME_1";

    public const string Key_Star_Golbal = "KEY_STAR_GOBAL";

    public const string key_Curr_Time = "KEY_CURR_TIME";
    public const string key_Last_Time = "KEY_LAST_TIME";
    public const string key_Direct_Time = "KEY_DIRECT_TIME";
    public bool firstPlay = true;

    // Start is called before the first frame update
    public Sprite sprite_ball;
    //
    float day;
    float month;
    public bool getReward = false;
   public float hour;
   public float mininue;
   public float second;
    public bool chaceSound = false;
    public void Open_Off_Sound(bool open)
    {
        On_Off_Sound(open);
    }

    

    public int GetStarGolbal()
    {
        if (!PlayerPrefs.HasKey(Key_Star_Golbal))
        {
            PlayerPrefs.SetInt(Key_Star_Golbal, GameController.instance.Get_Star());

            return PlayerPrefs.GetInt(Key_Star_Golbal);
        }
        else
        {
            return PlayerPrefs.GetInt(Key_Star_Golbal);

        }
    }
    public void AddStarGolabal(int amount)
    {
        if (!PlayerPrefs.HasKey(Key_Star_Golbal))
        {
            PlayerPrefs.SetInt(Key_Star_Golbal, GameController.instance.Get_Star());
            PlayerPrefs.SetInt(Key_Star_Golbal, GameController.instance.Get_Star());
        }
        else
        {
            int Star = GetStarGolbal() + amount;
            PlayerPrefs.SetInt(Key_Star_Golbal, Star);

        }

    }

    public void Save_Star(int count)
    {




        PlayerPrefs.SetInt(Key_Star, count);
        PlayerPrefs.Save();

    }



    public int Get_Star()
    {
        if (!PlayerPrefs.HasKey(Key_Star))
        {
            PlayerPrefs.SetInt(Key_Star, 0);
            PlayerPrefs.Save();
            return 0;

        }
        int m = PlayerPrefs.GetInt(Key_Star);
        return m;

    }



    public void setBackGround(Card card)
    {
        if (!PlayerPrefs.HasKey(Key_BackGround))
        {
            PlayerPrefs.SetInt(Key_BackGround, 1);
            PlayerPrefs.Save();
        }
        PlayerPrefs.SetInt(Key_BackGround, card.id);
        Debug.Log("DA THAU DOI BG : " + PlayerPrefs.GetInt(Key_BackGround));
        PlayerPrefs.Save();
    }

    public Sprite SpriteBackGround()
    {
        if (!PlayerPrefs.HasKey(Key_BackGround))
        {
            return backGroundCard[0].ballImage;
        }
        else
        {
            //  Debug.Log("DA THAU DOI BG");
            int i = PlayerPrefs.GetInt(Key_BackGround);


            return backGroundCard[i - 1].ballImage;
        }
    }
    public Sprite getBG_0()
    {
        if (!PlayerPrefs.HasKey(Key_BackGround))
        {
            return backGroundCard[0].BgMode3_0;
        }
        else
        {
            //  Debug.Log("DA THAU DOI BG");
            int i = PlayerPrefs.GetInt(Key_BackGround);


            return backGroundCard[i - 1].BgMode3_0;
        }
    }
    public Sprite getBG_1()
    {
        if (!PlayerPrefs.HasKey(Key_BackGround))
        {
            return backGroundCard[0].BgMode3_1;
        }
        else
        {
            //  Debug.Log("DA THAU DOI BG");
            int i = PlayerPrefs.GetInt(Key_BackGround);


            return backGroundCard[i - 1].BgMode3_1;
        }
    }
    public Sprite SpriteGun()
    {
        if (!PlayerPrefs.HasKey(Key_BackGround))
        {
            return backGroundCard[0].Gun;
        }
        else
        {
            //  Debug.Log("DA THAU DOI BG");
            int i = PlayerPrefs.GetInt(Key_BackGround);


            return backGroundCard[i - 1].Gun;
        }
    }
    public Sprite[] SpriteBas()
    {
        if (!PlayerPrefs.HasKey(Key_BackGround))
        {
            return backGroundCard[0].bas;
        }
        else
        {
            //  Debug.Log("DA THAU DOI BG");
            int i = PlayerPrefs.GetInt(Key_BackGround);


            return backGroundCard[i - 1].bas;
        }
    }
    public Sprite[] SpriteBas_Mode1()
    {
        if (!PlayerPrefs.HasKey(Key_BackGround))
        {
            return backGroundCard[0].bas_mode_1;
        }
        else
        {
            //  Debug.Log("DA THAU DOI BG");
            int i = PlayerPrefs.GetInt(Key_BackGround);


            return backGroundCard[i - 1].bas_mode_1;
        }
    }

    public Sprite getSprite(Card card)
    {
        if (!PlayerPrefs.HasKey(Key_Image))
        {
            PlayerPrefs.SetInt(Key_Image, 1);
        }
        PlayerPrefs.SetInt(Key_Image, card.id);
        PlayerPrefs.Save();



        return card.ballImage;




    }

    public Sprite SpriteBall()
    {
        int i = PlayerPrefs.GetInt(Key_Image);
        if (i == 0)
        {
            return card[0].ballImage;
        }
        else
        {
            return card[i - 1].ballImage;
        }
    }
    public int GetSortEff()
    {
        int i = PlayerPrefs.GetInt(Key_Image);
        if (i == 0)
        {
            return card[0].Sort_Effect;
        }
        else
        {
            return card[i - 1].Sort_Effect;
        }
    }




    public void SaveScore(int Score)
    {

        if (!PlayerPrefs.HasKey(Key_Money))
        {
            PlayerPrefs.SetInt(Key_Money, 0);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey(Key_Money))
        {
            if (PlayerPrefs.GetInt(Key_Money) < Score)
            {
                PlayerPrefs.SetInt(Key_Money, Score);
                PlayerPrefs.Save();

            }





        }


    }
    public void SaveSCoreModeDropDrap(int Score)
    {
        if (!PlayerPrefs.HasKey(Key_Score_Mode_1))
        {
            PlayerPrefs.SetInt(Key_Score_Mode_1, 0);
            PlayerPrefs.Save();

        }
        else
        {
            if (PlayerPrefs.GetInt(Key_Score_Mode_1) < Score)
            {
                PlayerPrefs.SetInt(Key_Score_Mode_1,Score);
                PlayerPrefs.Save();
            }
        }
       

    }
    public int GetHighScoreModeDropDrap()
    {
        if (!PlayerPrefs.HasKey(Key_Score_Mode_1))
        {
            return 0;
        }
        else
        {

            return PlayerPrefs.GetInt(Key_Score_Mode_1);
        }
    }

    public void SaveScoreTapTap(int Score)
    {
        if (!PlayerPrefs.HasKey(Key_Score_Mode_2))
        {
            PlayerPrefs.SetInt(Key_Score_Mode_2, 0);
            PlayerPrefs.Save();

        }
        else
        {
            if (PlayerPrefs.GetInt(Key_Score_Mode_2) < Score)
            {
                PlayerPrefs.SetInt(Key_Score_Mode_2, Score);
                PlayerPrefs.Save();
            }
        }
    }

    public int GetHighScoreModeTapTAp()
    {
        if (!PlayerPrefs.HasKey(Key_Score_Mode_2))
        {
            return 0;
        }
        else
        {

            return PlayerPrefs.GetInt(Key_Score_Mode_2);
        }
    }
    public void SaveScoreShoot(int Score)
    {
        if (!PlayerPrefs.HasKey(Key_Score_Mode_3))
        {
            PlayerPrefs.SetInt(Key_Score_Mode_3, 0);
            PlayerPrefs.Save();

        }
        else
        {
            if (PlayerPrefs.GetInt(Key_Score_Mode_3) < Score)
            {
                PlayerPrefs.SetInt(Key_Score_Mode_3, Score);
                PlayerPrefs.Save();
            }
        }
    }

    public int GetHighScoreModeSHoot()
    {
        if (!PlayerPrefs.HasKey(Key_Score_Mode_3))
        {
            return 0;
        }
        else
        {

            return PlayerPrefs.GetInt(Key_Score_Mode_3);
        }
    }

    public int getHighScore()
    {
        if (!PlayerPrefs.HasKey(Key_Money))
        {
            return 0;
        }
        else
        {
            int score = PlayerPrefs.GetInt(Key_Money);

            return score;
        }

    }

    public void earnMoney(int cost)
    {
        if (!PlayerPrefs.HasKey(Key_Star))
        {
            return;
        }
        else
        {
            int score = PlayerPrefs.GetInt(Key_Star);
            Debug.Log(PlayerPrefs.GetInt(Key_Star));
            score = score - cost;


            PlayerPrefs.SetInt(Key_Star, score);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetInt(Key_Star));
        }

    }


    private void Awake()


  

    {
        Application.targetFrameRate = 60;
        if (instance != null)
        {

            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }


        listplayer = Load();

        Debug.Log("SO LUONG PLAYER :  " + Load().Count);
        DontDestroyOnLoad(gameObject);

        GameStart = false;
        GamePause = false;
       // PlayerPrefs.DeleteKey(firstTime);
        if (PlayerPrefs.HasKey(firstTime))
        {

        


            firstPlay = false;
        }
        else
        {
            PlayerPrefs.SetInt(firstTime, 0);
            firstPlay = true;
            Debug.Log("Ko co Key");
        }

        
        if (firstPlay)
        {
            if (PlayerPrefs.HasKey(Key_Star_Golbal))
            {
                PlayerPrefs.DeleteKey(Key_Star_Golbal);
            }
            else
            {
                PlayerPrefs.SetInt(Key_Star_Golbal, 0);
            }
            Set_Time_Last();
            Save_Star(50);
            SaveScore(0);
            for (int i = 0; i < card.Length; i++)
            {
                card[i].isBuying = true;
                card[i].isUsing = false;
                card[i].isUse = false;
                card[i].TimeAdsToGetBall = 0;
            }
            card[0].isBuying = false;
            card[0].isUsing = true;
            card[0].isUse = true;
            for (int i = 0; i < ChallengesCard.Length; i++)
            {
                ChallengesCard[i].isCompleQuest = false;
                ChallengesCard[i].levelCurr = 0;
                ChallengesCard[i].isTake = 0;
                for (int j = 0; j < 5; j++)
                {
                    //   if (j == 0)
                    //    {
                    //       ChallengesCard[j].TakeRewardGame[j] = true;
                    //   }
                    //  else
                    {

                        ChallengesCard[i].TakeRewardGame[j] = false;
                    }

                }
            }
            List<BallInfor> List = new List<BallInfor>();
            for (int i = 0; i < card.Length; i++)
            {
                BallInfor infor = new BallInfor();
                if (i == 0)
                {

                    infor.isBuing = false;
                    infor.isUse = true;
                    infor.isUsing = true;

                }
                else
                {
                    infor.isBuing = true;
                    infor.isUse = false;
                    infor.isUsing = false;


                }
                List.Add(infor);
            }
            ListInfoBall listInfo = new ListInfoBall(List);
            string key = JsonUtility.ToJson(listInfo);
            PlayerPrefs.SetString(key_List_Ball, key);
            PlayerPrefs.Save();


            List<AchievementInfor> list = new List<AchievementInfor>();
            for (int i = 0; i < ChallengesCard.Length; i++)
            {
                AchievementInfor infor = new AchievementInfor();
                infor.level = ChallengesCard[i].levelCurr;
                infor.isTake = ChallengesCard[i].isTake;
                infor.isComplete = ChallengesCard[i].isComplete;
                for (int j = 0; j < 5; j++)
                {

                    infor.reward[j] = ChallengesCard[i].TakeRewardGame[j];

                }
                list.Add(infor);
                ListAchievement achievement = new ListAchievement(list);
                string key_1 = JsonUtility.ToJson(achievement);
                PlayerPrefs.SetString(key_List_Achie, key_1);
                PlayerPrefs.Save();

            }



        }
        else
        {

            UpdateInforBall_To_Game();
            Update_InforAchieve_ToGame();


        }


        width = Camera.main.pixelWidth;

        height = Camera.main.pixelHeight;

        Debug.Log(width + "  " + height);
        leftbottom = Vector2.zero;

        rightbottom = new Vector2(width, 0);

        rightup = new Vector2(width, height);

        leftup = new Vector2(0, height);

        leftBottomReal = Camera.main.ScreenToWorldPoint(leftbottom);
        //    Debug.Log(leftBottomReal);
        rightBottomReal = Camera.main.ScreenToWorldPoint(rightbottom);
        leftUpRead = Camera.main.ScreenToWorldPoint(leftup);
        rightUpReal = Camera.main.ScreenToWorldPoint(rightup);

        widthReal = Vector2.Distance(leftBottomReal, rightBottomReal);

        heightReal = Vector2.Distance(leftBottomReal, leftUpRead);

    }

    void Start()
    {

        if (!PlayerPrefs.HasKey(Key_Score_Mode_1))
        {
            PlayerPrefs.SetInt(Key_Score_Mode_1, 0);
        }
        if (!PlayerPrefs.HasKey(Key_Score_Mode_2))
        {
            PlayerPrefs.SetInt(Key_Score_Mode_2, 0);
        }
    
        if (!PlayerPrefs.HasKey(Key_Score_Mode_3))
        {
         PlayerPrefs.SetInt(Key_Score_Mode_3, 0);
        }
        SetLastTime();


        if (GameController.instance.get_On_Off_Sound() == 1)
        {

            GameController.instance.On_Off_Sound_Game(false);
        }
        else
        {



            GameController.instance.On_Off_Sound_Game(true);
        }

        //  Debug.Log(" Num Of Player : "+player.Count);


        Shop();
        if (!PlayerPrefs.HasKey(key_Sound))
        {
            PlayerPrefs.SetInt(key_Sound, 1);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey(key_vibration))
        {
            PlayerPrefs.SetInt(key_vibration, 1);
            PlayerPrefs.Save();
        }

        if (!PlayerPrefs.HasKey(Key_Money))
        {
            PlayerPrefs.SetInt(Key_Money, 0);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey(Key_Star))
        {
            PlayerPrefs.SetInt(Key_Star, 0);
            PlayerPrefs.Save();
            Debug.Log("CO KEY ROI");

        }



        //   Debug.Log(Get_Star());
        ///Init Point Width Height
        ///
      
        InitTime();

    }

    public int get_On_Off_Sound()
    {

        return PlayerPrefs.GetInt(key_Sound);
    }
    public void set_On_Off_Sound(int on)
    {
        if (on == 0)
        {
            GameController.instance.On_Off_Sound(true);
        }
        else
        {

            GameController.instance.On_Off_Sound(false);

        }
        PlayerPrefs.SetInt(key_Sound, on);
        PlayerPrefs.Save();
    }

    public int get_On_Off_Vibration()
    {
        return PlayerPrefs.GetInt(key_vibration);
    }
    public void set_On_Off_vibration(int on)
    {
        PlayerPrefs.SetInt(key_vibration, on);
    }
    public int star;
    // Update is called once per frame
    void Update()
    {
        width = Screen.width;

        height = Screen.height;

        leftbottom = Vector2.zero;

        rightbottom = new Vector2(width, 0);

        rightup = new Vector2(width, height);

        leftup = new Vector2(0, height);

        leftBottomReal = Camera.main.ScreenToWorldPoint(leftbottom);
        //    Debug.Log(leftBottomReal);
        rightBottomReal = Camera.main.ScreenToWorldPoint(rightbottom);
        leftUpRead = Camera.main.ScreenToWorldPoint(leftup);
        rightUpReal = Camera.main.ScreenToWorldPoint(rightup);

        widthReal = Vector2.Distance(leftBottomReal, rightBottomReal);

        heightReal = Vector2.Distance(leftUpRead, leftBottomReal);


        Debug.Log("Get Star : "+Get_Star());
        int Star = GameController.instance.GetStarGolbal();
        if (Star - ChallengesCard[22].isTake > 0)
        {
            ChallengesCard[22].AddAmount(Star - (int)ChallengesCard[22].isTake);
        }
        RunningTime();
        // Debug.Log(GameController.instance.get_On_Off_Sound());
    }

    public void Save()
    {
        /*
        List list = new List(listplayer);

        string json = JsonUtility.ToJson(list);

        Debug.Log("Key : " + json);

        PlayerPrefs.SetString("Load_And_Save", json);


        PlayerPrefs.Save();
        */
    }

    public void AddPlayerAndSave(int score, string name)
    {

        /*
        PlayerInfor player = new PlayerInfor(name, score);

        this.listplayer.Add(player);



            Save();
            */


    }

    public List<PlayerInfor> Load()
    {
        /*
        string json = PlayerPrefs.GetString("Load_And_Save");

         listplayer = JsonUtility.FromJson<List>(json).ListPlayer;
      
    */

        return listplayer;
    }



    public void Shop()
    {
        if (!firstPlay)
        {

            ListBallDetail = LoadShop();



        }
        else
        {
            ListBallDetail = new List<BallDetail>();
            for (int i = 0; i < 20; i++)
            {
                BallDetail ball = new BallDetail();
                ball.id = i;
                ball.isLock = true;
                ListBallDetail.Add(ball);



            }
            List list = new List(ListBallDetail);
            string json = JsonUtility.ToJson(list);
            PlayerPrefs.SetString(key_Shop, json);
            PlayerPrefs.Save();
            string key_json = PlayerPrefs.GetString(key_Shop);
            Debug.Log("BAN CHOI K PHAI LAN DAU");
            Debug.Log("SO LUONG VAT PHAM TRONG CUA HANG LA : " + JsonUtility.FromJson<List>(key_json).listShop.Count);

            if (PlayerPrefs.HasKey(key_Shop))
            {
                Debug.Log("Da co key");
            }

        }

    }

    public void UpdateShop(int id)
    {

    }

    public void SaveShop()
    {
        List list = new List(ListBallDetail);
        string json = JsonUtility.ToJson(list);
        PlayerPrefs.SetString(key_Shop, json);
        PlayerPrefs.Save();
    }

    public List<BallDetail> LoadShop()
    {
        string key_json = PlayerPrefs.GetString(key_List_Ball);
        Debug.Log("SO LUONG VAT PHAM TRONG CUA HANG LA : " + JsonUtility.FromJson<List>(key_json).listShop.Count);
        return JsonUtility.FromJson<List>(key_json).listShop;
    }


    // Buy Stuff
    public void Buy(int id)
    {
        for (int i = 0; i < 20; i++)
        {
            if (i == id)
            {

                ListBallDetail[i].isLock = false;

                SaveShop();
                return;

            }
        }
    }

    public void setFirst()
    {
        PlayerPrefs.SetString(firstTime, "1");

        PlayerPrefs.Save();

    }

    public void setSprite(Sprite sprite)
    {
        sprite_ball = sprite;
    }

    public Sprite getSprite()
    {
        return sprite_ball;
    }
    public void Set_Mode_Game(GAME_MODE_TYPE Game)
    {
        switch (Game)
        {
            case GAME_MODE_TYPE.MODE_1:
                Game_Type = GAME_MODE_TYPE.MODE_1;
                break;
            case GAME_MODE_TYPE.MODE_2:
                Game_Type = GAME_MODE_TYPE.MODE_2;
                break;
            case GAME_MODE_TYPE.MODE_3:
                Game_Type = GAME_MODE_TYPE.MODE_3;
                break;
            case GAME_MODE_TYPE.MODE_4:
                Game_Type = GAME_MODE_TYPE.MODE_4;
                break;


        }

    }
    public void UpdateInforBall_To_Game()
    {
        List<BallInfor> list = JsonUtility.FromJson<ListInfoBall>(PlayerPrefs.GetString(key_List_Ball)).list;
        for (int i = 0; i < card.Length; i++)
        {
            card[i].isBuying = list[i].isBuing;
            card[i].isUse = list[i].isUse;
            card[i].isUsing = list[i].isUsing;
            card[i].TimeAdsToGetBall = list[i].TimeGetRewardAds;
        }
    }
    public void Update_To_Infor()
    {
        List<BallInfor> list = JsonUtility.FromJson<ListInfoBall>(PlayerPrefs.GetString(key_List_Ball)).list;
        for (int i = 0; i < list.Count; i++)
        {
            list[i].isBuing = card[i].isBuying;
            list[i].isUse = card[i].isUse;
            list[i].isUsing = card[i].isUsing;
            list[i].TimeGetRewardAds = card[i].TimeAdsToGetBall;
        }
        ListInfoBall listInfo = new ListInfoBall(list);
        string key = JsonUtility.ToJson(listInfo);
        PlayerPrefs.SetString(key_List_Ball, key);
        PlayerPrefs.Save();
    }

    public void Update_InforAchieve_ToGame()
    {
        Debug.Log("UPDATE :");
        List<AchievementInfor> list = JsonUtility.FromJson<ListAchievement>(PlayerPrefs.GetString(key_List_Achie)).list;
        for (int i = 0; i < ChallengesCard.Length; i++)
        {

            ChallengesCard[i].levelCurr = list[i].level;
            ChallengesCard[i].isTake = list[i].isTake;
            for (int j = 0; j < 5; j++)
            {
                // Debug.Log(ChallengesCard[i].id +" "+j);
                ChallengesCard[i].TakeRewardGame[j] = list[i].reward[j];
                ChallengesCard[i].isCompleQuest = list[i].isComplete;

            }


        }


    }

    public void Update_To_Infor_Achieve()
    {
        Debug.Log("UPDATE :");
        List<AchievementInfor> list = JsonUtility.FromJson<ListAchievement>(PlayerPrefs.GetString(key_List_Achie)).list;
        for (int i = 0; i < list.Count; i++)
        {
            list[i].level = ChallengesCard[i].levelCurr;
            list[i].isTake = ChallengesCard[i].isTake;
         //   Debug.Log("ACHIEVEMENT :"+ChallengesCard[i] +"  "+ list[i].isComplete);
            list[i].isComplete = ChallengesCard[i].isCompleQuest;
            
            for (int j = 0; j < 5; j++)
            {

                list[i].reward[j] = ChallengesCard[i].TakeRewardGame[j];

            }
        }
        ListAchievement listInfo = new ListAchievement(list);
        string key = JsonUtility.ToJson(listInfo);
        PlayerPrefs.SetString(key_List_Achie, key);
        PlayerPrefs.Save();
    }



    public float Deltatime(Time_Game timelast, Time_Game timeCurr)
    {

        return timeCurr.TotalTime() - timelast.TotalTime();
    }
    float Delta = 0;
    float time_to_take_reward = 7200;
    public Time_Game time_reward;
    Time_Game time_curr;
    Time_Game time_last;
    Time_Game time_direct;
    bool UpdateTime = false;
    public void Set_Time_Last()
    {
        //time_last = new Time_Game(System.DateTime.Now.Month, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);


        Time_Game time = new Time_Game(0, 0, System.DateTime.Now.Hour , System.DateTime.Now.Minute, System.DateTime.Now.Second);
        Debug.Log(time.TotalTime());
        if (!PlayerPrefs.HasKey(key_Direct_Time))
        {

            time = new Time_Game(0,0,0, 0,0);
          
            string json = JsonUtility.ToJson(time);

            PlayerPrefs.SetString(key_Direct_Time, json);
            PlayerPrefs.Save();
            Debug.Log(JsonUtility.FromJson<Get_Time>(PlayerPrefs.GetString(key_Direct_Time)).time.TotalTime());
        }
        else
        {
            time = new Time_Game(0, System.DateTime.DaysInMonth(System.DateTime.Today.Year,System.DateTime.Today.Month), System.DateTime.Now.Hour+2, System.DateTime.Now.Minute, System.DateTime.Now.Second);
            Debug.Log(time.TotalTime());
            string json = JsonUtility.ToJson(time);
            PlayerPrefs.SetString(key_Direct_Time, json);
            PlayerPrefs.Save();
            Debug.Log(JsonUtility.FromJson<Time_Game>(PlayerPrefs.GetString(key_Direct_Time)).Day+"  "+ JsonUtility.FromJson<Time_Game>(PlayerPrefs.GetString(key_Direct_Time)).Hour);
        }
        InitTime();
    }

    public void InitTime()
    {

        time_direct = JsonUtility.FromJson<Time_Game>(PlayerPrefs.GetString(key_Direct_Time));
        time_curr = new Time_Game(0, System.DateTime.DaysInMonth(System.DateTime.Today.Year, System.DateTime.Today.Month), System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);
        Delta = Deltatime(time_curr, time_direct);
  //      Debug.Log(time_curr.Day +"   "+time_curr.Hour+"  " + time_direct.Day+"  "+time_direct.Hour);
        Debug.Log(Delta);
     
        if (Delta > 0 && Delta <=7200)
        {
            getReward = false;

           
            hour = Delta % 60 % 60;

            mininue = (Delta - hour * 60 * 60) % 60;

            second = (Delta - hour * 60 * 60 - mininue * 60) % 1;

        }
        else
        {
            getReward = true;
            hour = 0;
            mininue = 0;
            second = 0;

        }












    }
    public void RunningTime()
    {
        Delta -= UnityEngine.Time.deltaTime;
        if (Delta > 0 && Delta <= 7200)
        {
            getReward = false;

            day = Delta / 24 / 60 / 60;

            hour = (Delta / 60 / 60)%24;

            mininue = (Delta / 60) % 60;

            second = (Delta - hour / 60 / 60) % 60;
        //    Debug.Log("Delta :" + Delta +"DAY"+" "+"HOUR :" + hour + "MINUE : " + mininue + " SECOND : " + second);
        }
        else
        {
            hour = 0;
            mininue = 0;
            second = 0;
            getReward = true;
        }

        // time_reward = new Time(0, 0, hour, mininue, second);
    }
    public void SetLastTime()
    {
        time_last = new Time_Game(System.DateTime.Now.Month, System.DateTime.Now.Day, System.DateTime.Now.Hour, System.DateTime.Now.Minute, System.DateTime.Now.Second);

    }

    public void On_Off_Sound_Game(bool open)
    {

        On_Off_Sound(open);
    }

    [System.Serializable]
    public class Time_Game
    {
      public  int Month = 0;
      public   int Day = 0;
      public  float Hour = 0;
      public  float Miniue = 0;
      public  float Second = 0;
        public Time_Game(int Month, int Day, float Hour, float Miniue, float Second)
        {
            this.Month = Month;
            this.Hour = Hour;
            this.Day = Day;
            this.Miniue = Miniue;
            this.Second = Second;
        }
        public float TotalTime()
        {
            return Second + Miniue * 60 + Hour * 60 * 60 + Day * 24 * 60 * 60 + Month * 30 * 24 * 60 * 60;
        }
        public string Text()
        {
            return Hour + "  " + Miniue + "  " + Second;

        }
    }
    [System.Serializable]
    public class Get_Time
    {
        public Time_Game time;
        public Get_Time(Time_Game time)
        {
            this.time = time;

        }

    }
}
     
