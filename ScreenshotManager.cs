using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System;

public class ScreenshotManager : MonoBehaviour
{
    public Button camara; //esto es para crear los recuadros que luego se arrastran
    public ScrollRect screenshotScrollView;
    public Button fullscreenImageObject;
    public Button filtroImageObject;
    public Button closeFullScreen;
    public Button saveImage;
    public Button shareImage;
    public GameObject atras;
    public GameObject btnCamara;
    public GameObject brujula;
    public GameObject textoEnc;
    public GameObject imagen;
    public GameObject busto;
    public GameObject textoFelicitacion;
    public Slider brightnessSlider;
    public Slider contrastSlider;
    public Slider saturationSlider;
    public Slider hueSlider;
    public Button filtroByn;
    public Button filtroSepia;
    public Button filtroNegativo;
    public Button filtroOriginal;
    public GameObject containerFiltro;
    private Image fullscreenImage; // aqui se guarda la variable de la imagen a la que pulsamos para hacer la previsualizacion en el menu de fotos
    private bool isFullscreenActive = false; // sirve para controlar si esta en pantalla completa o no
    private string screenshotPath; // ruta donde se guardan las screenshots
    private Image spriteOriginal;
    private Texture2D originalTexture;
    private Texture2D adjustedTexture;

    private void Awake() //es como el Start pero se ejecuta mas tarde
    {
        /*PC    
        string fileName = "screenshot" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".png";
        
        screenshotPath = Path.Combine(Application.persistentDataPath, fileName); // coge la ruta de la app y guarda ahi las capturas temporalmente
        */
        camara.onClick.AddListener(TakeScreenshot); // esta escuchando que cuando se le haga click, haga la funcion TakeScreenshot
        // se ejecuta en el Awake porque no se puede en Start, porque en el Start no se inicializan los componentes que se declaran aqui
        fullscreenImageObject.gameObject.SetActive(false);  // se oculta fullscreenImageObject porque sino se superpondria al menu de fotos
        filtroImageObject.gameObject.SetActive(false);
        filtroByn.gameObject.SetActive(false);
        filtroSepia.gameObject.SetActive(false);
        filtroNegativo.gameObject.SetActive(false);
        filtroOriginal.gameObject.SetActive(false);
        containerFiltro.gameObject.SetActive(false);
        brightnessSlider.gameObject.SetActive(false);
        contrastSlider.gameObject.SetActive(false);
        saturationSlider.gameObject.SetActive(false);
        hueSlider.gameObject.SetActive(false);
        closeFullScreen.gameObject.SetActive(false);
        saveImage.gameObject.SetActive(false);
        shareImage.gameObject.SetActive(false);
    }

    public void TakeScreenshot() // se ejecuta cuando se hace click en la camara
    {
        atras.transform.localScale = Vector3.zero; // se ponen los objetos de la pantalla en oculto para que al hacer la captura solo se muestra lo que aparece en la camara
        btnCamara.transform.localScale = Vector3.zero;
        brujula.transform.localScale = Vector3.zero;
        textoEnc.transform.localScale = Vector3.zero;
        busto.transform.localScale = Vector3.zero;
        textoFelicitacion.SetActive(false);
        imagen.transform.localScale = Vector3.zero; // (estas a tantos m) vector 3 es que la x,y,z lo pone todo a cero
        /* PC
        ScreenCapture.CaptureScreenshot(screenshotPath); // es una funcion interna que hace una captura y la guarda en la ruta anterior
        */
        StartCoroutine(WaitForScreenshot()); // llama a la funcion 'esperar a hacerse la captura' para hacer lo de abajo
    }

