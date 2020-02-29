using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAchievement : MonoBehaviour
{
    public Challenges[] card;
    int count10;
    int count5;
    int countWall;
    int countFulLForce;
    int countGolbal = 50;
    int countGolbal100 = 100;
    bool UpdateGolbalCount100 = true;
    int CountdoubleWall;
    int CountdoubleWall_1;
    bool UpdateDoubleWall_1 = true;
    bool UpdateDoubleWall = true;
    bool UpdatePerfectCount5 = true;
    bool UpdatePerfectCount10 = true;
    bool UpdateWalltCount = true;
    bool UpdateFullForcetCount = true;
    bool UpdateGolbalCount = true;
    bool UpdateDoubleSkill = true;
    bool UpdateTripleSkill = true;
    float Star = 0;

    // Start is called before the first frame update
    public void Rest()
    {
        countGolbal100 = 100;
        UpdateGolbalCount100 = true;

        UpdateDoubleWall_1 = true;
        CountdoubleWall_1 =0;
        CountdoubleWall = 0;
        countWall = 0;
        count5 = 0;
        count10 = 0;
        countFulLForce = 0;
          countGolbal = 50;
        UpdateDoubleWall = true;
       UpdateGolbalCount = true;
        UpdateTripleSkill = true;
        UpdateDoubleSkill = true;
        UpdateFullForcetCount = true;
        UpdateWalltCount = true;
        UpdatePerfectCount5 = true;
        UpdatePerfectCount10 = true;
    }
   
    void Start()
    {

        MODE_GAME.Reset_Mode_1 += Rest;
        MODE_GAME.Reset_Mode_2 += Rest;
        MODE_GAME.Reset_Mode_3 += Rest;
        card = GameController.instance.ChallengesCard;
    }

    public void UpdateStar()
    {
       

    }
    // Update is called once per frame
    void Update()
    {

        GameController.instance.Update_To_Infor_Achieve();



        switch (GameController.instance.Game_Type)
        {
            case GAME_MODE_TYPE.MODE_1:                              // 1: 5 perfect   15: 50 ro 1 luc   18 : 100 ro trong 1 luot     19: 5 ban lien tiep khong cham dat   21:nem vao ro  8:  10Perfect

                 
                //UpdatePerfectCount = true;
                if (UpdatePerfectCount5)                                 // 5 perfect
                {

                    if (BallPlayer.instance.count_perfect % 6 == 0)
                    {
                        card[1].AddAmount(1);
                        //update
                        UpdatePerfectCount5 = false;
                        count5= BallPlayer.instance.count_perfect;
                    }
                    }
                 
                     if(count5 != BallPlayer.instance.count_perfect)
                    {

                        UpdatePerfectCount5 = true;
                    }
                    
                
              

                 //   UpdatePerfectCount = true;
                    if (UpdatePerfectCount10)
                    {
                    if (BallPlayer.instance.count_perfect % 11 == 0)
                    {
                        card[8].AddAmount(1);
                        //update
                        UpdatePerfectCount10 = false;
                        count10 = BallPlayer.instance.count_perfect;
                    }
                    }

                    if (count10 != BallPlayer.instance.count_perfect)
                    {

                        UpdatePerfectCount10 = true;
                    }


                //trung 50 ro

             
                   
                    if (BallPlayer.instance.Score >= countGolbal && BallPlayer.instance.Score != 0)
                    {
                    if (UpdateGolbalCount)
                    {
                        Debug.Log("UPDATE SCORE");
                        card[15].AddAmount(1);
                        //update
                       // UpdateGolbalCount = false;
                        countGolbal+=50;
                    }
                    }
                    /*
                if (countGolbal != BallPlayer.instance.Score)
                {

                    UpdateGolbalCount = true;
                }

                */
                //trung 100 ro

                if (UpdateGolbalCount100)
                    {
                    if (BallPlayer.instance.Score > countGolbal100 && BallPlayer.instance.Score!=0)
                    {
                        card[18].AddAmount(1);
                        //update
                     //   UpdateGolbalCount100 = false;
                        countGolbal100 += 100;
                    }
                    }
                /*
                    if (countGolbal100 != BallPlayer.instance.Score)
                    {

                        UpdateGolbalCount100 = true;
                    }
                    */
                
            /////// is  Ground
              
                    if (UpdateWalltCount)
                    {
                    if (BallPlayer.instance.countGround % 5 == 0 && BallPlayer.instance.countGround!=0)
                    {
                        card[19].AddAmount(1);
                        //update
                        UpdateWalltCount = false;
                        countWall = BallPlayer.instance.countGround;
                    }
                   
                    }

                    if (countWall != BallPlayer.instance.countGround)
                    {

                        UpdateWalltCount = true;
                    }


                // 5 count two time:
                if (BallPlayer.instance.CountTapTap != 0)
                {
                    card[21].AddAmount(1);
                    BallPlayer.instance.CountTapTap = 0;
                }
                break;
            case GAME_MODE_TYPE.MODE_2:                                       // 2 : 5 perfect           3: tripleSkill     9: 10 Perfect     10:Perfect 5 lan 10s     13:50 in 1 game          16:100 ro in 1 game 
                // Perfect 5
              


                    if (UpdatePerfectCount5)
                    {
                    if (Ball_Player_2.instance.countPerfect % 6 == 0)
                    {
                        card[2].AddAmount(1);
                        UpdatePerfectCount5 = false;
                        count5 = Ball_Player_2.instance.countPerfect;

                    }
                    
                        
                     
                    }

                    if (count5 != Ball_Player_2.instance.countPerfect)
                    {

                        UpdatePerfectCount5 = true;
                    }

                    
                
                // Perfect 10
                
                    if (UpdatePerfectCount10)
                    {

                    if (Ball_Player_2.instance.countPerfect % 11 == 0&&Ball_Player_2.instance.countPerfect!=0)
                    {
                        card[9].AddAmount(1);
                        //update
                        UpdatePerfectCount10 = false;
                        count10 = Ball_Player_2.instance.countPerfect;
                    }
                    }

                    if (count10 != Ball_Player_2.instance.countPerfect)
                    {

                        UpdatePerfectCount10 = true;
                    }


                
                //trung 50 ro
               
                
                
                    if (Ball_Player_2.instance.Score > countGolbal && Ball_Player_2.instance.Score!=0)
                    {
                    Debug.Log(card[13].id+"  "+ card[16].id);
                   
                    //update
                    card[13].AddAmount(1);
                        //  UpdateGolbalCount = false;
                        countGolbal += 50;
                    }
                    

                    


                
                //trung 100 ro
             
                   
                    if (Ball_Player_2.instance.Score >countGolbal100 && Ball_Player_2.instance.Score != 0)
                    {
                        //update
                        // UpdateGolbalCount = false;
                        countGolbal100 += 100;
                        card[16].AddAmount(1);
                    }
                    

                   


               
                // 1 luc 3 ro  ::4

                if(UpdateDoubleSkill & Ball_Player_2.instance.CountDoubleKill!=0)
                {
                    Ball_Player_2.instance.CountDoubleKill = 0;
                    card[5].AddAmount(1);

                }
                // 1 luc 2 ro  ::6
                if (UpdateDoubleSkill & Ball_Player_2.instance.CountTripleKill != 0)
                {
                    Ball_Player_2.instance.CountTripleKill = 0;
                 
                    card[3].AddAmount(1);
                }




               
               

                break;
            case GAME_MODE_TYPE.MODE_3:          //DunkShot
                                                 //trung 100 ro
                                                 //1:5:7:12:15:21   
             
                    if (UpdatePerfectCount5)
                    {
                    if (BoardControl.instance.CountPerfect % 6== 0&& BoardControl.instance.CountPerfect != 0)
                    {
                        //update
                        card[0].AddAmount(1);
                        UpdatePerfectCount5 = false;
                        count5 = BoardControl.instance.CountPerfect;


                    }
                    }

                    if (count5 != BoardControl.instance.CountPerfect && BoardControl.instance.CountPerfect != 0)
                    {

                        UpdatePerfectCount5 = true;
                    }

                /*
                if (BoardControl.instance.CountPerfect != 0)
                {
                    if (UpdatePerfectCount)
                    {
                        card[11].AddAmount(count);
                        //update
                        UpdatePerfectCount = false;
                        count = BoardControl.instance.CountPerfect ;
                    }

                    if (count != BoardControl.instance.CountPerfect && BoardControl.instance.CountPerfect != 0)
                    {

                        UpdatePerfectCount = true;
                    }

                }
                */
                /// Count Wall
                if (BoardControl.instance.countWall != 0)
                {
                    if (UpdateWalltCount)
                    {
                        card[4].AddAmount(1);
                        //update
                        UpdateWalltCount = false;
                        countWall = BoardControl.instance.countWall;
                    }

                    if (countWall != BoardControl.instance.countWall)
                    {

                        UpdateWalltCount = true;
                    }

                }

                //FUll luc
                if (BoardControl.instance.countFullForce!=0)
                {
                    if (UpdateFullForcetCount)
                    {
                        card[6].AddAmount(1);
                        //update
                        UpdateFullForcetCount = false;
                        countFulLForce = BoardControl.instance.countFullForce;
                    }

                    if (countFulLForce != BoardControl.instance.countFullForce)
                    {

                        UpdateFullForcetCount = true;
                    }

                }

                //Count 50 Ball Golbal
          //      Debug.Log(BoardControl.instance.countGolbal);
                if (BoardControl.instance.Score >= countGolbal && BoardControl.instance.Score!=0)
                {
                    if (UpdateGolbalCount)
                    {
                        Debug.Log("Update");
                        card[14].AddAmount(1);
                        //update
                     //   Debug.Log("UpdateDonShot"+card[14].id);
                        Debug.Log(card[14].isTake);
                        //   UpdateGolbalCount = false;
                        countGolbal += 50;
                    }

                    if (countGolbal != (int)BoardControl.instance.Score )
                    {

                      //  UpdateGolbalCount = true;
                    }

                }

                if (BoardControl.instance.Score >=countGolbal100 && BoardControl.instance.Score != 0)
                {
                    if (UpdateGolbalCount)
                    {
                        Debug.Log("Update");
                        card[17].AddAmount(1);
                        //update
                      //  Debug.Log("UpdateDonShot" + card[14].id);
                      //  Debug.Log(card[17].isTake);
                        // UpdateGolbalCount = false;
                        countGolbal100 += 100;
                    }
                    if (countGolbal != (int)BoardControl.instance.Score)
                    {

                       // UpdateGolbalCount = true;
                    }

                }

                ///Cham Tuong 2 lan
                if (BoardControl.instance.CountIntereableWall != 0 && UpdateDoubleWall_1)
                {
                    CountdoubleWall_1 = BoardControl.instance.CountIntereableWall;
                    card[20].AddAmount(1);
                    UpdateDoubleWall_1 = false;
                }
                if (BoardControl.instance.CountIntereableWall != CountdoubleWall_1)
                {
                    UpdateDoubleWall_1 = true;
                }
               // 5 lan lien tiep cham truong 2 lan

                if (BoardControl.instance.CountIntereableWall%5==0 && UpdateDoubleWall)
                {
                    UpdateDoubleWall = false;
                    card[21].AddAmount(1);
                 
                }
                if(BoardControl.instance.CountIntereableWall != CountdoubleWall)
                {
                    UpdateDoubleWall = true;
                } 


                break;
                
        }
    }
}
