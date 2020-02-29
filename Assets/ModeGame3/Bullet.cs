using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    bool isInterflect = true;
    public ParticleSystem Eff_3;
    public ParticleSystem Eff_4;
    public ParticleSystem Eff_5;
    public int countDestroy=0;
    public int indexBullet;
    public int countGolbal;
    public bool DoubleBall = true;
    public bool TripleBall = true;
    public  float Mass = 1;
    public bool Burn = false;
    public float timeBurn = 0;
    public float tiemSpawn = 0;
    public GameObject Practice_Ball;

    public GameObject PracticeSuperBurn;

    public bool isOut=false;

    public Rigidbody body;

    public Transform Transform;
    public bool isGround = false;
    public TYPE_ACTION Type_Action =TYPE_ACTION.NORMAL;

    public SpriteRenderer SpriteRenderer;
    public Color color;
    public float delaytime =1f;
    public Vector2 Veclocity;
    public int index;

   public  int CheckDead = 0;
    float TimeCheck = 0;
    public void setkey()
    {
        index = Gun.instance.indexBullet++;

    }
    // Start is called before the first frame update
    bool done = false;
    private void Awake()
    {
        indexBullet = Gun.index++;

    }
    void Start()
    {
        if (GroupBoard.instace.count == 1)
        {
           

        }
        else
        {
            GroupBoard.instace.count=1;

        }
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = GameController.instance.SpriteBall();

        Transform = gameObject.transform;

        body = GetComponent<Rigidbody>();

        color = SpriteRenderer.color;
    }
    
    // Update is called once per frame
    void setMass(int mass)
    {
        Mass = mass;
    }


    void Update()
    {
       


        if (!GameController.instance.GameOver && !(GameController.instance.GamePause))
        {
           
                SpawnPractice(Type_Action);

            
        
            if (!body.isKinematic)
            {
                if (TimeCheck > 0)
                {
                    TimeCheck-=Time.deltaTime;
                }
                else
                {
                    
                    TimeCheck = 5;
                    pos_new = transform.position;

                }


             
                if((Vector2)transform.position == pos_new)
                {
                    CheckDead++;
                    if (CheckDead > 100)
                    {
                        isGround = true;
                        Destroy(1);
                    }
                }
                else
                {
                    CheckDead = 0;
                }
                //   Debug.Log(body.velocity);
                if (!isOut)
                {
                    if (isGround && Vector2.SqrMagnitude(body.velocity)<0.4f)
                    {
                        isOut = true;
                        Gun.instance.WaitForEndBall = false;
                        Destroy(1);


                    }
                }
            }
        }
        
    }
    public void Shoot(Vector3 direct)
    {
      
        gameObject.layer = 13;
        transform.parent = Gun.instance.WorldSpace;
        GetComponent<SphereCollider>().isTrigger = false;
        body.isKinematic = false;
        body.velocity = (direct);
       
        Veclocity = body.velocity;
        Debug.Log("SHOOT : " + body.velocity);


    }
    public void Destroy(float time)
    {
        Gun.instance.isShotting.Remove(this);
        StartCoroutine(Destroy_Ball(time));
    }

    IEnumerator Destroy_Ball(float time)
    {

        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ObsBottom" || collision.gameObject.name == "BottomBoardObs")
        {

            isGround = true;
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Star")
        {
          
            GameController.instance.AddStarGolabal(1);
            Ball_Player_2.instance.addStar(1);
            other.GetComponent<Star>().Destroy();

        }
     
      if(other.gameObject.layer != 1 &&other.gameObject.layer!=8&& other.gameObject.layer != 9 && other.gameObject.layer != 15 && other.gameObject.layer!=0 && other.gameObject.layer != 13 )
        {
            Debug.Log(other.gameObject.layer + "+++++++++++++++" + other.gameObject.name);
            isInterflect = false;
            if (other.gameObject.name == "Second" || other.gameObject.name == "Bound")
            {
                isInterflect = true;
            }
        }
        else
        {
            isInterflect = true;
        }
    }
    public void ChangeDirect()
    {
        if (isInterflect)
        {
            if (!done)
            {
   
                body.velocity = Vector2.Reflect(Veclocity,Vector3.forward);

                done = true;
            }
        }
       
       


    }
   public Vector2 pos_new;
   float time=0;
   public   int count=0;
   int timelife=8;
   float posY = 0;
   public float time_1 = 0;
    public bool isMove()
    {
        if (count > 5)
        {
            return true;
        }
        if (time_1 < 0)
        {
            posY = transform.position.y;
            time_1 = 1f;
        }
        else
        {
            time_1 -= Time.deltaTime;
        }

        if (time < 0)
        {
            if (posY == transform.position.y)
            {
                count++;


            }
            
           
              
            
          
            time = 0.1f;
      
        }
        else
        {
           
            time -=Time.deltaTime;
            return false;
        }
        return false;
    }
    public void SpawnPractice(TYPE_ACTION _ACTION)
    {
      int  indexball = GameController.instance.GetSortEff();
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

                        
                       

                        tiemSpawn = 0.05f;
                    }
                    else
                    {
                        tiemSpawn -= Time.deltaTime;

                    }

                }
                break;

        }


    }

}
