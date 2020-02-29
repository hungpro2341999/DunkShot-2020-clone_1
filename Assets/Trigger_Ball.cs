using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Ball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer==14 || collision.gameObject.layer == 13)
        transform.parent.parent.GetComponent<DestroyMySelf>().Coll_Basket_Ball();
        AutioControl.instance.GetAudio("DunkshotBasket").Play();
    }
}
