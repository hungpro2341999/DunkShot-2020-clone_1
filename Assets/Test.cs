using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public GAME_MODE_TYPE GAME_MODE_TYPE;
    public SkeletonGraphic skeleton;
    [SpineAnimation]
    public string idle, tapAnim;

    private void Start()
    {
        skeleton.AnimationState.Complete += OnComplete;
        skeleton.AnimationState.SetAnimation(0, idle, true);
    }

    private void OnComplete(TrackEntry trackEntry)
    {
        var animName = trackEntry.Animation.Name;
        if(animName == tapAnim)
        {

            switch (GAME_MODE_TYPE)
        {
            case GAME_MODE_TYPE.MODE_1:
                ScreenStart.instance.StartMode_1();
                break;
            case GAME_MODE_TYPE.MODE_2:
                ScreenStart.instance.StartMode_2();
                break;
            case GAME_MODE_TYPE.MODE_3:
                ScreenStart.instance.StartMode_3();
                break;

                  
        }
    }

        }
        

   
    
    public void Tap()
    {
     //   Debug.Log("In");
        skeleton.AnimationState.ClearTracks();
        skeleton.AnimationState.SetAnimation(0, tapAnim, false);


    }
    

}
