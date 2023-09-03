using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using System;
using DG.Tweening;
using Vuforia;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GPS : MonoBehaviour
{
    public Text textoDistancia;
    public Text textoEncontrados;
    public RectTransform PantallaFinal;
    public GameObject ARCamara;
    public GameObject textoFelicitacion;
    public GameObject Inicio;
    public AudioSource audioSource;
    public AudioClip soundEffect;
    public Button botonCamara;
    public RectTransform TextoFelTrans;

    /*TermaPequena: 38.0367072, -3.6251989
    TermaAndamio: 38.0357148, -3.6240969
    TermaArcos: 38.0360245, -3.6234796
    CasaMosaico: 38.0349582, -3.6247839
    SalidaMosaico: 38.0348181, -3.6250022
    Torre: 38.0324098, -3.6243493
    */

    private List<string> imagenesDetectadas = new List<string>(); //es una lista para guardar el nombre de las imagenes que han sido detectadas
    int encontrados = 0; // al iniciar la app hay cero encontrados
    int indicePuntos = 0; // con 0 estoy sacando la primera latitud, primera longitud, primera pista, y corresponde con la terma pequeña
    float[] puntosLatitud = new float[] { 38.0367072f, 38.0357148f, 38.0360245f, 38.0349582f, 38.0348181f, 38.0324098f }; //se declara un array con los puntos de latitud
    float[] puntosLongitud = new float[] { -3.6251989f, -3.6240969f, -3.6234796f, -3.6247839f, -3.6250022f, -3.6243493f }; //se declara un array con los puntos de longitud
    string[] pistas = new string[] {"Se encuentra al SO siguiendo el camino inicial.", "Se encuentra al SE desde el último punto.", "Se encuentra al NE desde el último punto.", "Se encuentra al NE desde el último punto.", "Se encuentra al S desde el último punto.", "Se encuentra al SE desde el último punto."};

    private void Start()
    {
        //añado funcion observer para cada image target hijo del elemento Empezar
        // cojo el menu empezar y para cada elemento que tenga una propiedad 'observer' (son los image target -> los aumentos), se añade la siguiente funcion
        ObserverBehaviour[] observers = GetComponentsInChildren<ObserverBehaviour>(); 
        foreach (ObserverBehaviour observer in observers)
        {
            observer.OnTargetStatusChanged += OnTargetStatusChanged; // cada image target tiene una propiedad de estado, y cuando se detecta, el estado cambia. con esta funcion lo que controlas es saber cuando el estado cambia
        }
        if (observers.Length == 0) // si Empezar no tiene ningun hijo con la propiedad 'observer' devuelve el error (nunca va a pasar pero se puso para las pruebas)
        {
            Debug.LogError("ObserverBehaviour not found in any children.");
        }

        PantallaFinal.DOAnchorPos(new Vector2 (0, -3000), 0f, false); // la pantalla de victoria la oculto al inicio de la app, -3000 es abajo del todo, no se ve
        textoEncontrados.text = "Encontrados: " + encontrados.ToString() + "/6"; // variable que actualiza dinamicamente el texto Encontrados
        // Comprobar si hay permisos de localizacion sino se piden al usuario
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation)) {
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }
        if (Permission.HasUserAuthorizedPermission(Permission.CoarseLocation) && !Permission.HasUserAuthorizedPermission(Permission.FineLocation)) {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

                // Verificar si se ha otorgado el permiso
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite))
        {
            // Solicitar permiso al usuario
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
            return;
        }

        // Empieza la actualizacion de la ubicacion
        StartCoroutine(Localizar()); // StartCoroutine es como si fuese un if (una palabra reservada)

        botonCamara.onClick.AddListener(async () => // cuando hago click al boton de foto
        {
            // cambiamos el texto de detectados, el de las pistas etc
            if (encontrados < 6 || indicePuntos < 6) { //si encontrados es menor que 6 (solo hay 6 marcadores)
                encontrados = imagenesDetectadas.Count; // se suma 1 al contador. Coge la lista de imagenes detectadas y con la funcion .Count te devuelve el valor del numero total de nombres que tiene imagenes detectadas. Y este numero se le asigna a Encontrados
                indicePuntos = imagenesDetectadas.Count;
                textoEncontrados.text = "Encontrados: " + encontrados.ToString() + "/6";
            }
            if (encontrados==6) { //cuando encontrados ya es 6, se han detectado todos los marcadores
                await Task.Delay(2000);
                ARCamara.transform.DOScale(new Vector3(0,0,0), 0f); // se pone la camara oculta
                TextoFelTrans.DOAnchorPos(new Vector2 (656, -1367), 0, false);
                textoFelicitacion.transform.DOScale(new Vector3(1,1,1), 0f);
                PantallaFinal.transform.DOScale(new Vector3(1,1,1), 0f); //se muestra la pantalla final
                PantallaFinal.DOAnchorPos(new Vector2 (0, 0), 0.5f, false) // pasamos de -3000 a 0 (al centro de la pantalla), se hace en 0.5 segundos 
                    .OnComplete(() => { // cuando se completa la transicion anterior
                        // Aquí es donde reproducimos el sonido
                        audioSource.PlayOneShot(soundEffect); // audioSource es un objeto (que se arrastra), y soundEffect es un audio que me descargue
                        Inicio.transform.DOScale(new Vector3(1,1,1), 0.3f); // se muestra el boton de inicio de la app para volver a el
                    });
            }
        });
    }

    IEnumerator Localizar() //(function, palabra reservada)
    {
        // Esperar hasta que se active el servicio
        while (!Input.location.isEnabledByUser) { //mientras los permisos de localizacion no esten activados por el usuario
            yield return new WaitForSeconds(1); // espera un segundo para volver a pedir la localizacion y comprobar si tiene los permisos
        }

        // Empieza el servicio de localizacion
        Input.location.Start();

        // tiempo de espera hasta que se inicialice
        int maxTiempoEspera = 20; 
        while (Input.location.status == LocationServiceStatus.Initializing && maxTiempoEspera > 0) { //mientras el estado es que se esta Inicializando y el tiempo de espera (20) es mayor que 0, entra
            yield return new WaitForSeconds(1); //vuelve a esperar 1 seg para comprobar si aun esta Inicializandose
            maxTiempoEspera--; //al maximo tiempo de espera le quita 1 (es como una cuenta atras)
        }

        // si falla
        if (maxTiempoEspera == 0) { //si el tiempo max de espera (20) llega a 0
            Debug.Log("Tiempo de espera superado para la inicialización del servicio de ubicación"); //entonces volveria a arriba del todo de esta funcion
            yield break; 
        }

        // si esta funcionando
        if (Input.location.status == LocationServiceStatus.Running) { //ahora si el estado esta corriendo, es decir, funcionando
            while (true) {
                // recoge la ubicacion actual
                LocationInfo ubicacion = Input.location.lastData;

                // calcula la distancia al punto segun las coordenadas - Esto es la formula Haversine
                const float radioTierra = 6371000; // meters // const es una constante, no se puede variar como una variable. 
                var dLat = Mathf.Deg2Rad * (puntosLatitud[indicePuntos] - ubicacion.latitude);
                var dLon = Mathf.Deg2Rad * (puntosLongitud[indicePuntos] - ubicacion.longitude);
                var a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
                        Mathf.Cos(Mathf.Deg2Rad * ubicacion.latitude) * Mathf.Cos(Mathf.Deg2Rad * puntosLatitud[indicePuntos]) *
                        Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
                var c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
                var distancia = radioTierra * c;
                distancia = (float)Math.Round(distancia, 1); // este es el resultado de la formula

                // actualiza en texto distancia segun el resultado de la formula
                textoDistancia.text = "Distancia hasta el siguiente punto: " + distancia + "m. \nPista: " + pistas[indicePuntos]; //anade la pista correspondiente al indice puntos actual

                // tiempo de esperar para la siguiente actualizacion
                yield return new WaitForSeconds(1); //espera 1 seg para volver a pedir de nuevo tu ubicacion
            }
        }
        // si falla al iniciarse
        else {
            Debug.Log("El servicio de ubicación ha fallado al iniciarse");
            yield break;
        }
    }


    private void OnTargetStatusChanged(ObserverBehaviour observerBehaviour, TargetStatus targetStatus) // lo de 'observer' de arriba. 
    // esta funcion se ejecuta cuando el estado del image target cambia. Se coge el image target y su Status
    {
        // comprobamos cuando el estado sea TRACKED y NORMAL (hay muchos estados mas)
        if (targetStatus.Status == Status.TRACKED && targetStatus.StatusInfo == StatusInfo.NORMAL) 
        {
            // recogemos el nombre de la image target
            string nombreImagen = observerBehaviour.TargetName; // ej: TermaPequena

            // comprobamos si ha sido detectada previamente y no es una funcion llamada DeviceObserver
            if (!imagenesDetectadas.Contains(nombreImagen) && nombreImagen != "DeviceObserver") // el objeto cuando lo detecta tiene 2 nombres, ej TermaPEquena, y otro nombre que es 'DeviceObserver', este se pone solo en Unity
            // si en la lista no existe el nombre de la imagen (inicialmente hay 0 nombres en la lista), se anade el nombre. Asi si se detecta mas de 1 vez la TermaPequena, el contador solo suma 1
            {
                // añadimos el nombre a la lista de detectadas
                imagenesDetectadas.Add(nombreImagen); // aqui se anadian los 2 nombres, y sumaba 2. ahora no 
            }
        }
    }
}