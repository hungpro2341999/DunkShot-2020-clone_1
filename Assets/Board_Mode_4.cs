using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Mode_4 : MonoBehaviour
{
    public Board_Mode_1 Board_3;
    public GameObject Practice_Ball;
    public Transform posStar;
    public bool isDead = false;
    public bool isTrajectory = false;
    public static Board_Mode_4 instance = null;
    public int index = 0;
    public bool isInterecable = false;
    public Vector2 PosMousePosition;
    public float Strenght = 10;
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
    float time = 0;
    public Board_Rotation Board_1;
    Vector2 VectorReflect;
    Vector2 MaxVectorReflect;
    public float Star;
    public float Score;
    public bool isDone = false;

    public void addStar(int amount)
    {
        Star += amount;
    }
    public float getStar()
    {
        return Star;
    }

    private void Awake()
    {
        GetComponent<Rigidbody2D>().simulated = false;
        Board_3 = transform.parent.parent.Find("Net").GetComponent<Board_Mode_1>();
    }

    // Start is called before the first frame update

    public void onReset()
    {  
        Player.instance.transform.position = Player.instance.Reset_pos;
        Player.instance.transform.gameObject.layer = 14;
        Player.instance.Body.isKinematic = true;
        Player.instance.Body.velocity = Vector2.zero;

        Debug.Log("REset player");

    }
    public int GetKey()
    {
        index++;
        return index;
    }

    
    void Start()
    {
        Vector2 MaxVectorReflect = VectorReflect * 5;
         
        GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBall();
        Reset_pos = transform.position;
       // transform.GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBall();
        Body = GetComponent<Rigidbody2D>();
        Scale = Board.transform.localScale;
        Scale_1 = Board_1.transform.localScale;
        pos = transform.position;
        Trajector();

    }

    // Update is called once per frame
    void Update()
    {
        //Time.timeScale = 1.4f;
        if (!GameController.instance.GameOver || !GameController.instance.GamePause)
        {



            DirectRotation();
        width = GetComponent<SpriteRenderer>().size.x;
        height = GetComponent<SpriteRenderer>().size.y;
        max = width;
            SpawnPractice();


            if (GameController.instance.leftBottomReal.y <= transform.position.y)
            {

                if (gameObject.layer == 13)
                {
                    Body.isKinematic = true;
                    MoveFollowPlayer.instance.moveCamera = false;
                    MouseMove();
                    CheckIntercalbe();
                    if (isInterecable)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            Click_1 = true;

                        }

                    }



                    if (Click_1)
                    {
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
                            float dis = Vector2.Distance(pos,new_pos);

                            Vector2 scale1 = new Vector2(Scale.x, (Scale.y + dis) * 0.5f);
                            Vector2 scale2 = new Vector2(Scale_1.x, (Scale_1.y + dis) * 0.5f);
                            Board.SetScale(scale1);
                            Board_1.SetScale(scale2);
                            transform.position = new_pos;
                        }
                        else
                        {
                            new_pos_1 = pos - (DirectRotation_1().normalized * (max));
                           
                            transform.position = new_pos_1;
                            float dis = Vector2.Distance(pos, new_pos_1);
                            Vector2 scale1 = new Vector2(Scale.x, (Scale.y + dis) * 0.5f);
                            Vector2 scale2 = new Vector2(Scale_1.x, (Scale_1.y + dis) * 0.5f);
                            Board.SetScale(scale1);
                            Board_1.SetScale(scale2);
                            //   Debug.Log(scale1 + " " + scale2);

                        }

                        

                    }
                    else
                    {
                        
                    }
                    if (Input.GetMouseButtonUp(0) && Click_1)
                    {
                        //   Debug.Log("Mosue_Up");
                        Click_2 = true;
                        Click_1 = false;
                      

                    }
                    if (Click_2 && !Click_1)
                    {


                        Board.ResetScale();
                        Board_1.ResetScale();

                        Shoot(DirectAndForce());
                        transform.position = pos;
                       

                        StartCoroutine(RestTrigger());

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
        if (gameObject.layer == 16)
        {
            RaycastHit2D ray;
            ray = Physics2D.CircleCast(transform.position, 1, new Vector2(1, 1));
          

                if (ray.collider != null)
                {
                if (ray.collider.gameObject.layer == 17)
                {
                    SpawnEffect.instance.Set_System(Paractice_Type.PUM, ray.point, "", null);

                }
                   

                }

            }
          
        }
       
    
    public void MouseMove()
    {
        PosMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);



    }
    public bool CheckIntercalbe()
    {
        if (Vector2.Distance(PosMousePosition, transform.position) < 0.5f)
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
        float angle = Mathf.Atan2(direct.y, direct.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


    }
    public void Move(Vector2 pos)
    {
        transform.position = pos;
    }

    // 13: Ball
    public void Shoot(Vector2 strengh)
    {
        gameObject.layer = 16;
       
        Body.simulated = true;
        Body.simulated = true;  
        Body.isKinematic = false;
        //   OnEnable_mode(true);
        if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_4)
        {
            Board_3.Shot = true;
          //  Board_3.SpawnBallEffect();
        }
        transform.parent = GameObject.Find("Mode_4").transform;
        // transform.parent = GameObject.Find("Mode_4").transform;

        Body.velocity = strengh;
        posVelocity = strengh;
      
    }
    public void OnEnable_mode(bool enable)
    {
        //  transform.parent.parent.transform.Find("Net/Body").GetComponent<TargetJoint2D>().enabled = enable;

        transform.parent.parent.transform.Find("Net/Body").GetComponent<Rigidbody2D>().isKinematic = !enable;
        transform.parent.parent.transform.Find("Net/Body").GetComponent<Rigidbody2D>().simulated = enable;


    }

    public void Reset()
    {
        pos = Vector2.zero;
        pos = transform.position;
        Scale = Board.transform.localScale;
        isDone = false;
    }
    IEnumerator RestTrigger()
    {
      //  trigger.setActive(false);
        yield return new WaitForSeconds(Time.deltaTime * 2);
   //     trigger.setActive(true);
    }
    public Vector2 DirectAndForce()
    {

        Vector2 force = pos - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(pos, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > width)
        {

            Vector2 maxForce = (pos - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized * (width);

            force = maxForce;

        }
        if (Vector2.SqrMagnitude(force) < 0.3f)
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
    int count = 0;
    public void changeDirect()
    {
        count++;
        if (count > 1000)
        {
            isDone = true;
        }
        //      Body.velocity *= Vector2.left;
        //   Vector2 direct = Body.velocity;
        if (!isDone)
        {
            // isDone = true;
            if (Vector2.SqrMagnitude(VectorReflect) < Vector2.SqrMagnitude(MaxVectorReflect))
            {

                VectorReflect *= Vector2.left * 2f;
            }
            else
            {
                VectorReflect *= Vector2.left;
            }
          
            Body.velocity = VectorReflect;
      

        }
        

    }

    public void Destoy()
    {
        Destroy(gameObject);
    }

    /// <summary>
    ///  
    /// </summary>
    /// 

    public Transform[] dot;
    public int CountNode = 30;
    public void Trajector()
    {
       

    }
    /// <summary>
    /// /
    /// </summary>
    float ShiftDot = 2;
    
   
   

    public void SpawnPractice()
    {

      




    }

    // 16 : HOOl

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name== "BottomBody2D")
        {
            Destoy();
        }
    }



}
