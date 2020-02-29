using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelController : MonoBehaviour
{
   
    public float[] level;

    public float[] coolTimeLevel;

    public float[] levelCamera;

   public static LevelController instance= null;

    public int levelcurr = 0;
    public float Timelevelcurr = 0;


    public bool UpdateLv = false;

    public bool UpdateBoard = false;

    public bool UpdateCamera = false;

    public bool UpdateTime = false;
   public  float count = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        levelcurr = 0;

        coolTimeLevel = new float[] {25,25,25,25,25};

        level = new float[] { 1,1, 1, 1f, 1 };

        levelCamera = new float[] { 6.4f, 6.4f, 6.4f, 6.4f, 6.4f };
    }

    void Start()
    {

       

     

    }

    // Update is called once per frame
    void Update()
    {



    }

     public void UpdateLevel()
    {
        if (levelcurr < level.Length-1)
        {
            levelcurr++;

            UpdateLv = true;

            UpdateTime = true;

            UpdateBoard = true;


            UpdateCamera = true;
        }
        else
        {
            levelcurr = level.Length-1;

          
        }

       

     

       

    }

    public int getLevel()
    {
        return levelcurr;
    }

    public float getlevelBoard()
    {
        return level[levelcurr];
    }


    public float getLevelCurr()
    {

        

        return level[levelcurr];

     
    }

    public float getCoolTimeLevel()
    {
      
        count++;
        Timelevelcurr = Mathf.Clamp(coolTimeLevel[levelcurr] -= 0.8f * count, 15, 30);
        return  Timelevelcurr;

      
    }

    public float getLevelCamera()
    {

        return levelCamera[levelcurr];

    }

}
