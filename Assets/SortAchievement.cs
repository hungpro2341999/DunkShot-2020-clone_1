using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortAchievement : MonoBehaviour
{
    bool Up = false;
    public static SortAchievement instance = null;
    // Start is called before the first frame update
    void Awake()
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
    private void Start()
    {
       
    }
    private void Update()
    {
        if (!Up)
        {
            Sort();
        }
        else
        {
            Up = true;
        }
            
        
    }


    public  void Sort()
    {
        List<Transform> game = new List<Transform>();
        int count = transform.childCount;
        Debug.Log("INDEX : " + count);
        for (int i = 0; i < count; i++)
        {
            Transform trans = transform.GetChild(i);


            if (trans.GetComponent<ChallengesDispaly>().CompleteQuest)
            {
                game.Add(trans);
            }
        }
        for(int i = 0; i < game.Count; i++)
        {
            game[i].SetSiblingIndex(count - 1);
        }
    }
}
