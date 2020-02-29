 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallDisplay : MonoBehaviour
{
    public bool isBuyByAds = false;
    public Card card;
     
     
    // Start is called before the first frame update
    void Start()
    {
       
        if (isBuyByAds)
        {
            transform.Find("Ads").gameObject.SetActive(true);
            transform.Find("Cost").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Ads").gameObject.SetActive(false);
            transform.Find("Cost").gameObject.SetActive(true);
        }
         UpdateBall();
    }

    // Update is called once per frame
    void Update()
    {

        


    }
    public void UpdateBall()
    {
        transform.Find("Image").GetComponent<Image>().sprite = card.ballImage;
        if (!isBuyByAds)
        {
            transform.Find("Cost/Text").GetComponent<Text>().text = "" + card.cost;

            

            if (card.isBuying)
            {
                transform.Find("Cost").gameObject.SetActive(true);


            }
        }
        else
        {
            if (card.isBuying)
            {
                transform.Find("Ads/Text").GetComponent<Text>().text = "" + card.TimeAdsToGetBall+"/"+card.MaxTimeAdsToGetBall;
                transform.Find("Ads").gameObject.SetActive(true);
            }
        }

            if (card.isUse)
            {
                GetComponent<Button>().interactable = true;
            if (!isBuyByAds)
            {
                transform.Find("Cost").gameObject.SetActive(false);
            }
            else
            {
                transform.Find("Ads").gameObject.SetActive(false);
            }
                

                if (card.isUsing)
                {
                    transform.Find("Chose").gameObject.SetActive(true);
                }
                else
                {

                    transform.Find("Chose").gameObject.SetActive(false);
                }


            }
            else
            {
                GetComponent<Button>().interactable = false;
                transform.Find("Chose").gameObject.SetActive(false);
            }


        if (!isBuyByAds)
        {

            if (card.isBuying)
            {
                transform.Find("Cost").gameObject.SetActive(true);

                if (card.isUse)
                {
                    GetComponent<Button>().interactable = true;
                    transform.Find("Cost").gameObject.SetActive(false);

                    if (card.isUsing)
                    {
                        transform.Find("Chose").gameObject.SetActive(true);
                    }
                    else
                    {

                        transform.Find("Chose").gameObject.SetActive(false);
                    }
                }

            }
        }
        else
        {
            if (card.isBuying)
            {
                transform.Find("Ads").gameObject.SetActive(true);

                if (card.isUse)
                {
                    GetComponent<Button>().interactable = true;
                    transform.Find("Ads").gameObject.SetActive(false);

                    if (card.isUsing)
                    {
                        transform.Find("Chose").gameObject.SetActive(true);
                    }
                    else
                    {

                        transform.Find("Chose").gameObject.SetActive(false);
                    }
                }

            }
        }


       
    }

    public bool Chose()
    {
        Vector2 pos = Input.mousePosition;
        Card[] cardarr = GameController.instance.card;
        if (Vector2.Distance(GetComponent<RectTransform>().position, pos) < 120)
        {
          
        }


        return true;

       
    }

    public void Buy()
    {
       
        if (card.isBuying)
        {
            if (card.cost < GameController.instance.Get_Star())
            {
                AutioControl.instance.GetAudio("UnlockNewBall").Play();
                GameController.instance.earnMoney((int)card.cost);
                card.isBuying = false;
                card.isUse = true;

                Debug.Log("DA MUA : " + GameController.instance.Get_Star());
                transform.Find("Cost").gameObject.SetActive(false);
                GetComponent<Button>().interactable = true;

            }
        }
        //  UpdateBall();
      
           


        
        GameController.instance.Update_To_Infor();
       
    }

    public void Use()
    {
      
     

        if (card.isUse)
        {

          
           
            GameController.instance.getSprite(card);

            Card[] cardarr = GameController.instance.card;
            for (int i = 0; i < cardarr.Length; i++)
            {
                if (cardarr[i].id == card.id)
                {
                    cardarr[i].isUsing = true;
                    GameController.instance.chaceSound = !GameController.instance.chaceSound;
                    if (GameController.instance.chaceSound)
                    {

                        AutioControl.instance.GetAudio("changeball (1)").Play();
                    }
                    else
                    {
                        AutioControl.instance.GetAudio("changeball (2)").Play();
                    }
                 
                   

                }
                else
                {
                   
                    cardarr[i].isUsing = false;
                }

            }
            for (int i = 0; i < cardarr.Length; i++)
            {
                transform.parent.GetChild(i).GetComponent<BallDisplay>().UpdateBall();

            }

        }
      
        GameController.instance.Update_To_Infor();
        UpdateBall();
    }

    public void BuyByAds()
    {
        if (card.isBuying)
        {
            if (ManagerAds.Ins.IsRewardVideoAvailable())
            {
                ManagerAds.Ins.ShowRewardedVideo(success =>
                {

                    if (success)
                    {

                        card.TimeAdsToGetBall++;
                        transform.Find("Ads/Text").GetComponent<Text>().text = "" + card.TimeAdsToGetBall + "/" + card.MaxTimeAdsToGetBall;
                        if (card.TimeAdsToGetBall >= card.MaxTimeAdsToGetBall)
                        {
                            AutioControl.instance.GetAudio("UnlockNewBall").Play();

                            GameController.instance.earnMoney(0);
                            card.isBuying = false;
                            card.isUse = true;
                            transform.Find("Ads").gameObject.SetActive(false);
                            GetComponent<Button>().interactable = true;
                        }
                    }
                       
                  

                });
               
            }
            
        }
        GameController.instance.Update_To_Infor();
    }
  
    }
    
