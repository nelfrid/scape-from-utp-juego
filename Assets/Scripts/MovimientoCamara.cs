using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public float velocidadCamara = 5f;
    private bool juegoTerminado = false;

    void Update()
    {
        if (juegoTerminado) return;

        // La cámara se mueve hacia la derecha constantemente
        transform.Translate(Vector3.right * velocidadCamara * Time.deltaTime);
    }

    public void DetenerCamara()
    {
        juegoTerminado = true;
    }
}