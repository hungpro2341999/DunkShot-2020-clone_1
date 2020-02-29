using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




[System.Serializable]
public class PlayerInfor
{
    public string name;
    public int score;

    public PlayerInfor(string name,int score)
    {
        this.name = name;

        this.score = score;

    }
}
