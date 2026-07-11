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

        MovimientoCamara cam = FindAnyObjectByType<MovimientoCamara>();
        if (cam != null)
        {
            cam.DetenerCamara();
        }
    }

    void Update()
    {
        // Evaluamos la posición del jugador respecto a la cámara principal
        if (Camera.main != null && vidaActual > 0)
        {
            Vector3 posicionEnPantalla = Camera.main.WorldToViewportPoint(transform.position);

            // Si la posición X es menor que 0, significa que el jugador se salió por la izquierda de la pantalla
            if (posicionEnPantalla.x < 0f)
            {
                Debug.Log("¡La cámara consumió al jugador!");

                // Forzamos que la vida llegue a 0 inmediatamente
                vidaActual = 0;
                Morir();
            }
        }
    }
}