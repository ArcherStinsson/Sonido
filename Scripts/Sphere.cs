using UnityEngine;


public class Sphere : MonoBehaviour
{
    AudioSource myAudio;
    bool moviendo = false;

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.loop = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            moviendo = true;
            if (!myAudio.isPlaying)
            {
                myAudio.Play();
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            moviendo = false;
            myAudio.Stop();
        }

        if (moviendo)
        {
            transform.Translate(Vector3.forward * 5f * Time.deltaTime);
        }
    }
}