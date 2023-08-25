using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

[SerializeField]float timeToWaitNextLevel = 0f;



AudioSource audioSource;
[SerializeField] AudioClip finishAudioClip;
[SerializeField] AudioClip failAudioClip;
[SerializeField] ParticleSystem successParticle;
[SerializeField] ParticleSystem explosionParticle;

ParticleSystem allParticles;


bool isTransitioning = false;


void Start()
{
    allParticles = GetComponent<ParticleSystem>();
    audioSource = GetComponent<AudioSource>();
}

void OnCollisionEnter (Collision other)
{
    if(isTransitioning){return;}

    switch(other.gameObject.tag)
    {
    
        case "Friendly":
        Debug.Log("This is a friendly object");
        break;

        case "Finish":
        audioSource.PlayOneShot(finishAudioClip);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel",timeToWaitNextLevel);
        
        
        break;

        case "Fuel":
        Debug.Log("You picked up fuel");
        break;

        default:
        
        StartCrashSequence();
        
        break;
    }
}
    

void StartCrashSequence()
{
    
   isTransitioning = true;
audioSource.Stop();
explosionParticle.Play();
    audioSource.PlayOneShot(failAudioClip);
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", 3f);
    
    
    
    
}
   void ReloadLevel()
   { 
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
    SceneManager.LoadScene(currentSceneIndex); 
    
   }




   void NextLevel()
   {
        isTransitioning = true;
    audioSource.Stop();
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        int nextSceneIndex = currentSceneIndex + 1;


        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
        nextSceneIndex = 0;
        }


         SceneManager.LoadScene(nextSceneIndex);
        
        
    
    
   }
}
