using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiftTime : MonoBehaviour
{
    public Transform reward;
   
    public Transform Get_reward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameController.instance.getReward);
        if (reward.gameObject.active)
        {
          
          reward.transform.Find("TextTime").GetComponent<Text>().text = (int)GameController.instance.hour + ":" + (int)GameController.instance.mininue + ":" + (int)GameController.instance.second;
            reward.transform.Find("TextTime (1)").GetComponent<Text>().text = (int)GameController.instance.hour + ":" + (int)GameController.instance.mininue + ":" + (int)GameController.instance.second;
        }
        if (!GameController.instance.getReward)
        {
            Get_reward.gameObject.SetActive(false);
            reward.gameObject.SetActive(true);

        }
        else
        {
            Get_reward.gameObject.SetActive(true);
            reward.gameObject.SetActive(false);

        }
      
            
   }
    public void setTimeReward()
    {
       
       
            GameController.instance.Set_Time_Last();
        GameController.instance.InitTime();


    }
}
