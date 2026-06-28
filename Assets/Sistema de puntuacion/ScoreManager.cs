using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class ScoreManager : MonoBehaviour
{
    // Instancia estática para que sea accesible desde cualquier otro script
    public static ScoreManager Instance { get; private set; }

    [Header("Configuración de UI")]
    [SerializeField] private TextMeshProUGUI scoreText; // Arrastra tu texto aquí en el Inspector

    private int currentScore = 0;

    private void Awake()
    {
        // Aseguramos que solo exista un ScoreManager en la escena
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    // Método público que llamarán otros scripts para sumar puntos
    public void AddPoints(int pointsToAdd)
    {
        currentScore += pointsToAdd;
        UpdateScoreUI();
    }

    // Método para reiniciar el puntaje cuando el bucle vuelva a empezar de cero (si aplica)
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    // Actualiza el componente de texto en la pantalla
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntaje: " + currentScore.ToString();
        }
    }
}