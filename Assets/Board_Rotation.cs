using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Rotation : MonoBehaviour
{
    public Vector2 pos_location;
    public Quaternion rotatePos;
    public Vector2 PosMousePosition;
  
    // Start is called before the first frame update
    void Start()
    {
        pos_location = transform.localScale;
        rotatePos =   transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        MouseMove();
    }

    public void Direct()
    {
        float angle = Mathf.Atan2(DirectRotation().y, DirectRotation().x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

    }
    public Vector2 DirectRotation()
    {
        return ((Vector2)transform.position - PosMousePosition);

    }
    public void MouseMove()
    {
        PosMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


    }
    public void SetScale(Vector2 scale)
    {


        transform.localScale = scale;
    }
    public void ResetScale()
    {

        StartCoroutine(reset());
    }
    public void RestRotate()
    {

    }
    IEnumerator reset()
    {

        while ((Vector2)transform.localScale != pos_location)
        {

            Vector2 a = Vector2.MoveTowards(transform.localScale, pos_location, Time.deltaTime*3);

            transform.localScale = a;
            yield return new WaitForSeconds(0);
        }



    }

}
