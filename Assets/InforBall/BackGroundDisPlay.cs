using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackGroundDisPlay : MonoBehaviour
{

    public Card card;
    public Transform BG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Find("Image").GetComponent<Image>().sprite = card.Spriteface;
        if (card.isUsing)
        {
            BG.gameObject.SetActive(true);
        }
        else
        {
            BG.gameObject.SetActive(false);
        }
    }
    public void Use()
    {
            
            GameController.instance.setBackGround(card);

            Card[] cardarr = GameController.instance.backGroundCard;
            for (int i = 0; i < cardarr.Length; i++)
            {
                if (cardarr[i].id == card.id)
                {
                GameController.instance.chaceSound = !GameController.instance.chaceSound;
                if (GameController.instance.chaceSound)
                {

                    AutioControl.instance.GetAudio("changeball (1)").Play();
                }
                else
                {
                    AutioControl.instance.GetAudio("changeball (2)").Play();
                }
                cardarr[i].isUsing = true;
                }
                else
                {
                    cardarr[i].isUsing = false;
                }

            }

        }
    
}
