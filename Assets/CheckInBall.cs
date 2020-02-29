using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInBall : MonoBehaviour
{
    public Transform PointEffect;
    public const string point0 = "POINT_5";
    public const string point1 = "POINT_6";
    public Dictionary<string, int> Check_Ball = new Dictionary<string, int>();
    public int key;
    // Start is called before the first frame update
    void Start()
    {
        Init_Key();
    }
    public bool Result()
    {
        if (Check_Ball[point1] == 1 || Check_Ball[point0] == 1)
        {
            return false;
        }
        return true;

    }
    public void ResetKey()
    {
        Check_Ball[point0] = 0;
        Check_Ball[point1] = 0;

    }
    public void Init_Key()
    {
        Check_Ball.Add(point0, 0);
        Check_Ball.Add(point1, 0);



    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.layer == 14)
        {
            collision.gameObject.layer = 13;
            collision.gameObject.transform.parent = transform.parent.parent;
            JointBoard(collision.gameObject.GetComponent<BoardControl>());
        } 
    }
    IEnumerator ResetVelocity(Rigidbody2D Body)
    {
        
        yield return new WaitForSeconds(0.3f);
        Body.velocity = Vector2.zero;
        

    }
    public void JointBoard(BoardControl Player)
    {
        transform.parent.parent.GetComponent<Board_Mode_1>().Reset_Rotation();
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
        Player.isDone = false;
        if (key != Player.Key)
        {
            LevelController.instance.UpdateTime = true;
            if (Result())
            {
                if (Check_Ball[point0] == 0 && Check_Ball[point1] == 0)
                {
                    SpawnEffect.instance.Set_System(PointEffect.position, "+PERFECT X" + BoardControl.instance.CountPerfect, null, 0);
                    SpawnEffect.instance.Set_System(PointEffect.position, "+" + BoardControl.instance.CountPerfect * 3, null, 0.5f);
                    int x = BoardControl.instance.CountPerfect;

                    BoardControl.instance.TakeScore(Paractice_Type.PERFECT);

                }

            }

            else
            {
                if ((Check_Ball[point0] + Check_Ball[point1]) == 1)
                {
                    SpawnEffect.instance.Set_System(PointEffect.position, "COOL", null, 0);
                    SpawnEffect.instance.Set_System(PointEffect.position, "+2", null, 0.5f);
                    BoardControl.instance.TakeScore(Paractice_Type.NORMAL);
                    BoardControl.instance.AddScore(1);

                }
                else
                {
                    int x = BoardControl.instance.CountPerfect;
                    BoardControl.instance.TakeScore(Paractice_Type.NORMAL);


                    SpawnEffect.instance.Set_System(PointEffect.position, "+1", null, 0.5f);


                }



            }
           transform.parent.parent.GetComponent<Board_Mode_1>().Reset_Rotation();
            Debug.Log("Get key");
            Player.NextKey();
            SpawnerCtrl.instante.chanceBoard = true;
            MoveFollowPlayer.instance.new_Collider_Camera();
        }

    }
   
}
