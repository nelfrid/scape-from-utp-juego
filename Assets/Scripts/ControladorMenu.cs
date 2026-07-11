using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorMenu : MonoBehaviour
{
    private string escenaProxima = "SeleccionDePersonajes"; // Nombre de la escena a cargar
    [Header("Iconos de Sonido")]
    public Image componentImageButtonSonido;
    public Sprite iconoSonidoActivo;
    public Sprite iconoSonidoInactivo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SiguienteEscena();
        }

    }

    void SiguienteEscena()
    {
        SceneManager.LoadScene(escenaProxima);
    }

    public void MostrarTutorial()
    {
        Debug.Log("Mostrar tutorial");
    }

    public void SilenciarAudio()
    {
        if (AudioListener.volume > 0)
        {
            AudioListener.volume = 0f;
            Debug.Log("Audio Silenciado");
            if(componentImageButtonSonido != null && iconoSonidoInactivo != null)
            {
                componentImageButtonSonido.sprite = iconoSonidoInactivo;
            }

        }

        else
        {
            AudioListener.volume = 1f;
            Debug.Log("Audio Activado");
            if(componentImageButtonSonido != null && iconoSonidoActivo != null)
            {
                componentImageButtonSonido.sprite = iconoSonidoActivo;
            }   
        }
    }
}

