using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]


public class ListInfoBall
{
    public List<BallInfor> list = new List<BallInfor>();
   public  ListInfoBall(List<BallInfor> balls)
    {
        list = balls;
    }

}
   
    

