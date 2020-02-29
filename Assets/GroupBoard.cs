using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupBoard : MonoBehaviour
{

    
    public  List<GameObject> Group_Board = new List<GameObject>();
    public int CountBall;
    public static GroupBoard instace;
    public int count =0;
    public Transform PositionEffect;
    public float numRemove;

    public List<int> CheckTRipeDoubleBall = new List<int>();
    // Start is called before the first frame update
    public void On_reset_mode_2()
    {
        GroupBoard.instace.Clear();
    }

    private void Awake()
    {
        
        if (instace != null)
        {

            Destroy(gameObject);
        }
        else
        {

            instace = this;
        }
    }
    void Start()
    {
        MODE_GAME.Reset_Mode_2 += On_reset_mode_2;
        numRemove = Group_Board.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.instance.GameOver && !(GameController.instance.GamePause))
        {
            CountBall = Gun.instance.BulletInShoot.Count;

            if (CountBall == 0 && Group_Board.Count !=0)
            {
                Bullet[] game = GameObject.FindObjectsOfType<Bullet>();
                if(CheckGameOver(game))
                {

                    GameObject.Find("ScreenGameplay").GetComponent<ScreenGamePlay>().GameOverWindow();
                }
            }
            else
                
            {
                if (Group_Board.Count == 0)
                {
                    Bullet[] game = GameObject.FindObjectsOfType<Bullet>();
                    if (!CheckGameOver(game))
                    {
                        Gun.instance.ChanceDirect();
                        Gun.instance.RestBullet(5);
                        SpawnerCtrl.instante.chanceBoard = true;
                        LevelController.instance.UpdateLevel();
                    }
                }

            }
            foreach(var bullet in Gun.instance.isShotting)
            {

                if (bullet.countGolbal == 2)
                {
                    if (bullet.DoubleBall)
                    {
                        SpawnEffect.instance.Set_System(Paractice_Type.STATUS, Camera.main.transform.position, "DOUBLE SKILL", null);
                        Ball_Player_2.instance.TakeScore(Paractice_Type.DOUBLE);
                        bullet.DoubleBall = false;

                    }

                }
                else if (bullet.countGolbal == 3)
                {
                    if (bullet.TripleBall)
                    {
                        SpawnEffect.instance.Set_System(Paractice_Type.STATUS, Camera.main.transform.position, "TRIPLE SKILL", null);
                        Ball_Player_2.instance.TakeScore(Paractice_Type.TRIPLE);
                        bullet.TripleBall = false;
                    }
                }
                
            }


            
        }
         

    }
    public void CheckEffect()
    {
       
    }
    public void Clear()
    {
        Group_Board.Clear();
    }
    public void RemoveBoard(GameObject board)
    {
        Group_Board.Remove(board);
    }
    public void AddBoard(GameObject board)
    {
        Group_Board.Add(board);
    }
    public bool CheckGameOver(Bullet[] bullets)
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].isOut)
            {
                return false;
            }



        }
        return true;


    }

}
