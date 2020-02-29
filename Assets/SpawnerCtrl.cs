using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCtrl : MonoBehaviour
{


    //Mode 1//
    /// <summary>
    /// 
    /// </summary>

    float max = 0.8f;

    float min = 0.2f;
    public GameObject Board_Mode_1_Obj;

    public static SpawnerCtrl instante = null;

    public bool chanceBoard = false;

    public GameObject BoardLeft;

    public GameObject BoardRight;

    public GameObject BoardLeft_mode3;

    public GameObject BoardRight_mode3;


    public GameObject Star;

    public int coolTimeSpawnBoard;

    int maxY;
    int minY;
    
    
    public GameObject obsRight;

    public GameObject obsLeft;

    public GameObject obsUp;

    public GameObject obsBottom;



    public int Level = 1;

    public GameObject Point;

    public List<Board> list_board = new List<Board>();
    public Transform Level_12345;
    public GameObject playerMode_1;

    public GameObject[] Monster;
    public Transform MonsterTrans;
    //1-20 : NORMAL
    //20-30:GO DOWN
    //30-40:GO Up
    /// <summary>
    ///                                     MODE_GAME_PLAY_1
    /// </summary>
    /// <param name="Board"></param>
    /// 

  

   public void SpawnMonster(int index)
    {
       GameObject  game = Monster[0];

     
        Instantiate(Monster[0],Vector3.zero,Quaternion.identity,MonsterTrans);

    }

    public void Set_Up_Level(GameObject Board)
    {

        int i = 100;
        int x = Random.Range(0, i);
        Random.Range(0, x);

        if (x > 0 && x < 8)
        {
            // Down Up
            Board.GetComponent<Board>().Board_type = BOARD_TYPE.TOP_DOWN;

        }
        else if (x >= 8 && x < 16)
        {

            // Left Right
            Board.GetComponent<Board>().Board_type = BOARD_TYPE.LEFT_RIGHT;


        }
        else if (x >= 16 && x < 22)

        {
            //Cross Right
            Board.GetComponent<Board>().Board_type = BOARD_TYPE.ROTAION;


        }
        else if (x > 22 && x < 28)

        {
            //Cross Left
            Board.GetComponent<Board>().Board_type = BOARD_TYPE.ROTAION;

        }
        else if (x >= 28 && x < 32)
        {
            Board.GetComponent<Board>().Board_type = BOARD_TYPE.JERK_TOP_DOWN;
            //Lerp Down Up
        }
        else if (x >= 32 && x < 36)
        {
            // Lerp Right Left
            Board.GetComponent<Board>().Board_type = BOARD_TYPE.JERK_TOP_DOWN;
            Board.GetComponent<Board>().jerk = true;
        }
        else if (x >= 36 && x < 38)
        {
            //Lerp Cross Right
            Board.GetComponent<Board>().jerk = true;
            Board.GetComponent<Board>().Board_type = BOARD_TYPE.JERK_RIGHT_LEFT_CROSS;
        }
        else if (x >= 38 && x < 40)
        {
            Board.GetComponent<Board>().jerk = true;
            Board.GetComponent<Board>().Board_type = BOARD_TYPE.JERK_TOP_DOWN_CROSS;
            // Lerp Corss  Left
        }
        else
        {
            Board.GetComponent<Board>().jerk = true;
            Board.GetComponent<Board>().Board_type = BOARD_TYPE.NORMAL;

            // Normal
        }








    }
   

    public void Awake()
    {
        maxY = Screen.width;
        if (instante != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instante = this;
        }




    }

    public void Spawn_onRest()
    {
       
        Board[] boards = GameObject.FindObjectsOfType<Board>();
        if (boards != null)
        {
            for (int i = 0; i < boards.Length; i++)
            {
                boards[i].Destroy();
            }
        }


        switch (GameController.instance.Game_Type)
        {
            case GAME_MODE_TYPE.MODE_1:

                var a = Instantiate(BoardLeft, new Vector3(-9.52523f, -0.5259463f, 0), Quaternion.identity, GameObject.Find("Mode_2").transform).GetComponent<Board>();
                Level = 0;

                break;
            case GAME_MODE_TYPE.MODE_2:
                SpawnLeft = true;



                Level = 0;

                break;
            case GAME_MODE_TYPE.MODE_3:
                Level = 0;
               
                DestroyMySelf[] destroy = GameObject.FindObjectsOfType<DestroyMySelf>();
                Debug.Log("Rest 1");
                for (int i = 0; i < destroy.Length; i++)
                {
                    destroy[i].Destroy();
                }
                var obj = Instantiate(playerMode_1, new Vector3(1.85f, -3.64f, 0), Quaternion.identity, GameObject.Find("Mode_1").transform);
                obj.transform.Find("Net").GetComponent<Board_Mode_1>().TakeBall();
                Instantiate(Board_Mode_1_Obj, new Vector3(-1.185633f, -1.02f, 0), Quaternion.identity, GameObject.Find("Mode_1").transform);
                Level = 0;
              
                break;
            

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MODE_GAME.Reset_Mode_1 += Spawn_onRest;
        MODE_GAME.Reset_Mode_2 += Spawn_onRest;
        MODE_GAME.Reset_Mode_3 += Spawn_onRest;
        obsRight = GameObject.Find("RightBoradObs");

        obsLeft = GameObject.Find("LeftBoradObs");

        obsUp = GameObject.Find("UpBoardObs");

        obsBottom = GameObject.Find("BottomBoardObs");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Spawn");
            chanceBoard = true;
        }
      
         
      
     //   Debug.Log(GameController.instance.Game_Type);
        switch (GameController.instance.Game_Type)
        {
            case GAME_MODE_TYPE.MODE_1:
                obsRight.transform.position = new Vector3(GameController.instance.rightBottomReal.x, GameController.instance.rightBottomReal.y + GameController.instance.heightReal/ 2, 0);
                obsLeft.transform.position = new Vector3(GameController.instance.leftBottomReal.x, GameController.instance.leftBottomReal.y + GameController.instance.heightReal / 2, 0);
                obsUp.transform.position = new Vector3(GameController.instance.leftBottomReal.x + GameController.instance.widthReal / 2, GameController.instance.leftUpRead.y + 3f, 0);
                obsBottom.transform.position = new Vector3(GameController.instance.leftBottomReal.x + GameController.instance.widthReal / 2, GameController.instance.leftBottomReal.y, 0);
                SetCollider();
                ChanceBoard(GameObject.Find("Mode_2").transform);

                break;
            case GAME_MODE_TYPE.MODE_2:
                obsRight.transform.position = new Vector3(GameController.instance.rightBottomReal.x, GameController.instance.rightBottomReal.y + GameController.instance.heightReal / 2, 0);
                obsLeft.transform.position = new Vector3(GameController.instance.leftBottomReal.x, GameController.instance.leftBottomReal.y + GameController.instance.heightReal / 2, 0);
                obsUp.transform.position = new Vector3(GameController.instance.leftBottomReal.x + GameController.instance.widthReal / 2, GameController.instance.leftUpRead.y+2, 0);
                obsBottom.transform.position = new Vector3(GameController.instance.leftBottomReal.x + GameController.instance.widthReal / 2, GameController.instance.leftBottomReal.y, 0);
                SetCollider();
                ChanceBoard_2(GameObject.Find("Mode_3").transform);



                break;
            case GAME_MODE_TYPE.MODE_3:
                obsRight.transform.position = new Vector3(GameController.instance.rightBottomReal.x, GameController.instance.rightBottomReal.y + GameController.instance.heightReal / 2, 0);
                obsLeft.transform.position = new Vector3(GameController.instance.leftBottomReal.x, GameController.instance.leftBottomReal.y + GameController.instance.heightReal / 2, 0);
                obsUp.transform.position = new Vector3(GameController.instance.leftBottomReal.x + GameController.instance.widthReal / 2, GameController.instance.leftUpRead.y + 1.7f, 0);
                obsBottom.transform.position = new Vector3(GameController.instance.leftBottomReal.x + GameController.instance.widthReal / 2, GameController.instance.leftBottomReal.y-3f, 0);
                SetCollider();
             ChanceBoard_3(GameObject.Find("Mode_1").transform);
                break;
            case GAME_MODE_TYPE.MODE_4:
                obsRight.transform.position = new Vector3(GameController.instance.rightBottomReal.x, GameController.instance.rightBottomReal.y + GameController.instance.heightReal / 2, 0);
                obsLeft.transform.position = new Vector3(GameController.instance.leftBottomReal.x, GameController.instance.leftBottomReal.y + GameController.instance.heightReal / 2, 0);
                obsUp.transform.position = new Vector3(GameController.instance.leftBottomReal.x + GameController.instance.widthReal / 2, GameController.instance.leftUpRead.y + 3f, 0);
                obsBottom.transform.position = new Vector3(GameController.instance.leftBottomReal.x + GameController.instance.widthReal / 2, GameController.instance.leftBottomReal.y - 3f, 0);
               
                break;


        }


    }

    public void SetCollider()
    {
        BoxCollider[] boxCollider = obsLeft.GetComponentsInChildren<BoxCollider>();
        BoxCollider[] boxColliders = obsRight.GetComponentsInChildren<BoxCollider>();
        switch (GameController.instance.Game_Type)
        {
            case GAME_MODE_TYPE.MODE_1:




                for (int i = 0; i < boxCollider.Length; i++)
                {
                    boxCollider[i].isTrigger = true;
                    boxColliders[i].isTrigger = true;

                }

                break;
            case GAME_MODE_TYPE.MODE_2:


                for (int i = 0; i < boxColliders.Length; i++)
                {
                    boxColliders[i].isTrigger = false;
                    boxColliders[i].isTrigger = false;
                }




                break;
            case GAME_MODE_TYPE.MODE_3:

                break;


        }

    }




    public void SetLevelCamera()
    {
        Camera.main.orthographicSize = LevelController.instance.getLevelCamera();

    }





    void ChanceBoard(Transform worldspace)
    {



        if (SpawnerCtrl.instante.chanceBoard == true)
        {



            chanceBoard = false;

            GameObject destroyObjectLeft = GameObject.FindGameObjectWithTag("BoardLeft");

            GameObject destroyObjectRight = GameObject.FindGameObjectWithTag("BoardRight");

            if (destroyObjectLeft != null)
            {
                //Spawn Board Right 
                destroyObjectLeft.gameObject.tag = "BoardLeftOut";

                GameObject.Find("Ball").GetComponent<BallPlayer>().ChanceDirect();

                destroyObjectLeft.GetComponent<Board>().OutBoard = true;

                Vector2 RightBottom = GameController.instance.rightBottomReal;

                Vector2 RightUp =GameController.instance.rightUpReal;

                //  Debug.Log(ScreenToWordPoint(GameController.instance.rightbottom) + "  " + GameController.instance.rightbottom);
                float xSpawn = RightBottom.x;

                float ySpawn = Random.Range(RightBottom.y + GameController.instance.heightReal * 0.25f, RightUp.y - GameController.instance.heightReal * 0.25f);

            //    Debug.Log(new Vector2(xSpawn, ySpawn));

                if (Level <= 5)
                {


                    GameObject game_1 = Instantiate(BoardRight, new Vector2(xSpawn, ySpawn), Quaternion.identity, worldspace);
                    Debug.Log("SPAWN BOARD");


                }
                else
                {
                    //  Set_Up_Level(xSpawn, ySpawn,BoardRight);

                    float distance = 3;
                    GameObject game_1 = Instantiate(BoardRight, new Vector2(xSpawn, ySpawn), Quaternion.identity, worldspace);

                    Set_Up_Level(game_1);

                }
                Level++;

            }
            else
            {

                Level++;
                destroyObjectRight.gameObject.tag = "BoardRightOut";

                GameObject.Find("Ball").GetComponent<BallPlayer>().ChanceDirect();

                destroyObjectRight.GetComponent<Board>().OutBoard = true;

                Vector2 LeftBottom =GameController.instance.leftBottomReal;

                Vector2 LeftUp = GameController.instance.leftUpRead;



                float xSpawn = LeftBottom.x;

                float ySpawn = Random.Range(LeftBottom.y + GameController.instance.heightReal*0.25f, LeftUp.y - GameController.instance.heightReal*0.25f);



                Debug.Log("TOA DO :: :" + LeftBottom.y * min + "  " + LeftUp.y * max);




                if (Level < 5)
                {


                    GameObject game_2 = Instantiate(BoardLeft, new Vector2(xSpawn, ySpawn), Quaternion.identity, worldspace);

                    game_2.GetComponent<Board>().Board_type = BOARD_TYPE.NORMAL;



                }
                else
                {

                    float distance = 3f;
                    GameObject game_2 = Instantiate(BoardLeft, new Vector2(xSpawn, ySpawn), Quaternion.identity, worldspace);
                    Set_Up_Level(game_2);

                }
                Level++;

       //         Debug.Log(xSpawn + "  " + ySpawn);





            }




        }
    }









    Vector2 ScreenToWordPoint(Vector2 pos)
    {



        Vector2 point = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, Camera.main.nearClipPlane));

        return point;


    }



    //////////////////////// MODE GAME 3/////////////////////////////
    ///


    public bool SpawnLeft = false;
    void ChanceBoard_2(Transform worldSpace)
    {

        if (SpawnerCtrl.instante.chanceBoard == true)
        {


            chanceBoard = false;


            //      GameObject destroyObjectLeft = GameObject.FindGameObjectWithTag("BoardLeft");

            //  GameObject destroyObjectRight = GameObject.FindGameObjectWithTag("BoardRight");

            if (SpawnLeft)
            {
                //Spawn Board Right 
                //  destroyObjectLeft.gameObject.tag = "BoardLeftOut";

                //   GameObject.Find("Ball").GetComponent<BallPlayer>().ChanceDirect();

                //   destroyObjectLeft.GetComponent<Board>().OutBoard = true;

                //    Vector2 RightBottom = ScreenToWordPoint(GameController.instance.rightbottom);

                //   Vector2 RightUp = ScreenToWordPoint(GameController.instance.rightup);

                //  Debug.Log(ScreenToWordPoint(GameController.instance.rightbottom) + "  " + GameController.instance.rightbottom);
                //     float xSpawn = RightBottom.x;

                //     float ySpawn = Random.Range(RightBottom.y * min, RightUp.y * max);

                //    Debug.Log(new Vector2(xSpawn, ySpawn));

                SpawnGroup(SpawnLeft);
                SpawnLeft = !SpawnLeft;
                /*
                if (Level <= 5)
                {


                    GameObject game_1 = Instantiate(BoardRight, new Vector2(xSpawn, ySpawn), Quaternion.identity, worldSpace);
                    Debug.Log("SPAWN BOARD");

                }
                else
                {
                    //  Set_Up_Level(xSpawn, ySpawn,BoardRight);

                    float distance = 3;
                    GameObject game_1 = Instantiate(BoardRight, new Vector2(xSpawn, ySpawn), Quaternion.identity, worldSpace);

                    Set_Up_Level(game_1);

                }
                Level++;
                */
            }
            else
            {
                SpawnGroup(SpawnLeft);
                SpawnLeft = !SpawnLeft;

                /*
                Level++;
                destroyObjectRight.gameObject.tag = "BoardRightOut";


                //     GameObject.Find("Ball").GetComponent<BallPlayer>().ChanceDirect();

                destroyObjectRight.GetComponent<Board>().OutBoard = true;

                Vector2 LeftBottom = ScreenToWordPoint(GameController.instance.leftbottom);

                Vector2 LeftUp = ScreenToWordPoint(GameController.instance.leftup);



                float xSpawn = LeftBottom.x;

                float ySpawn = Random.Range(LeftBottom.y + 1.5f, LeftUp.y - 1.5f);



                Debug.Log("TOA DO :: :" + LeftBottom.y * min + "  " + LeftUp.y * max);




                if (Level < 5)
                {


                    GameObject game_2 = Instantiate(BoardLeft, new Vector2(xSpawn, ySpawn), Quaternion.identity, worldSpace);

                    game_2.GetComponent<Board>().Board_type = BOARD_TYPE.NORMAL;



                }
                else
                {

                    float distance = 3f;
                    GameObject game_2 = Instantiate(BoardLeft, new Vector2(xSpawn, ySpawn), Quaternion.identity, worldSpace);
                    Set_Up_Level(game_2);

                }
                Level++;

                Debug.Log(xSpawn + "  " + ySpawn);





            }


    */

            }
        }
    }



      public  void ChanceBoard_3(Transform worldSpace)
        {

            if (SpawnerCtrl.instante.chanceBoard == true)
            {

            SpawnerCtrl.instante.chanceBoard = false;

            StartCoroutine( Spawn(worldSpace));
        
        }


        }
    IEnumerator Spawn(Transform worldSpace)
    {





        //       Debug.Log("Spawn_1");

        float width = GameController.instance.widthReal;
        float height = GameController.instance.heightReal;
        float posX0 = GameController.instance.leftUpRead.x;
        float posX1 = GameController.instance.rightUpReal.x;
        Debug.Log(Vector2.Distance(GameController.instance.leftUpRead, GameController.instance.rightUpReal));
        float posY = height * 0.3f;

        GameObject[] Board = GameObject.FindGameObjectsWithTag("Board");
        GameObject BoardLife = null;
        GameObject BoardDestroy = null;
        bool done = false;
        Debug.Log(Board.Length);
        for (int i = 0; i < Board.Length; i++)
        {
            if (Board[i].transform.Find("Basket/Trigger").GetComponent<CheckInBall>().key != BoardControl.instance.Key)
            {
                BoardDestroy = Board[i];

            }
            else
            {
                BoardLife = Board[i];
            }




        }






            float Space = height * 0.35f;
            float x = BoardLife.transform.position.x;
            float Posy = BoardLife.transform.position.y + Space;
            BoardLife.GetComponent<Board_Mode_1>().movePath = false;
            while (!done)
            {
                float posX = Random.Range(posX0, posX1);
        //        Debug.Log(posX);
                Debug.Log(posX0 + " " + posX1);
                Debug.Log(posX1 - width * 0.3f + "  " + posX0 + width * 0.3f);
                Debug.Log(height * 0.3f + "  " + height);
                if (posX > posX1 - GameController.instance.widthReal*0.27f || posX < posX0 + GameController.instance.widthReal * 0.27f || (posX > x - GameController.instance.widthReal * 0.175f && posX < x + GameController.instance.widthReal * 0.175f))
                {
                    Debug.Log("No Spaw");
                    
                }
                else
                {
                    var a = Instantiate(Board_Mode_1_Obj, new Vector2(posX, Posy), Quaternion.identity, worldSpace);
                    if (posX < x)
                    {
                        a.GetComponent<Board_Mode_1>().RotationLeft = true;

                    }
                    else
                    {

                        a.GetComponent<Board_Mode_1>().RotationLeft = false;
                    }

                    a.transform.Find("Basket/Trigger").GetComponent<CheckInBall>().key = BoardControl.instance.Key + 1;
                  
                    Set_Up_Level_Mode_1(a);
                    done = true;
                    BoardDestroy.GetComponent<DestroyMySelf>().Destroy();
                }


            }



            /*

         //   Debug.Log("1 :" + posY);
            bool done = false;
            GameObject Board = null;
            GameObject Board_1 = null;

            while (!done)
            {
                Board = GameObject.FindGameObjectWithTag("Net_In");
                Board_1 = GameObject.FindGameObjectWithTag("Net_Out");
                if (Board != null && Board_1 != null)
                {

                    done = true;

                }
                yield return new WaitForSeconds(0.02f);
              //  Debug.Log("2 :" + posY);

            }
         //   Debug.Log("Spawn");


            Debug.Log("3 :" + posY);
            float width_object = Board_Mode_1_Obj.transform.Find("Img").GetComponent<SpriteRenderer>().size.x;


            float minWidth = GameController.instance.leftUpRead.x + GameController.instance.widthReal*0.2f;
            float maxWidth = GameController.instance.rightUpReal.x - GameController.instance.widthReal*0.2f;

           Debug.Log(minWidth + "  " + maxWidth);

            posY += Board.transform.position.y;
            Board.transform.Find("Net").GetComponent<Board_Mode_1>().movePath = false;
            Board.transform.Find("Img").GetComponent<Scale>().Pos = (Vector2)Board.transform.Find("Net/Body/Pos").transform.position;
            //     Debug.Log("2 :" + posY); 
            int i = Random.Range(0, 1);
            Vector2 pos = Board.transform.position;
            float xMin = 0;
            float xMax = 0;

        //  if (Level > 4)
     //   {


                if (i == 0)
                {

                    xMin = posX0 + width_object;
                    xMax = posX1 - width_object;
                    float x = Random.Range(xMin, xMax);
                    x = Mathf.Clamp(x, minWidth, maxWidth);
            Debug.Log(minWidth+" "+ maxWidth);
            Debug.Log(x);
                    var a = Instantiate(SpawnerCtrl.instante.Board_Mode_1_Obj, new Vector2(x, posY), Quaternion.identity, worldSpace);
                    Debug.Log(a.transform.position);
                    var b = a.transform.Find("Net").gameObject;
                    a.transform.Find("Net/Trigger").GetComponent<CheclInBall>().getNextKey();
                 //   Set_Up_Level_Mode_1(b);
                    b.GetComponent<Board_Mode_1>().RotationLeft = true;
            Debug.Log(xMax + " " + xMin);


        }
                else
                {

               xMin = posX0 + width_object;
               xMax = posX1 - width_object;
            Debug.Log(xMin + "  " + xMax);
                    float x = Random.Range(xMin, xMax);
                    x = Mathf.Clamp(x, minWidth, maxWidth);
                    var a = Instantiate(Board_Mode_1_Obj, new Vector2(x, posY), Quaternion.identity, worldSpace);
            Debug.Log(a.transform.position);
            var b = a.transform.Find("Net").gameObject;
                    b.GetComponent<Board_Mode_1>().RotationLeft = false;
                    a.transform.Find("Net/Trigger").GetComponent<CheclInBall>().getNextKey();
                    Debug.Log(xMin + "  " + xMax);
                    Set_Up_Level_Mode_1(b);
                }

      //    }
         //   else
        //  {
       //    Level_123456(Level, worldSpace, posY);


       //   }
            Level++;
        Board_1.transform.Find("Img").GetComponent<Scale>().destroy = true;
        Board_1.GetComponent<DestroyMySelf>().Destroy();
        */
            yield return new WaitForSeconds(0);
        
    }


    public Vector2[] setPos5level;
    public void Level_123456(int level,Transform trs,float distance)
    {   
      
       
      
        var obj = Instantiate(Board_Mode_1_Obj,new Vector2(setPos5level[level].x,distance), Quaternion.identity, trs);
        obj.transform.Find("Net/Trigger").GetComponent<CheclInBall>().getNextKey();


    }
   



    public void Set_Up_Level_Mode_1(GameObject Board)
    {


        int i = 100;
        int x = Random.Range(0, i);
        Random.Range(0, x);

        if (x > 0 && x < 8)
        {
            // Down Up
            Board.GetComponent<Board_Mode_1>().Board_type = BOARD_TYPE.TOP_DOWN;

        }
        else if (x >= 8 && x < 16)
        {

            // Left Right
            Board.GetComponent<Board_Mode_1>().Board_type = BOARD_TYPE.LEFT_RIGHT;


        }
        else if (x >= 16 && x < 22)

        {
            //Cross Right
            Board.GetComponent<Board_Mode_1>().Board_type = BOARD_TYPE.ROTAION;


        }
        else if (x > 22 && x < 28)

        {
            //Cross Left
            Board.GetComponent<Board_Mode_1>().Board_type = BOARD_TYPE.ROTAION;
        }
        else if (x >= 28 && x < 32)
        {
            Board.GetComponent<Board_Mode_1>().Board_type = BOARD_TYPE.JERK_TOP_DOWN;
            //Lerp Down Up
        }
        else if (x >= 32 && x < 36)
        {
            // Lerp Right Left
            Board.GetComponent<Board_Mode_1>().Board_type = BOARD_TYPE.JERK_TOP_DOWN;
        }

        else if (x >= 36 && x < 38)
        {
            //Lerp Cross Right

            Board.GetComponent<Board_Mode_1>().Board_type = BOARD_TYPE.JERK_RIGHT_LEFT_CROSS;
        }
        else if (x >= 38 && x < 40)
        {

            Board.GetComponent<Board_Mode_1>().Board_type = BOARD_TYPE.JERK_TOP_DOWN_CROSS;
            // Lerp Corss  Left
        }
        else
        {

            Board.GetComponent<Board_Mode_1>().Board_type = BOARD_TYPE.NORMAL;

            // Normal
        }
        

    }
    GameObject player;
    GameObject net_1;
    Vector3 pos;
    Vector3 pos_1;
    public void onRest()
    {
       


    }
    public void getState()
    {
     

    }
    public void DestroyAllObject_Mode_1()
    {
    

    }
    public enum Group_Spawn {DOWN_UP,LEFT_RIGHT,NORMAL,ROTATION,DOWN_UP_JERK,LEFT_RIGHT_JERK}; 




    public void SpawnGroup(bool isLeft)
    {
        isLeft = Gun.instance.isLeft;
        Vector3[] posGroup = null;
        bool done = false;
        float maxy0 = GameController.instance.leftBottomReal.y+ GameController.instance.heightReal * 0.275f;
        float maxy1 = GameController.instance.leftUpRead.y - GameController.instance.heightReal*0.135f;
        float space = GameController.instance.heightReal * 0.2f;
     //   Debug.Log(maxy0 + " " + maxy1+" "+space);
        float x = 0;
        if (isLeft)
        {
             x = GameController.instance.leftBottomReal.x;

        }
        else
        {
            x = GameController.instance.rightBottomReal.x;
        }
        int count = Random.Range(2, 4);
        Debug.Log(count);
        while (!done)
        {
            
            float y = Random.Range(maxy0, maxy1);
           
            posGroup = new Vector3[count];
            done = true;
            for (int i = 0; i < count; i++)
            {
                
               float PosY =  y - (i * space);
                if (PosY < maxy0)
                {

                    done = false;
                    
                }
                else
                {
                    posGroup[i].x = x;
                    posGroup[i].y = PosY;
                    
                }
            }
           

        }
       
        GameObject[] game = new GameObject[posGroup.Length];
        if (isLeft)
        {
            for (int i = 0; i < posGroup.Length; i++)
            {

              //  Debug.Log("" + i + "  " + posGroup[i]);
               var a =  Instantiate(BoardLeft_mode3, posGroup[i], Quaternion.identity, ModeCtrl.instance.getWorldSpace());
                game[i] = a;
                GroupBoard.instace.AddBoard(game[i]);
            }

        }
        else
        {
            for (int i = 0; i < posGroup.Length; i++)
            {

           //     Debug.Log("" + i + "  " + posGroup[i]);
              var a =   Instantiate(BoardRight_mode3, posGroup[i], Quaternion.identity, ModeCtrl.instance.getWorldSpace());
                game[i] = a;
                GroupBoard.instace.AddBoard(game[i]);
            }
        }
        if (Level > 5)
        {

            Set_Up_Level_Group(game);
        }
        Level++;
      

    }
  


    public void Set_Up_Level_Group(GameObject[] Board)
    {

        int i = 100;
        int x = Random.Range(0, i);
        Random.Range(0, x);

        if (x > 0 && x < 8)
        {
            // Down Up
            for(int j=0;j<Board.Length;j++)
            Board[j].GetComponent<Board>().Board_type = BOARD_TYPE.TOP_DOWN;

        }
        else if (x >= 8 && x < 16)
        {

            // Left Right
            for (int j = 0; j < Board.Length; j++)
              Board[j].GetComponent<Board>().Board_type = BOARD_TYPE.LEFT_RIGHT;


        }
        else if (x >= 16 && x < 22)

        {
            //Cross lrft right
            for (int j = 0; j < Board.Length; j++)
              Board[j].GetComponent<Board>().Board_type = BOARD_TYPE.LEFT_RIGHT;


        }
        else if (x > 22 && x < 28)

        {
            for (int j = 0; j < Board.Length; j++)
                Board[j].GetComponent<Board>().Board_type = BOARD_TYPE.ROTAION;
        }
        else if (x >= 28 && x < 32)
        {
            for (int j = 0; j < Board.Length; j++)
                Board[j].GetComponent<Board>().Board_type = BOARD_TYPE.JERK_TOP_DOWN ;
            //Lerp Down Up
        }
        else if (x >= 32 && x < 36)
        {
            // Lerp Right Left
            for (int j = 0; j < Board.Length; j++)
            {
                Board[j].GetComponent<Board>().Board_type = BOARD_TYPE.JERK_LEFT_RIGHT;
                Board[j].GetComponent<Board>().jerk = true;

            }
                
        }
        else if (x >= 36 && x < 38)
        {
            for (int j = 0; j < Board.Length; j++)
            {
                Board[j].GetComponent<Board>().Board_type = BOARD_TYPE.JERK_RIGHT_LEFT_CROSS;
                Board[j].GetComponent<Board>().jerk = true;

            }
            //Lerp Cross Right
         
        }
        else if (x >= 38 && x < 40)
        {
            for (int j = 0; j < Board.Length; j++)
            {
                Board[j].GetComponent<Board>().Board_type = BOARD_TYPE.JERK_TOP_DOWN_CROSS;
                Board[j].GetComponent<Board>().jerk = true;

            }
           
            // Lerp Corss  Left
        }
        else
        {
            for (int j = 0; j < Board.Length; j++)
            {
                Board[j].GetComponent<Board>().Board_type = BOARD_TYPE.NORMAL;
              

            }

            // Normal
        }








    }

}


