using UnityEngine;

public partial class GameController : MonoBehaviour
{

    [Header("Prefabs Disponibles")]
    public GameObject prefabA;
    public GameObject prefabS;
    public GameObject prefabD;

    private GameObject prefabSeleccionado;


    // Las alturas Y de tus 3 filas
    public float[] posicionesY = new float[] { 2f, 0f, -2f };

    // Los nombres EXACTOS de tus Layers en Unity
    public string[] nombresLayers = new string[] { "Ruta0", "Ruta1", "Ruta2" };

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A)) { prefabSeleccionado = prefabA; Debug.Log("Seleccionado: Prefab A"); }
        if (Input.GetKeyDown(KeyCode.S)) { prefabSeleccionado = prefabS; Debug.Log("Seleccionado: Prefab S"); }
        if (Input.GetKeyDown(KeyCode.D)) { prefabSeleccionado = prefabD; Debug.Log("Seleccionado: Prefab D"); }

        if (Input.GetKeyDown(KeyCode.Alpha1)) InstanciarEnFila(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) InstanciarEnFila(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) InstanciarEnFila(2);
    }

    void InstanciarEnFila(int indice)
    {
        // Validar que se haya seleccionado un prefab
        if (prefabSeleccionado == null)
        {
            Debug.LogError("Error: Debes seleccionar un prefab primero (presiona A, S o D)");
            return;
        }

        // 1. Crear la posición (X puede ser 0 o donde quieras que aparezcan)
        Vector3 posicionSpawn = new Vector3(0, posicionesY[indice], 0);

        // 2. Instanciar
        GameObject nuevo = Instantiate(prefabSeleccionado, posicionSpawn, Quaternion.identity);
        
        // 3. Asignar el Layer
        int layerID = LayerMask.NameToLayer(nombresLayers[indice]);

        if (layerID != -1) // Verificar que el layer exista
        {
            nuevo.layer = layerID;
            // Si el objeto tiene hijos, aplicamos el layer a todos
            AsignarLayerAHijos(nuevo, layerID);
            Debug.Log("Instanciado en " + nombresLayers[indice]);
        }
        else
        {
            Debug.LogError("¡Error! No existe el Layer llamado: " + nombresLayers[indice]);
        }
    }

    void AsignarLayerAHijos(GameObject padre, int nuevaLayer)
    {
        padre.layer = nuevaLayer;
        foreach (Transform hijo in padre.transform)
        {
            AsignarLayerAHijos(hijo.gameObject, nuevaLayer);
        }
    }
}
