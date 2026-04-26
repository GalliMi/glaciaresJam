using UnityEngine;

public class cartelero : MonoBehaviour
{

    public GameObject cartel;
    public Sprite[] imagenes;
    bool dejoCartel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!dejoCartel) { 
        transform.Translate(Vector3.right * Time.deltaTime);
    } else
        {

            transform.Translate(Vector3.left * Time.deltaTime * 2);

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject cartelInstanciado = Instantiate(cartel, transform.position, Quaternion.identity);

            int indiceRandom = Random.Range(0, imagenes.Length);
            Sprite spriteElegido = imagenes[indiceRandom];

            SpriteRenderer sr = cartelInstanciado.GetComponent<SpriteRenderer>();
            sr.sprite = spriteElegido;

            Destroy(cartelInstanciado, 10f);


            dejoCartel = true;
        }


    }
}
