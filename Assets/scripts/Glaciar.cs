using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using UnityEngine.SceneManagement;


public class Glaciar : MonoBehaviour
{
    public float maxHealth = 500f;
    public float currentHealth;

    public Image healthBar;
    public Image healthBarFondo;

    public bool periglaciarInvadido = false;
    public int cantidadGruas = 0;

    public Canvas canvas;

    public SpriteRenderer fondo;
    public Sprite[] imagenes;

    public GameObject[] faunaYFlora;

    public AudioSource audioSource;
    public AudioClip nuevoClip;

    bool cambioAudio = false;

    public GameController gameController;

   public int minerasActivas = 3;

    //  public Renderer fadeFinal;
    public SpriteRenderer fadeFinal;

    public GameObject botonReintentar;
    public GameObject botonSalir;


    void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (periglaciarInvadido)
        {
           // Debug.Log("Cantidad gruas :::: " + cantidadGruas);
          
            if (!healthBar.gameObject.activeSelf)
            {
                healthBar.gameObject.SetActive(true);
                healthBarFondo.gameObject.SetActive(true);

            }


        }


        if(cantidadGruas == 1)
        {
            TakeDamage(0.9f * Time.deltaTime);
            for (int i = 0; i < faunaYFlora.Length; i++)
            {
                faunaYFlora[i].gameObject.SetActive(false);
            }
        } else if (cantidadGruas == 2)
        {
            if (!cambioAudio)
            {
                audioSource.clip = nuevoClip;
                audioSource.Play();
                cambioAudio = true;
            }

            TakeDamage(4f * Time.deltaTime);
            fondo.sprite =imagenes[1];
        } else if(cantidadGruas == 3)
        {
            

            TakeDamage(15.0f * Time.deltaTime);
            fondo.sprite = imagenes[2];
        }


        //Debug.Log("MINERAS ACTIVAS " + minerasActivas);

       /* if (minerasActivas == 0 && cantidadGruas < 3)
        {
            StartCoroutine(FadeToBlack());
            audioSource.Stop();
            canvas.enabled = false;

        }*/



    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        Debug.Log("FIN del juego u.u");
        canvas.enabled = false;
        audioSource.Stop();

        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
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

        botonReintentar.SetActive(true);
        botonSalir.SetActive(true);


    }



}
