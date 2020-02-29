using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsToContine : MonoBehaviour
{
    public Text Star;
    public float MaxTime;
    public Image ImgTimeLife;
    float Time = 0;
    bool canSee = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Star.text = GameController.instance.Get_Star().ToString();
        Time += UnityEngine.Time.deltaTime;
        /*
        float ratio = Time / MaxTime;
        if (ratio < 1)
        {
        //    Debug.Log(ratio);
            ImgTimeLife.fillAmount = ratio;
            canSee = true;
        }
        else
        {
            if (canSee)
            {
                ManagerAds.Ins.ShowInterstitial();
                canSee = false;
            }
         

        }
        */
    }
}
