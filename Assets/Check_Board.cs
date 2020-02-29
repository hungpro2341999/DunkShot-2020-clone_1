using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Check_Board : MonoBehaviour
{

    public Transform PosEffect;

    int count = 0;
    public int ScoreCurrent = 0;
    public float cooltime = 0;
    float currCoolTimeLevel = 0;
    public bool isOut = false;
    public Dictionary<string, int> Score;

    public const string Key_Score = "SCORE";
    public const string Key_Board = "BOARD";
    public const string Point_1 = "POINT_1";
    public const string Point_2 = "POINT_2";
    public const string Point_3 = "POINT_3";
    public const string Point_4 = "POINT_4";
    public const string Point_Mid = "POINT_MID";
    
    public bool isPerfect = false;
    public bool isGolbal = false;
    public bool isBound = false;
    public bool first = false;
    public bool second = false;
    public bool oneCount = true;
    public void Awake()
    {
      

    }


    public void init()
    {

        Score = new Dictionary<string, int>();



    }
    // Start is called before the first frame update
    void Start()
    {
        init();
        Score.Add(Key_Board, 0);

        Score.Add(Point_1, 0);
        Score.Add(Point_2, 0);
        Score.Add(Point_3, 0);
        Score.Add(Point_4, 0);
        Score.Add(Point_Mid, 0);
       
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(""+Score[Key_Board]+"  "+Score[Point_1]+"  "+Score[Point_2]+"  "+Score[Point_3]+" "+Score[Point_4]);

        if (!GameController.instance.GameOver && !GameController.instance.GamePause)
        {

            currCoolTimeLevel = LevelController.instance.getCoolTimeLevel();

            if (!GameController.instance.GameOver)
            {


           


                }

                if (!isGolbal)
                    if (isGoard()&&!isOut)
                    {
                    isOut = true;
                    //  if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_1) {
                    //    BallPlayer.instance.isGround = false;

                    //       }
                    if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_2)
                    {
                        Debug.Log("DESTROY BY :: " + indexBulletDestroy);
                        if (!isUpdate)
                        {
                            foreach (var a in Gun.instance.isShotting)
                            {
                                if (a.indexBullet == indexBulletDestroy)
                                {
                                    a.countGolbal++;
                                    Debug.Log(a.countGolbal);

                                }

                                isUpdate = true;
                            }

                        }

                    }


                    isPerfect = Result();
                        count = 0;
                        if (isPerfect)
                        {





                            if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_1)
                            {
                              
                            
                                {
                              //      Debug.Log("VAO");
                                    int x = BallPlayer.instance.count_perfect;
                                    BallPlayer.instance.Check_Effect(Paractice_Type.PERFECT);
                                    x++;

                                    SpawnEffect.instance.Set_System(PosEffect.position, "PERFECT X" + x, null, 0);
                                    if (x - 1 != 1)
                                    {
                                        SpawnEffect.instance.Set_System(PosEffect.position, "+" + x * 3, null, 0.5f);

                                    }
                                    LevelController.instance.UpdateTime = true;
                                }
                            }
                            else
                            {
                                int x = Ball_Player_2.instance.countPerfect;
                                x++;

                                SpawnEffect.instance.Set_System(PosEffect.position, "PERFECT X" + x, null, 0);
                                if (x - 1 != 1)
                                {
                                    SpawnEffect.instance.Set_System(PosEffect.position, "+" + x * 3, null, 0.5f);

                                }
                                LevelController.instance.UpdateTime = true;
                                Ball_Player_2.instance.TakeScore(Paractice_Type.PERFECT);
                            //    Gun.instance.RestBullet(3);
                                LevelController.instance.UpdateTime = true;
                            }


                        }
                        else
                        {
                           
                           
                            if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_1)
                            {
                                if ((Score[Key_Board]) + Score[Point_1] + Score[Point_2] == 1)
                            {
                             
                                SpawnEffect.instance.Set_System(PosEffect.position, "COOL", null, 0);
                                SpawnEffect.instance.Set_System(PosEffect.position, "+2", null, 0.5f);
                                 BallPlayer.instance.AddSCore(1);
                                BallPlayer.instance.Check_Effect(Paractice_Type.NORMAL);
                                    LevelController.instance.UpdateTime = true;
                                }
                                else
                                {
                                    BallPlayer.instance.Check_Effect(Paractice_Type.NORMAL);
                                    LevelController.instance.UpdateTime = true;
                                    SpawnEffect.instance.Set_System(Paractice_Type.NORMAL, PosEffect.position, "", null);

                                }
                               
                            }
                            else

                            {
                                
                                if ((Score[Key_Board]) + Score[Point_1] + Score[Point_2] == 1)
                                {

                                    SpawnEffect.instance.Set_System(PosEffect.position, "COOL", null, 0);
                                    SpawnEffect.instance.Set_System(PosEffect.position, "+2", null, 0.5f);
                                    Ball_Player_2.instance.AddSCore(1);
                                    Ball_Player_2.instance.TakeScore(Paractice_Type.NORMAL);
                                    LevelController.instance.UpdateTime = true;
                                   // Gun.instance.RestBullet(3);
                                }
                                else
                                {
                                    SpawnEffect.instance.Set_System(Paractice_Type.NORMAL, PosEffect.position, "", null);
                                    Ball_Player_2.instance.TakeScore(Paractice_Type.NORMAL);
                                    LevelController.instance.UpdateTime = true;
                               
                                //    Gun.instance.RestBullet(3);

                                }
                                    
                              

                            }


                        }

                      //  Debug.Log("VAO");
                        GameObject.FindObjectOfType<Board>().SpawnPefect();

                        isGolbal = true;
                        if(GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_1)
                        {
                            LevelController.instance.UpdateLevel();
                            SpawnerCtrl.instante.chanceBoard = true;

                        }
                        if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_2)
                        {
                            GroupBoard.instace.numRemove++;
                            GroupBoard.instace.Group_Board.Remove(gameObject);
                            if (gameObject.tag == "BoardLeft")
                            {
                                transform.GetComponent<Board>().OutBoard = true;
                               gameObject.tag = "BoardLeftOut";

                            }
                            else
                            {
                                transform.GetComponent<Board>().OutBoard = true;
                               gameObject.tag = "BoardRightOut";
                            }
                          

                        }

                        ///    if (Gun.instance != null)
                        //   {
                        //    Gun.instance.ChanceDirect();

                        //  }
                       

                        }
         

        }
             

        }
        
    

   

    public void AddSCore(string key,int value)
    {
        int a = GetSCore(key);

        a += value;
        Score.Add(key, a);
        
    }

    public int GetSCore(String key)
    {
        return Score[key] ;
    }

    public bool isGoard()
    {

        if (notGobal)
        {
            return false;
        }
        if (Score[Point_4] == 1 && Score[Point_Mid] == 1 && Score[Point_3] == 0)
        {
            Reset_board();
            return false;

        }
        if (Score[Point_3] == 1 && Score[Point_4] == 1)
            {
                return true;

            }
        return false;

        }
        
        
       

    
     

    
    public void Reset_board()
    {

     
        Score[Key_Board] = 0;
        Score[Point_1] = 0;
        Score[Point_2] = 0;
        Score[Point_3] = 0;
        Score[Point_4] = 0;
        Score[Point_Mid] = 0;
        first = false;
        second = false;
        notGobal = false;
        indexBulletDestroy = 0;


    }

    public bool Result()
    {
       
        if (Score[Point_1] == 0 &&(Score[Point_2] == 0 && Score[Key_Board] == 0))
        {
            Debug.Log("Perfect");
            return true;

        }
        else
        {
            Debug.Log("Normal");
            return false;
        }

        

    }

    /// <summary>
    /// //////
    /// </summary>
    public bool notGobal = false;
    public int indexBulletDestroy = 0;
   bool isUpdate = false;
    public void SetValueKey(string key,int Value)
    {
        if (!notGobal)
        {
            Debug.Log("KEY :" + key);
            Score[key] = Value;
            if (key == "POINT_4")
            {
                if (Score["POINT_3"] == 0)
                {
                    notGobal = true;
                }
            }
        }
       
    }
    public void DestroyBy(int index,string key)
    {
        if (key == "POINT_3")
        {

            indexBulletDestroy = index;
        }
    }

    
}

    
    

