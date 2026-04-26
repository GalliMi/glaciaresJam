using UnityEngine;

public class cartelero : MonoBehaviour
{

    public GameObject cartel;
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
            Destroy(cartelInstanciado, 8f);


            dejoCartel = true;
        }


    }
}
