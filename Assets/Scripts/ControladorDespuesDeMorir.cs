using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDespuesDeMorir : MonoBehaviour
{
    private bool isDead = false;

    // Esto se llama desde SaludJugador
    public void ActivarModoMuerte()
    {
        isDead = true;
        Time.timeScale = 0f; // Pausa el juego
    }

    void Update()
    {
        if (!isDead) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f; // Reanuda tiempo
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reinicia nivel
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("SeleccionDePersonajes"); // Cambia por tu nombre exacto de escena
        }
    }

    // Esto dibuja el texto en pantalla SIN NECESIDAD DE CANVAS
    void OnGUI()
    {
        if (isDead)
        {
            GUI.skin.label.fontSize = 70;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;

            // Dibuja el cartel de "HAS MUERTO"
            string texto = "HAS MUERTO\nPresiona R para Reintentar\nPresiona B para Seleccion";

            GUI.skin.label.normal.textColor = Color.white;
            GUI.Label(new Rect(3, 3, Screen.width, Screen.height), texto);
            GUI.skin.label.normal.textColor = Color.black;
            GUI.Label(new Rect(0, 0, Screen.width, Screen.height), texto);
        }
    }
}
