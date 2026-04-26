using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controla un botón de salida que aparece en la esquina superior derecha durante el juego.
/// Permite al jugador salir del juego en cualquier momento, volviendo al menú principal o cerrando la aplicación.
/// </summary>
public class BotonSalirJuego : MonoBehaviour
{
    [Header("Referencias a UI")]
    [Tooltip("Botón de salir")]
    public Button botonSalir;
    
    [Tooltip("Texto del botón")]
    public TextMeshProUGUI textoBoton;
    
    [Tooltip("Canvas del botón para mostrar/ocultar")]
    public GameObject canvasBoton;

    [Header("Configuración")]
    [Tooltip("Texto que muestra el botón")]
    public string textoSalir = "Salir";
    
    [Tooltip("Nombre de la escena del menú principal")]
    public string nombreEscenaMenu = "MenuPrincipal";
    
    [Tooltip("Si es true, sale al menú principal. Si es false, cierra la aplicación")]
    public bool salirAlMenu = true;

    [Header("Configuración Visual")]
    [Tooltip("Posición del botón en pantalla (coordenadas de pantalla)")]
    public Vector2 posicionBoton = new Vector2(10, 10);
    
    [Tooltip("Tamaño del botón")]
    public Vector2 tamañoBoton = new Vector2(100, 40);

    void Start()
    {
        // Inicializar el botón y su posición
        ConfigurarBoton();
        
        // Configurar el evento del botón
        ConfigurarEventoBoton();
        
        //Configurar colores del botón a gris claro
        ConfigurarColoresBoton();
        
        Debug.Log("Botón de salir del juego inicializado");
    }

    /// <summary>
    /// Configura la posición y apariencia visual del botón
    /// </summary>
    void ConfigurarBoton()
    {
        // Si no se asignó un canvas, intentamos encontrarlo o crear uno
        if (canvasBoton == null)
        {
            // Buscar un canvas existente
            Canvas[] canvases = FindObjectsOfType<Canvas>();
            if (canvases.Length > 0)
            {
                canvasBoton = canvases[0].gameObject;
            }
            else
            {
                Debug.LogWarning("No se encontró un Canvas. Creando uno nuevo...");
                CrearCanvasBoton();
            }
        }

        // Si no se asignó un botón, intentamos encontrarlo o crearlo
        if (botonSalir == null)
        {
            botonSalir = GetComponentInChildren<Button>();
            if (botonSalir == null)
            {
                Debug.LogWarning("No se encontró un componente Button. Creando botón automáticamente...");
                CrearBotonAutomaticamente();
            }
        }

        // Configurar el texto del botón
        if (textoBoton == null)
        {
            textoBoton = botonSalir.GetComponentInChildren<TextMeshProUGUI>();
            if (textoBoton == null)
            {
                Debug.LogWarning("No se encontró texto TextMeshPro en el botón");
            }
        }

        // Establecer el texto
        if (textoBoton != null)
        {
            textoBoton.text = textoSalir;
        }

        // Asegurarse de que el botón esté visible
        if (canvasBoton != null)
        {
            canvasBoton.SetActive(true);
        }

        Debug.Log("Botón configurado correctamente");
    }

    /// <summary>
    /// Configura los colores del botón de salir a gris claro
    /// </summary>
    void ConfigurarColoresBoton()
    {
        if (botonSalir != null)
        {
            // Obtener el componente Image del botón
            Image imagenBoton = botonSalir.GetComponent<Image>();
            if (imagenBoton != null)
            {
                // Color gris claro opaco
                Color colorGrisClaro = new Color(0.8f, 0.8f, 0.8f, 1f); // RGB: 204, 204, 204
                
                // Cambiar el color base
                imagenBoton.color = colorGrisClaro;
                
                // Configurar colores para diferentes estados del botón
                ColorBlock colores = botonSalir.colors;
                colores.normalColor = colorGrisClaro;
              colores.highlightedColor = new Color(0.196f, 0.820f, 0.690f, 1f);
                colores.pressedColor = new Color(0.6f, 0.6f, 0.6f, 1f); // Gris más oscuro para presionado
                colores.selectedColor = colorGrisClaro;
                botonSalir.colors = colores;
                
                Debug.Log("Color del botón de salir configurado a gris claro");
            }
            else
            {
                Debug.LogWarning("No se encontró componente Image en el botón de salir");
            }
        }
        else
        {
            Debug.LogWarning("Botón de salir es null, no se pueden configurar colores");
        }
    }

