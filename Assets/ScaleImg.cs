using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleImg : MonoBehaviour
{
    public Transform Original;
    public Transform Direct;
    public float diss = 0;
    bool UpdatePoint = false;
    // Start is called before the first frame update
    void Start()
    {
        Original.position = Direct.position;
        StartCoroutine(Reize());
    }
    private void LateUpdate()
    {
        if (!UpdatePoint)
        {
            transform.localScale = Vector3.one;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        if(!GameController.instance.GameOver && !GameController.instance.GamePause)
        {
            if (BoardControl.instance != null)
            {
                if (!BoardControl.instance.Click_1)
                {
                    float dis = Vector2.Distance(Original.position, Direct.position);
                    //        Debug.Log(dis);
                    float Scaley = 1 + dis;

                    Vector2 scale = transform.localScale;
                    scale.y = Scaley;
                    scale.y = Mathf.Clamp(scale.y, 1f, 3f);
                    //    Debug.Log(scale.y);
                    transform.localScale = scale;
                }
            }
            else
            {

                float dis = Vector2.Distance(Original.position, Direct.position);
                //        Debug.Log(dis);
                float Scaley = 1 + dis;

                Vector2 scale = transform.localScale;
                scale.y = Scaley;
                scale.y = Mathf.Clamp(scale.y, 1f, 3f);
                //    Debug.Log(scale.y);
                transform.localScale = scale;



            }
           

        }
        
 

        
    }
   
    IEnumerator Reize()
    {
        UpdatePoint = false;
        yield return new WaitForSeconds(0.6f);
        UpdatePoint = true;
    }

}
