using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{

   public  Transform offSound;

    public Transform onSound;

    public Transform OffVibration;
    public Transform OnVibration;
    // Start is called before the first frame update
    void Start()
    {
      
        if (GameController.instance.get_On_Off_Sound() == 1)
        {
            onSound.gameObject.SetActive(true);
            offSound.gameObject.SetActive(false);
         
          //  GameController.instance.On_Off_Sound_Game(true);
        }
        else
        {
            onSound.gameObject.SetActive(false);
            offSound.gameObject.SetActive(true);

        
           // GameController.instance.On_Off_Sound_Game(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Click_Sound()
    {
        if (GameController.instance.get_On_Off_Sound() == 1)
        {
            onSound.gameObject.SetActive(false);
            offSound.gameObject.SetActive(true);
            GameController.instance.set_On_Off_Sound(0);
     
            
        }
        else
        {
            onSound.gameObject.SetActive(true);
            offSound.gameObject.SetActive(false);
            GameController.instance.set_On_Off_Sound(1);
         
          
          
        }

    }
    
    public void Click_Vibration()
    {
        if (GameController.instance.get_On_Off_Vibration() == 1)
        {
            OffVibration.gameObject.SetActive(false);
            OnVibration.gameObject.SetActive(true);

            GameController.instance.set_On_Off_vibration(0);
        }
        else
        {
            OffVibration.gameObject.SetActive(true);
            OnVibration.gameObject.SetActive(false);

            GameController.instance.set_On_Off_vibration(1);
        }

    }
    public void Rate_Us()
    {
        ManagerAds.Ins.RateApp();

    }

    public void MoreGame()
    {
        ManagerAds.Ins.MoreGame();

    }
}

