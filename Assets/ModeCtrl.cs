using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeCtrl : MonoBehaviour
{

    public static ModeCtrl instance;
    public MODE_GAME[] MODE_GAME;

    // Start is called before the first frame update
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
        
        Open_Mode(GameController.instance.Game_Type);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void Open_Mode(GAME_MODE_TYPE game)
    {
        for(int i = 0; i < MODE_GAME.Length; i++)
        {
            if (game == MODE_GAME[i].type_mode)
            {
                MODE_GAME[i].Open_Mode();
                Time.timeScale = 2;
                continue;
            }
            else
            {
                MODE_GAME[i].Close_Mode();

            }
        }
    }
    public Transform getWorldSpace()
    {
        Transform trans = null;
        switch (GameController.instance.Game_Type)
        {
            case GAME_MODE_TYPE.MODE_1:
                trans =   MODE_GAME[0].transform;
                break;
            case GAME_MODE_TYPE.MODE_2:
                trans = MODE_GAME[1].transform;
                break;
            case GAME_MODE_TYPE.MODE_3:
                trans =  MODE_GAME[2].transform;
                break;
            case GAME_MODE_TYPE.MODE_4:
                trans = MODE_GAME[3].transform;
                break;
        }
        return trans;
    }
}
