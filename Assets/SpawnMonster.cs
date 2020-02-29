using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2((GameController.instance.leftUpRead.x + GameController.instance.widthReal / 2), GameController.instance.leftUpRead.y-GameController.instance.heightReal*0.05f);
        SpawnerCtrl.instante.SpawnMonster(Random.Range(0,4));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
