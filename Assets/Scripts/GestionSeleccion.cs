using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;   

public class GestionSeleccion : MonoBehaviour
{
    private int personajeSeleccion = 0;
    [Header("Visualizacion y Sonidos")]
    [Tooltip("Arrastra aqui los objetos de la UI que quieras activar o desactivar")]
    public GameObject[] personajesUI;
    public GameObject panelError;
    public AudioSource reproductorSonido;
    public AudioClip sonidoSeleccion;

    public void ReproducirSonidoBoton()
    {
        if (sonidoSeleccion!= null)
        {
            AudioSource.PlayClipAtPoint(sonidoSeleccion, Camera.main.transform.position);
        }
    }

    void Start()
    {
        personajeSeleccion = 0;
        ActualizarVisual(-1);

        if (panelError != null) panelError.SetActive(false);
    }

    public void SeleccionarPersonaje(int indicePersonaje)
    {
        personajeSeleccion = indicePersonaje + 1;
        PlayerPrefs.SetInt("PersonajeElegido", personajeSeleccion);
        PlayerPrefs.Save();

        ActualizarVisual(indicePersonaje);
        Debug.Log("Has seleccionado al personaje: " + personajeSeleccion);

    }
    private void ActualizarVisual(int indice)
    {
        for (int i = 0; i < personajesUI.Length; i++)
        {
            if (personajesUI[i] != null)
            {
                personajesUI[i].SetActive(false);
            }
        }

        if (indice >= 0 && indice < personajesUI.Length && personajesUI[indice] != null)
        {
            personajesUI[indice].SetActive(true);
            
        }
    }
    public void IniciarJuego()
    {
        
        if(personajeSeleccion == 0)
        {
            
            if(panelError != null)
            {
                panelError.SetActive(true);
            }
            else
            {
                StartCoroutine(EsperarYCambiarEscena());

            }

        }
        
    }

    public void CerrarPanelError()
    {
        if (panelError != null)
        {
            panelError.SetActive(false);
        }
    }

    public IEnumerator EsperarYCambiarEscena()
    {
        yield return new WaitForSeconds(0.2f); // Espera 0.2 segundos
        SceneManager.LoadScene("Nivel 1");
    }

    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú principal...");    
        SceneManager.LoadScene("MenuPrincipal");
    }
}

