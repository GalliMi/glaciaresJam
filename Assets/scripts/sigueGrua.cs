using UnityEngine;

public sealed class sigueGrua : MonoBehaviour
{
    public Transform grua; // Arrastrar personajes aca
     Vector3 offset = new Vector3(50, 50, 0); // Ajuste: derecha y arriba

    private void LateUpdate()
    {
        if (grua != null)
        {
            // Convierte la posicion 3D del mundo a posicion 2D de pantalla
            Vector3 screenPos = Camera.main.WorldToScreenPoint(grua.position);

            // Aplica la posicion + el offset
            transform.position = screenPos + offset;
        }
    }
}
