using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class infoController : MonoBehaviour
{
    [System.Serializable]
    public class InfoData
    {
        public string texto;
        public Sprite imagen;
    }

    [Header("Estado")]
    public int instancia = 0;
    public bool pausa = false;

    [Header("UI")]
    public TextMeshProUGUI instanciaTexto;
    public Image imagenUI;
    public Button botonCierre;
    private Image panelImage;

    [Header("Contenido")]
    public InfoData[] infos;

    private float velocidadAnterior = 1f;
    private bool yaEstabaPausado = false;

    void Awake()
    {
        panelImage = GetComponent<Image>();
    }

    void Start()
    {
        botonCierre.onClick.AddListener(Cerrar);
        MostrarUI(false); // aseguramos que arranque oculto
    }

    void Update()
    {
        if (pausa)
            ActivarPausa();
        else
            DesactivarPausa();
    }

    void ActivarPausa()
    {
        if (yaEstabaPausado) return;

        velocidadAnterior = Time.timeScale;
        Time.timeScale = 0f;
        yaEstabaPausado = true;

        Debug.Log("Juego pausado. Velocidad guardada: " + velocidadAnterior);

        MostrarUI(true);

        if (instancia >= 0 && instancia < infos.Length+1)
        {
            //instanciaTexto.text = infos[instancia].texto;
            Debug.Log("IMAGEN " + (instancia - 1));
            imagenUI.sprite = infos[instancia-1].imagen;
        }
        else
        {
            Debug.LogWarning("Instancia fuera de rango");
        }
    }

    void DesactivarPausa()
    {
        if (!yaEstabaPausado) return;

        Time.timeScale = velocidadAnterior;
        yaEstabaPausado = false;

        Debug.Log("Juego reanudado a: " + velocidadAnterior);

        MostrarUI(false);
    }

    void MostrarUI(bool estado)
    {
        panelImage.enabled = estado;
        botonCierre.gameObject.SetActive(estado);
        instanciaTexto.gameObject.SetActive(estado);
        imagenUI.gameObject.SetActive(estado);
    }

    void Cerrar()
    {
        pausa = false;
    }
}