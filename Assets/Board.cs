using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Board : MonoBehaviour
{
    public SpriteRenderer bas1;
    public SpriteRenderer bas2;
    public BOARD_TYPE Board_type;

    public GameObject PerfectPractice;

    public GameObject Star;

    public Transform PosStar;

    public float maxRotation = -10;

    public Transform trsRotation;

    public float amount;

    public Point[] point;


    public float angle;
    public Transform Path;
    public bool OutBoard = false;

    public GameObject PointObj;

    public Vector2[] listPath;

    bool isUpdate = false;
    public bool movePath = false;

    public bool visibleStar = false;

   
    private void Destroy_Board()
    {
        Destroy(gameObject);
    }

    public void setListPoint(Vector2[] point)
    {
        listPath = point;

    }
    private void Awake()
    {
     







    }
    public void setTypeBoard(BOARD_TYPE type)
    {
        Board_type = type;
       

    }
    public void Board_On_Rest()
    {
        Destroy_Board();
    }
    // Start is called before the first frame update
    void Start()
    {



        Sprite[] bas = GameController.instance.SpriteBas();

        bas1.sprite = bas[1];
        bas2.sprite = bas[0];


        if (gameObject.tag == "BoardLeft")
            {
                transform.position = new Vector2(Camera.main.ScreenToWorldPoint(GameController.instance.leftbottom).x,transform.position.y);


            }
            else
            {
                transform.position = new Vector2(Camera.main.ScreenToWorldPoint(GameController.instance.rightbottom).x, transform.position.y);
            }
            
        switch (GameController.instance.Game_Type)
        {
            case GAME_MODE_TYPE.MODE_1:
           
                break;
                
            case GAME_MODE_TYPE.MODE_2:
                break;
        }

        init();
        int i1 = Random.Range(0, 100);
        if (i1 > 0 && i1 < 25)
        {
            visibleStar = true;
            if (visibleStar)
            {
                SpawnEffect.instance.Set_System(Paractice_Type.STAR, PosStar.position, "", null);

            }
        }

        if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_1)
        {
            ClothSphereColliderPair[] clothSphereColliderPairs = new ClothSphereColliderPair[3];

            clothSphereColliderPairs[0].first = GameObject.Find("Ball").GetComponent<SphereCollider>();

            clothSphereColliderPairs[0].second = GameObject.Find("Ball").GetComponent<SphereCollider>();

            transform.Find("Hoot/Net").GetComponent<Cloth>().sphereColliders = clothSphereColliderPairs;

        }
        else if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_2)
        {
            Transform[] trs = GameObject.Find("Mode_3/GunShootBall/PositonBullet").transform.GetComponentsInChildren<Transform>();
            ClothSphereColliderPair[] clothSphereColliderPairs = new ClothSphereColliderPair[trs.Length];
            for (int i = 0; i < trs.Length; i++)
            {
              

                clothSphereColliderPairs[i].first = trs[i].GetComponent<SphereCollider>();

                clothSphereColliderPairs[i].second = trs[i].GetComponent<SphereCollider>();



            }
            transform.Find("Hoot/Net").GetComponent<Cloth>().sphereColliders = clothSphereColliderPairs;


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

                SetPathRightLeft(transform.position.x, transform.position.y, this.gameObject, 3);
                generatePoint();
                movePath = true;
                break;


            case BOARD_TYPE.JERK_TOP_DOWN:

                SetPathGoDown(transform.position.x, transform.position.y, this.gameObject, 3);
                generatePoint();
                movePath = true;
                speed = 2;
                break;

            case BOARD_TYPE.JERK_LEFT_RIGHT:

                movePath = true;
                SetPathRightLeft(transform.position.x, transform.position.y, this.gameObject, 3);
                generatePoint();

                break;
            case BOARD_TYPE.JERK_RIGHT_LEFT_CROSS:

                movePath = true;
                RotationBoard(30, 0);
                SetPathRightLeft(transform.position.x, transform.position.y, this.gameObject, 3);
                generatePoint();
                speed = 2;
                return;
            case BOARD_TYPE.JERK_TOP_DOWN_CROSS:

                movePath = true;
                RotationBoard(30, 0);
                SetPathGoDown(transform.position.x, transform.position.y, this.gameObject, 3);
                generatePoint();
                speed = 2;

                return;

            default:

                // Not Do SomeThing
                return;

        }


    }

    public bool jerk = false;
    public void UpdatePoint()
    {

        if (listPath != null)
        {

            if (!isUpdate)
            {
                if (listPath.Length > 1)
                {

              //      Debug.Log("Set Path");
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

        //   Debug.Log(GameController.instance.leftbottom + " " + GameController.instance.leftup);

        if (!OutBoard)
        {
            if (movePath)
            {

                MoveFlowPath(1);
            }



        }
        else
        {
           
            Out_Board();
            StartCoroutine(Destroy_Board(1));
        }


        Rotation();



    }

    public void Destroy()
    {

        Destroy(gameObject);

    }

    public void SpawnPefect()
    {
        //Transform trs = transform.Find("Paractice");

        //  Vector2 position = trs.position;

        //  Instantiate(PerfectPractice, position, Quaternion.identity, null);


    }



    public void Rotation()
    {
        switch (GameController.instance.Game_Type)
        {
            case GAME_MODE_TYPE.MODE_1:
                if (gameObject.tag == "BoardLeftOut")
                {
                    
                    Vector2 rotation = transform.Find("3").position-trsRotation.position; 
                    float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                    trsRotation.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                }
                else if


                (gameObject.tag == "BoardLeft")
                {
                    Vector2 rotation = transform.Find("3").position - trsRotation.position;
                    float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                    trsRotation.rotation = Quaternion.AngleAxis(angle, Vector3.forward);






                }
                else if (gameObject.tag == "BoardRight")
                {
                    Vector2 rotation = trsRotation.position - transform.Find("3").position;
                    float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                    trsRotation.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                }
                else if (gameObject.tag == "BoardRightOut")
                {
                        Vector2 rotation = trsRotation.position- transform.Find("3").position;
                         float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                      trsRotation.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                break;
            case GAME_MODE_TYPE.MODE_2:
                if (gameObject.tag == "BoardLeftOut")
                {
                    Vector2 rotation = transform.Find("3").position - trsRotation.position;
                    float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                    trsRotation.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                }
                else if


                (gameObject.tag == "BoardLeft")
                {
                    Vector2 rotation = transform.Find("3").position - trsRotation.position;
                    float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                    trsRotation.rotation = Quaternion.AngleAxis(angle, Vector3.forward);






                }
                else if (gameObject.tag == "BoardRight")
                {
                    Vector2 rotation = trsRotation.position - transform.Find("3").position;
                    float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                    trsRotation.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                }
                else if (gameObject.tag == "BoardRightOut")
                {
                    Vector2 rotation = trsRotation.position - transform.Find("3").position;
                    float angle = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                    trsRotation.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                }
                break;


        }

       


    }
    public void RotationBoard(float angle, int moveToWard)
    {

        if (gameObject.tag == "BoardRight")
        {
            Vector3 pos = transform.position;
            pos.x += moveToWard;

            transform.position = pos;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Vector3 pos = transform.position;
            pos.x -= moveToWard;

            transform.position = pos;

            transform.rotation = Quaternion.AngleAxis(-angle, Vector3.forward);

        }
    }


    public float time = 0;
    public int speed = 1;
    public bool doSomeThing = true;
    public float timeDelay = 0.75f;

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
                    Move(speed*1.75f);


                break;
            case BOARD_TYPE.JERK_TOP_DOWN_CROSS:
                if (doSomeThing)
                    Move(speed*1.75f);
                break;
            case BOARD_TYPE.JERK_RIGHT_LEFT_CROSS:
                if (doSomeThing)
                    Move(speed*1.75f);
                jerk = true;
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
                Debug.Log("DELAY ::: "+ point[index].delayTime);

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

        yield return new WaitForSeconds(0.75f);

        doSomeThing = true;
    }
    public float speedOutBoard;



    IEnumerator Destroy_Board(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public void Out_Board()
    {
      
           
                if (gameObject.tag == "BoardRightOut")
                {
                    Vector3 pos = transform.position;

                    pos.x += Time.deltaTime * 3;

                    transform.position = pos;
                }
                else if (gameObject.tag == "BoardLeftOut")
                {

                    Vector3 pos = transform.position;

                    pos.x -= Time.deltaTime * 3;

                    transform.position = pos;




                }
           


        
    }

    public void SetPathGoDown(float PosX, float PosY, GameObject board, float distance)
    {
        int i = Random.Range(0, 1);
        Vector2 point = new Vector2(PosX, PosY);
          distance = GameController.instance.heightReal*0.3f;


        if (i == 0)
        {
            Vector2 point_1 = new Vector2();
            point_1 = new Vector2(PosX, (PosY + distance));
            
            Vector2[] points = { point, point_1 };
            board.GetComponent<Board>().setListPoint(points);
        }
        else
        {
            Vector2 point_4 = new Vector2();
            point_4 = new Vector2(PosX, (PosY - distance));

            Vector2[] points_4 = { point, point_4 };
            board.GetComponent<Board>().setListPoint(points_4);



        }
        if (PosY + distance > GameController.instance.leftUpRead.y)
        {
            Vector2 point_2 = new Vector2();


            point_2 = new Vector2(PosX, (PosY - distance));

            Vector2[] points_1 = { point, point_2 };
            board.GetComponent<Board>().setListPoint(points_1);

        }
        if (PosY - distance <= GameController.instance.leftBottomReal.y)
        {
            Vector2 point_3 = new Vector2();


            point_3 = new Vector2(PosX, (PosY + distance));
            Vector2[] points_1 = { point, point_3 };
            board.GetComponent<Board>().setListPoint(points_1);

        }




    }
    public void SetPathRightLeft(float PosX, float PosY, GameObject board,float distance)
    {

        distance = GameController.instance.widthReal * 0.3f;
        if (board.tag == "BoardLeft")
        {
            Vector2 point = new Vector2(PosX, PosY);

            Vector2 point_1 = new Vector2();

            point_1 = new Vector2(PosX + distance, PosY);

            Vector2[] points = { point, point_1 };
            board.GetComponent<Board>().setListPoint(points);

        }
        else
        {
            Vector2 point = new Vector2(PosX, PosY);

            Vector2 point_1 = new Vector2();

            point_1 = new Vector2(PosX - distance, PosY);

            Vector2[] points = { point, point_1 };
            board.GetComponent<Board>().setListPoint(points);


        }



    }
    public void Spawn_Board_Mode_2(bool isLeft)
    { bool done = false;
        while (1==1)
        {

            if (isLeft)
            {
                float x = GameController.instance.leftbottom.x;
                float y = Random.Range(GameController.instance.leftBottomReal.y, GameController.instance.leftUpRead.y);
                int count = Random.Range(1, 3);
                for(int i = 0; i < count; i++)
                {


                }

            }
            else
            {



            }
        }

    }
}



   




    



  




    


