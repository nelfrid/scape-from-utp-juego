using UnityEngine;

public class SueloInfinito : MonoBehaviour
{
    private Transform camaraTransform;
    private float longitudSuelo;

    void Start()
    {
        // Buscamos la cámara principal en la escena
        if (Camera.main != null)
        {
            camaraTransform = Camera.main.transform;
        }

        // Obtenemos el tamaño exacto del suelo en el eje X usando su Collider
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            longitudSuelo = collider.size.x * transform.localScale.x;
        }
    }

    void Update()
    {
        // Si la cámara ya avanzó más allá de la mitad de este suelo...
        if (camaraTransform != null && camaraTransform.position.x > transform.position.x + longitudSuelo)
        {
            // ...movemos este suelo hacia adelante (dos veces su tamaño para ponerse al frente del otro)
            Vector3 nuevaPosicion = transform.position;
            nuevaPosicion.x += (longitudSuelo * 2) - 1.5f;
            transform.position = nuevaPosicion;
        }
    }
}