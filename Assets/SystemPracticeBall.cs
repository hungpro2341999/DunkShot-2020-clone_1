using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SystemPracticeBall : MonoBehaviour
{

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.position;
        pos.z = 1;
        transform.position = pos;
        StartCoroutine(Dead_Practice(time));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    IEnumerator Dead_Practice(float time)
    {

        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
