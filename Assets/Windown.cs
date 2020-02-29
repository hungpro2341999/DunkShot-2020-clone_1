using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Windown : MonoBehaviour
{
    public TypeWindown type;
     public Animator anim;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(type == TypeWindown.Start)
        {

        }
        if (gameObject.name == "Screen Start")
        {
            transform.Find("PointStar/Text").GetComponent<Text>().text = GameController.instance.Get_Star().ToString();
        }
    }
        
    public void CloseWindow()
    {
        if (anim == null || type == TypeWindown.Mode)
        {
            gameObject.SetActive(false);
         

        }
        else
        {

            Close();

        }
  
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);

        if (anim != null)
        {
            Open();

        }
     
    }

    public void Open()
    {
        if(anim!=null)
        anim.SetBool("Open", true);
    }
    public void Close()
    {
        
        if (anim != null)
            anim.SetBool("Open", false);
    }
    public void CloseWindow_Ver1()
    {
        Debug.Log(transform.gameObject.name);
        switch (type)
        {
            case TypeWindown.Mode:
                gameObject.SetActive(false);
                break;

            case TypeWindown.Shop:
                ScreenStart.instance.CloseWIndow();
                gameObject.SetActive(false);
                break;
            case TypeWindown.Chanlegend:
                ScreenStart.instance.CloseWIndow();
                gameObject.SetActive(false);
                break;
            case TypeWindown.Rate:
                ScreenStart.instance.CloseWIndow();
                gameObject.SetActive(false);
                break;
            case TypeWindown.ShopPlay:
                gameObject.SetActive(false);
                break;
            case TypeWindown.SettingPlay:
                gameObject.SetActive(false);
                break;
            default :
                gameObject.SetActive(false);
                break;

        }
    
    }


}
