using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{

    public GameObject Practice_Ball;
    public Transform posStar;
    
    public bool isTrajectory = false;
    public static Player instance = null;
    public int index = 0;
    public bool isInterecable =false;
    public Vector2 PosMousePosition;
    public float Strenght = 30;
    public bool Click_1 = false;
    public bool Click_2 = false;
    public float max;
    public Vector2 pos;
    public Vector2 Reset_pos;
    public Vector2 Scale;
    public Vector2 Scale_1;
    public Board_Mode_1 Board;
    public Rigidbody2D Body;
    public Rotate rotate;
    public CheclInBall trigger;
    public bool isCickBall;
   
    public Vector2 posVelocity;
    public float width;
    public float height;
    float time=0;
    public Board_Rotation Board_1;
    public Vector2 VectorReflect;
    public float Star;
    public float Score;
    public bool isDone = false;

    int CountNormal = 1;
    bool done = true;
    public bool Still10s = true;
    
    public int CountIntereableWall;
    public bool twoWall = false;

    // Start is called before the first frame update
   
    public void onReset()
    {
        Star = GameController.instance.Get_Star();
        Player.instance.transform.position = Player.instance.Reset_pos;
       Player.instance.transform.gameObject.layer = 14;
       Player.instance.Body.isKinematic = true;
       Player.instance.Body.velocity = Vector2.zero;

        Debug.Log("REset player");
        Score = 0;

    }
    public int GetKey()
    {
        index++;
        return index;
    }
  
   // float Timestill1os = 0;
   // float TimeReas10s;
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
    public void onReset_Mode()
    {

    }
    void Start()
    {
        Star = GameController.instance.Get_Star();
         Strenght = 26;
        Reset_pos = transform.position;
  
        Body =   GetComponent<Rigidbody2D>();
        Scale = Board.transform.localScale;
        Scale_1 = Board_1.transform.localScale;
        pos = transform.position;
        width = GetComponent<SpriteRenderer>().size.x;
        height = GetComponent<SpriteRenderer>().size.y;
        max = width * 0.3f;
        Trajector();

    }
    public float fuck = 0.01f;
    // Update is called once per frame
    void Update()
    {

        transform.GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBall();
       // Time.timeScale = 1.4f;
        if (!GameController.instance.GameOver || !GameController.instance.GamePause)
        {


            
            DirectRotation();
            SpawnPractice();

           
            if (GameController.instance.leftBottomReal.y <= transform.position.y)
            {

                if (gameObject.layer == 13)
                {
                    Body.isKinematic = true;
                    MoveFollowPlayer.instance.moveCamera = false;
                    MouseMove();
                    CheckIntercalbe();
                 //   if (isInterecable)
               //    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Click_1 = true;
                      
                        }

               //     }



                    if (Click_1)
                    {
                        if (done)
                        {
                          transform.localPosition = Vector3.zero;
                            done = false;
                        }
                  
                        Direct(DirectRotation());
                        Board.Direct();
                        rotate.Direct();
                        Board_1.Direct();
                        Vector2 new_pos = PosMousePosition;
                        Vector2 new_pos_1 = PosMousePosition;

                                     /*
                                        if (Vector2.Distance(new_pos, pos) < max && Vector2.Distance(new_pos, pos) > 0)
                                        {
                                            float dis = Vector2.Distance(new_pos, pos);

                                            Vector2 scale1 = new Vector2(Scale.x, (Scale.y + dis) * 0.5f);
                                            Board.SetScale(scale1);
                                            Board_1.SetScale(scale1);
                                            transform.position = new_pos;
                                        }
                                        else if (Vector2.Distance(new_pos, pos) > max)
                                        {
                                            new_pos_1 = pos - (2 * DirectRotation_1().normalized * max * 0.5f);

                                            transform.position = new_pos_1;
                                            float dis = Vector2.Distance(new_pos_1, pos);
                                            Vector2 scale1 = new Vector2(Scale.x, (Scale.y + dis) * 0.5f);
                                            Board.SetScale(scale1);
                                            Board_1.SetScale(scale1);

                                        }
                                        */
                        if (Vector2.Distance(new_pos, pos) < max)
                        {
                            float dis = Vector2.Distance(new_pos, pos);

                            Vector2 scale1 = new Vector2(Scale.x, (Scale.y + dis*0.8f));
                        //    Vector2 scale2 = new Vector2(Scale_1.x, (Scale_1.y +dis));
                            Board.SetScale(scale1);
                      //     Board_1.SetScale(scale2);
                            transform.position = new_pos;
                            FullForce = false;
                        }
                        else
                        {
                            FullForce = true;
                            new_pos_1 = pos - (DirectRotation_1().normalized * max);

                            transform.position = new_pos_1;
                            float dis = Vector2.Distance(new_pos_1, pos);
                            Vector2 scale1 = new Vector2(Scale.x, (Scale.y + dis*0.8f));
                    //      Vector2 scale2 = new Vector2(Scale_1.x, (Scale_1.y +dis));
                            Board.SetScale(scale1);
                     //      Board_1.SetScale(scale2);
                       //      Debug.Log(scale1 + " " + scale2);
//
                        }

                        SimulateTrajectory(transform.position, DirectAndForce());
                     

                    }
                    else
                    {
                        enableTrajectory(false);
                    }
                    if (Input.GetMouseButtonUp(0) && Click_1)
                    {
                        //   Debug.Log("Mosue_Up");
                        Click_2 = true;
                        Click_1 = false;
                        enableTrajectory(false);

                    }
                    if (Click_2 && !Click_1)
                    {


                        Board.ResetScale();
                        Board_1.ResetScale();
                      
                        Shoot(DirectAndForce());
                        transform.position = pos;


                   
                        Click_2 = false;
                        VectorReflect = DirectAndForce();
                    }


                }
                else
                {
                    MoveFollowPlayer.instance.moveCamera = true;

                    time += 10;

                    // transform.rotation = Quaternion.AngleAxis(time, Vector3.forward);

                }
                // Debug.Log(Body.velocity);
            }
        }
        else
        {
            GameObject.Find("ScreenGameplay").GetComponent<ScreenGamePlay>().GameOverWindow();

        }
    }
    public void MouseMove()
    {
        PosMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);



    }
    public bool  CheckIntercalbe()
    {
        if (Vector2.Distance(PosMousePosition, transform.position)<0.5f)
        {
            isInterecable = true;
            return true;
        }
        else
        {
            isInterecable = false;
        }
        return true;
       

    }

    public Vector2 DirectRotation()
    {
        return ((Vector2)transform.position - PosMousePosition);
    }
    public Vector2 DirectRotation_1()
    {
        return ((Vector2)pos - PosMousePosition);
    }

    public void Direct(Vector2 direct)
    {
        float angle = Mathf.Atan2(direct.y,direct.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    

    }
    public void Move(Vector2 pos)
    {
        transform.position = pos;
    }

   // 13: Ball
  
 public void Shoot(Vector2 strengh)
    {
       
        gameObject.layer = 14;
        RestTrigger(0.5f);
        Body.simulated = true;
     
        Body.isKinematic = false;
        //   OnEnable_mode(false);
          transform.parent = GameObject.Find("Mode_1").transform;
      

          enableTrajectory(false);


        Body.velocity = strengh;
        posVelocity = strengh;

    }
    public void OnEnable_mode(bool enable)
    {

     //   transform.parent.Find("Trigeer").GetComponent<Che>
     //       transform.parent.Find("Net/Body").GetComponent<PolygonCollider2D>().enabled = enable;
        //transform.parent.parent.transform.Find("Hool/Pos2").GetComponent<CircleCollider2D>().enabled = enable;
    //    transform.parent.parent.transform.Find("Hool/Pos1").GetComponent<CircleCollider2D>().enabled = enable;

        //transform.parent.parent.transform.Find("Net/Body").GetComponent<Rigidbody2D>().simulated = enable;


    }

    public void Reset()
    {
        pos = Vector2.zero;
      pos = transform.position;
        Scale = Board.transform.localScale;
        isDone = false;
    }
    IEnumerator RestTrigger(float time)
    {
        trigger.setActive(false);
        yield return new WaitForSeconds(time);
        trigger.setActive(true);
    }
    public Vector2 DirectAndForce()
    {
        
        Vector2 force = pos - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Vector2.Distance(pos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > max)
        {
         
            Vector2 maxForce = (pos- (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized*(max);

           force = maxForce;
       
        }
        if (Vector2.SqrMagnitude(force) < max*0.3f)
        {
            if (transform.parent.parent != null)
            {

                Vector2 DirectForce = ((Vector2)transform.parent.parent.transform.position - pos);
                force = DirectForce;
            }
            else
            {
                return Vector2.zero;
            }
        }
           
        
        force *= Strenght;

      //  Debug.Log(force);

        return force;
    }
    public void changeDirect()
    {

        //      Body.velocity *= Vector2.left;
        //   Vector2 direct = Body.velocity;
        if (!isDone)
        {
            isDone = true;
            // Body.velocity = Vector2.Reflect(VectorReflect, Vector3.forward);
            Body.velocity = Vector2.Reflect(Body.velocity, Vector3.forward);
        
    }


    }
      


    /// <summary>
    ///  
    /// </summary>
    /// 
    
    public Transform[] dot;
    public int CountNode = 30;
    public void Trajector()
    {
        dot = new Transform[29];
        for(int i = 0; i < 28; i++)
        {
            string s = "Dot (" + i + ")";
          //  dot[i] = GameObject.Find("Mode_4/Dot").transform.Find(s);
           dot[i] = GameObject.Find("Mode_1/Dot").transform.Find(s);
            dot[i].gameObject.SetActive(false);
          
        }


    }
    /// <summary>
    /// /
    /// </summary>
    float ShiftDot = 2  ;
    public void SimulateTrajectory(Vector2 pos,Vector2 velocity)
    {
        enableTrajectory(true);
        float posX = pos.x;
        float posY = pos.y;
        bool isReflect = false;
        int j = 0;
        Vector2 posReflect = Vector2.zero;
        Vector2 new_vec = Vector2.zero;
        // RaycastHit2D hit;
        Vector3 Scale = new Vector3(0.5f, 0.5f, 1);
        Vector3 Scale0 = new Vector3(0.2f, 0.2f, 1);
        float dis = Vector3.Distance(Scale, Scale0);
        float space = dis / 28;
        for (int i = 0; i < 20; i++)
        {
            if (!isReflect)
            {
                float x = posX+ (velocity.x * Time.fixedDeltaTime*i*ShiftDot);
                float y = posY+ (velocity.y * Time.fixedDeltaTime*i*ShiftDot) + ((Physics.gravity.y*3 * Time.fixedDeltaTime * Time.fixedDeltaTime*i*i*ShiftDot*ShiftDot) / 2);
                Vector2 point = new Vector2(x,y);
                dot[i].gameObject.SetActive(true);
                dot[i].transform.position = point;
                if (dot[i].position.x > GameController.instance.rightBottomReal.x)
                {
               //     Debug.Log("Va Cham" + i);
                    isReflect = true;
                    posReflect = dot[i].transform.position;

                    posReflect.x = GameController.instance.rightBottomReal.x;
                    dot[i].position = posReflect;
                    new_vec = Vector2.Reflect(velocity, Vector3.forward);

                }
                else if (dot[i].position.x < GameController.instance.leftBottomReal.x)
                {
                    isReflect = true;
                    posReflect = dot[i].transform.position;

                    posReflect.x = GameController.instance.leftBottomReal.x;
                    dot[i].position = posReflect;
                    new_vec = Vector2.Reflect(velocity, Vector3.forward);

                }


            }
            else
            {
                   new_vec = Vector2.Reflect(velocity,Vector3.forward);
                new_vec -= Vector2.one * dis;

                    float x = posReflect.x - (new_vec.x * Time.fixedDeltaTime * j* ShiftDot);       
                    float y = posReflect.y + (new_vec.y * Time.fixedDeltaTime * j * ShiftDot) + ((Physics.gravity.y * 3 * Time.fixedDeltaTime * Time.fixedDeltaTime * j*j * ShiftDot * ShiftDot) / 2);
                    Vector2 point = new Vector2(x, y);
                    dot[i].gameObject.SetActive(true);
                    dot[i].transform.position = point;
                j++;
                
              




            }
            Scale = (Vector2)Scale - Vector2.one * space;

            dot[i].transform.localScale = Scale;





        }

    }
    public float diss;
    public void enableTrajectory(bool enable)
    {
       
        for (int i = 0; i < 25; i++)
        {

            string s = "Dot (" + i + ")";
         //   if(GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_3)
        //    {

       //         dot[i] = GameObject.Find("Mode_1/Dot").transform.Find(s);
               //    dot[i] = GameObject.Find("Mode_4/Dot").transform.Find(s);
         //       dot[i].gameObject.SetActive(enable);
      ////      }
      //      else
      //      {


                dot[i] = GameObject.Find("Mode_1/Dot").transform.Find(s);
                //    dot[i] = GameObject.Find("Mode_4/Dot").transform.Find(s);
                dot[i].gameObject.SetActive(enable);
        //    }
           

        }

    }
   

    public float GetStar()
    {

        return Star;
    }
   public void AddStar(int count)
    {
        Star += count;
    }
    public float GetScore()
    {
        return Score;
    }
    public void AddScore(int count)
    {
        Score += count;
    }

    public void SpawnPractice()
    {
       
        if (Body.velocity.x != 0 || Body.velocity.y != 0)
        {
            Vector3 pos = transform.position;
            pos.z = 1;
            if(GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_3)
            {


                GameObject game = Instantiate(Practice_Ball, pos, Quaternion.identity, GameObject.Find("Mode_1").transform);
            }
            else
            {
                GameObject game = Instantiate(Practice_Ball, pos, Quaternion.identity, GameObject.Find("Mode_4").transform);

            }
        
          
        

        }   





    }
    
    // 16 : HOOl
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 16)
        {
            
                  GameObject.Find("ScreenGameplay").GetComponent<ScreenGamePlay>().GameOverWindow();

            
       

        }
        if (collision.gameObject.tag == "Star")
        {
            AddStar(1);
            collision.gameObject.GetComponent<Star>().Destroy();
        }
    }
    bool isDead = false;
    public void ResetAndContine()
    {
      
    }


    /// <summary>
    /// ////////
    /// </summary>
    public int CountPerfect = 1;
    public bool isWall = false;
    public int countWall;
    public bool FullForce=false;
    public int countGolbal = 0;
    public int countFullForce = 0;
   

    public void TakeScore(Paractice_Type type)
    {
        switch (type)
        {
            case Paractice_Type.NORMAL:

                CountPerfect = 1;
                AddScore(1);
                // TestAction.instance.StartShakeCamera(1, 0.2f);
                if (FullForce)
                {
                    countFullForce++;

                }
                if (isWall)
                {
                    countWall++;

                }
                break;

            case Paractice_Type.PERFECT:
               AddScore(2 *CountPerfect);
                CountPerfect++;
                //         if (countPerfect > 2)
                //    {

                // TestAction.instance.StartBurnBall_2(10);
                //    }
                //TestAction.instance.reset = true;
                if (FullForce)
                {
                    countFullForce++;

                }
                if (isWall)
                {
                    countWall++;
                }
                break;
               

        }
        if (countWall ==2)
        {
            CountIntereableWall++;
            countWall = 0;
        }
        else
        {
            countWall = 0;
        }
      
        countGolbal++;
        FullForce = false;
       

    }
    IEnumerator SpawnNewBall()
    {


        yield return new WaitForSeconds(1);

    }
}
