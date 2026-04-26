using UnityEngine;
using UnityEngine.UI;


public class MineraBehaviour : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Image healthBar; // referencia al slider
    public Glaciar glaciar;

    public float velocidad = 1;

    public bool enPeriglaciar = false;

    public infoController info;
    public GameController gameCont;


    void Start()
        
    {
        
        currentHealth = maxHealth;
        healthBar.fillAmount = 1f;
    }

    private void Update()
    {

        ///MOVIMIENTO . SI ENTRA A LA ZONA PERIGLACIAR SE DETIENE 
        ///
     
        
            if (!enPeriglaciar)
            {
                transform.Translate(Vector3.left * velocidad * Time.deltaTime);

            }

        if (currentHealth <= 0)
        {
            Die();
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
        Debug.Log("El objeto muri�");
        gameCont.minerasDerrotadas++;
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ataque"))
        {
            Debug.Log("ataco");
            collision.gameObject.SetActive(false);
            TakeDamage(10f);
        }

        if (collision.gameObject.CompareTag("cartel"))
        {
            TakeDamage(20f);

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Static;
            Debug.Log("CARTEL");
        }


      


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("periglaciar"))
        {
            enPeriglaciar = true;

            glaciar.periglaciarInvadido = true;

            glaciar.cantidadGruas++;

            info.instancia++;

            info.pausa = true;

            Debug.Log("EN PERIGLACIAR");
        }
    }
}

