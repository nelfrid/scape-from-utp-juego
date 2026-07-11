using UnityEngine;

public class FondoInfinito : MonoBehaviour
{
    [System.Serializable]
    public class ZonaFondo
    {
        public string nombreZona;
        public Sprite spriteFondo;
        public int puntajeRequerido; // El puntaje mínimo en el ciclo para activar este fondo
    }

    [Header("Configuración de Fondos (Ordenar de menor a mayor puntaje)")]
    public ZonaFondo[] zonasDeJuego;

    [Header("Configuración de Movimiento")]
    [Tooltip("Entre más cercano a 0, más lento se moverá el fondo respecto a la cámara.")]
    public float efectoParallax = 0.2f;

    private SpriteRenderer spriteRenderer;
    private float offsetAcumulado = 0f;
    private MovimientoCamara scriptCamara;
    private int indiceActual = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        scriptCamara = FindAnyObjectByType<MovimientoCamara>();

        // Colocamos el primer fondo por defecto al iniciar
        if (zonasDeJuego != null && zonasDeJuego.Length > 0 && spriteRenderer != null)
        {
            spriteRenderer.sprite = zonasDeJuego[0].spriteFondo;
        }
    }

    void Update()
    {
        if (scriptCamara == null || spriteRenderer == null || zonasDeJuego == null || zonasDeJuego.Length == 0) return;

        // 1. Efecto Parallax Infinito (Desplazamiento continuo de la textura)
        float velocidadRelativa = scriptCamara.velocidadCamara * efectoParallax;
        offsetAcumulado += velocidadRelativa * Time.deltaTime;
        spriteRenderer.material.mainTextureOffset = new Vector2(offsetAcumulado, 0f);

        // 2. Obtener puntaje actual desde tus componentes
        int puntajeActual = ObtenerPuntajeDelJuego();

        // 3. Verificar si corresponde alternar o repetir el ciclo de fondos
        VerificarCambioDeFondo(puntajeActual);
    }

    void VerificarCambioDeFondo(int puntaje)
    {
        if (zonasDeJuego.Length <= 1) return;

        // Calculamos la duración total de un ciclo completo de fondos.
        // Tomamos el requerimiento del último fondo y le sumamos un margen estimado (ej: 500 puntos) 
        // para saber cuándo debe reiniciarse el bucle por completo.
        int puntajeMaximoCiclo = zonasDeJuego[zonasDeJuego.Length - 1].puntajeRequerido + 500;

        // El operador residuo (%) normaliza el puntaje, haciendo que "dé la vuelta" a 0 al superar el ciclo
        int puntajeNormalizado = puntaje % puntajeMaximoCiclo;

        int nuevoIndice = 0;

        // Buscamos cuál es el fondo adecuado para el puntaje actual dentro del ciclo
        for (int i = 0; i < zonasDeJuego.Length; i++)
        {
            if (puntajeNormalizado >= zonasDeJuego[i].puntajeRequerido)
            {
                nuevoIndice = i;
            }
        }

        // Si el fondo calculado es diferente al que se muestra en pantalla, hacemos el cambio
        if (nuevoIndice != indiceActual)
        {
            indiceActual = nuevoIndice;
            spriteRenderer.sprite = zonasDeJuego[indiceActual].spriteFondo;

            // Reiniciamos el offset para evitar saltos o estirones visuales bruscos al cambiar de textura
            offsetAcumulado = 0f;
            Debug.Log("¡Cambio de zona detectado de forma cíclica! Nueva zona: " + zonasDeJuego[indiceActual].nombreZona);
        }
    }

    int ObtenerPuntajeDelJuego()
    {
        // Buscamos primero el componente ScoreManager en la escena
        ScoreManager manejadorPuntaje = FindAnyObjectByType<ScoreManager>();
        if (manejadorPuntaje != null)
        {
            // Conectado con tu variable real de puntos
            return (int)manejadorPuntaje.currentScore;
        }

        // Si no lo encuentra, busca en la escena el script alternativo ScorePorTiempo
        ScorePorTiempo puntajeTiempo = FindAnyObjectByType<ScorePorTiempo>();
        if (puntajeTiempo != null)
        {
            // Conectado con tu variable real de puntos
            return (int)puntajeTiempo.currentScore;
        }

        return 0; // Retorno de seguridad si ningún script está activo en la escena
    }
}