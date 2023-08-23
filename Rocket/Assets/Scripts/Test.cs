using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
   float rotateZ = 1f;
   [SerializeField] float speedRotate = 10f;
    void Update()
    {
        Rotate_Rocket();
    }


    void Rotate_Rocket()
    {
        if(Input.GetKey(KeyCode.A)){
            Rotater(rotateZ);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            Rotater(-rotateZ);
        }
    }

    void Rotater(float RotatePerFrame)
    {
        transform.Rotate(0, 0, RotatePerFrame * speedRotate * Time.deltaTime);
    }
}
