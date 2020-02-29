using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopManager : MonoBehaviour
{
    public int[] BallBuyByAds;
   
 
    public List<BallDetail> listBallDetail;

    public Text text;

    public Text Star;

    public GameObject ball;

    public Transform parent;

    public Text text_Star;

    public Transform skill;

    public Transform backGround;

    public GameObject objBG;

    public Transform backGround_1;

    public Transform Skill;
        
    public Transform BackGround;

    public Sprite PressSkill;
    public Sprite OutSkill;
    public Sprite PressBackGround;
    public Sprite OutBackGround;
    /*
    public GameObject[] gameObjects1;

    Transform useManager;
    int from;
    int to;
    public int max = 5;
    public static bool updateShop = true;             // Updater per Shop chance
    public int length;
    // Start is called before the first frame update
    void Start()
    {
        useManager = transform;

       
        from = 0;
        to = max;

        length = gameObjects1.Length;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (updateShop)
        {
            updateShop = false;

            for (int i = from; i < from+max; i++)
            {
                GameObject card = gameObjects1[i];

                Instantiate(gameObjects1[i], transform);
            }

         

        }
       
    }

  public  void Next()
    {
        if (to < length)
        {
            Destroy();
            from++;
            to++;
            updateShop = true;
        }
        else
        {
            // nothing
        }
       
        
        

    }

   public  void Prev()
    {
        if (from  > 0)
        {
            Destroy();
            from--;
            to--;
            updateShop = true;
        }
        else
        {
            // nothing
        }

    }

    public void Destroy()
    {
        Card[] card = transform.GetComponentsInChildren<Card>(); 

        for (int i = 0; i < max; i++)
        {

           
         
        }
    }
    */


    public void Update()
    {
        if(Star!=null)
        Star.text = GameController.instance.Get_Star().ToString(); 
    }

    public void Start()
    {
        for (int j = 0; j < BallBuyByAds.Length; j++)
        {
            Debug.Log("ADS __ _" + BallBuyByAds[j]);

        }
            Application.targetFrameRate = 60;
        UpdateShop(GameController.instance.card,GameController.instance.backGroundCard);

        skill.gameObject.SetActive(true);
        backGround.gameObject.SetActive(false);

    }

    public void UpdateShop(Card[] card1,Card[] cards2)
    {
        Debug.Log("So Luong : " + card1.Length);
        for(int i = 0; i < card1.Length; i++)
        {
            bool isAds = false;
            ball.GetComponent<BallDisplay>().card = card1[i];
            for(int j = 0; j < BallBuyByAds.Length; j++)
            {
                Debug.Log("Count ADS : " + BallBuyByAds.Length);
                if (!isAds)
                {
                    if ((card1[i].id - 1) == BallBuyByAds[j])
                    {
                        ball.GetComponent<BallDisplay>().isBuyByAds = true;
                        card1[i].MaxTimeAdsToGetBall = 5;
                    
                        Debug.Log("ADD ADS : "+ BallBuyByAds[j]+"  "+ card1[i].id);
                        isAds = true;
                       
                    }
                    else
                    {
                        ball.GetComponent<BallDisplay>().isBuyByAds = false;

                    }
                }
              
            
            }
            Instantiate(ball, parent);


        }
        for(int i = 0; i < cards2.Length; i++)
        {
            objBG.GetComponent<BackGroundDisPlay>().card = cards2[i];

            Instantiate(objBG, backGround_1);
        }
       
    }
    
   
    public void BackGroundIsClick()
    {
        skill.gameObject.SetActive(false);
        backGround.gameObject.SetActive(true);
        Skill.GetComponent<Image>().sprite = OutSkill;
        BackGround.GetComponent<Image>().sprite = PressBackGround;
    }
    public void SkillIsClick()
    {
        skill.gameObject.SetActive(true);
        backGround.gameObject.SetActive(false);
        Skill.GetComponent<Image>().sprite = PressSkill ;
        BackGround.GetComponent<Image>().sprite = OutBackGround;
    }
    public void Get_Reward()
    {
        
        ManagerAds.Ins.ShowRewardedVideo(success =>

        {
            if (success)
            {
                int star = GameController.instance.Get_Star();
                star += 25;
                GameController.instance.Save_Star(star);

            }


        }
        );

    }
    
}
