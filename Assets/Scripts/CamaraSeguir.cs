using UnityEngine;

public class CamaraSeguir : MonoBehaviour
{
    public Transform jugador;
    public float suavizado = 5f;
    public float offsetX = 2f;

    void LateUpdate()
    {
        if (jugador == null) return;

        Vector3 nuevaPosicion = new Vector3(
            jugador.position.x + offsetX,
            transform.position.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            nuevaPosicion,
            suavizado * Time.deltaTime
        );
    }
}