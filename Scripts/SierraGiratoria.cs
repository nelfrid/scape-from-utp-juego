using UnityEngine;

public class SierraGiratoria : MonoBehaviour
{
    [Header("Configuración de Rotación")]
    public float velocidadRotacion = 300f; // Grados por segundo
    public bool girarAmanecillas = false; // Cambia la dirección

    void Update()
    {
        // Determinamos la dirección (1 o -1)
        float direccion = girarAmanecillas ? -1f : 1f;

        // Rotamos en el eje Z (el eje de profundidad en 2D)
        // Usamos Time.deltaTime para que gire independientemente de los FPS
        transform.Rotate(0, 0, velocidadRotacion * direccion * Time.deltaTime);
    }
}