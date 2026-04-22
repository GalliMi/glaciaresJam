using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Glaciar : MonoBehaviour
{
    public float maxHealth = 500f;
    public float currentHealth;

    public Image healthBar;
    public Image healthBarFondo;

    public bool periglaciarInvadido = false;
    public int cantidadGruas = 0;

    public Canvas canvas;

  //  public Renderer fadeFinal;
    public SpriteRenderer fadeFinal;

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
            Debug.Log("Cantidad gruas :::: " + cantidadGruas);
          
            if (!healthBar.gameObject.activeSelf)
            {
                healthBar.gameObject.SetActive(true);
                healthBarFondo.gameObject.SetActive(true);

            }


        }


        if(cantidadGruas == 1)
        {
            TakeDamage(0.005f);
        } else if (cantidadGruas == 2)
        {
            TakeDamage(0.02f);

        } else if(cantidadGruas == 3)
        {
            TakeDamage(0.05f);
        }


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
        Time.timeScale = 0f;
    }



}
