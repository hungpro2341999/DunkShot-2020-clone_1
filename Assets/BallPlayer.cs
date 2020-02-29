using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public enum TYPE_ACTION {NORMAL,BURN};
public class BallPlayer : MonoBehaviour
{

    public AudioSource Sound_Click_1;
    public ParticleSystem Eff_3;
    public ParticleSystem Eff_4;
    public ParticleSystem Eff_5;
   

    int indexball = 0;
    Transform WorldSpace;
    public TYPE_ACTION TYPE_ACTION = TYPE_ACTION.NORMAL;
    public static BallPlayer instance;
    public int Score=0;
    
    public bool gameOVer = false;
    
    public GameObject Practice_Ball;
    
    public GameObject PracticeSuperBurn;

    bool isDead = false;

    bool MovingToLeft = false;

    bool MovingToRight = false;

    float xLeft;

    float xRight;

    public Vector2 direct = new Vector2(1, 1);

    public float force;

    public Rigidbody body;

    bool chancePositon = false;

    int timelife = 20;

    public int count = 0;

   public  int Star;
    int coutwall;

    public Sprite Sprite;
    // Start is called before the first frame update
    //Check Effect
   

    public int count_normal=0;
    public int count_perfect=1;


  
    public int CountPerfect = 1;
    public bool isGround = true;
    public int countGround;
   
    public int countGolbal = 0;
    public int countFullForce = 0;

    public int countTap = 0;

    public int CountTapTap = 0;
    Vector3 pos;
    public bool Burn = false;
    public float timeBurn = 0;
    public float tiemSpawn = 0;
   public void Reset()
    {
        count_normal = 0;
        count_perfect = 1;
        countGround=0;
        isGround = false;
        countFullForce = 0;
        countTap = 0;
        CountTapTap = 0;

    }
  
    public void Check_Effect(Paractice_Type paractice_Type)
    {
        if (countTap == 2)
        {
            CountTapTap++;
            countTap = 0;
        }
        else
        {
            countTap = 0;
        }

        if (isGround)
        {
            countGround++;
           


        }
        else
        {
            isGround = true;
            countGround = 0;
        }

    

        countGolbal++;
        switch (paractice_Type)
        {
            case Paractice_Type.NORMAL:

                count_normal++;
                count_perfect = 1;
                AddScore(1);
            
                AutioControl.instance.GetAudio("Perfect_1").Play();




                TestAction.instance.reset = true;
                break;
            case Paractice_Type.PERFECT:
                count_perfect++;
                AddScore(3*count_perfect);
                count_normal = 0;
                if (count_normal > 3)
                {

                    timeBurn = 10;
                    Burn = false;
                    if (count_perfect > 4)
                    {

                        Burn = true;
                    }
                }
                if (CountPerfect > 7)
                {
                    string name = "Perfect_" +6;
                    AutioControl.instance.GetAudio(name).Play();

                }
                else
                {
                    string name = "Perfect_" + count_perfect;
                    AutioControl.instance.GetAudio(name).Play();

                }
               
                break;
                

        }
      
    }
    public void On_Rest()
    {
        LevelController.instance.count = 0;
        BallPlayer.instance.transform.position = pos;
        Score = 0;
        Star = GameController.instance.Get_Star();

        if (Mathf.Sign(direct.x) == 1)
        {
            direct.x = -direct.x;
        }
        GameController.instance.Save_Star(getStar());
       

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
    public void Continue()
    {
        Debug.Log("Continue_Mode_1");
        LevelController.instance.UpdateTime = true;
    }
    void Start()
    {
        GameController.instance.On_Off_Sound += On_Off_Sound;

        
        MODE_GAME.Reset_Mode_1 += Reset;
        MODE_GAME.Continue_Mode_1 += Continue;
        Star = GameController.instance.Get_Star();
       
        pos = transform.position;
        MODE_GAME.Reset_Mode_1 += On_Rest;

      WorldSpace = GameObject.Find("Mode_2").transform;
        Time.timeScale = 2f;
        

        if (!GameController.instance.firstPlay)
        {
            GameController.instance.setFirst();

        }

     
       // GameObject.Find("Mode_2/bgcloud").GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBackGround();
     
     

        Vector2 scale = transform.localScale;

        transform.localScale = scale;

   
    }

    // Update is called once per frame
    void Update()
    {

         Debug.Log(countGround);
        if (GameController.instance.SpriteBall() != null)
        {
            GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBall();
        }

        if (!GameController.instance.GameOver && !(GameController.instance.GamePause))
        {


            if (LevelController.instance.UpdateLv)
            {
                UpdateLevel();
            }


            if (!isDead)
            {

                if (Input.GetMouseButtonDown(0))
                {

                    AutioControl.instance.GetAudio("CLick").Play();
                    Vector2 veclocity = body.velocity;
                    veclocity.x = direct.x;
                    veclocity.y = direct.y;

                    body.velocity = veclocity * force;


                    countTap++;
                }

                //SpawnPractice(TYPE_ACTION);
                SpawnPractice(TYPE_ACTION);


            }

         
        }
    



    }

    public void On_Off_Sound(bool on)
    {
       // Sound_Click_1.mute = on;
        //Debug.Log("On_Off");

    }

    public void UpdateLevel()
    {
      

   

        LevelController.instance.UpdateLv = false;

        Vector2 scale = transform.localScale * LevelController.instance.getLevelCurr();

        transform.localScale = scale;

    }

    void setPosition(Vector2 pos)
    {
        gameObject.transform.position = new Vector3(pos.x,pos.y,transform.position.z);

    }

    // 8 right : 9 left
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 8)
        {
            count++;

            if (count%2==1)
            {
                MovingToLeft = true;
                MovingToRight = false;
                ChancePosition();

            }

        }
        if(collision.gameObject.layer == 9)
        {
            count++;

            if (count%2==1 )
            {
                MovingToLeft = false;
                MovingToRight = true;
                ChancePosition();

                
            }

        }

