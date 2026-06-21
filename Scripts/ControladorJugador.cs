using UnityEngine;

public class ControladorJugador : MonoBehaviour
{
    [Header("Estadísticas de Movimiento")]
    public float velocidad = 6f;
    public float fuerzaSalto = 12f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float movimientoX;
    private bool estaEnSuelo = false;

    void Start()
    {
        // Conectamos las variables con los componentes del jugador
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 1. CAPTURAR INPUTS (Se ejecuta cada frame)
        // Devuelve -1 (izquierda), 1 (derecha) o 0 (quieto)
        movimientoX = Input.GetAxisRaw("Horizontal");

        // Girar el sprite hacia donde caminamos
        if (movimientoX > 0)
        {
            spriteRenderer.flipX = false; // Mira a la derecha
        }
        else if (movimientoX < 0)
        {
            spriteRenderer.flipX = true; // Mira a la izquierda
        }

        // Detectar si presionamos la tecla de salto (Barra espaciadora por defecto)
        if (Input.GetButtonDown("Jump") && estaEnSuelo)
        {
            // Aplicamos velocidad hacia arriba
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            estaEnSuelo = false; // Ya no estamos en el suelo
        }
    }

    void FixedUpdate()
    {
        // 2. APLICAR FÍSICAS (Se ejecuta en intervalos fijos para no depender de los FPS)
        rb.linearVelocity = new Vector2(movimientoX * velocidad, rb.linearVelocity.y);
    }

    // 3. DETECTAR EL SUELO
    private void OnCollisionEnter2D(Collision2D colision)
    {
        // Si el objeto con el que chocamos tiene la etiqueta "Suelo"
        if (colision.gameObject.CompareTag("Suelo"))
        {
            estaEnSuelo = true; // Nos permite volver a saltar
        }
    }
}
