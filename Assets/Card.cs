using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Ball",menuName="Ball/Ball_1")]
public class Card : ScriptableObject
{
    public Sprite Spriteface;

    public int id;

    public float cost;

    public Sprite ballImage;

    public bool isBuying = true;

    public bool isUse = false;

    public bool isUsing = false;

    public Sprite[] bas;
    public Sprite[] bas_mode_1;
    public Sprite Gun;

    public Sprite BgMode3_0;
    public Sprite BgMode3_1;

    public int TimeAdsToGetBall;

    public int MaxTimeAdsToGetBall;
    public bool BuyByAds = false;
    public  int Sort_Effect;

}
