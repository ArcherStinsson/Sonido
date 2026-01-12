using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 20f;

    void Update()
    {
        float moverX = Input.GetAxis("Horizontal");
        float moverZ = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(moverX, 0, moverZ);

        transform.Translate(movimiento * velocidad * Time.deltaTime);
    }
}

