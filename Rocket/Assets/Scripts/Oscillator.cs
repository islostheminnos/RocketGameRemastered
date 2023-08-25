using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    //Objemizin baslangic noktasini alıyoruz ve serialize yapmiyoruz cunku baslangic noktasi zaten sahnemizde belli sabit kalabilir
    Vector3 startingPosition;

    //Sin dalgalarimizin dongu suresini ayarlamamız için değişken.
    [SerializeField]float period = 2f;

    //Objemizin kac birim hareket etmesini istedigimizi belirtiyoruz bunu ileride degistirmek isteyebiliriz o yuzden serialize olacak.
   [SerializeField] Vector3 movementVector;
   //Ornegin 10 birimi 0.5 ile carparsak 5 birim olur bu hareketi ayarlamak icin vectoru 0 ile 1 arasinda bir deger ile carpmam gerekiyor bu da factor olur.
   [SerializeField] [Range(0,1)] float movementFactor;
    void Start()
    {
        //Objemizin baslangic pozisyonunu zaten sahnede halihazırda olan pozisyonuna esitledik.
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon) {return;}
        //Sin dalgalarimizin -1 ile 1 arasindaki bir tam turu icin bu kodu yazdik
        float cycles = Time.time / period;

        //Bir dairenin tam capini bu degiskene bagladik
        const float tau = Mathf.PI*2;
        
        //Simdi her seyi bagladik ve bu sayede rowSinWave sürekli olarak 0 ile 1 arasinda giden bir deger oldu.
        float rawSinWave = Mathf.Sin(cycles*tau);

        //Degerimiz -1 ile 1 arasindaydi biz bu degere 1 ekledik ve artik 0 ile 2 arasinda olacak ancak biz 0 ile 1 arasındaki degerleri istedigimiz icin sonucu 2 ye bolduk.
        movementFactor = (rawSinWave + 1f) / 2f;

        // Vektor ile faktoru carpiyoruz yani 0.5*10 yaparsak 5 birimlik hareketi parca parca yapmis oluyor
        Vector3 offset = movementVector * movementFactor;
        //Son olarak pozisyonumuzu offsete esitliyoruz.
        transform.position = startingPosition + offset;
    }
}
