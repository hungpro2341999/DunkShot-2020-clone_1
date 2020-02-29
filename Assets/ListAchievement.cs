using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]

public class ListAchievement 
{
  public   List<AchievementInfor> list = new List<AchievementInfor>();
  public   ListAchievement(List<AchievementInfor> list)
    {
        this.list = list;
    }
}
