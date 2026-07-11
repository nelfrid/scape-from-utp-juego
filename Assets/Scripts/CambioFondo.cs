using UnityEngine;
using UnityEngine.UI;
using Unity.Collections;
using Unity.VisualScripting;
using System.Collections;

public class CambioFondo : MonoBehaviour
{
    [Header("Configuracion de Fondos")]
    public Sprite[] fondos;
    public float tiempoCambioFondo = 5f;
    public float velocidadFondo = 0.015f;
    public float duracionTransicion = 1f;

    [Header("Referencias de Interfaz")]
    public Image FondoPrincipal;
    public Image FondoTransicion;



    private int indiceFondo = 0;
    private Vector3 escalaInicial = new Vector3(1f, 1f, 1f);

    void Start()
    {
        if (fondos.Length == 0) return;
        {
            ForzarCentroYEscala(FondoPrincipal.rectTransform);
            ForzarCentroYEscala(FondoTransicion.rectTransform);

            FondoPrincipal.sprite = fondos[indiceFondo];
            FondoPrincipal.color = Color.white;

            Color c = Color.white;
            c.a = 0f;
            FondoTransicion.color = c;

            StartCoroutine(BucleFondos());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        FondoPrincipal.transform.localScale += Vector3.one * velocidadFondo * Time.deltaTime;

        if (FondoTransicion.color.a > 0f)
        {
            FondoTransicion.transform.localScale += Vector3.one * velocidadFondo * Time.deltaTime;
        }
    }

    IEnumerator BucleFondos()
    {
        while (true)
        {
            // Espera el tiempo configurado menos el tiempo que dura la transición
            yield return new WaitForSeconds(tiempoCambioFondo - duracionTransicion);
            int SiguienteIndice = (indiceFondo + 1) % fondos.Length;
            FondoTransicion.sprite = fondos[SiguienteIndice];
            FondoTransicion.color = new Color(1f, 1f, 1f, 0f);
            FondoTransicion.rectTransform.localScale = escalaInicial;
            FondoTransicion.rectTransform.anchoredPosition = Vector2.zero;

            float tiempo = 0;
            while (tiempo < duracionTransicion)
            {
                tiempo += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f,tiempo / duracionTransicion);
                Color c = FondoTransicion.color; 
                c.a = alpha;
                FondoTransicion.color = c;
                yield return null;
            }

            indiceFondo = SiguienteIndice;
            FondoPrincipal.sprite = fondos[indiceFondo];
            FondoPrincipal.rectTransform.localScale = FondoTransicion.rectTransform.localScale;
            FondoPrincipal.rectTransform.anchoredPosition = Vector2.zero;

            Color cReset = FondoTransicion.color;
            cReset.a = 0f;
            FondoTransicion.color = cReset;
        }
    }

    void ForzarCentroYEscala(RectTransform rect)
    {
        rect.anchoredPosition = Vector2.zero;
        rect.localScale = escalaInicial;
    }
}
