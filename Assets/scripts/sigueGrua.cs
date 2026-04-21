using UnityEngine;

public sealed class sigueGrua : MonoBehaviour
{
    public Transform grua; // Arrastra a tu personaje aquí
     Vector3 offset = new Vector3(100, 100, 0); // Ajuste: derecha y arriba

    private void LateUpdate()
    {
        if (grua != null)
        {
            // Convierte la posición 3D del mundo a posición 2D de pantalla
            Vector3 screenPos = Camera.main.WorldToScreenPoint(grua.position);

            // Aplica la posición + el offset
            transform.position = screenPos + offset;
        }
    }
}