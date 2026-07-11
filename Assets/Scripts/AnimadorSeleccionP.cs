using UnityEngine;
using UnityEngine.UI;

public class AnimadorSeleccionP : MonoBehaviour
{
    public Sprite[] cuadros;
    public float velocidadAnimacion = 0.3f;
    private Image imagenUI;
    private int indiceCuadro = 0;
    private float tempo;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imagenUI = GetComponent<Image>();   
        
    }

    // Update is called once per frame
    void Update()
    {
        tempo += Time.deltaTime;
        if (tempo >= velocidadAnimacion) 
        {
            indiceCuadro = (indiceCuadro + 1) % cuadros.Length;
            imagenUI.sprite = cuadros[indiceCuadro];
            tempo = 0;
        }
    }
}
