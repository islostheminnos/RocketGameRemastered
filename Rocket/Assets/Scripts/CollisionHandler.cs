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
bool colliderDisabled = false;


//Yukarida tanimladigimiz audio ve particle degiskenlerini cache'liyoruz.
void Start()
{
    allParticles = GetComponent<ParticleSystem>();
    audioSource = GetComponent<AudioSource>();
}


//Bug fix icin hile kullaniyoruz burayi silmeyi unutma
void Update()
{
    NextLevelCheat();
    Collision_Cheat();
}




void OnCollisionEnter (Collision other)
{
    if(isTransitioning){return;}

    switch(other.gameObject.tag)
    {
    //Dost objeye temas ederse hicbir sey yapmamasi icin yazdik
        case "Friendly":
        Debug.Log("Friendly");
        break;
    //Yakita temas ederse hicbir sey yapmamasi icin yazdik
        case "Fuel":
        Debug.Log("Fuel");
        break;
    //Finish cizgisine temas ettiginde success sesini ve particle'ını baslatiyor, hareketi kilitliyor ve bir sonraki leveli yüklemesi icin NextLevel methodunu cagiriyor.
        case "Finish":
        audioSource.PlayOneShot(finishAudioClip);
        successParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel",timeToWaitNextLevel);
        break;

    //Eger dost, yakit veya finishe temas etmezse roketimizi patlatıyor. Bu methodun icerisinde patlama efekti, tüm sesleri ve hareketleri durduran bir kod var.
        default:
        StartCrashSequence();
        break;
    }
}
    


//Gemimizin patlama kodu
void StartCrashSequence()
{
    
   isTransitioning = true;
audioSource.Stop();
explosionParticle.Play();
    audioSource.PlayOneShot(failAudioClip);
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", 3f);
    
    
    
    
}


//Gemimiz patlarsa seviyeyi bastan baslatma kodu burada
   void ReloadLevel()
   { 
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
    SceneManager.LoadScene(currentSceneIndex); 
    
   }



//Finish switchi icin calistirdigimiz sonraki seviyeyi yükleme kodu burada
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


//Update methodu icerisindeki hile kodumuzun methodu burada
    void NextLevelCheat()
    {

        if(Input.GetKeyDown(KeyCode.L))
        {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
        nextSceneIndex = 0;
        }


         SceneManager.LoadScene(nextSceneIndex);
        }
        
    }


//Collision kaldırma hilesi yukarıda update'te çalıştırıyoruz.
void Collision_Cheat()
{
    if(Input.GetKeyDown(KeyCode.C) && !colliderDisabled)
    {
        Debug.Log("Collider disabled");
        GetComponent<Collider>().enabled = false;
        colliderDisabled = true;
    }
    else if(Input.GetKeyDown(KeyCode.C) && colliderDisabled)
    {
        Debug.Log("Collider enabled");
        GetComponent<Collider>().enabled = true;
        colliderDisabled = false;
    }
}








}
