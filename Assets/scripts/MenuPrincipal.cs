using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // TextMeshPro

/// <summary>
/// Controla el menú principal del juego con botones de comenzar y salir.
/// Se encarga de gestionar la navegación inicial y cierre de la aplicación.
/// </summary>
public class MenuPrincipal : MonoBehaviour
{
    [Header("Referencias a UI")]
    [Tooltip("Botón para comenzar el juego")]
    public Button botonComenzar;
    
    [Tooltip("Botón para salir del juego")]
    public Button botonSalir;
    
    [Tooltip("Canvas del menú principal para mostrar/ocultar")]
    public GameObject canvasMenuPrincipal;

    [Header("Configuración")]
    [Tooltip("Nombre de la escena del juego a cargar")]
    public string nombreEscenaJuego = "SampleScene";

    void Start()
    {
        // Inicializar los botones y sus eventos
        ConfigurarBotones();
        
        // Configurar colores de los botones
        ConfigurarColoresBotones();
        
        // Asegurarse de que el tiempo del juego esté normal al iniciar el menú
        Time.timeScale = 1f;
        
        Debug.Log("Menú principal inicializado");
    }

    /// <summary>
    /// Configura los listeners de los botones del menú
    /// </summary>
    void ConfigurarBotones()
    {
        // Verificar que los botones estén asignados
        if (botonComenzar != null)
        {
            botonComenzar.onClick.AddListener(ComenzarJuego);
            Debug.Log("Botón 'Comenzar' configurado");
        }
        else
        {
            Debug.LogError("Error: Botón 'Comenzar' no asignado en el Inspector");
        }

        if (botonSalir != null)
        {
            botonSalir.onClick.AddListener(SalirDelJuego);
            Debug.Log("Botón 'Salir' configurado");
        }
        else
        {
            Debug.LogError("Error: Botón 'Salir' no asignado en el Inspector");
        }
    }

    /// <summary>
    /// Configura los colores de fondo de los botones a gris claro
    /// </summary>
    void ConfigurarColoresBotones()
    {
        // Color gris claro para los botones
        Color colorGrisClaro = new Color(0.8f, 0.8f, 0.8f, 1f); // RGB: 204, 204, 204
        
        // Configurar color del botón "Comenzar"
        if (botonComenzar != null)
        {
            Image imagenComenzar = botonComenzar.GetComponent<Image>();
            if (imagenComenzar != null)
            {
                imagenComenzar.color = colorGrisClaro;
                
                // También configurar colores para diferentes estados
                ColorBlock colores = botonComenzar.colors;
                colores.normalColor = colorGrisClaro;
                colores.highlightedColor = new Color(0.9f, 0.9f, 0.9f, 1f); // Gris más claro para hover
                colores.pressedColor = new Color(0.6f, 0.6f, 0.6f, 1f); // Gris más oscuro para presionado
                colores.selectedColor = colorGrisClaro;
                botonComenzar.colors = colores;
                
                Debug.Log("Color del botón 'Comenzar' configurado a gris claro");
            }
        }

        // Configurar color del botón "Salir"
        if (botonSalir != null)
        {
            Image imagenSalir = botonSalir.GetComponent<Image>();
            if (imagenSalir != null)
            {
                imagenSalir.color = colorGrisClaro;
                
                // También configurar colores para diferentes estados
                ColorBlock colores = botonSalir.colors;
                colores.normalColor = colorGrisClaro;
                colores.highlightedColor = new Color(0.9f, 0.9f, 0.9f, 1f); // Gris más claro para hover
                colores.pressedColor = new Color(0.6f, 0.6f, 0.6f, 1f); // Gris más oscuro para presionado
                colores.selectedColor = colorGrisClaro;
                botonSalir.colors = colores;
                
                Debug.Log("Color del botón 'Salir' configurado a gris claro");
            }
        }
    }

    /// <summary>
    /// Inicia el juego cargando la escena principal
    /// </summary>
    public void ComenzarJuego()
    {
        Debug.Log("Iniciando juego...");
        
        // Cargar la escena del juego
        if (!string.IsNullOrEmpty(nombreEscenaJuego))
        {
            SceneManager.LoadScene(nombreEscenaJuego);
        }
        else
        {
            Debug.LogError("Error: Nombre de escena del juego no configurado");
        }
    }

    /// <summary>
    /// Cierra la aplicación
    /// </summary>
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        
        // En el editor, detiene la reproducción
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // En build, cierra la aplicación
            Application.Quit();
        #endif
    }

    /// <summary>
    /// Muestra u oculta el menú principal
    /// </summary>
    /// <param name="mostrar">True para mostrar, false para ocultar</param>
    public void MostrarMenuPrincipal(bool mostrar)
    {
        if (canvasMenuPrincipal != null)
        {
            canvasMenuPrincipal.SetActive(mostrar);
        }
    }

    void OnDestroy()
    {
        // Limpiar los listeners para evitar memory leaks
        if (botonComenzar != null)
        {
            botonComenzar.onClick.RemoveAllListeners();
        }
        
        if (botonSalir != null)
        {
            botonSalir.onClick.RemoveAllListeners();
        }
    }
}
