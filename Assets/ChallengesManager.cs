using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChallengesManager : MonoBehaviour
{

    public GameObject Chanlegends;
    public Transform trs;
    public Text PointStar;
    bool Update_1 = false;
    // Start is called before the first frame update
    void Start()
    {
        Update_Chanlegend(GameController.instance.ChallengesCard);
      
    }

    // Update is called once per frame
    void Update()
    {
       
        if (transform.Find("BackGround/PointStar/Text") != null)
        {
            transform.Find("BackGround/PointStar/Text").GetComponent<Text>().text = GameController.instance.Get_Star().ToString();
        }
        

       
    }
    private void FixedUpdate()
    {
        if (!Update_1)
        {
            SortAchievement.instance.Sort();
            Update_1 = true;
        }
    }

    public void Update_Chanlegend(Challenges[] challenges)
    {
        for(int i = 0; i < challenges.Length; i++)
        {
            Chanlegends.GetComponent<ChallengesDispaly>().Challenges = challenges[i];
           GameObject game =   Instantiate(Chanlegends, trs);
            if(i==10 || i==11 || i == 12)
            {
                game.SetActive(false);

            }
          

        }
       
    }
}
