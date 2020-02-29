using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMangager : MonoBehaviour
{


    public GameObject[] gameObjects1;

    public GameObject prefabgame;

    Transform useManager;
    int from;
    int to;
    public int max = 5;
    public static bool updateShop = true;             // Updater per Shop chance
    public int length;
    // Start is called before the first frame update
    void Start()
    {
        useManager = transform;


        from = 0;
        to = max;

        length = gameObjects1.Length;


    }

    // Update is called once per frame
    void Update()
    {
        if (updateShop)
        {
            updateShop = false;

            for (int i = from; i < from + max; i++)
            {
                GameObject card = gameObjects1[i];

            
            }



        }

    }

    public void Next()
    {
        if (to < length)
        {
            Destroy();
            from++;
            to++;
            updateShop = true;
        }
        else
        {
            // nothing
        }




    }

    public void Prev()
    {
        if (from > 0)
        {
            Destroy();
            from--;
            to--;
            updateShop = true;
        }
        else
        {
            // nothing
        }

    }

    public void Destroy()
    {
        Card[] card = transform.GetComponentsInChildren<Card>();

        for (int i = 0; i < max; i++)
        {

    

        }
    }
}