        if (collision.gameObject.tag == "Star")
        {
            GameController.instance.AddStarGolabal(1);
            addStar(1);
            collision.GetComponent<Star>().Destroy();

        }




    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 10)
        {

            count++;
        }

        if(collision.gameObject.layer == 11)
        {

            count++;
        }


    }


    public void ChancePosition()
    {
        xLeft = Camera.main.ScreenToWorldPoint(GameController.instance.leftup).x;
        
        xRight = Camera.main.ScreenToWorldPoint(GameController.instance.rightup).x;
        if (count%2==1)
        {

            if (MovingToLeft && !MovingToRight)
            {
               
                setPosition(new Vector2(xLeft, transform.position.y));
                MovingToLeft = false;

            }
            else if(MovingToRight && !MovingToLeft)
            {
              

                MovingToRight = false;
                setPosition(new Vector2(xRight, transform.position.y));
            }

          



        }

     
    }

    public void ChanceDirect()
    {
        direct.x = -direct.x;



    }

    public void SpawEff()
    {
        GameObject game = Instantiate(Practice_Ball,transform.position, Quaternion.identity, null);
    }

    public void SpawnPractice(TYPE_ACTION _ACTION)
    {
        indexball = GameController.instance.GetSortEff();
        switch (_ACTION)
        {
            case TYPE_ACTION.NORMAL:
                if (body.velocity.x != 0 || body.velocity.y != 0)
                {
                    Burn = false;
                    if (tiemSpawn < 0)
                    {
                        if (indexball == 0)
                        {
                            SpawnEffect.instance.getEffect(indexball, 0, transform.position);
                            Eff_3.Stop();
                            
                            
                         
                        }
                        if(indexball == 1)
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
                        if(indexball == 4)
                        {
                            SpawnEffect.instance.getEffect(indexball, 0, transform.position);
                            Eff_5.Stop();
                        }
                         //  ParticleSystem.MainModule settings = Practice_Ball.transform.Find("Particle System").GetComponent<ParticleSystem>().main;
                        //   GetComponent<SpriteRenderer>().sprite.texture.GetPixelBilinear(40, 40);
                        //    settings.startColor = new ParticleSystem.MinMaxGradient(GetComponent<SpriteRenderer>().color);
                        tiemSpawn = 0.15f;
                    }
                    else
                    {
                        tiemSpawn -= Time.deltaTime;

                    }
                  


                }
                break;
             
            case TYPE_ACTION.BURN:

                if (body.velocity.x != 0 || body.velocity.y != 0)
                {
                    if (tiemSpawn < 0)
                    {
                        if (timeBurn > 0)
                        {
                            if (Burn)
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
                                //  SpawnEffect.instance.Set_System(Paractice_Type.SMOKE_LEVEL_3, new Vector3(transform.position.x, transform.position.y, 5), "",transform);
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

                                    Eff_3.Stop();

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
                        else
                        {

                            timeBurn -= Time.deltaTime;
                        }

                        tiemSpawn = 0;
                    }
                    else
                    {
                        tiemSpawn -= Time.deltaTime;

                    }

                }
                break;
                
        }
       

    }
 
    public void AddScore(int score)
    {
        Score += score;

    }
    public int getScore()
    {
        return Score;
    }


    public void AddSCore(int amount)
    {
        Score += amount;

    }
    public void addStar(int amout)
    {
        Star += amout;
        GameController.instance.Save_Star(Star);
        
    }
    public int GetScore()
    {
        return Score;
    }
    public int getStar()
    {
        return GameController.instance.Get_Star();
    }

    void RestGame()
    {

    }
}
