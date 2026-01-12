using System.Collections;
using UnityEngine;
using TMPro;

public class Salto : MonoBehaviour
{
    public float thrust = 10.0f;
    private Rigidbody2D rb2D;
    private bool isJumping = false;
    private int noCollisLayer;
    private int platInvLayer;

    public TextMeshProUGUI textoPuntuacion;
    public int puntuacionParaMejora = 50;
    public float aumentoDeSalto = 5.0f;
    private int puntuacionActual = 0;
    private bool mejoraAplicada = false;

    public AudioClip clipSalto;
    public AudioClip clipAterrizaje;
    public AudioClip clipMoneda;
    public AudioClip clipPowerUp;
    private AudioSource audioSource;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        noCollisLayer = LayerMask.NameToLayer("NoCollis");
        platInvLayer = LayerMask.NameToLayer("PlatInv");

        ActualizarUI();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb2D.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            isJumping = true;
            ReproducirSonido(clipSalto);
        }
    }

    void ReproducirSonido(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void ReproducirSonidoMoneda()
    {
        ReproducirSonido(clipMoneda);
    }

    public void AñadirPuntos(int puntos)
    {
        puntuacionActual += puntos;
        ActualizarUI();

        if (!mejoraAplicada && puntuacionActual >= puntuacionParaMejora)
        {
            mejoraAplicada = true;
            thrust += aumentoDeSalto;
            ReproducirSonido(clipPowerUp);
        }
    }

    void ActualizarUI()
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "Puntos: " + puntuacionActual;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("suelo") || other.gameObject.CompareTag("Plataforma"))
        {
            if (isJumping)
            {
                ReproducirSonido(clipAterrizaje);
            }
            isJumping = false;
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, 0f);
        }

        if (other.gameObject.CompareTag("Plataforma"))
        {
            transform.SetParent(other.transform);
        }

        if (other.gameObject.layer == platInvLayer)
        {
            SpriteRenderer sr = other.gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.enabled = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!this.isActiveAndEnabled) return;

        if (other.gameObject.layer != noCollisLayer)
        {
            if (other.gameObject.CompareTag("Plataforma"))
            {
                StartCoroutine(DelayedUnparent());
            }
        }
    }

    private IEnumerator DelayedUnparent()
    {
        yield return new WaitForEndOfFrame();
        transform.SetParent(null);
    }
}