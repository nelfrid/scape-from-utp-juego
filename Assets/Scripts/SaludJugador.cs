using UnityEngine;

public class SaludJugador : MonoBehaviour
{
    [Header("Configuración de Vida")]
    [SerializeField] private int vidaMaxima = 100;
    [SerializeField] private int vidaActual;
    private Animator miAnimator;
    public bool isDead = false;
    [SerializeField] private Sprite spriteMuerte;
    private SpriteRenderer sr;

    void Start()
    {
        vidaActual = vidaMaxima;
        miAnimator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>(); // Inicializamos el SpriteRenderer
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

    public void Morir()
    {
        if (isDead) return;
        isDead = true;

        // 1. Detener físicas
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        // 2. SOLUCIÓN INFALIBLE: Cambiar sprite y apagar Animator
        if (spriteMuerte != null && sr != null)
        {
            sr.sprite = spriteMuerte; // Cambia el dibujo al frame muerto
        }

        if (miAnimator != null)
        {
            miAnimator.enabled = false; // Apagamos el Animator para que no intente volver a Idle
        }

        GetComponent<Collider2D>().enabled = false;

        // Llamar a sistemas externos
        ScorePorTiempo scoreTiempo = FindAnyObjectByType<ScorePorTiempo>();
        if (scoreTiempo != null) scoreTiempo.DetenerPuntaje();

        MovimientoCamara cam = FindAnyObjectByType<MovimientoCamara>();
        if (cam != null) cam.DetenerCamara();

        ControladorDespuesDeMorir gestor = FindAnyObjectByType<ControladorDespuesDeMorir>();
        if (gestor != null)
        {
            gestor.ActivarModoMuerte();
        }
    }

    // Chequeo de pantalla (se queda aquí en Salud)
    void Update()
    {
        if (Camera.main != null && vidaActual > 0)
        {
            Vector3 posEnPantalla = Camera.main.WorldToViewportPoint(transform.position);
            if (posEnPantalla.x < 0f)
            {
                vidaActual = 0;
                Morir();
            }
        }
    }
}
