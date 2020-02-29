using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Effect : MonoBehaviour
{
    public GameObject Practice_Ball;
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

    public float Star;
    public float Score;
    public bool isDone = false;

    int CountNormal = 1;
    bool done = true;
    public bool Still10s = true;

    public int CountIntereableWall;
    public bool twoWall = false;



    public void NextKey()
    {
        Key++;
    }
    private void Awake()
    {
        if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_4)
        {

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
        GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBall();
        if (Input.GetKeyDown(KeyCode.R))
        {
           
        }
        SpawnPractice();

        if (gameObject.layer == 13)
        {

            Body.velocity = Body.velocity * 0.8f;
            if (Input.GetMouseButtonDown(0))
            {

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
                    float dis = Vector2.Distance(PosMouseOriginal, PosMosue());
                    Vector2 pos_org = (Vector2)PosOriginal.position;
                    this.directBall = direct;


                    if (dis < 0.6f && dis > -0.6f)
                    {
                        Vector2 direct0 = direct.normalized;
                        Vector2 direct1 = direct.normalized;
                        direct1.y = Mathf.Abs(direct1.y);
                        Vector2 Space = Vector2.one + direct1 * dis;
                        Vector2 Space_Scale = Vector2.one + direct1 * dis;
                        Set_Scale(ImgScale, Space_Scale);
                        Set_Scale(BasketNeedHold, Space_Scale);
                        Vector2 directBall = PosOriginal.position - PosLastBall.position;

                        pos_org -= dis * directBall.normalized;
                        PosBall.transform.position = (Vector2)pos_org;
                        direct = PosMouseOriginal - PosMosue();
                        force = dis * Strenght * direct0;

                        this.directBall = force;

                     //   SimulateTrajectory(Ball.transform.position, force);
                    }

                    else
                    {
                        dis = 0.6f;
                        Vector2 direct0 = direct.normalized;
                        Vector2 direct1 = direct.normalized;
                        direct1.y = Mathf.Abs(direct1.y);
                        //     direct0.y = Mathf.Abs(direct0.y);
                        Vector2 Space = Vector2.one + direct0 * dis;
                        Vector2 Space_Scale = Vector2.one + direct1 * dis;
                        Set_Scale(ImgScale, Space_Scale);
                        Set_Scale(BasketNeedHold, Space_Scale);
                        Vector2 directBall = PosOriginal.position - PosLastBall.position;

                        pos_org -= dis * directBall.normalized;
                        PosBall.transform.position = (Vector2)pos_org;
                        direct = PosMouseOriginal - PosMosue();
                        force = dis * Strenght * direct0;

                        this.directBall = force;
                        FullForce = true;
                     //   SimulateTrajectory(Ball.transform.position, force);
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
              transform.parent.GetComponent<Reward_Game>().rerotation();
                enableTrajectory(false);
                EnableMode(false);
                gameObject.layer = 14;
                transform.parent = null;
                StartCoroutine(EnableTrigger());
                ReflectVector = force;
                Shoot(force);
                Click_2 = true;
                Click_1 = false;
            }
            if (Click_2 = true && !Click_1)
            {

                ResetStatus();
                Click_2 = false;
            }
        }
        else
        {
            enableTrajectory(false);
        }
        if (gameObject.layer == 14)
        {
            RaycastHit2D ray;
            ray = Physics2D.CircleCast(transform.position, 1, new Vector2(1, 1));


            if (ray.collider != null)
            {

                if (ray.collider.gameObject.layer == 17)
                {
                    if (!outRange)
                    {
                        SpawnEffect.instance.Set_System(Paractice_Type.PUM, ray.point, "", null);
                        outRange = true;
                    }
                  

                }
                else
                {

                    outRange = false;
                }


            }
            else
            {
               
            }

        }
    }
    bool outRange = false;
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

        MouseMove = false;
        UpdatePoint = false;
        Click_2 = false;
    }
    public void RotateForWardVector(Vector2 Direct, Transform transform)
    {
        {
            float angle = Mathf.Atan2(Direct.y, Direct.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

    }
    public void Set_Scale(Transform transform, Vector2 scale)
    {
        Vector2 pos = transform.localScale;
        pos.y = Mathf.Clamp(scale.y, -1.5f, 1.5f);
        transform.localScale = pos;

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
        for (int i = 0; i < 28; i++)
        {
            string s = "Dot (" + i + ")";
            //  dot[i] = GameObject.Find("Mode_4/Dot").transform.Find(s);
            dot[i] = GameObject.Find("Mode_4/Dot").transform.Find(s);
            dot[i].gameObject.SetActive(false);

        }


    }
    float ShiftDot = 3;
    public void SimulateTrajectory(Vector2 pos, Vector2 velocity)
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
                float x = posX + (velocity.x * Time.fixedDeltaTime * i * ShiftDot);
                float y = posY + (velocity.y * Time.fixedDeltaTime * i * ShiftDot) + ((Physics.gravity.y * Time.fixedDeltaTime * Time.fixedDeltaTime * i * i * ShiftDot * ShiftDot) / 2);
                Vector2 point = new Vector2(x, y);
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
                new_vec = Vector2.Reflect(velocity, Vector3.forward);
                new_vec -= Vector2.one * dis;

                float x = posReflect.x - (new_vec.x * Time.fixedDeltaTime * j * ShiftDot);
                float y = posReflect.y + (new_vec.y * Time.fixedDeltaTime * j * ShiftDot) + ((Physics.gravity.y * Time.fixedDeltaTime * Time.fixedDeltaTime * j * j * ShiftDot * ShiftDot) / 2);
                Vector2 point = new Vector2(x, y);
                dot[i].gameObject.SetActive(true);
                dot[i].transform.position = point;
                j++;






            }
            Scale = (Vector2)Scale - Vector2.one * space;

            dot[i].transform.localScale = Scale;





        }

    }

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


            dot[i] = GameObject.Find("Mode_4/Dot").transform.Find(s);
            //    dot[i] = GameObject.Find("Mode_4/Dot").transform.Find(s);
            dot[i].gameObject.SetActive(enable);
            //    }


        }

    }
    float incre=0.5f;
    public void changeDirect()
    {

        //      Body.velocity *= Vector2.left;
        //   Vector2 direct = Body.velocity;
        Vector2 vec = ReflectVector;

     
        vec.x = -vec.x;
        incre += 0.5f;
        if (Mathf.Sign(vec.x)==1)
        {
         

        }
        else
        {
           
           
           
        }
        if (incre < 2.5f)
        {
          
        }


        ReflectVector = vec;
        ReflectVector.y = Mathf.Clamp(incre,0,5f);
        // Body.velocity = Vector2.Reflect(VectorReflect, Vector3.forward);
        Body.velocity = ReflectVector;

       


    }
    public void changeDirectX()
    {

        //      Body.velocity *= Vector2.left;
        //   Vector2 direct = Body.velocity;

        incre += 1.5f;
        Vector2 vec = ReflectVector;
        if (incre < 20f)
        {
            vec.y += 5f;
        }
        vec.y = -vec.y;


        // Body.velocity = Vector2.Reflect(VectorReflect, Vector3.forward);
        Body.velocity = vec;




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
            if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_3)
            {


                GameObject game = Instantiate(Practice_Ball, pos, Quaternion.identity, GameObject.Find("Mode_1").transform);
            }
            else
            {
                GameObject game = Instantiate(Practice_Ball, pos, Quaternion.identity, GameObject.Find("Mode_4").transform);

            }




        }





    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_3)
        {
            if (collision.gameObject.layer == 16)
            {

                GameObject.Find("ScreenGameplay").GetComponent<ScreenGamePlay>().GameOverWindow();




            }
        }
        else
        {
            if (collision.gameObject.layer == 16)
            {
                var a = GameObject.FindGameObjectWithTag("Board");
                Vector2 pos = a.transform.position;
                pos.y += GameController.instance.heightReal*0.05f;
                var b =  Instantiate(gameObject, pos, Quaternion.identity, GameObject.Find("Mode_4").transform);
                b.layer = 14;
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "Star")
        {
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
                break;

            case Paractice_Type.PERFECT:
                AddScore(2 * CountPerfect);
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


    }

    public void Continue()
    {
        LevelController.instance.UpdateTime = true;
        GameObject[] game = GameObject.FindGameObjectsWithTag("Board");
        for (int i = 0; i < game.Length; i++)
        {
            if (game[i].transform.Find("Basket/Trigger").GetComponent<CheckInBall>().key == Key)
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
