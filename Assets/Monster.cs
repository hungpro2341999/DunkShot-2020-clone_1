using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    public GameObject Star;
    public GameObject Star_2D;
    public int TakeHitBal=0;
    public int max = 35;
    public SpriteRenderer sprite;
    public Transform[] CrackSprt;
    public bool isCrack = false;
    int i = 0;
    bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < CrackSprt.Length; i++)
        {
            CrackSprt[i] = transform.GetChild(i);
            CrackSprt[i].gameObject.SetActive(false);
        }
        Debug.Log(transform.parent.name);
        Transform trs =  transform.parent.Find("Holder");
        if (trs != null) { }
       GetComponent<DistanceJoint2D>().connectedBody =  transform.parent.Find("Holder/chain 8").GetComponent<Rigidbody2D>();
        GetComponent<DistanceJoint2D>().connectedAnchor = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {

            if (TakeHitBal > max)
            {

                Destroy();
                isDead = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Destroy();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy();
        }
      
       

    }
    public void Crack()
    {
        if (isCrack)
        {
            isCrack = false;
        
            if (i < CrackSprt.Length)
            {
                CrackSprt[i].gameObject.SetActive(true);
              


            }
            i++;


        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int x = max / 3;
        if (collision.gameObject.layer == 14)
            {
                TakeHitBal++;

            if (TakeHitBal % x == 0 && TakeHitBal != 0)
            {
                AutioControl.instance.GetAudio("MonsterExplode").Play();
                isCrack = true;
                Crack();
            }

        }
      


    }


   

    public void Destroy()
    {   
        float widthStar = Star.GetComponent<SpriteRenderer>().size.x;
        float heightStar = Star.GetComponent<SpriteRenderer>().size.y;
       
        float width = 0;
        float height =0;
       
        Transform trs = GameObject.Find("Mode_4").transform;
        width = sprite.size.x;
        height = sprite.size.y;
        Vector2 pos = transform.position;
        //   Debug.Log(widthStar + " " + width);
        //   Debug.Log(heightStar + " " + height);
        
        pos.x = pos.x - width/2;
     //   Debug.Log(pos);
   //     Debug.Log(pos);
        for (float i = 0; i < pos.x + width*2; i += widthStar/2)
        {
            for (float j = pos.y + height/2; j > 0; j -= heightStar/2)
            {
                float x = pos.x + i;
                float y = pos.y - j;
                int r =  Random.Range(0, 2);
                Debug.Log(r);
               if (r==1)
                if (Physics2D.CircleCast(new Vector2(x,y),0.01f,Vector2.zero,17)){


                        RaycastHit2D ray = Physics2D.CircleCast(new Vector2(x, y), 0.01f, Vector2.zero, 17);
                        if(ray.collider.gameObject.layer == 17)
                        {
                            var a = Instantiate(Star_2D, new Vector2(x, y), Quaternion.identity, trs);
                            a.transform.localScale *= 0.5f;
                            a.GetComponent<Star>().Destroy();

                        }
                  
                }
           
            }
         
            
        }
        transform.Find("PumPum").GetComponent<ParticleSystem>().Play();
        transform.Find("PumPum").parent = null;
        SpawnEffect.instance.Set_System(Paractice_Type.PONG, transform.Find("PosExp").position, "", null);
        SpawnEffect.instance.Set_System(Paractice_Type.PUM, transform.Find("PosExp").position, "", null);


        transform.Find("Explosion").GetComponent<ParticleSystem>().startRotation3D = -transform.parent.rotation.eulerAngles / (180.0f / Mathf.PI);
        transform.Find("Explosion").GetComponent<ParticleSystem>().Play();
        transform.Find("Explosion").parent = null;
        
        AutioControl.instance.GetAudio("MonsterCongrat").Play();

        GameController.instance.Save_Star((int)Reward_Game.instance.GetStar());
        Debug.Log((int)Reward_Game.instance.GetStar());
        InvokeBlack();

        Destroy(gameObject);
      



            }

    IEnumerator BackToGame(float time)
    {
        yield return new WaitForSeconds(1f);
      
        ScreenGamePlay.instance.BlackBackGround();
     

    }
    
  
    public void InvokeBlack()
    {
        ScreenGamePlay.instance.BlackBackGround();
    }

}
    


    
