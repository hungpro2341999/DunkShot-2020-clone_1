using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EFFECT_TYPE { SCALE }

public class Effect : MonoBehaviour
{
    Vector3 scale_0;
    public float speed = 1;
    public float time = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        scale_0 = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, scale_0,Time.deltaTime*speed);
        if (Vector3.Distance(transform.localScale, scale_0) < 0.1f)
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
