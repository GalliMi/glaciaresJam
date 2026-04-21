using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float force = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // click izquierdo
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject proj = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);

        proj.layer = gameObject.layer;

        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();

        // Dirección hacia adelante + un poco hacia arriba para la parábola
        //Vector3 direction = transform.forward * 10f + transform.up * 0.5f;

        Vector2 direction = shootPoint.right + (shootPoint.up * 0.5f);

        rb.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }
}