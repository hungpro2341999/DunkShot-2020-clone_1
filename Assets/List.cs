using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class List 
{
    public List<PlayerInfor> ListPlayer = new List<PlayerInfor>();

    public List<BallDetail> listShop = new List<BallDetail>();

    public List(List<PlayerInfor> players)
    {
       ListPlayer = players;

    }
    public List(List<BallDetail> players)
    {
        listShop = players;

    }
}