    private IEnumerator WaitForScreenshot()
    {
        /* PC 
        yield return null; // para saber si entra la funcion o no

        byte[] screenshotData = File.ReadAllBytes(screenshotPath); // recoge los datos de la imagen y los mete en la variable screenshotData
        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height); // crea una textura 'Sprite' en 2D
        screenshotTexture.LoadImage(screenshotData); // en la textura 2D que he creado, pone los datos de la imagen
        */
        yield return new WaitForEndOfFrame();

        Texture2D screenshotTexture = ScreenCapture.CaptureScreenshotAsTexture();
        /*string fileName = "screenshot" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".png";
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(screenshotTexture, "MyScreenshots", fileName);
        yield return permission;

        if (permission == NativeGallery.Permission.Granted)
        {
            Debug.Log("Screenshot saved to gallery");
        }
        else
        {
            Debug.Log("Failed to save screenshot to gallery");
        }*/
        Sprite screenshotSprite = Sprite.Create(screenshotTexture, new Rect(0, 0, Screen.width, Screen.height), Vector2.zero); // declara una variable Sprite que tranforma la textura y los datos de la imagen en una imagen visible

        GameObject screenshotButtonObject = new GameObject("ScreenshotButton"); // crea el objeto boton que se anadira dentro del Scroll View
        screenshotButtonObject.transform.SetParent(screenshotScrollView.content.transform); // lo mete dentro del Scroll View

        RectTransform rectTransform = screenshotButtonObject.AddComponent<RectTransform>(); // dice que quieres editarlo 
        // lo edita: esta dando el tamano de anchura y altura, y x,y,z (si 1 es visible, 0 no, 2 es el doble)
        rectTransform.sizeDelta = new Vector2(240f, 375f); // altura y anchura
        rectTransform.localScale = new Vector3(1f, 1f, 1f); // tamano (x,y,z)

        Image screenshotImage = screenshotButtonObject.AddComponent<Image>(); // crea un componente que es una imagen y lo anade dentro del objeto boton creado antes
        screenshotImage.sprite = screenshotSprite; // le anade la imagen al objeto imagen

        //MINIATURAS
        //original
        Image screnshootFiltroOriginal = filtroOriginal.gameObject.GetComponent<Image>(); // crea un componente que es una imagen y lo anade dentro del objeto boton creado antes
        screnshootFiltroOriginal.sprite = screenshotSprite;
        //byn
        Image screnshootFiltroByn = filtroByn.gameObject.GetComponent<Image>(); // crea un componente que es una imagen y lo anade dentro del objeto boton creado antes
        screnshootFiltroByn.sprite = screenshotSprite;
        Texture2D filteredTextureByN = ApplyFilter(screnshootFiltroByn.sprite.texture, "byn");
        screnshootFiltroByn.sprite = Sprite.Create(filteredTextureByN, new Rect(0, 0, filteredTextureByN.width, filteredTextureByN.height), Vector2.one * 0.5f);
        //sepia
        Image screnshootFiltroSepia = filtroSepia.gameObject.GetComponent<Image>(); // crea un componente que es una imagen y lo anade dentro del objeto boton creado antes
        screnshootFiltroSepia.sprite = screenshotSprite;
        Texture2D filteredTextureSepia = ApplyFilter(screnshootFiltroSepia.sprite.texture, "sepia");
        screnshootFiltroSepia.sprite = Sprite.Create(filteredTextureSepia, new Rect(0, 0, filteredTextureSepia.width, filteredTextureSepia.height), Vector2.one * 0.5f);
        //negativo
        Image screnshootFiltroNegativo = filtroNegativo.gameObject.GetComponent<Image>(); // crea un componente que es una imagen y lo anade dentro del objeto boton creado antes
        screnshootFiltroNegativo.sprite = screenshotSprite;
        Texture2D filteredTextureNegativo = ApplyFilter(screnshootFiltroNegativo.sprite.texture, "negativo");
        screnshootFiltroNegativo.sprite = Sprite.Create(filteredTextureNegativo, new Rect(0, 0, filteredTextureNegativo.width, filteredTextureNegativo.height), Vector2.one * 0.5f);


        Button screenshotButton = screenshotButtonObject.AddComponent<Button>(); // crea un componente que es un boton para poder hacer click para que haga lo siguiente
        screenshotButton.onClick.AddListener(() => ShowFullscreenImage(screenshotSprite)); // al hacer click hace la funcion ShowFullscreenImage pasandole como parametro la imagen a la que se le ha hecho click
        
        Canvas.ForceUpdateCanvases(); // forzar un refresco para que se actualice y muestre los cambios
        screenshotScrollView.verticalNormalizedPosition = 0f; // la imagen este centrada en el scroll View
        
        atras.transform.localScale = new Vector3(1f, 1f, 1f); // aqui ya se ha hecho la captura y se vuelven a mostrar los botones que se habian ocultado
        btnCamara.transform.localScale = new Vector3(1f, 1f, 1f);
        brujula.transform.localScale = new Vector3(1f, 1f, 1f);
        textoEnc.transform.localScale = new Vector3(1f, 1f, 1f);
        imagen.transform.localScale = new Vector3(1f, 1f, 1f);
        textoFelicitacion.SetActive(true);
        busto.transform.localScale = new Vector3(2f, 4f, 0f);
    }

