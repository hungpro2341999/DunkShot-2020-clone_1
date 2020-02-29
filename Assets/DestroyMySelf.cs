using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMySelf : MonoBehaviour
{
   public  Transform bas;
   public  Transform bas_1;
    public bool Update_Point = false;
    public bool Update_Point_1 = false;
    public ParticleSystem Shoot;
    public ParticleSystem Coll_Basket;
    // Start is called before the first frame update
    void Start()
    {
        
        transform.Find("Basket").position = transform.position;
  //    transform.Find("Basket").GetComponent<TargetJoint2D>().anchor = transform.position;
         bas.GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBas_Mode1()[0];
         bas_1.GetComponent<SpriteRenderer>().sprite = GameController.instance.SpriteBas_Mode1()[1];
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.instance.GameOver && !GameController.instance.GamePause)
        {

             transform.Find("Basket").GetComponent<TargetJoint2D>().target = transform.position;
        if ((Vector2)transform.Find("Basket").position != (Vector2)transform.position)
        {
            if (!Update_Point)
            {
                transform.Find("Basket").position = transform.position;
                Update_Point = true;
            }
            if (Update_Point&&!Update_Point_1)
            {
                transform.Find("Basket").position = transform.position;
                Update_Point_1 = true;
            }

        }
    }
        }
           
    public void Destroy()
    {
        StartCoroutine(Destroy_Obj());
    }
    IEnumerator Destroy_Obj()
    {
        Vector3 scale = transform.localScale;
        while (transform.localScale != Vector3.zero)
        {
         //   Debug.Log("Huy");
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime*2);
        //    Debug.Log(transform);
            yield return new WaitForSeconds(0);
        }


        Destroy(gameObject);
    }
    public void Fire()
    {
        Shoot.Play();

    }
    public void Coll_Basket_Ball()
    {
        Coll_Basket.Play();
    }
}
