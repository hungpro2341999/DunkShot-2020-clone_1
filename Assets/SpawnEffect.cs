using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public  enum Paractice_Type  { NORMAL, PERFECT,STAR,STAR2D,PUM,PONG,STATUS,TRIPLE,DOUBLE,SMOKE_LEVEL_1, SMOKE_LEVEL_2, SMOKE_LEVEL_3,COLL_WALL,SHoot,BASKET_COLL};
public class SpawnEffect : MonoBehaviour
{
    public static SpawnEffect instance = null;


    class RegistryItem     // CALL ACTION
    {
        int _value;
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (listeners != null)
                {
                    listeners();
                }
            }
        }

        public event Action listeners;
    }
    public GameObject Pum;
    public GameObject Pong;
    public ParticleSystem Normal;
    public ParticleSystem Perfect;
    public GameObject NoramalObj;
    public GameObject PerfectObj;
    public GameObject Star;
    public GameObject Star2D;
    public GameObject Smoke_level_1;
    public GameObject Smoke_level_2;
    public GameObject Smoke_level_3;

    public GameObject Collison_Wall_2;
    public GameObject Shoot;
    public GameObject Collison_Basket;
    public List<GameObject> ListSortEffect_Level_1 = new List<GameObject>();
    public List<GameObject> ListSortEffect_Level_2 = new List<GameObject>();
    public List<GameObject> ListSortEffect_Level_3 = new List<GameObject>();

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
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            Instantiate(Collison_Wall_2, Vector2.zero, Quaternion.identity, null);
    }

    public void SpawnPerfect()
    {


    }

    public void SpawNormal()
    {

    }
    public void SpawnSystem()
    {


    }
    public void Set_System_Shoot(Vector2 pos)
    {
       // Instantiate(Shoot,pos,)

    }
    public void Set_System_Wall(Vector2 pos, Transform trans,bool isleft)
    {

        if (isleft)
        {
            pos.x = GameController.instance.leftBottomReal.x;
            
         Instantiate(Collison_Wall_2, pos, Quaternion.identity, null);
          Debug.Log(pos);

        }
        else
        {   
            pos.x = GameController.instance.rightBottomReal.x;
           Instantiate(Collison_Wall_2, pos, Quaternion.identity, null);
            Debug.Log(pos);
        }
    
   



    }



    public void Set_System(Paractice_Type type, Vector2 pos, string s, Transform trans)
    {
        switch (type)
        {

            case Paractice_Type.SMOKE_LEVEL_1:
                Instantiate(Smoke_level_1, pos, Quaternion.identity, transform);
                break;

            case Paractice_Type.SMOKE_LEVEL_2:
                Instantiate(Smoke_level_2, pos, Quaternion.identity, transform);
                break;

            case Paractice_Type.SMOKE_LEVEL_3:
                Instantiate(Smoke_level_3, pos, Quaternion.identity, transform);
                break;

            case Paractice_Type.NORMAL:
                Instantiate(NoramalObj, pos, Quaternion.identity, null);

                break;
            case Paractice_Type.PERFECT:
                Instantiate(PerfectObj, pos, Quaternion.identity, null);
                break;
            case Paractice_Type.STAR:
                Instantiate(Star, pos, Quaternion.identity, null);
                break;
            case Paractice_Type.STAR2D:
                Instantiate(Star2D, pos, Quaternion.identity, null);
                break;
            case Paractice_Type.PUM:
                Instantiate(Pum, pos, Quaternion.identity, null);
                break;
            case Paractice_Type.PONG:
              
                Instantiate(Pong,pos, Quaternion.identity, null);
                break;
            case Paractice_Type.STATUS:
                var a = Instantiate(NoramalObj, pos, Quaternion.identity, null);
                a.transform.Find("Text").GetComponent<TextMesh>().text = s ;
                // a.transform.Find("Text").GetComponent<TextMesh>().color = Color.blue;
                a.transform.localScale = new Vector3(1, 1, 1);
                Vector3 pos_1 = a.transform.position;
                pos_1.z = -7;
                a.transform.position = pos_1;
                a.transform.GetComponent<Effect>().enabled = false;
                a.transform.GetComponent<Status>().enabled = true;
                if (s == "DOUBLE SKILL")
                {

                    a.transform.Find("Text").GetComponent<TextMesh>().color = new Color(72, 210, 255);
                    /*
                    Color color;
                    ColorUtility.TryParseHtmlString("48d2ff", out color);
                    a.transform.Find("Text").GetComponent<TextMesh>().color = color;
                    */

                }
                else if (s == "TRIPLE SKILL")
                {
                    //  a.transform.Find("Text").GetComponent<TextMesh>().color = Color.HSVToRGB(0, 0, 85);
                    Color color = new Color();
                    ColorUtility.TryParseHtmlString("adeb27", out color);
                    a.transform.Find("Text").GetComponent<TextMesh>().color = color;

                }
                break;
        
            case Paractice_Type.BASKET_COLL:
                Instantiate(Collison_Basket, pos, Quaternion.identity, null);
                break;
        }
    }


        public GameObject SpawnShoot(Vector2 pos,Quaternion quaternion,Transform trans)
    {
         GameObject game =   Instantiate(Shoot, pos, quaternion, trans);
        return game;
    }


        public void Set_System( Vector2 pos, string s, Transform trans,float time)
        {
        StartCoroutine(Spawn(pos, s, trans, time));
        }

        IEnumerator Spawn(Vector2 pos, string s, Transform trans, float time)
        {
            yield return new WaitForSeconds(time);
        //    switch (type)
         //   {
           //     case Paractice_Type.NORMAL:
                   var a = Instantiate(NoramalObj, pos, Quaternion.identity, null);
                a.transform.Find("Text").GetComponent<TextMesh>().text = s;
        if (s == "COOL")
        {
            a.transform.Find("Text").GetComponent<TextMesh>().color = Color.HSVToRGB(0, 75, 60);
        }
        else if (s == "GREAT")
        {
            a.transform.Find("Text").GetComponent<TextMesh>().color = Color.HSVToRGB(79, 83, 92);

        }
      
        else
        {
            a.transform.Find("Text").GetComponent<TextMesh>().color = Color.HSVToRGB(0, 0, 85);
        }
              //      break;
              //  case Paractice_Type.PERFECT:
             //       var a =  Instantiate(PerfectObj, pos, Quaternion.identity, null);
            //    a.transform.GetComponent<TextMesh>().text = s;
           //     break;
           //     case Paractice_Type.STAR:
          //        var a=   Instantiate(Star, pos, Quaternion.identity, null);
          //      a.transform.GetComponent<TextMesh>().text = s;
          //      break;
          //      case Paractice_Type.STAR2D:
         //           var a = Instantiate(Star2D, pos, Quaternion.identity, null);
          //      a.transform.GetComponent<TextMesh>().text = s;
      //          break;
        //
      //      }
        }


    public void getEffect(int sort,int level,Vector2 pos)
    {

        if (level == 0)
        {
            Instantiate(ListSortEffect_Level_1[sort], pos, Quaternion.identity, null);
        }
        if (level == 1)
        {
            Instantiate(ListSortEffect_Level_2[sort], pos, Quaternion.identity, null);
        }
        if(level == 2)
        {
            Instantiate(ListSortEffect_Level_3[sort], pos, Quaternion.identity, null);
        }

    }

   
    }

    



