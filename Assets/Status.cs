using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    Vector3 direct;
    public float speed = 1;
    public float time = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        direct = transform.position + Vector3.up * GameController.instance.heightReal * 0.2f;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, direct, Time.deltaTime*1.5f);
        if (Vector3.Distance(transform.position, direct) < 0.1f)
        {
            StartCoroutine(Dead(time));
        }
    }

    public void visualEffect()
    {

    }
    IEnumerator Dead(float time)
    {

        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
