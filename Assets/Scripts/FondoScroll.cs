using UnityEngine;
using UnityEngine.UI;

public class FondoScroll : MonoBehaviour
{
    [Header("Configuración")]
    public RawImage imagenFondo;
    public float velocidadScroll = 0.05f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (imagenFondo != null)
        {
            Rect uvRect = imagenFondo.uvRect;
            uvRect.x += velocidadScroll * Time.deltaTime;
            imagenFondo.uvRect = uvRect;    
        }
        
    }
}
