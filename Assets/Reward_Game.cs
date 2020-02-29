using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward_Game : MonoBehaviour
{
    public static Reward_Game instance;
    public float Star;
    public float Score;
    private void Awake()
    {
        if(instance != null)
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
        
        Star = GameController.instance.Get_Star();   
    }
    public float GetStar()
    {

        return Star;
    }
    public void AddStar(int count)
    {
        Debug.Log(Star);
        Star += count;
    }
    public float GetScore()
    {
        return Score;
    }
    public void AddScore(int count)
    {
        Score += count;
    }
    private void Update()
    {
    //    Time.timeScale = 2;
      //  Debug.Log(Star);
    }
    public void rerotation()
    {
        StartCoroutine(ReRotation());
    }
    IEnumerator ReRotation()
    {
        yield return new WaitForSeconds(0.5f);



        float angle = transform.rotation.z;

        while (angle != 0)
        {
            angle = Mathf.MoveTowards(angle, 0, Time.deltaTime);

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            yield return new WaitForSeconds(0);
        }

    }
}
