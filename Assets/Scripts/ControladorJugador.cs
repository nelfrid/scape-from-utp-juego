using UnityEngine;
using System.Collections;

public class ControladorJugador : MonoBehaviour
{
    [Header("Estadísticas de Movimiento")]
    private SaludJugador salud;
    public float velocidadCaminar = 4f;  // <--- Modificado: Separamos velocidades para notar el cambio
    public float velocidadCorrer = 8f;
    public float fuerzaSalto = 12f;


    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator miAnimator;

    private float movimientoX;
    private bool estaEnSuelo = false;

    private bool takeDamage = false;
    // variables de control para la carrera por tiempo sostenido
    private float tiempoPresionado = 0f;
    public float tiempoParaCorrer = 0.8f; // Tiempo en segundos para pasar de caminar a correr

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        miAnimator = GetComponent<Animator>();
        salud = GetComponent<SaludJugador>();


    }

    void Update()
    {
        if (salud != null && salud.isDead) return;
        if (takeDamage) return;
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
            estaEnSuelo = false;
            miAnimator.SetBool("isJumping", true); // ¡Activa la animación aquí!
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        if (salud != null && salud.isDead)
        {
            rb.linearVelocity = Vector2.zero; // Detenemos el movimiento por completo
            return;
        }
        if (takeDamage) return;
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

        if (colision.gameObject.CompareTag("Obstaculo") && !takeDamage)
        {
            StartCoroutine(RutinaEmpuje(colision.transform));
        }
    }

    private IEnumerator RutinaEmpuje(Transform posicionObstaculo)
    {
        takeDamage = true;
        

        miAnimator.SetTrigger("takeDamage");
        Vector2 direccionEmpuje = (transform.position - posicionObstaculo.position).normalized;
        direccionEmpuje.y = 0.5f; // Le damos un pequeño empujón hacia arriba también
        rb.linearVelocity = Vector2.zero; // Frenamos al personaje
        rb.AddForce(direccionEmpuje * 5f, ForceMode2D.Impulse); // Empuje hacia atrás
        yield return new WaitForSeconds(0.4f);

        takeDamage = false;


    }
}
