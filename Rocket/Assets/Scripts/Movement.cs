using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float rotateSpeed = 10f;
    float rotateZ = 1f;
    Rigidbody rb;
   [SerializeField] AudioClip audioClip;
    AudioSource audioSource;
    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] float thrustPower = 1f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Rocket_Thrust();
        Rocket_Rotation();
    }

    void Rocket_Thrust(){
        if(Input.GetKey(KeyCode.Space)){
            
            rb.AddRelativeForce(0,thrustPower*Time.deltaTime,0);
            if(!audioSource.isPlaying){
                audioSource.PlayOneShot(audioClip);
                thrustParticle.Play();
            }
        }
         else{
            thrustParticle.Stop();
                audioSource.Stop();
            }
    }

    void Rocket_Rotation(){
  if(Input.GetKey(KeyCode.A))
        {
            RotateOnAxis(rotateZ);
        }

        else if(Input.GetKey(KeyCode.D)){
            RotateOnAxis(-rotateZ);
        }
    }

 void RotateOnAxis(float RotateThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(0, 0, RotateThisFrame * Time.deltaTime * rotateSpeed);
        rb.freezeRotation = false;
    }
}
