using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // libreria para las transiciones

public class UIManager : MonoBehaviour
{
    // Se importan los objetos del GameManager
    [SerializeField] private GameObject MenuPrincipal;
    [SerializeField] private GameObject Galeria;
    [SerializeField] private GameObject Empezar;
    [SerializeField] private GameObject Info;
    [SerializeField] private GameObject Ayuda;
    [SerializeField] private GameObject Historia;
    [SerializeField] private GameObject SiguienteTextoHistoria;
    [SerializeField] private GameObject AnteriorTextoHistoria;
    [SerializeField] private GameObject SiguienteEmpezar;
    [SerializeField] private GameObject AtrasEmpezar;

    // Se declaran las acciones de los objetos
    public RectTransform Portada;
    public RectTransform Texto1;
    public RectTransform Texto2;
    public RectTransform TextoFelicitacion;

    int AppIniciada=0; //cuando ejecutamos el programa
    //contador pagina texto historia
    int numTextoHistoria=0; //contador pagina historia

    float transicion = 0; //transicion si o no

    int numEmpezar=0; //contador pagina empezar

    // Start is called before the first frame update 
    void Start()
    {
        // Cuando se haga click en los objetos que tienen las siguientes acciones, ejecutaran las funciones
        GameManager.instance.OnMenuPrincipal += ActivarMenuPrincipal;
        GameManager.instance.OnGaleria += ActivarGaleria;
        GameManager.instance.OnEmpezar += ActivarEmpezar;
        GameManager.instance.OnInfo += ActivarInfo;
        GameManager.instance.OnAyuda += ActivarAyuda;
        GameManager.instance.OnHistoria += ActivarHistoria;
        GameManager.instance.OnSiguienteTextoHistoria += ActivarSiguienteTextoHistoria;
        GameManager.instance.OnAnteriorTextoHistoria += ActivarAnteriorTextoHistoria;
        GameManager.instance.OnSiguienteEmpezar += ActivarSiguienteEmpezar;
        GameManager.instance.OnAtrasEmpezar += ActivarAtrasEmpezar;
    }

    // Cuando click en btn Atras/Inicio, se oculta Galeria, Empezar, Camara... (todo) y se muestra  MenuPricipal
    private void ActivarMenuPrincipal()
    {
        
        if (AppIniciada==0) { //la aplicacion es la primera vez que se abre, aparecen los botones de primeras sin transicion
            AppIniciada=1;
        } else {              // si esta iniciada aparecen con transicion
            transicion= 0.3f;
        }

        // Pongo en todos los ejes 1 para ser mostrados. Las f son los segundos de transicion para hacer la accion
        MenuPrincipal.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0); //Este corresponde con el Panel, nunca tiene que tener transicion, por eso es 0 siempre
        //GetChild es coger todos los hijos y cogemos del 0 a X la accion que quiero que haga
        // DOScale es una libreria para modificar las medidas del objeto
        MenuPrincipal.transform.GetChild(1).transform.DOScale(new Vector3(1,1,1), transicion);
        MenuPrincipal.transform.GetChild(2).transform.DOScale(new Vector3(1,1,1), transicion);
        MenuPrincipal.transform.GetChild(3).transform.DOScale(new Vector3(1,1,1), transicion);
        MenuPrincipal.transform.GetChild(4).transform.DOScale(new Vector3(1,1,1), transicion);
        MenuPrincipal.transform.GetChild(5).transform.DOScale(new Vector3(1,1,1), transicion);
        MenuPrincipal.transform.GetChild(6).transform.DOScale(new Vector3(1,1,1), transicion);

        //con lo de arriba me carga el menu principal, y ocultamos los demas menus (abajo)

