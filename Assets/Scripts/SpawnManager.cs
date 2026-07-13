using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] prefabsPersonajes;
    public bool modoPrueba = false;
    public int indicePrueba = 1;

    void Start()
    {
        Debug.Log("SpawnManager: Empezando..."); // Chivato 1

        int idPersonajes;

        if (modoPrueba)
        {
            idPersonajes = indicePrueba;
            Debug.Log("Modo Prueba: Intentando spawnear indice " + idPersonajes);
        }
        else
        {
            idPersonajes = PlayerPrefs.GetInt("PersonajeElegido", 0);
            Debug.Log("Modo Normal: Intentando spawnear indice guardado " + idPersonajes);
        }

        if (idPersonajes > 0 && idPersonajes <= prefabsPersonajes.Length)
        {
            if (prefabsPersonajes[idPersonajes - 1] == null)
            {
                Debug.LogError("¡ERROR! El prefab en el array es NULO. Revisa el Inspector.");
            }
            else
            {
                Instantiate(prefabsPersonajes[idPersonajes - 1], transform.position, Quaternion.identity);
                Debug.Log("¡Ariel debería haber aparecido!");
            }
        }
        else
        {
            Debug.LogWarning("SpawnManager: Índice no válido o array vacío. Revisa el Inspector.");
        }
    }
}