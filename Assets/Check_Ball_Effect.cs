using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Ball_Effect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            collision.gameObject.layer = 13;
            collision.gameObject.transform.parent = transform.parent.parent;
            JointBoard(collision.gameObject.GetComponent<Ball_Effect>());
        }
    }
    IEnumerator ResetVelocity(Rigidbody2D Body)
    {


        yield return new WaitForSeconds(0);

    }
    public void JointBoard(Ball_Effect  Player)
    {
        // Player.Body.velocity = Player.Body.velocity / 3;
        Transform trans = transform.parent.parent;
        Player.BasketNeedHold = trans.Find("Basket");
        Player.Trigger = trans.Find("Basket/Trigger");
        Player.Hool = trans.Find("Hool");
        Player.ImgScale = trans.Find("ImgScale");
        Player.Ball = Player.transform;
        Player.PosBall = trans.Find("PosBall");
        Player.PosOriginal = trans.Find("PosBasketOriginal");
        Player.PosLastBall = trans.Find("Pos");
        Player.HoldBasket = trans;
        //     Player.EnableMode(true);
        StartCoroutine(ResetVelocity(Player.Body));

       
    }
}
