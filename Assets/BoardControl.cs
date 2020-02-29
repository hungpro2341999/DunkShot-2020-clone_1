using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BoardControl : MonoBehaviour
{
    
    public GameObject Normal;
    public GameObject Burn_Ball;
    public GameObject Burn_Ball_Ver_2;
    public int Key = 0;
    public static BoardControl instance;
    public Transform BasketNeedHold;   //Hold postion
    public Transform PosBall;
    public Transform Ball;
    public Transform Hool;
    public Transform ImgScale;
    public Transform Trigger;
    public Transform PosOriginal;
    public Transform PosLastBall;
    public Transform HoldBasket;
    public Transform PointShoot;
    public Vector2 pos_new;
    public float speed = 1;
    //
    Vector2 posOriginalHold;
    public bool Click_1 = false;
    public bool Click_2 = false;
    public bool UpdatePoint = false;
    public bool MouseMove = false;
    public Vector2 directBall;
    public Vector2 PosMouseOriginal = Vector2.zero;   //MaxScale = 1.5f

    public Rigidbody2D Body;
    Vector2 posOriginalBall;
     Vector2 force;
    Vector2 ReflectVector;
     public float Strenght = 0.5f;

    //
    bool InfectionWall = false;
    public float Star;
    public float Score;
    public bool isDone = false;

    int CountNormal = 1;
    bool done = true;
    public bool Still10s = true;

    public int CountIntereableWall;
    public bool twoWall = false;
    bool updateMuisc = false;

    float TimeCheck = 0;
    public int CheckDead = 0;

    public ParticleSystem Eff_3;
    public ParticleSystem Eff_4;
    public ParticleSystem Eff_5;
    public void initSprite()
    {
    //    ParticleSystem.MainModule settings = Practice_Ball.transform.Find("Particle System").GetComponent<ParticleSystem>().main;
    //   GetComponent<SpriteRenderer>().sprite.texture.GetPixelBilinear(40, 40);
    //    settings.startColor = new ParticleSystem.MinMaxGradient(GetComponent<SpriteRenderer>().color);

       
    }

    public void NextKey()
    {
        Key++;
    }
    private void Awake()
    {
        if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_4)
        {

        }
        else
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
       
    }
    // Start is called before the first frame update
    void Start()
    {
        Star = GameController.instance.Get_Star();
        MODE_GAME.Continue_Mode_3 += Continue;
        Body = GetComponent<Rigidbody2D>();
        Trajector();
  
    }

    // Update is called once per frame
    void Update()
                  
    {
        Debug.Log(countWall);
        //   initSprite();
        if (!GameController.instance.GamePause && !GameController.instance.GameOver)
        {
           
          //  Time.timeScale = 2;
            GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBall();
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("GamePlay");
            }
            SpawnPractice();

            if (gameObject.layer == 13)
            {
                Body.velocity = Body.velocity*0.9f;

                if (Input.GetMouseButtonDown(0))
                {
                    if (!updateMuisc)
                    {
                        
                        AutioControl.instance.GetAudio("TouchSound").Play();
                        updateMuisc = true;
                    }
                    Click_1 = true;

                    if (!UpdatePoint)
                    {
                        PosMouseOriginal = PosMosue();
                        UpdatePoint = true;
                     
                    }

                }
                if (Click_1)
                {
                 
                    if (!MouseMove)
                    {
                        if (PosMouseOriginal != PosMosue())
                        {

                            MouseMove = true;
                        }

                    }
                    if (MouseMove)
                    {
                        
                        CheckDead = 0; 
                        enableTrajectory(true);
                        posOriginalHold = transform.parent.position;
                        BasketNeedHold.GetComponent<TargetJoint2D>().target = posOriginalHold;
                        EnableMode(true);
                        Ball.transform.position = (Vector2)PosBall.transform.position;  // Set Positonn Ball

                        Vector2 direct = PosMouseOriginal - PosMosue();
                        //    RotateForWardVector(direct, ImgScale);
                        //  RotateForWardVector(direct, Hool);
                        //   RotateForWardVector(direct, BasketNeedHold);
                        RotateForWardVector(direct, transform.parent);
                        float dis = Vector2.Distance(PosMouseOriginal, PosMosue())*speed*0.02f;
                    //    Debug.Log(dis);
                        Vector2 pos_org = (Vector2)PosOriginal.position;
                        this.directBall = direct;


                        if (dis < 0.6f)
                        {
                            Vector2 direct0 = direct.normalized;
                            Vector2 direct1 = direct0;
                            direct1.y = Mathf.Abs(direct1.y);
                            Vector2 Space = Vector2.one + Vector2.one * dis;
                            if(Space.y == 0)
                            {

                                Space = Vector2.one +  dis*Vector2.one;
                            }
                            Debug.Log(direct1);

                            Vector2 Space_Scale = Vector2.one + direct1 * dis;
                         
                            Set_Scale(ImgScale, Space);
                            Set_Scale(BasketNeedHold, Space);
                            Vector2 directBall = PosOriginal.position - PosLastBall.position;

                            pos_org -= dis * directBall.normalized;
                            PosBall.transform.position = (Vector2)pos_org;
                            direct = PosMouseOriginal - PosMosue();
                            force = dis * Strenght * direct0;

                            this.directBall = force;

                            SimulateTrajectory(Ball.transform.position, force);
                        }

                        else
                        {
                            dis = 0.6f;
                            Vector2 direct0 = direct.normalized;
                            Vector2 direct1 = direct.normalized;
                            direct1.y = Mathf.Abs(direct1.y);
                              
                            Vector2 Space = Vector2.one + Vector2.one * dis;
                            if (Space.y == 0)
                            {

                                Space = Vector2.one + dis*Vector2.one;
                            }
                            Vector2 Space_Scale = Vector2.one + direct1 * dis;
                            Set_Scale(ImgScale,Space);
                            Set_Scale(BasketNeedHold, Space);
                            Vector2 directBall = PosOriginal.position - PosLastBall.position;
                         //   Debug.Log(direct1);
                            pos_org -= dis * directBall.normalized;
                            PosBall.transform.position = (Vector2)pos_org;
                            direct = PosMouseOriginal - PosMosue();
                            force = dis * Strenght * direct0;

                            this.directBall = force;
                            FullForce = true;
                            SimulateTrajectory(Ball.transform.position, force);
                        }


                        //    Vector2 pos = PosOriginal.transform.position;
                        //   pos.y = BasketNeedHold.localScale.y * pos.y;
                        Ball.position = (Vector2)PosBall.position;

                        Vector3 a = (Vector2)PosBall.transform.position;
                        a.z = 0;
                        PosBall.transform.position = (Vector2)a;



                    }


                }
                if (Input.GetMouseButtonUp(0))
                {
                    updateMuisc = false;
                    AutioControl.instance.GetAudio("POP").Play();
                    enableTrajectory(false);
                    EnableMode(false);
                    gameObject.layer = 14;
                    transform.parent.GetComponent<DestroyMySelf>().Fire();
                    
                    transform.parent = null;
                    StartCoroutine(EnableTrigger());
                    ReflectVector = force;
                    Shoot(force);
                    Click_2 = true;
                    Click_1 = false;
                    if (time_Burn_Ball > 0)
                    {
                        if (Burn_Ver_1)
                        {
                            AutioControl.instance.GetAudio("burn2").Play();

                        }
                        else
                        {
                            AutioControl.instance.GetAudio("burn3").Play();

                        }


                    }
                    else
                    {
                        AutioControl.instance.GetAudio("BallFly").Play();
                    }
                   

                }
                if (Click_2 = true && !Click_1)
                {

                    ResetStatus();
                    Click_2 = false;
                }
            }
            else
            {

                if (TimeCheck > 0)
                {
                    TimeCheck -= Time.deltaTime;
                }
                else
                {

                    TimeCheck = 5;
                    pos_new = transform.position;

                }



                if ((Vector2)transform.position == pos_new)
                {
                    CheckDead++;
                    if (CheckDead > 3)
                    {
                        GameObject.Find("ScreenGameplay").GetComponent<ScreenGamePlay>().GameOverWindow();
                    }
                }
                else
                {
                    CheckDead = 0;
                }

                enableTrajectory(false);
            }
        }
    }
    private void LateUpdate()
    {
        //Hold Point
        if (Click_1)
        {
            // posOriginalHold = transform.parent.position;
            BasketNeedHold.GetComponent<TargetJoint2D>().target = HoldBasket.position;
        }
        Vector3 pos = transform.position;
        pos.z = 0.01f;
        transform.position = pos;
     
    }
    public Vector2 PosMosue()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void ResetStatus()
    {
    //    StartCoroutine(RestScale(ImgScale));
        StartCoroutine(RestScale(BasketNeedHold));
        force = Vector2.zero;
         MouseMove = false;
        UpdatePoint = false;
        Click_2 = false;
    }
    public void RotateForWardVector(Vector2 Direct, Transform transform)
    {
        {
            float angle = Mathf.Atan2(Direct.y, Direct.x) * Mathf.Rad2Deg;
            transform.rotation =    Quaternion.AngleAxis(angle-90, Vector3.forward);
        }

    }
    public void Set_Scale(Transform transform,Vector2 scale)
    {
        Vector2 pos = transform.localScale;
        pos.y = Mathf.Clamp(scale.y,-1.5f,1.5f);
        transform.localScale =pos;

    }
    IEnumerator RestScale(Transform transform)
    {
        if (transform.gameObject.name == "ImgScale")
        {
            while ((Vector2)transform.localScale != new Vector2(1, 0.8f))
            {
                transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(1, 0.5f), Time.deltaTime);
                yield return new WaitForSeconds(0);
            }
        }
        while ((Vector2)transform.localScale != Vector2.one)
        {
            transform.localScale = Vector2.MoveTowards(transform.localScale, Vector2.one, Time.deltaTime);
        }
        yield return new WaitForSeconds(0);
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        return RotatePointAroundPivot(point, pivot, Quaternion.Euler(angles));
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        return rotation * (point - pivot) + pivot;
    }
    private void OnDrawGizmos()
    {
    }
    public void Shoot(Vector2 shoot)
    {
        Ball.GetComponent<Rigidbody2D>().velocity = shoot;


    }
    IEnumerator EnableTrigger()
    {
        Trigger.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Trigger.gameObject.SetActive(true);
    }
    public void EnableMode(bool open)
    {
        GetComponent<Rigidbody2D>().isKinematic = open;
        GetComponent<Rigidbody2D>().simulated = !open;
    }

    ////Trạectory
      public Transform[] dot;
    public int CountNode = 30;
    public void Trajector()
    {
        dot = new Transform[29];
        for(int i = 0; i< 18; i++)
        {
            string s = "Dot (" + i + ")";
    //  dot[i] = GameObject.Find("Mode_4/Dot").transform.Find(s);
    dot[i] = GameObject.Find("Mode_1/Dot").transform.Find(s);
            dot[i].gameObject.SetActive(false);
          
        }


    }
    float ShiftDot = 4.2f;
    public void SimulateTrajectory(Vector2 pos, Vector2 velocity)
    {
        
     

        enableTrajectory(true);
        float posX = pos.x;
        float posY = pos.y;
        bool isReflect = false;
        int j = 0;
        bool left = false;
        float radius = dot[0].GetComponent<SpriteRenderer>().size.x/2.5f;
        Vector2 posReflect = Vector2.zero;
        Vector2 new_vec = Vector2.zero;
        // RaycastHit2D hit;
        Vector3 Scale = new Vector3(0.5f, 0.5f, 1);
        Vector3 Scale0 = new Vector3(0.2f, 0.2f, 1);
        float dis = Vector3.Distance(Scale, Scale0);
        
        float space=dis/13;
        for (int i = 0; i < 18; i++)
        {
            if (!isReflect)
            {
                InfectionWall = false;
                float x = posX + (velocity.x * Time.fixedDeltaTime * i * ShiftDot);
                float y = posY + (velocity.y * Time.fixedDeltaTime * i * ShiftDot) + ((Physics.gravity.y * Time.fixedDeltaTime * Time.fixedDeltaTime * i * i * ShiftDot * ShiftDot) / 2);
                Vector2 point = new Vector2(x, y);
                dot[i].gameObject.SetActive(true);
                dot[i].transform.position = point;
                if (dot[i].position.x > GameController.instance.rightBottomReal.x)
                {
                  
                    left = false;
                    //     Debug.Log("Va Cham" + i);
                    isReflect = true;
                    posReflect = dot[i].transform.position;

                    posReflect.x = GameController.instance.rightBottomReal.x-0.2f;
                    dot[i].position = posReflect;
                    new_vec =  new Vector2(-velocity.x, velocity.y)/1.5f;
                    Vector2 posChange = dot[i - 1].position;
                    posChange.x = dot[i].position.x;
                    dot[i - 1].position = posChange;

                }
                else if (dot[i].position.x < GameController.instance.leftBottomReal.x)
                {
                    left = true;
                    isReflect = true;
                    posReflect = dot[i].transform.position;

                    posReflect.x = GameController.instance.leftBottomReal.x+0.2f;
                    dot[i].position = posReflect;
                    new_vec = new Vector2(-velocity.x, velocity.y)/1.5f;
                    Vector2 posChange = dot[i - 1].position;
                    posChange.x = dot[i].position.x;
                    dot[i - 1].position = posChange;
                   
                }
                 

            }
            else
            {
                //   new_vec = Vector2.Reflect(velocity, Vector3.forward);
                //  new_vec -= Vector2.one * dis;

             
                if (left)
                {

                    float x = posReflect.x + (new_vec.x * Time.fixedDeltaTime * j * ShiftDot);
                    float y = posReflect.y + (new_vec.y * Time.fixedDeltaTime * j * ShiftDot) + ((Physics.gravity.y* Time.fixedDeltaTime * Time.fixedDeltaTime * j * j * ShiftDot * ShiftDot) / 2);
                    Vector2 point = new Vector2(x, y);
                    dot[i].gameObject.SetActive(true);
                    dot[i].transform.position = point;
                    j++;
                }
                else
                {
                    float x = posReflect.x + (new_vec.x * Time.fixedDeltaTime * j * ShiftDot);
                    float y = posReflect.y + (new_vec.y * Time.fixedDeltaTime * j * ShiftDot) + ((Physics.gravity.y* Time.fixedDeltaTime * Time.fixedDeltaTime * j * j * ShiftDot * ShiftDot) / 2);
                    Vector2 point = new Vector2(x, y);
                    dot[i].gameObject.SetActive(true);
                    dot[i].transform.position = point;
                    j++;
                }
                InfectionWall = true;




                

            }

            Scale = (Vector2)Scale - Vector2.one * space;
            dot[i].transform.localScale = Scale;
            if (i >= 12)
            {
                dot[i].gameObject.SetActive(false);
            }





        }

    }

    public void enableTrajectory(bool enable)
    {

        for (int i = 0; i < 18; i++)
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
    
    public void changeDirect()
    {

        //   Body.velocity *= Vector2.left;
        //   Vector2 direct = Body.velocity;
        if (InfectionWall)
        {
            if (!isDone)
            {
                isDone = true;
                Body.velocity = new Vector2(-ReflectVector.x, ReflectVector.y) / 1.5f;
                //   Body.AddForce(Vector2.Reflect(ReflectVector, Vector3.forward));

            }
        }
     


    }
    public float GetStar()
    {

        return GameController.instance.Get_Star();
    }
    public void AddStar(int count)
    {
     
        Star += count;
        GameController.instance.Save_Star((int)Star);
    }
    public float GetScore()
    {
        return Score;
    }
    public void AddScore(int count)
    {
        
        Score += count;
    }
    
    public float time_Burn_Ball=0;
    public float time_spawn = 0f;
    bool Burn_Ver_1 = false;
   
   
    public void SpawnPractice()
    {
        int indexball = GameController.instance.GetSortEff();
        if (Body.velocity.x != 0 || Body.velocity.y != 0)
        {

            Vector3 pos = transform.position;
            pos.z = 1;
            if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_3)
            {
                if (time_spawn > 0)
                {
                    time_spawn -= Time.deltaTime;

                   

                }
                else
                {
                    time_spawn = 0.05f;
                   if(indexball == 2)
                    {
                        time_spawn = 0;
                    }
                    if (time_Burn_Ball < 0)
                    {
                        Burn_Ver_1 = false;

                        if (indexball == 0)
                        {
                            SpawnEffect.instance.getEffect(indexball, 0, transform.position);
                            Eff_3.Stop();
                        }
                        if (indexball == 1)
                        {
                            SpawnEffect.instance.getEffect(indexball, 0, transform.position);
                            Eff_3.Stop();
                        }

                        if (indexball == 2)
                        {
                            SpawnEffect.instance.getEffect(indexball, 0, transform.position);

                            Eff_3.Stop();

                        }
                        if (indexball == 3)
                        {
                            SpawnEffect.instance.getEffect(indexball, 0, transform.position);
                            Eff_4.Stop();
                        }
                        if (indexball == 4)
                        {
                            SpawnEffect.instance.getEffect(indexball, 0, transform.position);
                            Eff_5.Stop();
                        }

                    }
                    else
                    {
                        time_Burn_Ball -= Time.deltaTime;
                        if (Burn_Ver_1)
                        {

                            if (indexball == 0)
                            {
                                SpawnEffect.instance.getEffect(indexball, 1, transform.position);
                                Eff_3.Stop();
                            }
                            if (indexball == 1)
                            {
                                SpawnEffect.instance.getEffect(indexball, 1, transform.position);
                                Eff_3.Stop();
                            }

                            if (indexball == 2)
                            {
                                SpawnEffect.instance.getEffect(indexball, 1, transform.position);
                                if (!Eff_3.isPlaying)
                                {
                                    Eff_3.Play();
                                }


                            }
                            if (indexball == 3)
                            {
                                SpawnEffect.instance.getEffect(indexball, 1, transform.position);
                                if (!Eff_4.isPlaying)
                                {
                                    Eff_4.Play();
                                }
                            }
                            if (indexball == 4)
                            {
                                SpawnEffect.instance.getEffect(indexball, 1, transform.position);
                                if (!Eff_5.isPlaying)
                                {
                                    Eff_5.Play();
                                }
                            }
                        }
                        else
                        {
                            if (indexball == 0)
                            {
                                SpawnEffect.instance.getEffect(indexball, 2, transform.position);
                                Eff_3.Stop();
                            }
                            if (indexball == 1)
                            {
                                SpawnEffect.instance.getEffect(indexball, 2, transform.position);
                                Eff_3.Stop();
                            }

                            if (indexball == 2)
                            {
                                SpawnEffect.instance.getEffect(indexball, 2, transform.position);

                                if (!Eff_3.isPlaying)
                                {
                                    Eff_3.Play();
                                }

                            }
                            if (indexball == 3)
                            {
                                SpawnEffect.instance.getEffect(indexball, 2, transform.position);
                                Eff_4.Stop();
                            }
                            if (indexball == 4)
                            {
                                SpawnEffect.instance.getEffect(indexball, 2, transform.position);
                                Eff_5.Stop();
                            }

                        }

                    }
                }
                

               
            }
            




        }





    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 16)
        {

            GameObject.Find("ScreenGameplay").GetComponent<ScreenGamePlay>().GameOverWindow();




        }
        if (collision.gameObject.tag == "Star")
        {
            GameController.instance.AddStarGolabal(1);
            AddStar(1);
            collision.gameObject.GetComponent<Star>().Destroy();
        }
    }
    public int CountPerfect = 1;
    public bool isWall = false;
    public int countWall;
    public bool FullForce = false;
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
                AutioControl.instance.GetAudio("Perfect_1").Play();
                break;

            case Paractice_Type.PERFECT:
                  AddScore(3 * CountPerfect);
                 CountPerfect++;
                           if (CountPerfect > 2)
                      {
                    Burn_Ver_1 = true;
                    time_Burn_Ball = 10;
                    if (CountPerfect > 4)
                    {
                        time_Burn_Ball = 10;
                        Burn_Ver_1 = false;
                    }
                      }
                  
                if (FullForce)
                {
                    countFullForce++;

                }
                if (isWall)
                {
                    countWall++;
                }
                if (CountPerfect > 7)
                {
                    string name = "Perfect_" + 7;
                    AutioControl.instance.GetAudio(name).Play();

                }
                else
                {
                    string name = "Perfect_" + CountPerfect;
                    AutioControl.instance.GetAudio(name).Play();
                    TestAction.instance.reset = true;
                }
                break;


        }
        if (countWall == 2)
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

        countWall = 0;
    }
  
    public void Continue()
    {
        LevelController.instance.UpdateTime = true;
        GameObject[] game = GameObject.FindGameObjectsWithTag("Board");
        for(int i = 0; i < game.Length; i++)
        {
            if(game[i].transform.Find("Basket/Trigger").GetComponent<CheckInBall>().key == Key)
            {
                game[i].GetComponent<Board_Mode_1>().Reset_Rotation();
                Vector2 pos = game[i].transform.position;
                pos.y += 3;
                BoardControl.instance.Body.velocity = Vector2.zero;
                BoardControl.instance.transform.position = pos;
                LevelController.instance.UpdateTime = true;

            }
        }

    }
}
