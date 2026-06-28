using UnityEngine;

public class ScorePorTiempo : MonoBehaviour
{
    [Header("Configuración del Tiempo")]
    public float tiempoParaPunto = 1f; // Cuántos segundos deben pasar para ganar puntos
    public int puntosPorCiclo = 10;    // Cuántos puntos ganas cada vez que se cumple el tiempo

    private float timer = 0f;          // Nuestro cronómetro interno
    public bool elJuegoEstaActivo = true; // Control para detener el puntaje si pierdes

    void Update()
    {
        // 1. Verificamos que el juego no haya terminado
        if (elJuegoEstaActivo)
        {
            // 2. Le sumamos a nuestro cronómetro el tiempo que acaba de pasar
            timer += Time.deltaTime;

            // 3. Preguntamos: ¿El cronómetro ya alcanzó el tiempo objetivo (ej. 1 segundo)?
            if (timer >= tiempoParaPunto)
            {
                // 4. Mandamos a sumar los puntos al ScoreManager que hicimos antes
                ScoreManager.Instance.AddPoints(puntosPorCiclo);

                // 5. Reiniciamos el cronómetro a 0 para que vuelva a contar el siguiente segundo
                timer = 0f;
            }
        }
    }

    // Método para llamar cuando el jugador choca contra algo
    public void DetenerPuntaje()
    {
        elJuegoEstaActivo = false;
    }
}
