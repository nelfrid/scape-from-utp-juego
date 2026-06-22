using UnityEngine;

public class ControladorJugador : MonoBehaviour
{
    [Header("Estadísticas de Movimiento")]
    public float velocidadCaminar = 4f;  // <--- Modificado: Separamos velocidades para notar el cambio
    public float velocidadCorrer = 8f;
    public float fuerzaSalto = 12f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator miAnimator;

    private float movimientoX;
    private bool estaEnSuelo = false;

    // variables de control para la carrera por tiempo sostenido
    private float tiempoPresionado = 0f;
    public float tiempoParaCorrer = 0.8f; // Tiempo en segundos para pasar de caminar a correr

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        miAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. CAPTURAR INPUTS
        movimientoX = Input.GetAxisRaw("Horizontal");

        // --- LÓGICA DE TIEMPO PARA CAMINAR O CORRER ---
        if (movimientoX != 0)
        {
            // Mientras mantenga presionada una dirección, sumamos tiempo
            tiempoPresionado += Time.deltaTime;

            if (tiempoPresionado >= tiempoParaCorrer)
            {
                // Pasó el tiempo límite: CORRE
                miAnimator.SetBool("isRunning", true);
                miAnimator.SetBool("isWalking", false);
            }
            else
            {
                // Al inicio: CAMINA
                miAnimator.SetBool("isWalking", true);
                miAnimator.SetBool("isRunning", false);
            }
        }
        else
        {
            // Si suelta la tecla, reiniciamos el temporizador y apagamos animaciones
            tiempoPresionado = 0f;
            miAnimator.SetBool("isWalking", false);
            miAnimator.SetBool("isRunning", false);
        }
        // ----------------------------------------------

        // Girar el sprite hacia donde caminamos
        if (movimientoX > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movimientoX < 0)
        {
            spriteRenderer.flipX = true;
        }

        // Detectar el salto con la Barra Espaciadora ("Jump")
        if (Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            estaEnSuelo = false;
            miAnimator.SetBool("isJumping", true);
        }
    }

    void FixedUpdate()
    {
        // Aplicar la velocidad física correcta dependiendo de si está corriendo o caminando
        float velocidadActual = miAnimator.GetBool("isRunning") ? velocidadCorrer : velocidadCaminar;

        rb.linearVelocity = new Vector2(movimientoX * velocidadActual, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = true;
            miAnimator.SetBool("isJumping", false);
        }
    }
}