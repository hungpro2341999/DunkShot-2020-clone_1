using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ball_Mode_1 : MonoBehaviour
{

    public static Ball_Mode_1 instance = null;
    public int index = 0;
    public bool isInterecable = false;
    public Vector2 PosMousePosition;
    public float Strenght = 10;
    public bool Click_1 = false;
    public bool Click_2 = false;
    public float max;
    public Vector2 pos;
    public Vector2 Scale;
    public Vector2 Scale_1;
    public Board_Rotation Board;
    public Board_Rotation Board_1;
    public Rigidbody2D Body;
    public Rotate rotate;
    public CheclInBall trigger;
    public bool isCickBall;
    public string key = "BALL_IN_NET_1";
    public Vector2 posVelocity;
    public float width;
    public float height;
    float time = 0;
    // Start is called before the first frame update

    public int GetKey()
    {
        index++;
        return index;
    }

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
    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Scale = Board.transform.localScale;
        Scale_1 = Board.transform.localScale;
        pos = transform.position;
        width = GetComponent<SpriteRenderer>().size.x;
        height = GetComponent<SpriteRenderer>().size.y;
        max = width;

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.R))
        {
           
        }

        MouseMove();

        if (gameObject.layer == 13)
        {
            Body.isKinematic = true;
        
          
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
                Board_1.Direct();
               rotate.Direct();
                Vector2 new_pos = PosMousePosition;
                Vector2 new_pos_1 = PosMousePosition;


                if (Vector2.Distance(new_pos, pos) < max && Vector2.Distance(new_pos, pos) > 0)
                {
                    float dis = Vector2.Distance(new_pos, pos);

                    Vector2 scale1 = new Vector2(Scale.x, (Scale.y + dis) * 0.5f);
                    Vector2 scale2 = new Vector2(Scale_1.x, (Scale_1.y + dis) * 0.5f);
                    Board.SetScale(scale1);
                    Board_1.SetScale(scale2);
                    transform.position = new_pos;


                }
               
              else
                {

                    new_pos_1 = pos - (2 * DirectRotation_1().normalized * max * 0.5f);

                    transform.position = new_pos_1;
                    float dis = Vector2.Distance(new_pos_1, pos);
                    Vector2 scale1 = new Vector2(Scale.x, (Scale.y + dis) * 0.5f);
                    Vector2 scale2 = new Vector2(Scale_1.x, (Scale_1.y + dis) * 0.5f);
                    Board.SetScale(scale1);
                    Board_1.SetScale(scale2);

                }
    

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
             //   gameObject.layer = 14;
                StartCoroutine(RestTrigger());

                Click_2 = false;

            }


        }
        else
        {
          //  MoveFollowPlayer.instance.moveCamera = true;

            time += 10;

        //    transform.rotation = Quaternion.AngleAxis(time, Vector3.forward);


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
        /*
       
        Body.simulated = true;
        gameObject.layer = 14;
        Body.isKinematic = false;
        transform.parent = GameObject.Find("Mode_1").transform;
        Body.velocity = strengh;
        posVelocity = strengh;
        */
    }
    public void Reset()
    {
        pos = transform.position;
        Scale = Board.transform.localScale;
    }
    IEnumerator RestTrigger()
    {
    //    trigger.setActive(false);
        yield return new WaitForSeconds(0.5f);
     //   trigger.setActive(true);
    }
    public Vector2 DirectAndForce()
    {

        Vector2 force = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) > width + max)
        {

            Vector2 maxForce = (transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized * (width + max);

            force = maxForce;

        }
        if (Vector2.SqrMagnitude(force) > 0.3f)
        {
            if (transform.parent.parent != null)
            {

                Vector2 DirectForce = (transform.parent.parent.transform.position - transform.position);
                force = DirectForce;
            }
            else
            {
                return Vector2.zero;
            }
        }


        force *= Strenght;

        Debug.Log(force);

        return force;
    }
    public void changeDirect()
    {

        //      Body.velocity *= Vector2.left;
        //   Vector2 direct = Body.velocity;
        Body.velocity = (Vector2.Reflect(Body.velocity, Vector3.forward) * 2);
    }

}
