using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public partial class GameController : MonoBehaviour
{

    [Header("Prefabs Disponibles")]
    public GameObject prefabA;
    public GameObject prefabS;
    public GameObject prefabD;

    public GameObject fantasmaA;
    public GameObject fantasmaB;

    private GameObject prefabSeleccionado;

    private float seleccion;


    public float tiempoDeVidaA = 4f;
    public float tiempoDeVidaS = 7f;


    private bool puedeA = true;
    private bool puedeS = true;

    private GameObject ultimoA;
    private GameObject ultimoS;

    public Image cooldownA;
    public Image cooldownS;

    public int minerasDerrotadas= 0;

    public Canvas canvas;



    //  public Renderer fadeFinal;
    public SpriteRenderer fadeFinal;


    // Las alturas Y de tus 3 filass
    public float[] posicionesY = new float[] { 2f, 0f, -2f };

    // Los nombres EXACTOS de tus Layers en Unity
    public string[] nombresLayers = new string[] { "Ruta0", "Ruta1", "Ruta2" };

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A)) { prefabSeleccionado = prefabA; Debug.Log("Seleccionado: Prefab A"); }
        if (Input.GetKeyDown(KeyCode.S)) { prefabSeleccionado = prefabS; Debug.Log("Seleccionado: Prefab S"); }
       // if (Input.GetKeyDown(KeyCode.D)) { prefabSeleccionado = prefabD; Debug.Log("Seleccionado: Prefab D"); }

        if (Input.GetKeyDown(KeyCode.Alpha1)) InstanciarEnFila(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) InstanciarEnFila(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) InstanciarEnFila(2);

        Debug.Log("MINERASABAJO:" + minerasDerrotadas);

        if( minerasDerrotadas == 3)
        {

            StartCoroutine(FadeToWin());

        }

    }








    void InstanciarEnFila(int indice)
    {
        // Validar que se haya seleccionado un prefab
        if (prefabSeleccionado == null)
        {
            Debug.LogError("Error: Debes seleccionar un prefab primero (presiona A, S o D)");
            return;
        }


        if (prefabSeleccionado == prefabA && !puedeA) return;
        if (prefabSeleccionado == prefabS && !puedeS) return;


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

        if (prefabSeleccionado == prefabA)
        {
            ultimoA = nuevo;
            cooldownA.fillAmount = 1;
            StartCoroutine(ControlarInstancia(() => puedeA = false, () => puedeA = true, () => ultimoA, cooldownA,tiempoDeVidaA));
        }
        else if (prefabSeleccionado == prefabS)
        {
            ultimoS = nuevo;
            cooldownS.fillAmount = 1;
            StartCoroutine(ControlarInstancia(() => puedeS = false, () => puedeS = true, () => ultimoS, cooldownS,tiempoDeVidaS));
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

    IEnumerator ControlarInstancia(System.Action bloquear, System.Action desbloquear, System.Func<GameObject> obtenerObjeto, Image cooldownUI,float tiempoDeVida)
    {
        bloquear();

        float tiempo = tiempoDeVida;

        while (tiempo > 0)
        {
            tiempo -= Time.deltaTime;

           
            cooldownUI.fillAmount = tiempo / tiempoDeVida;

            yield return null;
        }

        cooldownUI.fillAmount = 0;

        GameObject obj = obtenerObjeto();

        if (obj != null)
        {
            Destroy(obj);
        }

        desbloquear();
    }


    IEnumerator FadeToWin()
    {
        canvas.enabled = false;

        Color color = fadeFinal.color;
        float fadeDuration = 2f; // Duración del fade en segundos
        float startAlpha = color.a;

        while (color.a < 1f)
        {
            color.a += Time.deltaTime / fadeDuration;
            color.a = Mathf.Clamp(color.a, startAlpha, 1f);
            fadeFinal.color = color;
            yield return null;
        }

        // Opcional: Detener el juego o cargar escena de game over
        Time.timeScale = 1f;



    }

}
