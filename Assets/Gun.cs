using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public static int index = 0;

    public Text counBallLeft;
    public Text counBallRight;


    public int Star;

    public static Gun instance = null;

    public Transform WorldSpace;

    public int BallinScoop;

    public Vector3 posMouse;

    public float angle = 45;

    public float time = 0;

    public float speed = 1;
    public List<Bullet> isShotting = new List<Bullet>();
    public List<Bullet> BulletInShoot;
    public Vector2[] listpos;
    public float ForceShoot = 10;

    public Transform Shoot;         // Shoot
    public Transform trsBullet;     //
    public float dis = 1;          //
    public Vector2 direct;
    public GameObject positonObj;
    public GameObject BallObj;
    public bool WaitForEndBall = false;
    public bool isLeft = true;
    float frame=0;
    public Image imgCountBall;
    public Image imgCountBall_1;

    public int indexBullet = 0;
     
    public void onRest()

    {
        index = 0;
        isShotting.Clear();
        isLeft = true;
        Gun.instance.RestBullet(5);
        SpawnerCtrl.instante.SpawnLeft = true;
        SpawnerCtrl.instante.chanceBoard = true;
    }
    int x;
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
    private void Start()

    { ForceShoot = 15;
        isLeft = true;
        imgCountBall.sprite = GameController.instance.SpriteBall();
        imgCountBall_1.sprite = GameController.instance.SpriteBall();
        Trajector();
        SpawnerCtrl.instante.SpawnLeft = true;
        SpawnerCtrl.instante.chanceBoard = true;
        //  SimulateTrajectory(Shoot.transform.position, Vector3.one * 30);
        trsBullet = transform.Find("PositonBullet").transform;
        MODE_GAME.Reset_Mode_2 += onRest;
        WorldSpace = transform.parent;
        BulletInShoot = new List<Bullet>();
        DirectShoot();
        listpos = SpawnPositon(BallinScoop, (Vector2)Shoot.transform.position, direct, 1f);
        for (int i = 0; i < listpos.Length; i++)
        {
            var p = Instantiate(BallObj, listpos[i], Quaternion.identity, trsBullet);
            BulletInShoot.Add(p.GetComponent<Bullet>());
        }
        //  for(int i = 0; i < BulletInShoot.Length; i++)
        //  {
        //       BulletInShoot[i].transform.position = listpos[i];
        //   }
        RaycastHit hit;
        Physics.Raycast(new Vector3(Shoot.position.x, Shoot.position.y, 0), Vector2.one*100, out hit, 0.5f);
        x1 = hit.point.x;

    }
    public float x1;
    public void RestBullet(int count)
    {
        ///CLear
        trsBullet = transform.Find("PositonBullet").transform;
        Bullet[] game = trsBullet.GetComponentsInChildren<Bullet>();

        if (game != null)
        {
            for (int i = 0; i < game.Length; i++)
            {
                game[i].Destroy(0);
            }
            BulletInShoot.Clear();

        }
       


        ///Reset
        ///

        listpos = SpawnPositon(count, (Vector2)Shoot.transform.position, direct, 0.06f);
        for (int i = 0; i < count; i++)
        {

            var p = Instantiate(BallObj, listpos[i], Quaternion.identity, trsBullet);
            if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_3)
            {

                

            }
            BulletInShoot.Add(p.GetComponent<Bullet>());

        }

    }
    
    IEnumerator Cooltime()
    {
        WaitForEndBall = true;
        yield return new WaitForSeconds(1.3f);
        WaitForEndBall = false;
    }

    private void Update()
    {
        DirectShoot();
        transform.Find("Img").GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteGun();
        Star = GameController.instance.Get_Star();
        counBallLeft.text = "X" + BulletInShoot.Count;
        counBallRight.text = "X" + BulletInShoot.Count;
        if (!GameController.instance.GameOver && !(GameController.instance.GamePause))
        {
          
            if (!isLeft)
            {

                counBallLeft.transform.parent.gameObject.SetActive(true);
                counBallRight.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                counBallLeft.transform.parent.gameObject.SetActive(false);
                counBallRight.transform.parent.gameObject.SetActive(true);
            }
            if (!WaitForEndBall)
            {
                MouseMove();
                AutoRotate();
          
                DirectShoot();
                if (Input.GetMouseButtonDown(0) && !ClickPause.Click)
                {
                    if (BulletInShoot.Count >0)
                    {
                        AutioControl.instance.GetAudio("Gun").Play();
                        Vector2 vectorShoot = new Vector2(DirectShoot().x, DirectShoot().y);
                        Debug.Log(vectorShoot);
                        BulletInShoot[0].Shoot(DirectAndForce());
                        isShotting.Add(BulletInShoot[0]);
                        BulletInShoot.Remove(BulletInShoot[0]);
                        Next();
                        StartCoroutine(Cooltime());
                       

                    }
                    else
                    {




                        Debug.Log("Out Of Bullet");
                    }
                        



                }
                else
                {
                   
                    

                     SimulateTrajectory(Shoot.transform.position, DirectAndForce());
                    
                    if (GameObject.FindObjectsOfType<Board>().Length > 0 && BulletInShoot.Count == 0)
                    {

                        Bullet[] game = GameObject.FindObjectsOfType<Bullet>();
                        if (CheckGameOver(game))
                        {
                            GameObject.Find("ScreenGameplay").GetComponent<ScreenGamePlay>().GameOverWindow();
                        }


                    }
        

                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                    transform.position = (Vector2)(isLeft ? Camera.main.ScreenToWorldPoint(GameController.instance.leftbottom) : Camera.main.ScreenToWorldPoint(GameController.instance.rightbottom));
                    // transform.localScale = new Vector2(transform.localScale.x * (isLeft ? 1 : -1), transform.localScale.y);
                    if (isLeft)
                    {

                        transform.localScale = new Vector3(1, transform.localScale.y, 1);
                    }
                    else
                    {

                        transform.localScale = new Vector3(-1, transform.localScale.y, 1);
                    }

                }
            }
        }
    }

    public Vector2 DirectAndForce()
    {
        
         return DirectShoot() * ForceShoot;

    }
    public void MouseMove()
    {
        posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        AutoRotate();

    }
    public void UpdateDirectShoot()
    {

    }
    public void AutoRotate()
    {

        time += Time.deltaTime * speed;

        float rotation = Mathf.Sin(time) * angle;

        if (Mathf.Sign(rotation) == -1)
        {
            rotation = -rotation;

        }


        transform.rotation = Quaternion.AngleAxis(20* (isLeft ? 1 : -1) +  rotation * (isLeft ? 1 : -1), Vector3.forward);



    }

    public Vector2[] SpawnPositon(int num, Vector2 pos, Vector2 direct, float distance)
    {

        Vector2[] listPosition = new Vector2[num];
        for (int i = 0; i < num; i++)
        {
            Vector2 pos_bullet = pos;
            listPosition[i] = pos - (i * DirectShoot());

        }
        return listPosition;
    }

    public void Next()
    {
        listpos = SpawnPositon(BallinScoop, (Vector2)Shoot.transform.position, direct, 1f);
        for (int i = 0; i < BulletInShoot.Count; i++)
        {

            BulletInShoot[i].transform.position = listpos[i];

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(Shoot.transform.position, transform.position);
    }
    public Vector2 DirectShoot()
    {
        direct = Shoot.transform.position - transform.position;

        return direct.normalized;
    }
    public void ChanceDirect()
    {
        isLeft = !isLeft;
       
    }

    public void GameOVer()
    {



    }
    public bool CheckGameOver(Bullet[] bullets)
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].isOut)
            {
                return false;
            }



        }
        return true;


    }
    public Transform[] dot;
    public int CountNode = 30;
    public float ShiftDot = 0.05f;
    public void Trajector()
    {
        dot = new Transform[41];
        for (int i = 0; i < 40; i++)
        {
            string s = "Dot (" + i + ")";
            dot[i] = GameObject.Find("Mode_3/Dot").transform.Find(s);
            dot[i].gameObject.SetActive(false);

        }


    }
    ///////
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="velocity"></param>
    /// 
    public void enableTrajectory(bool enable)
    {
        for (int i = 0; i < 40; i++)
        {

            string s = "Dot (" + i + ")";
            dot[i] = GameObject.Find("Mode_3/Dot").transform.Find(s);
            dot[i].gameObject.SetActive(enable);

        }

    }
    
    
    public void SimulateTrajectory(Vector2 pos, Vector2 velocity)
    {
        float Shift = 2.5f;
        enableTrajectory(true);
        float posX = pos.x;
        float posY = pos.y;
        bool isReflect = false;
        int j = 0;
        Vector2 posReflect = Vector2.zero;
     
        Vector2 new_vec = Vector3.zero;
        // RaycastHit2D hit;
        Vector3 Scale = new Vector3(0.5f, 0.5f, 1);
        Vector3 Scale0 = new Vector3(0.2f, 0.2f, 1);
            float dis = Vector3.Distance(Scale, Scale0);
           float space = dis / 25;
        float radius = dot[1].GetComponent<SpriteRenderer>().size.x / 2.5f;
        for (int i = 0; i < 25; i++)
        {

            if (!isReflect)
            {
       
                float x = posX + (velocity.x * Time.fixedDeltaTime * i* Shift);
                float y = posY + (velocity.y * Time.fixedDeltaTime * i * Shift) + ((Physics.gravity.y * Time.fixedDeltaTime * Time.fixedDeltaTime * i * i * Shift * Shift) / 2);
                Vector3 point = new Vector2(x, y);
                dot[i].gameObject.SetActive(true);
                dot[i].transform.position = point;
                RaycastHit2D ray;
                if (dot[i].position.x>GameController.instance.rightBottomReal.x )
                {

                     ray = Physics2D.Raycast(dot[i].position, DirectShoot(), 20, 2);
                    if (ray.collider == null)
                    {
                      ray = Physics2D.Raycast(dot[i].position, -DirectShoot(), 20, 2);
                    }

                    

                
                         //   Debug.Log("Va Cham" + i);

                            isReflect = true;
                         posReflect.y = ray.point.y;
                            posReflect =   dot[i].transform.position;
                        
                            posReflect.x = GameController.instance.rightBottomReal.x-radius*0.5f;
                 
                            dot[i].position = posReflect;
                      //     new_vec = Vector3.Reflect(velocity, Vector3.forward);
                    new_vec = new Vector2(-velocity.x, velocity.y)/1.5f;
                    dot[i].gameObject.SetActive(false);
                  
                   

                }
                else if(dot[i].position.x < GameController.instance.leftBottomReal.x)
                {
                    ray = Physics2D.Raycast(dot[i].position, DirectShoot(), 20, 2);
                    if (ray.collider == null)
                    {
                        ray = Physics2D.Raycast(dot[i].position, -DirectShoot(), 20, 2);
                    }
                    posReflect.y = ray.point.y;
                    isReflect = true;
                    posReflect = dot[i].transform.position;

                    posReflect.x = GameController.instance.leftBottomReal.x+radius*0.5f;
                    dot[i].position = posReflect;
                   // new_vec = Vector3.Reflect(velocity, Vector3.forward);
                    new_vec = new Vector2(-velocity.x, velocity.y)/1.5f;

                    dot[i].gameObject.SetActive(false);

                }

            }
            else
            {


                /*
                float x = posReflect.x+  (new_vec.x * Time.fixedDeltaTime * j * Shift);
                float y = posReflect.y + (new_vec.y * Time.fixedDeltaTime * j * Shift) + ((Physics.gravity.y *1.1f*Time.fixedDeltaTime * Time.fixedDeltaTime * j * j  * Shift * Shift) / 2);
                Vector2 point = new Vector2(x, y);
                dot[i].gameObject.SetActive(true);
                dot[i].transform.position = point;
             
                dot[i].transform.localScale = Scale;

                j++;
                */
                dot[i].gameObject.SetActive(false);





            }
          Scale = (Vector2)Scale - Vector2.one * space;

          dot[i].transform.localScale = Scale;



        }

        }
   
   public void SetParticeBullet(bool Burn,bool Fire)
    {
        for(int i = 0; i < BulletInShoot.Count; i++)
        {
            if (Fire)
            {
                BulletInShoot[i].Burn = Burn;
                if (Burn == true)
                {
                    BulletInShoot[i].Type_Action = TYPE_ACTION.BURN;

                }
                else
                {
                    BulletInShoot[i].Type_Action = TYPE_ACTION.BURN;
                }
            }
            else
            {
                BulletInShoot[i].Type_Action = TYPE_ACTION.NORMAL;
            }
          

        }
    }
  

}




