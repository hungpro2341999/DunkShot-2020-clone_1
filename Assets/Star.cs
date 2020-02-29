using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    
    public Vector3 posDirect;
    public float speed = 8;
    float angle = 10;
    // Start is called before the first frame update
    public void rotate()
    {
        angle += 10;

        Quaternion quaternion  =   Quaternion.AngleAxis(angle, Vector3.forward);
        transform.localRotation = quaternion;
    }
     public  void OnRest()
    {
        if (this != null)
        {
            Destroy(gameObject);
        }
        else
        {
        }

    }
    void Start()
    {
        switch (GameController.instance.Game_Type)
        {
            case GAME_MODE_TYPE.MODE_1:
                MODE_GAME.Reset_Mode_1 += OnRest;
                break;
            case GAME_MODE_TYPE.MODE_2:
                MODE_GAME.Reset_Mode_2 += OnRest;
                break;
            case GAME_MODE_TYPE.MODE_3:
                MODE_GAME.Reset_Mode_3 += OnRest;
                break;
           case GAME_MODE_TYPE.MODE_4:
                speed = 4.5f;
                break;

        }
      transform.parent = ModeCtrl.instance.getWorldSpace();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_4)
        {
            rotate();
        }
    }
    public void Destroy()
    {
        AutioControl.instance.GetAudio("Star").Play();
        StartCoroutine(StartDestoy());

      
      
    }
    IEnumerator StartDestoy()
    {
      
        if (GameObject.Find("Canvas/Star") != null)
        {
            if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_4)
            {
                yield return new WaitForSeconds(UnityEngine.Random.Range(0, 1.5f));
                posDirect = Camera.main.ScreenToWorldPoint(GameObject.Find("Canvas/Star").transform.position);

                yield return new WaitForSeconds(0.2f);
                while (posDirect != transform.position)
                {
                    posDirect = Camera.main.ScreenToWorldPoint(GameObject.Find("Canvas/Star").transform.position);

                    transform.position = Vector3.MoveTowards(transform.position, posDirect, Time.deltaTime * speed);
                    yield return new WaitForSeconds(0);
                }

            }
            else
            {
                posDirect = Camera.main.ScreenToWorldPoint(GameObject.Find("Canvas/Star").transform.position);

                while (posDirect != transform.position)
                {
                    if (posDirect == null)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        if (posDirect == null)
                        {
                            Destroy(gameObject);
                        }
                        posDirect = Camera.main.ScreenToWorldPoint(GameObject.Find("Canvas/Star").transform.position);

                        transform.position = Vector3.MoveTowards(transform.position, posDirect, Time.deltaTime * speed);
                    }
                  
                    yield return new WaitForSeconds(0);
                }

            }


            if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_4)
            {
                GameController.instance.AddStarGolabal(1);
                Debug.Log("Them Sao");  
                Reward_Game.instance.AddStar(1);

                Destroy(gameObject);
                

            }
            else
            {
                Destroy(gameObject);

            }

        }
        else
        {
            Destroy(gameObject);
        }
        
        


    }
    public void DestroyStar()
    {
        Destroy(gameObject);

    }
}
