using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HightScoreTable : MonoBehaviour
{
    
    Transform EntryPlayer;

    Transform ScorePlayer;

    float height = 60;

    List<PlayerInfor> listPlayer;

    List<Transform> transforms;


    // Start is called before the first frame update

    private void Awake()
    {
  
    }
    void Start()
    {

        listPlayer = GameController.instance.Load();
        if (listPlayer == null)
        {
            Debug.Log("NULL PLAYER");
        }

        transforms = new List<Transform>();






       EntryPlayer = transform.Find("EntryTemplate");

        ScorePlayer = EntryPlayer.Find("ScorePlayer");

        ScorePlayer.gameObject.SetActive(false);

      



        foreach(PlayerInfor player in listPlayer)
        {
            createListHightScore(EntryPlayer, ScorePlayer, transforms, player);
        }

      






    }
   

    private void createListHightScore(Transform EntryPlayer, Transform ScorePlayer, List<Transform> listTransform, PlayerInfor scoreEntry)
    {






        int rank = listTransform.Count + 1;


        Transform transHightScore = Instantiate(ScorePlayer, EntryPlayer);

        RectTransform rectTransform = transHightScore.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(0, -height * listTransform.Count);

        transHightScore.gameObject.SetActive(true);

        transHightScore.Find("Pos").GetComponent<Text>().text = "" + rank;



        switch (rank)
        {

            case 1:
                transHightScore.Find("Pos").GetComponent<Text>().text = "" + "1ST";
                break;
            case 2:
                transHightScore.Find("Pos").GetComponent<Text>().text = "" + "2ND";
                break;
            case 3:
                transHightScore.Find("Pos").GetComponent<Text>().text = "" + "3ND";
                break;
            default:
                transHightScore.Find("Pos").GetComponent<Text>().text = rank + "" + "ND";
                break;




        }

        transHightScore.Find("Name").GetComponent<Text>().text = scoreEntry.name;

        transHightScore.Find("Score").GetComponent<Text>().text = ""+scoreEntry.score;

        listTransform.Add(ScorePlayer);








    }

    // Update is called once per frame
    void Update()
    {

    }
   
   
}
