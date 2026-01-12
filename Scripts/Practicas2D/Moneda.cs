using UnityEngine;

public class Moneda : MonoBehaviour
{
    public int valorPuntos = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Salto scriptDelJugador = other.GetComponent<Salto>();

            if (scriptDelJugador != null)
            {
                scriptDelJugador.ReproducirSonidoMoneda();
                scriptDelJugador.AñadirPuntos(valorPuntos);
            }

            Destroy(gameObject);
        }
    }
}