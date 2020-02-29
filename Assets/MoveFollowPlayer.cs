using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFollowPlayer : MonoBehaviour
{
    List<GameObject> BG_0 = new List<GameObject>();
    public static MoveFollowPlayer instance;
    public BoardControl player;
    public float dis = 4;
    public bool moveCamera = false;
    public float Speed;
    public float maxCamera;
    public float minCamrea;
    public float speed = 0.05f;
    public GameObject BG_OBJ;
    public Vector3 posOriginal;
    public float maxreal;
    public float minReal;
    float posInit;
    // Start is called before the first frame update
    private void Awake()
    {

        posOriginal = Camera.main.transform.position;
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    
    float max;
    float min;
    Vector2 pos_Rest;
    public void setState()
    {
       
        if (player!=null){

            pos_Rest = GameController.instance.leftBottomReal;
            max = player.transform.position.y + GameController.instance.heightReal * 0.8f;
            min = player.transform.position.y - GameController.instance.heightReal * 0.2f;
        }
       
    }
    public void resetCamera()
    {
      //  SetBG(pos_Rest);
        maxCamera = max;
        minCamrea = min;
    }
    void Start()
    {

       posInit = GameController.instance.leftBottomReal.y;



        if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_3)
        StartCoroutine(SETBG());


            

           

       
       
        

        Vector3 pos = transform.position;
        pos.y = player.transform.position.y + dis;
        pos.z = -8f;
        transform.position = pos;
        MODE_GAME.Reset_Mode_3 += resetCamera;
      
        setState();
     
       
        maxCamera = player.transform.position.y + GameController.instance.heightReal * 0.5f;
        minCamrea = player.transform.position.y - GameController.instance.heightReal * 0.2f;
        maxreal = maxCamera * 2;
        minReal = minReal / 2;
    }

    IEnumerator SETBG()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 4; i++)
        {
            Vector2 poss = new Vector2(GameController.instance.leftBottomReal.x, GameController.instance.leftBottomReal.y + BG_OBJ.GetComponent<SpriteRenderer>().size.y * i);
            var a = Instantiate(BG_OBJ, poss, Quaternion.identity,GameObject.Find("Mode_1").transform);
            a.GetComponent<SpriteRenderer>().sprite = GameController.instance.getBG_1();
            BG_0.Add(a);


        }

    }
    // Update is called once per frame
    //private void OnDrawGizmos()
   // {
      //  Gizmos.DrawRay( new Vector2((GameController.instance.leftBottomReal.x),maxCamera), Vector3.right);
     //   Gizmos.DrawRay(new Vector2((GameController.instance.rightBottomReal.x),minCamrea), Vector3.right);
     //   Gizmos.DrawRay(transform.position, Vector3.right);
     //   Gizmos.color = Color.green; 
      //  Gizmos.DrawRay(player.transform.position, Vector3.right);
  //  }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            SetBG();
        }
        switch (GameController.instance.Game_Type)
        {
            
            case GAME_MODE_TYPE.MODE_1:
                transform.Find("bgcloud2").GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBackGround();

                break;
            case GAME_MODE_TYPE.MODE_2:
                transform.Find("bgcloud2").GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBackGround();
                break;
            case GAME_MODE_TYPE.MODE_3:
                transform.Find("bgcloud2").GetComponent<SpriteRenderer>().sprite = GameController.instance.getBG_0();
                for(int i = 0; i < BG_0.Count; i++)
                {
                    BG_0[i].GetComponent<SpriteRenderer>().sprite = GameController.instance.getBG_1();
                    
                }
           //     transform.parent.Find("bgcloud1").GetComponent<SpriteRenderer>().sprite = GameController.instance.getBG_1();
                break;
        }

        //   Debug.Log(minCamrea + "  " + maxCamera+" "+transform.position.y);

        //  if (minCamrea < transform.position.y && transform.position.y < maxCamera)
        //   {
        if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_3)
        {
            if(player.transform.position.y >maxreal|| player.transform.position.y < minReal)
            {
                moveCamera = false;
            }
            else
            {
                moveCamera = true;
            }

            if (moveCamera)
            {
                // Debug.Log(transform.position + "  " + player.transform.position);
                //     Debug.Log(minCamrea + " " + maxCamera + " " + player.transform.position.y);
                if (maxCamera > player.transform.position.y || player.transform.position.y > minCamrea)
                {

                    //      Debug.Log("NOT THING");
                }
                else
                {

                    //   if (GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_3)
                    //   {
                    //         Debug.Log("Update");
                    Vector3 pos = transform.position;
                    pos.y = player.transform.position.y - dis;
                    pos.z = -8f;
                    transform.position = pos;

                    //    Debug.Log(pos);
                    // }
                }


            }

        }else
        {
            transform.position = posOriginal;

        }
      
               


                    
             // 



        }
    //     if(GameController.instance.Game_Type == GAME_MODE_TYPE.MODE_1)
    //    {
    //      if (player.transform.position.y > BG[2].transform.position.y)
    //     {
    // setNextBG();
    //      }

    public Vector2 pos_3;
    public Vector2 pos_2;
   public void NextCamera()
    {
       
       
    }
    



    public void new_Collider_Camera()
    {
     //   maxCamera = player.transform.position.y + GameController.instance.heightReal * 0.3f;
      //  minCamrea = player.transform.position.y - GameController.instance.heightReal * 0.2f;
        //     Vector3 pos = transform.position;
           //  pos.y += dis;
        //   transform.position = pos;

          StartCoroutine(ResizeCamera());

    }
    public void SetBG()
    {
        int max = BG_0.Count;
      for(int i = max; i < max+4; i++)
        {
            Vector2 poss = new Vector2(GameController.instance.leftBottomReal.x,posInit + BG_OBJ.GetComponent<SpriteRenderer>().size.y * i);
            var a = Instantiate(BG_OBJ, poss, Quaternion.identity, GameObject.Find("Mode_1").transform);
            a.GetComponent<SpriteRenderer>().sprite = GameController.instance.getBG_1();
            BG_0.Add(a);
        }
        

    }



    IEnumerator ResizeCamera()
    {
        
        maxCamera = player.transform.position.y + GameController.instance.heightReal * 0.5f;
        minCamrea = player.transform.position.y - GameController.instance.heightReal * 0.2f;
        maxreal = maxCamera * 2;
        minReal = minReal / 2;
        Vector3 pos = player.transform.position;
        pos.y = player.transform.position.y ;
      
        pos.z = -9.8f;
        //
        if (transform.position.y > BG_0[BG_0.Count -2].transform.position.y)
        {
            SetBG();

        }
        float y0 = transform.position.y;
        float y1 = player.transform.position.y + 2;
   ///     Debug.Log(y0 + "  " + y1);
        while (y0 !=y1 )
        {
            y0 = Mathf.MoveTowards(y0,y1, Time.deltaTime*3);
            Vector2 pos_y = transform.position;
            pos_y.y = y0;
            transform.position = pos_y;
            Vector3 pos_zz = transform.position;
            pos_zz.z = -8;
            transform.position = pos_zz;
            yield return new WaitForSeconds(0);

        }
        Vector3 pos_z = transform.position;
        pos_z.z = -8;
        transform.position = pos_z;
       
        //moveCamera = false;
        yield return new WaitForSeconds(0);
    }
  
   
    
}
