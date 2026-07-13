using UnityEngine;

public class ObstaculoDanino : MonoBehaviour
{
    [Header("Configuración de Daño")]
    [Tooltip("Cantidad de vida que restará al jugador")]
    public int puntosDeDaño = 25;

    private void OnCollisionEnter2D(Collision2D oponente)
    {
        // Comparamos si lo que entró en contacto tiene la etiqueta "Player"
        if (oponente.gameObject.CompareTag("Player"))
        {
            // Intentamos obtener el script de salud del jugador
            SaludJugador salud = oponente.gameObject.GetComponent<SaludJugador>();

            if (salud != null)
            {
                salud.RecibirDaño(puntosDeDaño);
            }
        }
    }

    void Update()
    {
        
        if (Camera.main != null && transform.position.x < Camera.main.transform.position.x - 12f)
        {
            // Si la cámara ya pasó el obstáculo por 12 unidades, lo destruimos para liberar memoria
            Destroy(gameObject);
        }
    }
}