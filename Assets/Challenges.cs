using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Challenges", menuName = "Challenges/Challenges_1")]
public class Challenges : ScriptableObject
{
    public int id;  
    public Sprite Image;
    public string label;
    public string detail;
    public float maxBar = 100;
    public float isTake = 0;
    public int[] level;
    public int[] reward;
    public int levelCurr = 0;
    public bool[] TakeRewardGame = new bool[5];
    public bool isCompleQuest = false;
    
    public bool isComplete = false;
   
    public void AddAmount(int amount)
    {
        if (levelCurr < level.Length)
        {
            if (isTake <= level[levelCurr])
            {
                isTake += amount;


            }
           
        }
       

    }
    public void SetValue(int valuse)
    {
        isTake = valuse;  

    }


}
