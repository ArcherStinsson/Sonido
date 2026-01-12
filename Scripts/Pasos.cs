using UnityEngine;

public class PasosJugador : MonoBehaviour
{
    AudioSource myAudio;
    public AudioClip sonidoPaso;
    public float frecuenciaPaso = 0.5f;
    float cronometro;

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        cronometro = frecuenciaPaso;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0 || y != 0)
        {
            cronometro -= Time.deltaTime;

            if (cronometro <= 0)
            {
                myAudio.PlayOneShot(sonidoPaso);
                cronometro = frecuenciaPaso;
            }
        }
        else
        {
            cronometro = 0;
        }
    }
}