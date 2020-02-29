using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChallengesDispaly : MonoBehaviour
{
    public int id;
    public Challenges Challenges;
    public float time;

    public float width;
    public float timelife;
    public RectTransform rect;
    public bool isComplete = false;
    public bool isInteractible = false;
    public bool isTakeReward = false;
    public bool[] TakeRewardGame;
    public int indexReward=0;
    public bool CompleteQuest = false;
    public Transform completeQuest;
    public Transform Bar;
    public Image image;
    private void Start()
    {

        completeQuest = transform.Find("CompleteQuest");
        transform.Find("Image").GetComponent<Image>().sprite = Challenges.Image;
        transform.Find("Label").GetComponent<Text>().text = Challenges.label;
        transform.Find("Detail").GetComponent<Text>().text = Challenges.detail;
        Bar = transform.Find("Bar");
  
        width = transform.Find("Bar/BGTime").GetComponent<RectTransform>().rect.width;
      
       
        rect = transform.Find("Bar/LifeTime").GetComponent<RectTransform>();


        CheckReward();



    }
    
    public void CheckReward()
    {
      
        indexReward = -1;

        for (int i = 0; i < 5; i++)
        {
            if (Challenges.TakeRewardGame[i])
            {

                indexReward++;
            }


        }
        
        if(indexReward == 5)
        {

            CompleteQuest = true;
        }
        if (indexReward == -1)
        {
            indexReward = 0;
        }else if (indexReward >= 5)
        {
            indexReward = 4;
            if (!CompleteQuest)
            Challenges.isCompleQuest = true;
            CompleteQuest = true;
          
        }

    }

    private void Update()
    {
        if (!Challenges.isCompleQuest)
        {
            if (Challenges.levelCurr >= 5)
            {
                Challenges.levelCurr = 4;

            }
            if (Challenges.id == 1)
                Debug.Log(indexReward);
            transform.Find("Bar/Text").GetComponent<Text>().text = Challenges.isTake.ToString() + "/" + Challenges.level[Challenges.levelCurr];
            transform.Find("Text").GetComponent<Text>().text = Challenges.reward[indexReward].ToString();

            timelife = Challenges.isTake;
            // Debug.Log(Challenges.id);
            time = Challenges.level[Challenges.levelCurr];
            float ratio = timelife / time;
            if (ratio >= 1)
            {
                ratio = 1;
                if (!Challenges.isCompleQuest)

                {
                    isComplete = true;


                }
                else
                {

                    isComplete = false;

                }


            }


            if (!isComplete)
            {


                image.fillAmount = ratio;
                Debug.Log(ratio);
             //   rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width_2);
                transform.Find("Not_Complete").gameObject.SetActive(true);
                transform.Find("ComPlete").gameObject.SetActive(false);

            }
            else
            {
                //rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
                image.fillAmount = 1;
            }
            if (!Challenges.isCompleQuest)
            {
                if (isComplete)
                {

                    if (Challenges.TakeRewardGame[indexReward] || indexReward == 0)
                    {
                        transform.Find("Not_Complete").gameObject.SetActive(false);
                        transform.Find("ComPlete").gameObject.SetActive(true);


                    }

                }


            }
        }
        else
        {
            CompleteQuest = true;
            Bar.gameObject.SetActive(false);
            completeQuest.gameObject.SetActive(true);
        }
        if (Challenges.id == 23)
        {
            Debug.Log("LEVEL :" +Mathf.Clamp(Challenges.levelCurr, 0, 4));
            transform.Find("Detail").GetComponent<Text>().text = "Collect " + Challenges.reward[Mathf.Clamp(Challenges.levelCurr, 0, 4)] + " Stars";
        }


    }
    public void TakeReward()
    {
     
       
       
        if (indexReward == 0)
        {
            int amount = GameController.instance.Get_Star() + Challenges.reward[indexReward];
            GameController.instance.AddStarGolabal(Challenges.reward[indexReward]);
            GameController.instance.AddStarGolabal(1);
            GameController.instance.Save_Star(amount);
            Challenges.levelCurr++;
            Challenges.TakeRewardGame[0] = true;
            Challenges.TakeRewardGame[1] = true;
            isComplete = false;
            CheckReward();

        }
        else if (Challenges.TakeRewardGame[indexReward] && indexReward<5)
        {
          
            int amount = GameController.instance.Get_Star() + Challenges.reward[indexReward];
            GameController.instance.AddStarGolabal(Challenges.reward[indexReward]);
            GameController.instance.AddStarGolabal(1);
            GameController.instance.Save_Star(amount);

            
            if( indexReward <= 4)
            {
                Challenges.levelCurr++;
                     indexReward++;
                if (indexReward >=5)
                {
                    SortAchievement.instance.Sort();
                    indexReward = 4;
                    isComplete = false;
                    
                    Challenges.isCompleQuest = true;
                    CheckReward();
                  
                    GameController.instance.Update_To_Infor_Achieve();
                    return;
                   
                }


                Challenges.TakeRewardGame[indexReward] = true;
                    isComplete = false;
                   
                
              
              
              
              
            }
            else
            {
              
                isComplete = false;
            }
            CheckReward();

        }



        GameController.instance.Update_To_Infor_Achieve();
    }
   
}
