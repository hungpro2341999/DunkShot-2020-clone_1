using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class MODE_GAME : MonoBehaviour
{
    // Start is called before the first frame update
   
    public Transform trs;
    public GameObject mode;
    public GAME_MODE_TYPE type_mode;
    public delegate void OnReset();
    public delegate void OnContinue();
   
    public static event OnReset Reset_Mode_1;
    public static event OnReset Reset_Mode_2;
    public static event OnReset Reset_Mode_3;
    public static event OnContinue Continue_Mode_1;
    public static event OnContinue Continue_Mode_2;
    public static event OnContinue Continue_Mode_3;
  

    void Start()
    {
        trs = gameObject.transform;
        mode = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void Close_Mode()
    {
        gameObject.SetActive(false);
    }
 
    public void Open_Mode()
    {
        gameObject.SetActive(true);
    }
    public void ContineMode()
    {
        if (type_mode == GAME_MODE_TYPE.MODE_1)
            Continue_Mode_1();
        if (type_mode == GAME_MODE_TYPE.MODE_2)
        {
            Continue_Mode_2();
        }
        if (type_mode == GAME_MODE_TYPE.MODE_3)
        {

            Continue_Mode_3();
        }

    }
    public void RestMode()
    {
        if(type_mode == GAME_MODE_TYPE.MODE_1)
                   Reset_Mode_1();
        if (type_mode == GAME_MODE_TYPE.MODE_2)
        {
            Reset_Mode_2();
        }
        if(type_mode == GAME_MODE_TYPE.MODE_3)
        {

            Reset_Mode_3();
        }
    }
   
  
}
