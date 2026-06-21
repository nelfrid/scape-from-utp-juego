using UnityEngine;

public class ObstaculoDanino : MonoBehaviour
{
    [Header("Configuración de Daño")]
    [Tooltip("Cantidad de vida que restará al jugador")]
    public int puntosDeDaño = 25;

    private void OnTriggerEnter2D(Collider2D oponente)
    {
        // Comparamos si lo que entró en contacto tiene la etiqueta "Player"
        if (oponente.CompareTag("Player"))
        {
            // Intentamos obtener el script de salud del jugador
            SaludJugador salud = oponente.GetComponent<SaludJugador>();

            if (salud != null)
            {
                salud.RecibirDaño(puntosDeDaño);
            }
        }
    }
}