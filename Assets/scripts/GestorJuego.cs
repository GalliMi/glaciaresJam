using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // TextMeshPro

/// <summary>
/// Gestiona el estado general del juego, incluyendo el sistema de derrota
/// Monitorea las barras de energía de los perglaciares y controla el Game Over
/// </summary>
public class GestorJuego : MonoBehaviour
{
    [Header("Referencias a Perglaciares")]
    [Tooltip("Referencia al script Glaciar del periglaciar 1")]
    public Glaciar periglaciar1;
    
    [Tooltip("Referencia al script Glaciar del periglaciar 2")]
    public Glaciar periglaciar2;
    
    [Tooltip("Referencia al script Glaciar del periglaciar 3")]
    public Glaciar periglaciar3;

    [Header("UI de Game Over")]
    [Tooltip("Canvas de Game Over para mostrar cuando el jugador pierde")]
    public GameObject canvasGameOver;
    
    [Tooltip("Texto del mensaje de Game Over")]
    public TextMeshProUGUI textoGameOver;
    
    [Tooltip("Botón para reiniciar el juego")]
    public Button botonReiniciar;
    
    [Tooltip("Botón para volver al menú principal")]
    public Button botonMenuPrincipal;

    [Header("Configuración")]
    [Tooltip("Mensaje que se muestra al perder")]
    public string mensajeDerrota = "Has perdido el juego";
    
    [Tooltip("Nombre de la escena del menú principal")]
    public string nombreEscenaMenu = "MenuPrincipal";
    
    [Tooltip("Nombre de la escena actual del juego")]
    public string nombreEscenaJuego = "SampleScene";

    [Header("Estado del Juego")]
    [Tooltip("Indica si el juego ha terminado")]
    public bool juegoTerminado = false;

    // Singleton para acceso global
    public static GestorJuego Instance { get; private set; }

    void Awake()
    {
        // Configurar singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Inicializar el estado del juego
        InicializarJuego();
        
        // Configurar listeners de botones
        ConfigurarBotones();
        
        Debug.Log("GestorJuego inicializado");
    }

    void Update()
    {
        // Solo verificar estado de derrota si el juego no ha terminado
        if (!juegoTerminado)
        {
            VerificarEstadoDerrota();
        }
    }

    /// <summary>
    /// Inicializa el estado del juego y oculta la UI de Game Over
    /// </summary>
    void InicializarJuego()
    {
        juegoTerminado = false;
        Time.timeScale = 1f; // Asegurar que el tiempo del juego esté normal
        
        // Ocultar canvas de Game Over
        if (canvasGameOver != null)
        {
            canvasGameOver.SetActive(false);
        }
        
        Debug.Log("Juego inicializado - Tiempo del juego: " + Time.timeScale);
    }

    /// <summary>
    /// Configura los listeners de los botones de Game Over
    /// </summary>
    void ConfigurarBotones()
    {
        // Configurar botón de reiniciar
        if (botonReiniciar != null)
        {
            botonReiniciar.onClick.AddListener(ReiniciarJuego);
            Debug.Log("Botón 'Reiniciar' configurado");
        }
        else
        {
            Debug.LogWarning("Advertencia: Botón 'Reiniciar' no asignado en el Inspector");
        }

        // Configurar botón de menú principal
        if (botonMenuPrincipal != null)
        {
            botonMenuPrincipal.onClick.AddListener(VolverAlMenu);
            Debug.Log("Botón 'Menú Principal' configurado");
        }
        else
        {
            Debug.LogWarning("Advertencia: Botón 'Menú Principal' no asignado en el Inspector");
        }
    }

    /// <summary>
    /// Verifica si todos los perglaciares han sido derrotados
    /// </summary>
    void VerificarEstadoDerrota()
    {
        // Verificar que todas las referencias estén asignadas
        if (periglaciar1 == null || periglaciar2 == null || periglaciar3 == null)
        {
            Debug.LogWarning("Advertencia: Faltan referencias a perglaciares en el Inspector");
            return;
        }

        // Verificar si las tres barras de energía están en cero
        bool periglaciar1Derrotado = periglaciar1.currentHealth <= 0;
        bool periglaciar2Derrotado = periglaciar2.currentHealth <= 0;
        bool periglaciar3Derrotado = periglaciar3.currentHealth <= 0;

        // Si todos los perglaciares están derrotados, activar Game Over
        if (periglaciar1Derrotado && periglaciar2Derrotado && periglaciar3Derrotado)
        {
            ActivarGameOver();
        }
    }

    /// <summary>
    /// Activa el estado de Game Over
    /// </summary>
    void ActivarGameOver()
    {
        if (juegoTerminado) return; // Evitar múltiples activaciones

        juegoTerminado = true;
        Time.timeScale = 0f; // Detener el tiempo del juego
        
        Debug.Log("GAME OVER - Todos los perglaciares han sido derrotados");

        // Mostrar UI de Game Over
        if (canvasGameOver != null)
        {
            canvasGameOver.SetActive(true);
            
            // Configurar texto de Game Over
            if (textoGameOver != null)
            {
                textoGameOver.text = mensajeDerrota;
            }
        }
        else
        {
            Debug.LogError("Error: Canvas de Game Over no asignado");
        }

        // Opcional: Desactivar otros elementos del juego
        DesactivarElementosJuego();
    }

    /// <summary>
    /// Desactiva elementos del juego cuando termina
    /// </summary>
    void DesactivarElementosJuego()
    {
        // Desactivar controles del jugador
        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
        {
            jugador.SetActive(false);
        }

        // Desactivar otros elementos si es necesario
        // Por ejemplo: spawners, enemigos, etc.
    }

    /// <summary>
    /// Reinicia el juego recargando la escena actual
    /// </summary>
    public void ReiniciarJuego()
    {
        Debug.Log("Reiniciando juego...");
        
        // Restaurar tiempo del juego antes de recargar
        Time.timeScale = 1f;
        
        // Recargar escena actual
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
    /// Vuelve al menú principal
    /// </summary>
    public void VolverAlMenu()
    {
        Debug.Log("Volviendo al menú principal...");
        
        // Restaurar tiempo del juego antes de cambiar de escena
        Time.timeScale = 1f;
        
        // Cargar escena del menú principal
        if (!string.IsNullOrEmpty(nombreEscenaMenu))
        {
            SceneManager.LoadScene(nombreEscenaMenu);
        }
        else
        {
            Debug.LogError("Error: Nombre de escena del menú no configurado");
        }
    }

    /// <summary>
    /// Método público para forzar el Game Over (útil para pruebas)
    /// </summary>
    public void ForzarGameOver()
    {
        Debug.Log("Forzando Game Over...");
        ActivarGameOver();
    }

    void OnDestroy()
    {
        // Limpiar listeners para evitar memory leaks
        if (botonReiniciar != null)
        {
            botonReiniciar.onClick.RemoveAllListeners();
        }
        
        if (botonMenuPrincipal != null)
        {
            botonMenuPrincipal.onClick.RemoveAllListeners();
        }
        
        // Limpiar singleton si este era la instancia
        if (Instance == this)
        {
            Instance = null;
        }
    }

    /// <summary>
    /// Método para verificar si el juego está activo (útil para otros scripts)
    /// </summary>
    /// <returns>True si el juego está activo, false si ha terminado</returns>
    public bool JuegoActivo()
    {
        return !juegoTerminado;
    }
}
