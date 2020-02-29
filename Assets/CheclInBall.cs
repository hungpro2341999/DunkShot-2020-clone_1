using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class CheclInBall : MonoBehaviour
{
    
    public int key_board = 0;
    public Transform PointBall;
    public Transform PointLast;
    public Transform PointNormal;
    public float Speed =3f;
    public bool isPerfect = false;
    public Transform Board;
    
    public Transform Hool;
    Vector2 local_scale;
    public Transform PointEffect;
    public Dictionary<string,int> Check_Ball = new Dictionary<string,int>();
    //  public TakeBall TakeBall;
    // Start is called before the first frame update
    public const string point0 = "POINT_5";
    public const string point1 = "POINT_6";

    public bool Result()
    {
        if(Check_Ball[point1]==1 || Check_Ball[point0] == 1)
        {
            return false;
        }
        return true;

    }
    public void ResetKey()
    {
        Check_Ball[point0] = 0;
        Check_Ball[point1] = 0;

    }
    public void Init_Key()
    {
        Check_Ball.Add(point0, 0);
        Check_Ball.Add(point1, 0);



    }

    void Start()
    {
        Init_Key();
        local_scale = Board.transform.localScale;
    }
    public int getKey()
    {
        key_board = Player.instance.index; 
        return Player.instance.index;

    }
    public int getNextKey()
    {
        key_board = Player.instance.index + 1;

        return key_board;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GamePlay");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {


            Vector2 direct = collision.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;

            // collision.gameObject.transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //    collision.gameObject.transform.GetComponent<Rigidbody2D>().isKinematic = true;

            //  //   StartCoroutine(ReSize(collision.transform, 1, -direct));
            //    // StartCoroutine(ReRotation());
            //   collision.gameObject.transform.GetComponent<Rigidbody2D>().simulated = false;

            StartCoroutine(ReSize(collision.transform));
        }
    }
   
    IEnumerator ReRotation()
    {
        float angle_hool = Hool.transform.rotation.z;
        float angle = Board.transform.rotation.z;
        while (angle != 0 && angle_hool!=0)
        {
            angle_hool = Mathf.MoveTowards(angle_hool, 0, Time.deltaTime * Speed);
            angle = Mathf.MoveTowards(angle, 0, Time.deltaTime * Speed);
            transform.parent.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Hool.rotation = Quaternion.AngleAxis(angle_hool, Vector3.forward);
            yield return new WaitForSeconds(0);
        }
    }
    IEnumerator ReSize(Transform transform_1)
    {
        /*
          Vector2 pos = transform_1.position;
          Vector2 directPoint = PointLast.transform.position;
          Vector2 directNormal = PointNormal.transform.position;

          if (Vector2.SqrMagnitude(transform_1.gameObject.GetComponent<Rigidbody2D>().velocity) > 0f)
          {
              while ((Vector2)transform_1.position != directPoint)
              {
                  transform_1.position = Vector2.MoveTowards(transform_1.position, directPoint, Time.deltaTime * Speed);
                  //  Debug.Log(transform_1.position+" ///"+" "+directPoint +"//"+ directPoint_1);
                  float dis = Vector2.Distance(pos, transform_1.position) * 0.2f;
                  Vector2 scale = local_scale;
                  scale.y += dis;
                  Board.transform.localScale = scale;



                  yield return new WaitForSeconds(0);

              }



              while ((Vector2)transform_1.position != directNormal)
              {
                  transform_1.position = Vector2.MoveTowards(transform_1.position, directNormal, Time.deltaTime * Speed);
                  //  Debug.Log(transform_1.position+" ///"+" "+directPoint +"//"+ directPoint_1);
                  float dis = Vector2.Distance(pos, transform_1.position) * 0.3f;
                  Vector2 scale = local_scale;
                  scale.y += dis;
                  Board.transform.localScale = scale;
                  yield return new WaitForSeconds(0);

              }

          }
          
        transform_1.GetComponent<Rigidbody2D>().simulated = false;
        transform_1.gameObject.layer = 13;
        Board.transform.localScale = local_scale;
        transform_1.gameObject.transform.parent = PointBall;
        transform_1.gameObject.transform.localPosition = Vector3.zero;

        transform_1.GetComponent<Player>().Board = transform.parent.parent.Find("Net").GetComponent<Board_Mode_1>();
        transform_1.GetComponent<Player>().rotate = transform.parent.parent.Find("Hool").GetComponent<Rotate>();
        transform_1.GetComponent<Player>().trigger = transform.GetComponent<CheclInBall>();
        transform_1.gameObject.transform.GetComponent<Rigidbody2D>().isKinematic = true;
        transform_1.GetComponent<Player>().Reset();

        //  gameObject.SetActive(false);
        if (key_board != Player.instance.index)
        {
            Debug.Log("SPAWN BOARD");
            Player.instance.index = key_board;
            SpawnerCtrl.instante.chanceBoard = true;
            MoveFollowPlayer.instance.new_Collider_Camera();
        }
         */

        yield return new WaitForSeconds(0f);
        StartCoroutine(StarMode_BallInNet(transform_1));

    }
    IEnumerator StarMode_BallInNet(Transform transform_1)
    {
        Debug.Log(transform.parent.parent.name);
        Debug.Log("Cham");  
        yield return new WaitForSeconds(0.6f);
 
        transform_1.gameObject.layer = 13;
       Board.transform.localScale = local_scale;
        transform_1.gameObject.transform.parent = PointBall;
       transform_1.gameObject.transform.localPosition = Vector3.zero;
       
      while (transform_1.gameObject.transform.localPosition!=Vector3.zero)
      {
         transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero,Time.deltaTime);
       yield return new WaitForSeconds(0);
           
        }


        transform_1.GetComponent<Player>().Board = transform.parent.parent.Find("Net").GetComponent<Board_Mode_1>();
        transform_1.GetComponent<Player>().rotate = transform.parent.parent.Find("Hool").GetComponent<Rotate>();
        transform_1.GetComponent<Player>().trigger = transform.GetComponent<CheclInBall>();
        transform_1.GetComponent<Player>().Board_1 = transform.parent.parent.transform.Find("Img").GetComponent<Board_Rotation>();
      transform_1.gameObject.transform.GetComponent<Rigidbody2D>().isKinematic = false;
       transform_1.GetComponent<Rigidbody2D>().simulated = false;
   
        transform_1.GetComponent<Player>().isWall = false;
        transform_1.GetComponent<Player>().FullForce = false;
        transform_1.GetComponent<Player>().Reset();
      //  transform_1.GetComponent<Player>().OnEnable_mode(false);
       // gameObject.SetActive(false);
        if (key_board != Player.instance.index)
        {
            LevelController.instance.UpdateTime = true;
            if (Result())
            {
                if (Check_Ball[point0] == 0 && Check_Ball[point1] == 0)
                {
                    SpawnEffect.instance.Set_System(PointEffect.position, "+PERFECT X"+Player.instance.CountPerfect, null, 0);
                    SpawnEffect.instance.Set_System(PointEffect.position, "+"+Player.instance.CountPerfect*2, null, 0.5f);
                    int x = Player.instance.CountPerfect;
                   
                    Player.instance.TakeScore(Paractice_Type.PERFECT);

                }

            }
      
            else
            {
                if ((Check_Ball[point0] + Check_Ball[point1]) == 1)
                {
                    SpawnEffect.instance.Set_System(PointEffect.position, "+NOBLE", null, 0);
                    SpawnEffect.instance.Set_System(PointEffect.position, "+1", null, 0.5f);
                    Player.instance.TakeScore(Paractice_Type.NORMAL);

                }
                else
                {
                     int x = Player.instance.CountPerfect;
                    Player.instance.TakeScore(Paractice_Type.NORMAL);

                    
                    SpawnEffect.instance.Set_System(PointEffect.position, "+1", null, 0.5f);
                  

                }
                


            }

            // SpawnEffect.instance.Set_System(Paractice_Type.PERFECT, PointEffect.position, "",null);
            //  Player.instance.TakeScore(Paractice_Type.PERFECT);
            Debug.Log("SPAWN BOARD");
            Player.instance.index = key_board;
            SpawnerCtrl.instante.chanceBoard = true;
            MoveFollowPlayer.instance.new_Collider_Camera();
            //  transform.parent.parent.Find("Net").GetComponent<Board_Mode_1>().Board_type = BOARD_TYPE.NORMAL;
        }
        else
        {
            transform_1.GetComponent<Player>().countWall  = 0;
        }
           
        }

        public void setActive(bool a)
        {
            gameObject.SetActive(a);
        }


    }
