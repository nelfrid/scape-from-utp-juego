using UnityEngine;

public class Pendulo : MonoBehaviour
{
    [Header("Configuración del Péndulo")]
    public float anguloMaximo = 45f; // Hasta dónde llega a los lados
    public float velocidad = 2f; // Qué tan rápido se balancea

    private float anguloInicialZ;

    void Start()
    {
        // Guardamos la rotación inicial en Z por si colocaste el hacha inclinada
        anguloInicialZ = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        // Calculamos el ángulo actual usando la función Seno en base al tiempo
        float anguloActual = anguloMaximo * Mathf.Sin(Time.time * velocidad);

        // Aplicamos la rotación al objeto en el eje Z
        transform.rotation = Quaternion.Euler(0, 0, anguloInicialZ + anguloActual);
    }
}
