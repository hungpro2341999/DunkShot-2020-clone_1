using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestAction : MonoBehaviour
{
    public static TestAction instance = null;
    public float timeBurn = 20;
    public event Action OnUpdate = null;
    public bool reset = false;
    // Start is called before the first frame update
    public bool done = false;
    SpriteRenderer Sprite_Normal;

    public Sprite Sprite;
    public Color Color_Normal_BALL;
    public ParticleSystem settings;
    public Color Color_Normal_Partices;

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

        //Partice
       

    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_1)
            {
                //////////// For Mode_1
                settings = BallPlayer.instance.Practice_Ball.GetComponent<ParticleSystem>();


                //Ball
                Sprite_Normal = BallPlayer.instance.GetComponent<SpriteRenderer>();
                Sprite = BallPlayer.instance.GetComponent<SpriteRenderer>().sprite;
                Color_Normal_BALL = Sprite_Normal.color;

                done = true;
            }
        }

        if (OnUpdate != null)
        {
          
            OnUpdate();

        }
        else
        {

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartShakeCamera(1, 0.15f);
        }

    }





    ParticleSystem particle;
    public void StartBurnBall(float time)
    {
        //   ParticleSystem.MainModule settings = BallPlayer.instance.Practice_Ball.GetComponent<ParticleSystem>().main;
        //    settings.startColor = Color.HSVToRGB(0, 50, 50);


        BallPlayer.instance.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(10, 10, 10);

        BallPlayer.instance.TYPE_ACTION = TYPE_ACTION.BURN;
        Action Update_Action = null;
        Action Remove_Action = null;
        float timelife = time;

        // 
        Update_Action = () =>
        {
            if (timelife > 0)
            {
                if (reset)
                {
                    timelife = time;
                    reset = false;

                }
                timelife -= Time.deltaTime;
                //   Debug.Log(timelife);
            }
            else
            {
                Remove_Action();
                OnRestStatus();
            }
        };
        Remove_Action = () =>
        {
            OnUpdate -= Update_Action;
            BallPlayer.instance.TYPE_ACTION = TYPE_ACTION.NORMAL;
        };
        if (OnUpdate == null)
        {

            OnUpdate += Update_Action;

        }




    }



    public void OnRestStatus()
    {

        BallPlayer.instance.TYPE_ACTION = TYPE_ACTION.NORMAL;

        //   Sprite_Normal.sprite = Sprite;

        //   settings.startColor = Color_Normal_Partices;   

        BallPlayer.instance.GetComponent<SpriteRenderer>().color = Color_Normal_BALL;

        //    Sprite_Normal.color = Color_Normal_BALL;



    }
    /// <summary>
    /// /////////////////              FOR MODE 2 :
    /// </summary>
    public void OnRestStatus_2()
    {
        List<Bullet> bullets = Gun.instance.BulletInShoot;
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].Type_Action = TYPE_ACTION.NORMAL;
            bullets[i].SpriteRenderer.color = bullets[i].color;


        }

    }



    public void StartBurnBall_2(float time)
    {
        //   ParticleSystem.MainModule settings = BallPlayer.instance.Practice_Ball.GetComponent<ParticleSystem>().main;
        //    settings.startColor = Color.HSVToRGB(0, 50, 50);



        List<Bullet> bullets = Gun.instance.BulletInShoot;

        Action Update_Action = null;
        Action Remove_Action = null;
        float timelife = time;
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].Type_Action = TYPE_ACTION.BURN;
            bullets[i].SpriteRenderer.color = Color.HSVToRGB(30, 30, 30);


        }

        // 
        Update_Action = () =>
        {
            if (timelife > 0)
            {
                if (reset)
                {
                    timelife = time;
                    reset = false;
                    Debug.Log(timelife);
                }
                timelife -= Time.deltaTime;
                //Debug.Log(timelife);
            }
            else
            {
                Remove_Action();
                OnRestStatus_2();
            }
        };
        Remove_Action = () =>
        {
            OnUpdate -= Update_Action;

        };
        if (OnUpdate == null)
        {

            OnUpdate += Update_Action;
            
        }

       
       

    }
    float shakeDuration = 1f;
    Vector3 originalPos;

  
    
    public void StartShakeCamera(float time, float ratio)
    {
      Transform camTransform = Camera.main.transform;
        Action Update_Action = null;
        Action Remove_Action = null;
        float timelife = time;

        float shakeAmount = ratio;
        originalPos = Camera.main.transform.position;


        ///
        Update_Action = () =>
        {
            if (timelife > 0)
            {
               
               
                  
                  
                        camTransform.localPosition = Camera.main.transform.position + UnityEngine.Random.insideUnitSphere * shakeAmount;
                      //  Debug.Log(camTransform.position);
                       
                  //      Debug.Log(camTransform.localPosition);
                  
                     
                      
                
                timelife -= Time.deltaTime;
                //   Debug.Log(timelife);
            }
            else
            {
                Remove_Action();
              
            }
        };
        Remove_Action = () =>
        {
            Camera.main.transform.position = originalPos;

            OnUpdate -= Update_Action;
         
        };
        if (OnUpdate == null)
        {

            OnUpdate += Update_Action;

        }

    }


   
       


}





    
