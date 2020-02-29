using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortBall : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        List<Transform> trans = new List<Transform>();
        List<Transform> sort1 = new List<Transform>();
        List<Transform> sort2 = new List<Transform>();
        List<Transform> sort3 = new List<Transform>();
        List<Transform> sort4 = new List<Transform>();
        List<Transform> sort5 = new List<Transform>();


        int count;
      
        
        count = transform.childCount-1;
        Debug.Log("SORT ____________  " + count);
        for (int i = 0; i < count; i++)
        {
            trans.Add(transform.GetChild(0));
        }
        for (int i = 0; i < trans.Count; i++)
        {
            if (trans[i].GetComponent<BallDisplay>().card.Sort_Effect == 1)
            {
                sort1.Add(trans[i]);
            }
            if (trans[i].GetComponent<BallDisplay>().card.Sort_Effect == 2)
            {
                sort2.Add(trans[i]);

            }
            if (trans[i].GetComponent<BallDisplay>().card.Sort_Effect == 3)
            {
                sort3.Add(trans[i]);

            }
            if (trans[i].GetComponent<BallDisplay>().card.Sort_Effect == 4)
            {
                sort4.Add(trans[i]);

               

            }
            if (trans[i].GetComponent<BallDisplay>().card.Sort_Effect == 5)
            {
                sort5.Add(trans[i]);

            }
        }
        for(int i = 0; i < sort5.Count; i++)
        {
            sort5[i].SetSiblingIndex(count);
        }
        for (int i = 0; i < sort4.Count; i++)
        {
            sort4[i].SetSiblingIndex(count);
        }
        for (int i = 0; i < sort3.Count; i++)
        {
            sort3[i].SetSiblingIndex(count);
        }
        for (int i = 0; i < sort2.Count; i++)
        {
            sort2[i].SetSiblingIndex(count);
        }
        for (int i = 0; i < sort1.Count; i++)
        {
            sort1[i].SetSiblingIndex(count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Sort();
        }
    }
    public void Sort()
    {
        List<Transform> trans = new List<Transform>();
        List<Transform> sort1 = new List<Transform>();
        List<Transform> sort2 = new List<Transform>();
        List<Transform> sort3 = new List<Transform>();
        List<Transform> sort4 = new List<Transform>();
        List<Transform> sort5 = new List<Transform>();


        int count;


        count = transform.childCount - 2;
        Debug.Log("SORT ____________  " + count);
        for (int i = 0; i < count; i++)
        {
            trans.Add(transform.GetChild(0));
        }
        for (int i = 0; i < trans.Count; i++)
        {
            if (trans[i].GetComponent<BallDisplay>().card.Sort_Effect == 1)
            {
                sort1.Add(trans[i]);
            }
            if (trans[i].GetComponent<BallDisplay>().card.Sort_Effect == 2)
            {
                sort2.Add(trans[i]);

            }
            if (trans[i].GetComponent<BallDisplay>().card.Sort_Effect == 3)
            {
                sort3.Add(trans[i]);

            }
            if (trans[i].GetComponent<BallDisplay>().card.Sort_Effect == 4)
            {
                sort4.Add(trans[i]);



            }
            if (trans[i].GetComponent<BallDisplay>().card.Sort_Effect == 5)
            {
                sort5.Add(trans[i]);

            }
        }
        for (int i = 0; i < sort5.Count; i++)
        {
            sort5[i].SetSiblingIndex(count);
        }
        for (int i = 0; i < sort4.Count; i++)
        {
            sort4[i].SetSiblingIndex(count);
        }
        for (int i = 0; i < sort3.Count; i++)
        {
            sort3[i].SetSiblingIndex(count);
        }
        for (int i = 0; i < sort2.Count; i++)
        {
            sort2[i].SetSiblingIndex(count);
        }
        for (int i = 0; i < sort1.Count; i++)
        {
            sort1[i].SetSiblingIndex(count);
        }

    }
}