    public void ShowFullscreenImage(Sprite screenshotSprite) // llama al hacer click en la imagen de fotos, como parametro pasa la imagen 'Sprite'
    {
        if (!isFullscreenActive) // si cuando le hacemos click, la variable isFullscreenActive es false entra aqui
        {
            fullscreenImage = fullscreenImageObject.GetComponent<Image>(); // guardas en la variable fullscreenImage el componente image del objeto fullscreenImageObject
            fullscreenImage.sprite = screenshotSprite; // el valor es la imagen Sprite que es el parametro que hemos pasado (como si se estuviese arrastrando)
            spriteOriginal = filtroImageObject.GetComponent<Image>();
            spriteOriginal.sprite = screenshotSprite;
            originalTexture = fullscreenImage.sprite.texture;

            // Establecer los valores iniciales de las barras deslizantes
            brightnessSlider.value = 0.5f;
            contrastSlider.value = 0.5f;
            saturationSlider.value = 0.5f;
            hueSlider.value = 0.5f;

            fullscreenImageObject.gameObject.SetActive(true); // estaba inactivo y ahora se muestra
            isFullscreenActive = true; // la variable ahora es true
            closeFullScreen.onClick.AddListener(() => ShowFullscreenImage(fullscreenImage.sprite)); // se anade un Listener que al hacer click y se vuelve a ejecutar la funcion, para cerrarlo
            saveImage.onClick.AddListener(() => saveImg("ok"));
            shareImage.onClick.AddListener(() => shareImg("ok"));
            filtroByn.gameObject.SetActive(true);
            filtroSepia.gameObject.SetActive(true);
            filtroNegativo.gameObject.SetActive(true);
            filtroOriginal.gameObject.SetActive(true);
            containerFiltro.gameObject.SetActive(true);
            brightnessSlider.gameObject.SetActive(true);
            contrastSlider.gameObject.SetActive(true);
            saturationSlider.gameObject.SetActive(true);
            hueSlider.gameObject.SetActive(true);
            closeFullScreen.gameObject.SetActive(true);
            saveImage.gameObject.SetActive(true);
            shareImage.gameObject.SetActive(true);
            filtroByn.onClick.AddListener(() => ApplySelectedFilter("byn"));
            filtroSepia.onClick.AddListener(() => ApplySelectedFilter("sepia"));
            filtroNegativo.onClick.AddListener(() => ApplySelectedFilter("negativo"));
            filtroOriginal.onClick.AddListener(() => ApplySelectedFilter("original"));

            // Suscribirse a los eventos de las barras deslizantes
            brightnessSlider.onValueChanged.AddListener(AdjustTexture);
            contrastSlider.onValueChanged.AddListener(AdjustTexture);
            saturationSlider.onValueChanged.AddListener(AdjustTexture);
            hueSlider.onValueChanged.AddListener(AdjustTexture);
        }
        else
        {
            fullscreenImageObject.gameObject.SetActive(false); // como ya se quiere cerrar, se oculta
            filtroByn.gameObject.SetActive(false);
            filtroSepia.gameObject.SetActive(false);
            filtroNegativo.gameObject.SetActive(false);
            filtroOriginal.gameObject.SetActive(false);
            containerFiltro.gameObject.SetActive(false);
            isFullscreenActive = false; // ahora le digo otra vez que ya no esta en pantalla completa, es false

             // Desactivar las barras deslizantes
            brightnessSlider.gameObject.SetActive(false);
            contrastSlider.gameObject.SetActive(false);
            saturationSlider.gameObject.SetActive(false);
            hueSlider.gameObject.SetActive(false);
            closeFullScreen.gameObject.SetActive(false);
            saveImage.gameObject.SetActive(false);
            shareImage.gameObject.SetActive(false);
        }
    }

