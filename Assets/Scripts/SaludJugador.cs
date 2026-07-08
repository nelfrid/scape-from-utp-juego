using UnityEngine;

public class SaludJugador : MonoBehaviour
{
    public int vidaMaxima = 100;
    private int vidaActual;

    // 1. Declaramos la variable para el Animator del personaje
    private Animator anim;

    void Start()
    {
        vidaActual = vidaMaxima;

        // 2. Buscamos y guardamos el componente Animator al iniciar el juego
        anim = GetComponent<Animator>();
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        Debug.Log("¡Auch! Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("El jugador ha muerto.");

        // 3. Activamos el Trigger en el Animator para que empiece la animación
        // Asegúrate de cambiar "Muerte" por el nombre exacto que le pusieron al parámetro en Unity
        if (anim != null)
        {
            anim.SetTrigger("Muerte");
        }

        // 4. Detenemos los puntos llamando al script de ScorePorTiempo que está en el GameManager
        ScorePorTiempo scoreTiempo = FindAnyObjectByType<ScorePorTiempo>();
        if (scoreTiempo != null)
        {
            scoreTiempo.DetenerPuntaje();
        }
    }
}