    /// <summary>
    /// Crea un botón automáticamente si no existe uno
    /// </summary>
    void CrearBotonAutomaticamente()
    {
        // Si no hay canvas, crear uno primero
        if (canvasBoton == null)
        {
            CrearCanvasBoton();
        }

        // Crear el botón como hijo de este GameObject
        GameObject botonGO = new GameObject("BotonSalir");
        botonGO.transform.SetParent(transform, false);

        // Añadir componente Button
        botonSalir = botonGO.AddComponent<Button>();

        // Añadir imagen de fondo
        Image imagenFondo = botonGO.AddComponent<Image>();
        imagenFondo.color = new Color(0.8f, 0.8f, 0.8f, 1f); // Gris claro opaco

        // Crear el texto
        GameObject textoGO = new GameObject("Texto");
        textoGO.transform.SetParent(botonGO.transform, false);
        textoBoton = textoGO.AddComponent<TextMeshProUGUI>();
        textoBoton.text = textoSalir;
        textoBoton.fontSize = 16;
        textoBoton.color = Color.white;
        textoBoton.alignment = TextAlignmentOptions.Center;

        // Configurar RectTransform del botón para que esté en la esquina superior derecha
        RectTransform botonRect = botonGO.GetComponent<RectTransform>();
        botonRect.anchorMin = new Vector2(1, 1); // Esquina superior derecha
        botonRect.anchorMax = new Vector2(1, 1);
        botonRect.pivot = new Vector2(1, 1);
        botonRect.anchoredPosition = new Vector2(-posicionBoton.x, -posicionBoton.y);
        botonRect.sizeDelta = tamañoBoton;

        // Configurar RectTransform del texto
        RectTransform textoRect = textoGO.GetComponent<RectTransform>();
        textoRect.anchorMin = Vector2.zero;
        textoRect.anchorMax = Vector2.one;
        textoRect.offsetMin = Vector2.zero;
        textoRect.offsetMax = Vector2.zero;

        // Si el canvas es de tipo Screen Space Overlay, necesitamos ajustar la posición
        if (canvasBoton != null)
        {
            Canvas canvas = canvasBoton.GetComponent<Canvas>();
            if (canvas != null && canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                // Mover el botón para que sea hijo del canvas en lugar de este GameObject
                botonGO.transform.SetParent(canvasBoton.transform, false);
                Debug.Log("Botón movido al Canvas para Screen Space Overlay");
            }
        }

        Debug.Log("Botón creado automáticamente");
    }

    /// <summary>
    /// Crea un canvas y botón automáticamente si no existen
    /// </summary>
    void CrearCanvasBoton()
    {
        // Crear el Canvas
        GameObject nuevoCanvas = new GameObject("CanvasSalir");
        Canvas canvas = nuevoCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = nuevoCanvas.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        nuevoCanvas.AddComponent<GraphicRaycaster>();

        // Crear el botón
        GameObject botonGO = new GameObject("BotonSalir");
        botonGO.transform.SetParent(nuevoCanvas.transform, false);

        // Añadir componente Button
        botonSalir = botonGO.AddComponent<Button>();

        // Añadir imagen de fondo
        Image imagenFondo = botonGO.AddComponent<Image>();
        imagenFondo.color = new Color(0.8f, 0.8f, 0.8f, 1f); // Gris claro opaco

        // Crear el texto
        GameObject textoGO = new GameObject("Texto");
        textoGO.transform.SetParent(botonGO.transform, false);
        textoBoton = textoGO.AddComponent<TextMeshProUGUI>();
        textoBoton.text = textoSalir;
        textoBoton.fontSize = 16;
        textoBoton.color = Color.white;
        textoBoton.alignment = TextAlignmentOptions.Center;

        // Configurar RectTransform del botón
        RectTransform botonRect = botonGO.GetComponent<RectTransform>();
        botonRect.anchorMin = new Vector2(1, 1); // Esquina superior derecha
        botonRect.anchorMax = new Vector2(1, 1);
        botonRect.pivot = new Vector2(1, 1);
        botonRect.anchoredPosition = new Vector2(-posicionBoton.x, -posicionBoton.y);
        botonRect.sizeDelta = tamañoBoton;

        // Configurar RectTransform del texto
        RectTransform textoRect = textoGO.GetComponent<RectTransform>();
        textoRect.anchorMin = Vector2.zero;
        textoRect.anchorMax = Vector2.one;
        textoRect.offsetMin = Vector2.zero;
        textoRect.offsetMax = Vector2.zero;

        canvasBoton = nuevoCanvas;
        Debug.Log("Canvas y botón creados automáticamente");
    }

    /// <summary>
    /// Configura el evento onClick del botón
    /// </summary>
    void ConfigurarEventoBoton()
    {
        if (botonSalir != null)
        {
            botonSalir.onClick.AddListener(SalirDelJuego);
            Debug.Log("Evento del botón de salir configurado");
        }
        else
        {
            Debug.LogError("Error: No se puede configurar el evento porque botonSalir es null");
        }
    }

    /// <summary>
    /// Se ejecuta cuando el jugador presiona el botón de salir
    /// </summary>
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        
        // Restaurar el tiempo del juego antes de salir
        Time.timeScale = 1f;

        if (salirAlMenu)
        {
            // Volver al menú principal
            if (!string.IsNullOrEmpty(nombreEscenaMenu))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(nombreEscenaMenu);
            }
            else
            {
                Debug.LogError("Error: Nombre de escena del menú no configurado");
            }
        }
        else
        {
            // Cerrar la aplicación
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }

    /// <summary>
    /// Muestra u oculta el botón de salir
    /// </summary>
    /// <param name="mostrar">True para mostrar, false para ocultar</param>
    public void MostrarBoton(bool mostrar)
    {
        if (canvasBoton != null)
        {
            canvasBoton.SetActive(mostrar);
        }
    }

    /// <summary>
    /// Cambia el comportamiento del botón (salir al menú o cerrar aplicación)
    /// </summary>
    /// <param name="alMenu">True para salir al menú, false para cerrar aplicación</param>
    public void SetModoSalida(bool alMenu)
    {
        salirAlMenu = alMenu;
        
        // Actualizar el texto del botón según el modo
        if (textoBoton != null)
        {
            textoBoton.text = alMenu ? "Menú" : "Salir";
        }
    }

    void OnDestroy()
    {
        // Limpiar el listener para evitar memory leaks
        if (botonSalir != null)
        {
            botonSalir.onClick.RemoveAllListeners();
        }
    }
}
