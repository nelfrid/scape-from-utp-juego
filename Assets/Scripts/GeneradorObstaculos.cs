using UnityEngine;

public class GeneradorObstaculos : MonoBehaviour
{
    [Header("Obstáculos disponibles")]
    public GameObject[] prefabsObstaculos;

    [Header("Configuración de Tiempos")]
    public float tiempoInicial = 2f;      // Cuánto tarda en salir el primero
    public float tiempoEntreSpawns = 2.5f; // Cada cuántos segundos sale uno nuevo

    [Header("Configuración de Posición")]
    public float distanciaAdelanteCamara = 12f; // Qué tan adelante de la cámara aparecerán
    public float alturaSueloY = -3.5f;           // La altura fija del suelo donde se pararán

    private Transform camaraTransform;
    private bool juegoTerminado = false;

    void Start()
    {
        if (Camera.main != null)
        {
            camaraTransform = Camera.main.transform;
        }

        // Iniciamos el bucle repetitivo de generación
        InvokeRepeating("GenerarObstaculo", tiempoInicial, tiempoEntreSpawns);
    }

    void Update()
    {
        
    }

    void GenerarObstaculo()
    {
        if (juegoTerminado || camaraTransform == null || prefabsObstaculos == null || prefabsObstaculos.Length == 0)
            return;

        // 1. Elegir un obstáculo al azar de la lista
        int indiceAleatorio = Random.Range(0, prefabsObstaculos.Length);
        GameObject obstaculoElegido = prefabsObstaculos[indiceAleatorio];

        // 2. Calcular la posición X (un margen adelante de la posición actual de la cámara)
        float posicionX = camaraTransform.position.x + distanciaAdelanteCamara;

        // 3. Crear el vector de posición final
        Vector3 posicionSpawn = new Vector3(posicionX, alturaSueloY, 0f);

        // 4. Instanciar el objeto en la escena
        Instantiate(obstaculoElegido, posicionSpawn, Quaternion.identity);
    }

    public void DetenerGenerador()
    {
        juegoTerminado = true;
        CancelInvoke("GenerarObstaculo");
    }
}