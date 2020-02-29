using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Mode_1 : MonoBehaviour

{
    public LayerMask BoardLayer;

    public BOARD_TYPE Board_type;

 
    public Transform Hool;
    

    public Transform PosStar;

    public float maxRotation = -10;

    public Transform trsRotation;

    public float amount;

    public Point[] point;

    public bool jerk = false;

    public float angle;
    public Transform Path;
    public bool OutBoard = false;

    public GameObject PointObj;

    public Vector2[] listPath;

    bool isUpdate = false;
    public bool movePath = false;

    public bool visibleStar = false;
    /// <summary>
    /// ///////////////////
    /// </summary>
    public bool isInterecable = false;
    public Vector2 PosMousePosition;


    public float max = 1;
    public Vector2 pos;
    public static Vector2 pos_location;

    public string key = "BALL_IN_NET_1";

    // Set Level
    public bool doSomeThing = true;
    public Transform Board_2;
    public Transform Board;
    public Transform pointLast;
    Quaternion rotation;
    // Start is called before the first frame update
   public  Transform BodyPos;
    Vector2 pos_1;
  



    public void MouseMove()
    {
        PosMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);



    }


    public void Direct()
    {
        float angle = Mathf.Atan2(DirectRotation().y, DirectRotation().x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);


    }

    public Vector2 DirectRotation()
    {
        return ((Vector2)transform.position - PosMousePosition);

    }

    public void SetScale(Vector2 scale)
    {


        transform.localScale = scale;
    }
    public void ResetScale()
    {

        StartCoroutine(reset());
    }
    IEnumerator reset()
    {

        while ((Vector2)transform.localScale != pos_location)
        {

            Vector2 a = Vector2.MoveTowards(transform.localScale, pos_location, Time.deltaTime*10);

            transform.localScale = a;
            yield return new WaitForSeconds(0);
        }



    }
   
  

    public void setListPoint(Vector2[] point)
    {
        listPath = point;

    }
    private void Awake()
    {


        //   ClothSphereColliderPair[] clothSphereColliderPairs = new ClothSphereColliderPair[3];

        //   clothSphereColliderPairs[0].first = GameObject.Find("Ball").GetComponent<SphereCollider>();

        //    clothSphereColliderPairs[0].second = GameObject.Find("Ball").GetComponent<SphereCollider>();

        //  transform.Find("Hoot/ball (2) 1").GetComponent<Cloth>().sphereColliders = clothSphereColliderPairs;
        pos_location = transform.localScale;

    }
    public void setTypeBoard(BOARD_TYPE type)
    {
        Board_type = type;


    }
   
    // Start is called before the first frame update
    void Start()
    {
     // transform.Find("Body").transform.position = BodyPos.position;

     // BodyPos = transform.Find("Body");
    //   pos_1 = BodyPos.transform.position;


        ///For Mode 1
        doSomeThing = true;
     //   rotation = transform.rotation;
     //   transform.Find("Body").transform.position = BodyPos.transform.position;

    //  pos = transform.position;
      
        ////////
      
        init();
        int i1 = Random.Range(0, 100);
        if (i1 > 0 && i1 < 25)
        {
            visibleStar = true;
            if (visibleStar)
            {
                if (PosStar != null)
                {
                    SpawnEffect.instance.Set_System(Paractice_Type.STAR2D, PosStar.position, "",ModeCtrl.instance.getWorldSpace());
                }
           
            }
        }
        
    }

    private void BallPlayer_Reset_Mode_1()
    {
        throw new System.NotImplementedException();
    }

    public void generatePoint()
    {

        if (Board_type != BOARD_TYPE.NORMAL)
        {
            UpdatePoint();

            initPoint();

        }
    }

    public void init()
    {
      
        switch (Board_type)
        {
            case BOARD_TYPE.NORMAL:


                break;

            case BOARD_TYPE.ROTAION:
                if (doSomeThing)
                {
                    RotationBoard(30, 0);
                    doSomeThing = false;
                }

                break;
            case BOARD_TYPE.TOP_DOWN:
                movePath = true;

                SetPathGoDown(transform.position.x, transform.position.y, this.gameObject, 3);
                generatePoint();

                break;

            case BOARD_TYPE.LEFT_RIGHT:
                setJerk();
                SetPathRightLeft(transform.position.x, transform.position.y, this.gameObject, 3);
                generatePoint();
                movePath = true;
                break;


            case BOARD_TYPE.JERK_TOP_DOWN:
                setJerk();
                SetPathGoDown(transform.position.x, transform.position.y, this.gameObject, 3);
                generatePoint();
                movePath = true;
                
                break;

            case BOARD_TYPE.JERK_LEFT_RIGHT:

                movePath = true;
                SetPathRightLeft(transform.position.x, transform.position.y, this.gameObject, 3);
                generatePoint();

                break;
            case BOARD_TYPE.JERK_RIGHT_LEFT_CROSS:
                setJerk();
                movePath = true;
                RotationBoard(30, 0);
                SetPathRightLeft(transform.position.x, transform.position.y, this.gameObject, 3);
                generatePoint();
               
                return;
            case BOARD_TYPE.JERK_TOP_DOWN_CROSS:
                setJerk();
                movePath = true;
                RotationBoard(30, 0);
                SetPathGoDown(transform.position.x, transform.position.y, this.gameObject, 3);
                generatePoint();
                

                return;

            default:

                // Not Do SomeThing
                return;

        }


    }

    
    public void UpdatePoint()
    {
        if (listPath != null)
        {

            if (!isUpdate)
            {
                if (listPath.Length > 1)
                {

                //    Debug.Log("Set Path");
                    point = new Point[listPath.Length];

                    for (int i = 0; i < point.Length; i++)
                    {
                        var p = PointObj;




                        p.GetComponent<Point>().pos = listPath[i];

                        GameObject g = Instantiate(p, Path);
                        g.transform.position = listPath[i];


                        point[i] = g.GetComponent<Point>();
                        if (jerk == true)
                        {
                            point[i].isDelay = true;
                            point[i].delayTime = timeDelay;


                        }

                    }

                }
                isUpdate = true;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameController.instance.GameOver && !GameController.instance.GamePause)
        {
         
          //  transform.Find("Body").transform.position = pos;

   
            
            //   BodyPos.position = 
            //   Debug.Log(GameController.instance.leftbottom + " " + GameController.instance.leftup);
            MouseMove();

            if (movePath)
            {

                MoveFlowPath(1);
            }
            

        }



    }
    private void LateUpdate()
    {
        /*
        if (!GameController.instance.GameOver && !GameController.instance.GamePause)
        {
            Vector2 pos = transform.Find("Body").transform.position;
            pos.y = Mathf.Clamp(pos.y, -999999, BodyPos.position.y);
            transform.Find("Body").GetComponent<TargetJoint2D>().target = BodyPos.position;
            transform.Find("Body").GetComponent<TargetJoint2D>().target = BodyPos.transform.position;
            transform.Find("Body").transform.position = pos;
        }
        */
    }

    public void Destroy()
    {

        Destroy(gameObject);

    }

    public void SpawnPefect()
    {
       
    }



    public void Rotation()
    {
    }
    public bool RotationLeft = false;
    public void RotationBoard(float angle, int moveToWard)
    {

        if (RotationLeft)
        {
           

            transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);
        }
        else
        {


            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
    }

    /// <summary>
    /// Set Path For Board
    /// </summary>
    public float time = 0;
    public int speed = 1;
  
    public float timeDelay =1f;

    public int index = 0;
    public int inDexLast;
    public Vector2 destinationVec;


    public void MoveFlowPath(float amount)
    {

        switch (Board_type)
        {

            case BOARD_TYPE.LEFT_RIGHT:
                if (doSomeThing)
                    Move(speed);
                break;

            case BOARD_TYPE.JERK_LEFT_RIGHT:
                if (doSomeThing)
                    Move(speed);
                break;
            case BOARD_TYPE.TOP_DOWN:
                if (doSomeThing)
                    Move(speed);
                break;
            case BOARD_TYPE.JERK_TOP_DOWN:

                if (doSomeThing)
                    Move(speed);


                break;
            case BOARD_TYPE.JERK_TOP_DOWN_CROSS:
                if (doSomeThing)
                    Move(speed);
                break;
            case BOARD_TYPE.JERK_RIGHT_LEFT_CROSS:
                if (doSomeThing)
                    Move(speed);
                break;

        }


    }

    public void Move(float speed)
    {

        if (!isNext(transform.position, point[inDexLast]))
        {


            transform.position = Vector2.MoveTowards(transform.position, point[inDexLast].pos, Time.deltaTime * speed);

        }
        else
        {


            moveNext();
        }


    }

    void initPoint()
    {
        if (point.Length != 1)
        {


            index = 0;
            inDexLast = index + 1;


            transform.position = point[index].pos;


        }
    }

    bool check = false;

    void moveNext()
    {
        index = inDexLast;

        inDexLast = index + 1;

        if (inDexLast >= point.Length)
        {

            inDexLast = 0;

        }
    }

    bool isNext(Vector2 a, Point b)
    {
        if (Vector2.Distance(a, b.pos) < 0.2f)
        {

            if (b.isDelay)
            {
               Debug.Log("DELAY ::: ");

                StartCoroutine(DelayTime(point[index].delayTime));
            }

            return true;
        }
        else
        {
            return false;
        }
    }


    IEnumerator DelayTime(float time)
    {
        doSomeThing = false;

        yield return new WaitForSeconds(time);

        doSomeThing = true;
    }
    public float speedOutBoard;



  

    public void Out_Board()
    {


    }

    public void SetPathGoDown(float PosX, float PosY, GameObject board, int distance)
    {
        int i = Random.Range(0, 1);
        Vector2 point = new Vector2(PosX, PosY);


        if (i == 0)
        {
            Vector2 point_1 = new Vector2();
            point_1 = new Vector2(PosX, (PosY + distance));

            Vector2[] points = { point, point_1 };
            board.GetComponent<Board_Mode_1>().setListPoint(points);
        }
        else
        {
            Vector2 point_4 = new Vector2();
            point_4 = new Vector2(PosX, (PosY - distance));

            Vector2[] points_4 = { point, point_4 };
            board.GetComponent<Board_Mode_1>().setListPoint(points_4);



        }
        if (PosY + distance > Camera.main.ScreenToWorldPoint(GameController.instance.leftUpRead).y)
        {
            Vector2 point_2 = new Vector2();


            point_2 = new Vector2(PosX, (PosY - distance));

            Vector2[] points_1 = { point, point_2 };
            board.GetComponent<Board_Mode_1>().setListPoint(points_1);

        }
        if (PosY - distance <= Camera.main.ScreenToWorldPoint(GameController.instance.leftBottomReal).y)
        {
            Vector2 point_3 = new Vector2();


            point_3 = new Vector2(PosX, (PosY + distance));
            Vector2[] points_1 = { point, point_3 };
            board.GetComponent<Board>().setListPoint(points_1);

        }




    }
    public void SetPathRightLeft(float PosX, float PosY, GameObject board, int distance)
    {
        if (RotationLeft)
        {
            Vector2 point = new Vector2(PosX, PosY);

            Vector2 point_1 = new Vector2();

            point_1 = new Vector2(PosX + distance, PosY);

            Vector2[] points = { point, point_1 };
            board.GetComponent<Board_Mode_1>().setListPoint(points);

        }
        else
        {
            Vector2 point = new Vector2(PosX, PosY);

            Vector2 point_1 = new Vector2();

            point_1 = new Vector2(PosX - distance, PosY);

            Vector2[] points = { point, point_1 };
            board.GetComponent<Board_Mode_1>().setListPoint(points);


        }



    }
    public void setJerk()
    {
        jerk = true;
    }
    

    public void  NetHaveBall()
    {
        BoardLayer = 14;

    }

    public void TakeBall()
    {
        Player.instance.transform.gameObject.layer = 14;
        Player.instance.transform.parent = transform.parent.transform.Find("PointNoramal").transform;
        Player.instance.transform.localPosition = Vector3.zero;
      
    }
    public GameObject Ball;
    public Transform trans;
    public bool Shot;
    public int count = 30;
    

    public void Reset_Rotation()
    {
        StartCoroutine(ReRotation());
        if (Shot)
        {
            StartCoroutine(SpawnBall(1));
        }
            Shot = false;
        }
    

    IEnumerator SpawnBall(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Transform trs = transform.parent;
            yield return new WaitForSeconds(1.5f);
            var a = Instantiate(Ball, trans.position, Quaternion.identity, trans);
            a.GetComponent<Board_Mode_4>().Board = trs.Find("Net").GetComponent<Board_Mode_1>();
            a.GetComponent<Board_Mode_4>().rotate = trs.Find("Hool").GetComponent<Rotate>();
            a.GetComponent<Board_Mode_4>().Board_1 = trs.Find("Img").GetComponent<Board_Rotation>();
            a.gameObject.layer = 13;
        }
    }
    public void StartNewBall()
    {

    }
    IEnumerator ReRotation()
    {
        yield return new WaitForSeconds(0.5f);
       
      
     
        float angle = transform.rotation.z;
     
        while (angle != 0)
        {
            angle = Mathf.MoveTowards(angle, 0, Time.deltaTime);
          
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            yield return new WaitForSeconds(0);
        }
       
    }

}


   