    private void AdjustTexture(float valueOk)
    {
        float brightness = brightnessSlider.value * 2f; // Sin multiplicar por 2f
        float contrast = contrastSlider.value * 2f;
        float saturation = saturationSlider.value * 2f;
        float hue = (hueSlider.value - 0.5f) * 2f;

            // Crear una nueva textura para contener los valores ajustados
            adjustedTexture = new Texture2D(fullscreenImage.sprite.texture.width, fullscreenImage.sprite.texture.height);

            // Obtener los píxeles originales de la textura
            Color[] originalPixels = fullscreenImage.sprite.texture.GetPixels();

            // Ajustar cada píxel en la textura
            for (int i = 0; i < originalPixels.Length; i++)
            {
                Color originalColor = originalPixels[i];

                // Ajustar el brillo
                Color adjustedColor = originalColor * brightness;

                // Ajustar el contraste
                adjustedColor.r = (adjustedColor.r - 0.5f) * contrast + 0.5f;
                adjustedColor.g = (adjustedColor.g - 0.5f) * contrast + 0.5f;
                adjustedColor.b = (adjustedColor.b - 0.5f) * contrast + 0.5f;

                // Manejar el caso en que el brillo es menor que 0.5
                if (brightness < 0.5f)
                {
                    float darknessFactor = 1f - brightness * 2f; // Calcular el factor de oscurecimiento

                    adjustedColor = Color.Lerp(adjustedColor, Color.black, darknessFactor);
                }

               // Ajustar la saturación
                float maxChannel = Mathf.Max(adjustedColor.r, adjustedColor.g, adjustedColor.b);
                adjustedColor.r = Mathf.Lerp(maxChannel, adjustedColor.r, saturation);
                adjustedColor.g = Mathf.Lerp(maxChannel, adjustedColor.g, saturation);
                adjustedColor.b = Mathf.Lerp(maxChannel, adjustedColor.b, saturation);

                // Ajustar el tono
                float h, s, v;
                Color.RGBToHSV(adjustedColor, out h, out s, out v);
                h += hue;
                h = Mathf.Repeat(h, 1f); // Asegurar que el valor del tono esté en el rango 0-1
                adjustedColor = Color.HSVToRGB(h, s, v);

                // Establecer el color ajustado en la nueva textura
                adjustedTexture.SetPixel(i % adjustedTexture.width, i / adjustedTexture.width, adjustedColor);
            }

            // Aplicar los cambios a la textura
            adjustedTexture.Apply();
        fullscreenImage.sprite = Sprite.Create(adjustedTexture, new Rect(0, 0, adjustedTexture.width, adjustedTexture.height), Vector2.one * 0.5f);
    }

    private Texture2D ApplyFilter(Texture2D texture, string filter)
    {
        Color[] pixels = texture.GetPixels();

        switch (filter)
        {
            case "byn":
                for (int i = 0; i < pixels.Length; i++)
                {
                    float grayscale = (pixels[i].r + pixels[i].g + pixels[i].b) / 3f;
                    pixels[i] = new Color(grayscale, grayscale, grayscale, pixels[i].a);
                }
                break;
            case "sepia":
                for (int i = 0; i < pixels.Length; i++)
                {
                    float r = pixels[i].r;
                    float g = pixels[i].g;
                    float b = pixels[i].b;

                    float tr = 0.393f * r + 0.769f * g + 0.189f * b;
                    float tg = 0.349f * r + 0.686f * g + 0.168f * b;
                    float tb = 0.272f * r + 0.534f * g + 0.131f * b;

                    pixels[i] = new Color(tr, tg, tb, pixels[i].a);
                }
                break;
            case "negativo":
                for (int i = 0; i < pixels.Length; i++)
                {
                    pixels[i] = new Color(1f - pixels[i].r, 1f - pixels[i].g, 1f - pixels[i].b, pixels[i].a);
                }
                break;
            // Agrega más casos para otros filtros...
            case "original":
                // No se aplica ningún filtro
                break;
        }

        Texture2D filteredTexture = new Texture2D(texture.width, texture.height);
        filteredTexture.SetPixels(pixels);
        filteredTexture.Apply();

        return filteredTexture;
    }

    public void ApplySelectedFilter(string filtro)
    {
        // Obtener el filtro seleccionado desde la UI (por ejemplo, un Dropdown)
        string selectedFilter = filtro; // Asegúrate de implementar esta función para obtener el filtro seleccionado desde la UI

        // Obtener la textura del sprite
        Texture2D spriteTexture = spriteOriginal.sprite.texture;

        // Aplicar el filtro seleccionado a la textura del sprite
        Texture2D filteredTexture = ApplyFilter(spriteTexture, selectedFilter);

        brightnessSlider.value = 0.5f;
        contrastSlider.value = 0.5f;
        saturationSlider.value = 0.5f;
        hueSlider.value = 0.5f;

        // Actualizar la textura del sprite con el filtro aplicado
        fullscreenImage.sprite = Sprite.Create(filteredTexture, new Rect(0, 0, filteredTexture.width, filteredTexture.height), Vector2.one * 0.5f);
    }

    private void saveImg(string ok)
    {
        string fileName = "screenshot" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".png";
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(fullscreenImage.sprite.texture, "MyScreenshots", fileName);
        if (permission == NativeGallery.Permission.Granted)
        {
            Debug.Log("Screenshot saved to gallery");
        }
        else
        {
            Debug.Log("Failed to save screenshot to gallery");
        }
    }

    private void shareImg(string ok)
    {
        Texture2D textureShare = fullscreenImage.sprite.texture;

        // Guardar la imagen en una ubicación temporal
        string fileName = "screenshot" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".png";
        string path = Path.Combine(Application.temporaryCachePath, "share.png");
        File.WriteAllBytes(path, textureShare.EncodeToPNG());
    
        // Compartir la imagen utilizando la funcionalidad del sistema operativo
        new NativeShare().AddFile(path).Share();
    }
}