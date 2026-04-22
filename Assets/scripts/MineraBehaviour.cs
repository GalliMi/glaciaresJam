using UnityEngine;
using UnityEngine.UI;


public class MineraBehaviour : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public Image healthBar; // referencia al slider
    public Glaciar glaciar;

    public float velocidad = 1;


    bool enPeriglaciar = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.fillAmount = 1f;
        //healthBar.value = currentHealth;
    }

    private void Update()
    {

        ///MOVIMIENTO . SI ENTRA A LA ZONA PERIGLACIAR SE DETIENE 
        if (!enPeriglaciar)
        {
            transform.Translate(Vector3.left  *velocidad * Time.deltaTime);

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
           // collision.rigidbody;
        }


      


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("periglaciar"))
        {
            enPeriglaciar = true;

            glaciar.periglaciarInvadido = true;

            glaciar.cantidadGruas++;

            Debug.Log("EN PERIGLACIAR");
            // collision.rigidbody;
        }
    }
}