        //Ponemos en todos los ejes 0 para ser mostrados. Las f son 0 segundos de transicion para hacer la accion
        Galeria.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0);
        Galeria.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), transicion);
        Galeria.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), transicion);

        Historia.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0);
        Historia.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), transicion);
        Historia.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), transicion);
        Historia.transform.GetChild(3).transform.DOScale(new Vector3(0,0,0), transicion);
        Historia.transform.GetChild(4).transform.DOScale(new Vector3(0,0,0), transicion);
        Historia.transform.GetChild(5).transform.DOScale(new Vector3(0,0,0), transicion);

        Info.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0);
        Info.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), transicion);
        Info.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), transicion);

        Ayuda.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0);
        Ayuda.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), transicion);
        Ayuda.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), transicion);
            
        Empezar.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0);
        Empezar.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), transicion);
        Empezar.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), transicion);
        Empezar.transform.GetChild(3).transform.DOScale(new Vector3(0,0,0), transicion);
        Empezar.transform.GetChild(4).transform.DOScale(new Vector3(0,0,0), transicion);
        Empezar.transform.GetChild(5).transform.DOScale(new Vector3(0,0,0), transicion);
        Empezar.transform.GetChild(6).transform.DOScale(new Vector3(0,0,0), transicion);
        Empezar.transform.GetChild(7).transform.DOScale(new Vector3(0,0,0), transicion);
        Empezar.transform.GetChild(8).transform.DOScale(new Vector3(0,0,0), transicion);
        Empezar.transform.GetChild(9).transform.DOScale(new Vector3(0,0,0), transicion);
        Empezar.transform.GetChild(10).transform.DOScale(new Vector3(0,0,0), transicion);
        Empezar.transform.GetChild(11).transform.DOScale(new Vector3(0,0,0), transicion);
        Empezar.transform.GetChild(12).transform.DOScale(new Vector3(0,0,0), transicion);
    }

    //Cuando click en boton Galeria, se oculta MenuPricipal y se muestra Galeria
    private void ActivarGaleria()
    {
        //Ponemos en todos los ejes 0 para ser ocultos. Las f son los segundos de transicion para hacer la accion
        MenuPrincipal.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0f);
        MenuPrincipal.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(3).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(4).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(5).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(6).transform.DOScale(new Vector3(0,0,0), 0.3f);

        //Ponemos en todos los ejes 1 para ser mostrados. Las f son los segundos de transicion para hacer la accion
        Galeria.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0f);
        Galeria.transform.GetChild(1).transform.DOScale(new Vector3(1,1,1), 0.3f);
        Galeria.transform.GetChild(2).transform.DOScale(new Vector3(1,1,1), 0.5f);
    }

    private void ActivarHistoria()
    {
        numTextoHistoria=0;
        //Ponemos en todos los ejes 0 para ser ocultos. Las f son los segundos de transicion para hacer la accion
        MenuPrincipal.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0f);
        MenuPrincipal.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(3).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(4).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(5).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(6).transform.DOScale(new Vector3(0,0,0), 0.3f);

        //Ponemos en todos los ejes 1 para ser mostrados. Las f son los segundos de transicion para hacer la accion
        Historia.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0f);
        Historia.transform.GetChild(1).transform.DOScale(new Vector3(1,1,1), 0.3f);
        Historia.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0f);
        Historia.transform.GetChild(3).transform.DOScale(new Vector3(0,0,0), 0f);
        Historia.transform.GetChild(4).transform.DOScale(new Vector3(1,1,1), 0.3f);
        Historia.transform.GetChild(5).transform.DOScale(new Vector3(1,1,1), 0.3f);

        //movemos el texto a su posicion
        Texto1.DOAnchorPos(new Vector2 (1500, 130), 0, false); //la portada y el texto estan como desplazados
        Texto2.DOAnchorPos(new Vector2 (1500, 130), 0, false);
    }


    private void ActivarSiguienteTextoHistoria() {
        //si es la portada desplazamos portada y texto1
        if (numTextoHistoria==0) { //si estamos en la portada, le sumamos 1 a la variable de pagina (++) 
            numTextoHistoria++;
            Historia.transform.GetChild(2).transform.DOScale(new Vector3(1,1,1), 0f); //se activa el texto de historia
            Portada.DOAnchorPos(new Vector2 (-1500, 130), 0.3f, false); //desplazamiento en el eje x (horizontalmente) -1500, y 130 es el eje y
            Texto1.DOAnchorPos(new Vector2 (0, 130), 0.3f, false); 

        } else if (numTextoHistoria==1) {
            //si es texto1
            numTextoHistoria++;
            Historia.transform.GetChild(3).transform.DOScale(new Vector3(1,1,1), 0f);
            Texto1.DOAnchorPos(new Vector2 (-1500, 130), 0.3f, false);
            Texto2.DOAnchorPos(new Vector2 (0, 130), 0.3f, false);
        }
        
    }

    private void ActivarAnteriorTextoHistoria() {
        //si es la portada
        if (numTextoHistoria==0) { // si estamos en la portada de historia y le damos a ATRAS
            ActivarMenuPrincipal();
        } else if(numTextoHistoria==1) {
            numTextoHistoria--; //sino, le restamos 1 a variable de pagina, y se pasa a la pagina anterior de historia (portada)
            Portada.DOAnchorPos(new Vector2 (0, 130), 0.3f, false); // ahora se ve la portada
            Texto1.DOAnchorPos(new Vector2 (1500, 130), 0.3f, false); 
        } else if(numTextoHistoria==2){
            numTextoHistoria--;
            Texto2.DOAnchorPos(new Vector2 (1500, 130), 0.3f, false); 
            Texto1.DOAnchorPos(new Vector2 (0, 130), 0.3f, false); 
        }
    }

    private void ActivarInfo(){
        //Ponemos en todos los ejes 0 para ser ocultos. Las f son los segundos de transicion para hacer la accion
        MenuPrincipal.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0f);
        MenuPrincipal.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(3).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(4).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(5).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(6).transform.DOScale(new Vector3(0,0,0), 0.3f);

        //Ponemos en todos los ejes 1 para ser mostrados. Las f son los segundos de transicion para hacer la accion
        Info.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0f);
        Info.transform.GetChild(1).transform.DOScale(new Vector3(1.2f,0.7f,1), 0.3f); //1.2 y 0.7 es la escala de la info
        Info.transform.GetChild(2).transform.DOScale(new Vector3(1,1,1), 0.3f);
    }

        private void ActivarAyuda(){
        //Ponemos en todos los ejes 0 para ser ocultos. Las f son los segundos de transicion para hacer la accion
        MenuPrincipal.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0f);
        MenuPrincipal.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(3).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(4).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(5).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(6).transform.DOScale(new Vector3(0,0,0), 0.3f);

        //Ponemos en todos los ejes 1 para ser mostrados. Las f son los segundos de transicion para hacer la accion
        Ayuda.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0f);
        Ayuda.transform.GetChild(1).transform.DOScale(new Vector3(1.2f,0.7f,1), 0.3f); //1.2 y 0.7 es la escala de la info
        Ayuda.transform.GetChild(2).transform.DOScale(new Vector3(1,1,1), 0.3f);
    }

     private void ActivarEmpezar()
    {
        numEmpezar=0;
        //Ponemos en todos los ejes 0 para ser ocultos. Las f son los segundos de transicion para hacer la accion
        // esto oculta el menu principal
        MenuPrincipal.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0f);
        MenuPrincipal.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(2).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(3).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(4).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(5).transform.DOScale(new Vector3(0,0,0), 0.3f);
        MenuPrincipal.transform.GetChild(6).transform.DOScale(new Vector3(0,0,0), 0.3f);

        //Ponemos en todos los ejes 1 para ser mostrados. Las f son los segundos de transicion para hacer la accion
        Empezar.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0f);
        Empezar.transform.GetChild(1).transform.DOScale(new Vector3(1,1,1), 0.3f);
        Empezar.transform.GetChild(2).transform.DOScale(new Vector3(1,1,1), 0.3f);
        Empezar.transform.GetChild(3).transform.DOScale(new Vector3(1,1,1), 0.3f); 

        TextoFelicitacion.DOAnchorPos(new Vector2 (1600, -1300), 0, false);
    }

    private void ActivarSiguienteEmpezar()
    {
        if (numEmpezar==0){
            numEmpezar++;

            //Ponemos en todos los ejes 0 para ser ocultos. Las f son los segundos de transicion para hacer la accion
            Empezar.transform.GetChild(0).transform.DOScale(new Vector3(0,0,0), 0f);
            // el boton de atras ya los estoy mostrando, por eso lo quito de aqui. ahora estoy ocultando el panel (0), el texto (3) y el siguiente (2)
            Empezar.transform.GetChild(1).transform.DOScale(new Vector3(0,0,0), 0.3f); // boton de siguiente
            Empezar.transform.GetChild(3).transform.DOScale(new Vector3(0,0,0), 0.3f);
            Empezar.transform.GetChild(4).transform.DOScale(new Vector3(1,1,1), 0.3f); // este es el boton de camara (4)
            Empezar.transform.GetChild(5).transform.DOScale(new Vector3(1,1,1), 0.3f); // este es el boton de brujula (5)
            Empezar.transform.GetChild(6).transform.DOScale(new Vector3(1,1,1), 0.3f); // este es texto orientativo (6)
            Empezar.transform.GetChild(7).transform.DOScale(new Vector3(1,1,1), 0.3f); // este es el boton de ARcamera (7)
            Empezar.transform.GetChild(9).transform.DOScale(new Vector3(1,1,1), 0.3f); // este es el boton de ARcamera (7)
            Empezar.transform.GetChild(11).transform.DOScale(new Vector3(2,4,0), 0.3f);
        }
    }

    private void ActivarAtrasEmpezar() //hace lo contrario de ActivarSiguienteEmpezar 
    {
        if (numEmpezar==0){
            ActivarMenuPrincipal();
        }

        else {
            numEmpezar--;

            //Ponemos en todos los ejes 1 para ser mostrados. Las f son los segundos de transicion para hacer la accion
            Empezar.transform.GetChild(0).transform.DOScale(new Vector3(1,1,1), 0f);
            // el boton de atras ya los estoy mostrando, por eso lo quito de aqui. ahora estoy ocultando el panel (0), el texto (3) y el siguiente (2)
            Empezar.transform.GetChild(1).transform.DOScale(new Vector3(1,1,1), 0.3f); // boton de siguiente
            Empezar.transform.GetChild(3).transform.DOScale(new Vector3(1,1,1), 0.3f);
            Empezar.transform.GetChild(4).transform.DOScale(new Vector3(0,0,0), 0.3f); // este es el boton de camara (4)
            Empezar.transform.GetChild(5).transform.DOScale(new Vector3(0,0,0), 0.3f); // este es el boton de brujula (5)
            Empezar.transform.GetChild(6).transform.DOScale(new Vector3(0,0,0), 0.3f); // este es texto orientativo (6)
            Empezar.transform.GetChild(7).transform.DOScale(new Vector3(0,0,0), 0.3f); // este es el boton de ARcamera (7)
            Empezar.transform.GetChild(8).transform.DOScale(new Vector3(0,0,0), 0.3f); // este es el boton de ARcamera (7)
            Empezar.transform.GetChild(9).transform.DOScale(new Vector3(0,0,0), 0.3f); // este es el boton de ARcamera (7)
            Empezar.transform.GetChild(11).transform.DOScale(new Vector3(0,0,0), 0.3f);
        }
    }

}
