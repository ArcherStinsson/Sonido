using UnityEngine;



public class ColisionJugador : MonoBehaviour
{
    AudioSource myAudio;
    public AudioClip sonidoChoque;

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Sphere"))
        {
            myAudio.PlayOneShot(sonidoChoque);
        }
    }
}