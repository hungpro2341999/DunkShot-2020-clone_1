using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public ParticleSystem shadow;

    private void Update()
    {
       
        shadow.startRotation3D = -transform.parent.rotation.eulerAngles / (180.0f / Mathf.PI);
    }

}
