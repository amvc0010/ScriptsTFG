using UnityEngine;
using UnityEngine.UI;

public class BrujulaUI : MonoBehaviour
{
    public RawImage imagenBrujula; //se declara la imagen de la brujula (flecha roja)

    private Compass brujula; // este es el componente brujula del unity

    void Start()
    {
        brujula = Input.compass; //al iniciar la app, guardas el componente brujula (compass) en la variable brujula
        brujula.enabled = true; // se activa
    }

    void Update() //una vez esta iniciado
    {
        float heading = Input.compass.trueHeading; //se crea la variable heading para indicarle a la brujula la posicion en vertical del telefono
        Quaternion rotation = Quaternion.Euler(0, 0, heading); // ej: si giro el telefono a la derecha, que lo detecte y gire a la derecha la brujula
        imagenBrujula.transform.rotation = rotation; //actualiza la imagen de la brujula para que vaya apuntando
    }
}