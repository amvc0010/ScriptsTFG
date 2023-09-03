using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    // Declaracion de objetos
    public event Action OnMenuPrincipal;
    public event Action OnGaleria;
    public event Action OnEmpezar;
    public event Action OnHistoria;
    public event Action OnInfo;
    public event Action OnAyuda;
    public event Action OnSiguienteTextoHistoria;
    public event Action OnAnteriorTextoHistoria;
    public event Action OnSiguienteEmpezar;
    public event Action OnAtrasEmpezar;

    public static GameManager instance;

    private void Awake()
    {
        if (instance!=null && instance!=this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance=this;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        MenuPrincipal(); // cuando la aplicacion inicie, llame al evento MenuPrincipal
    }

    public void MenuPrincipal()
    {
        OnMenuPrincipal?.Invoke();
        Debug.Log("Menu Principal Activated");
    }

    public void Galeria()
    {
        OnGaleria?.Invoke();
        Debug.Log("Galeria Activated");
    }

    public void Empezar()
    {
        OnEmpezar?.Invoke();
        Debug.Log("Camara Activated");
    }

    public void SiguienteEmpezar () {
        OnSiguienteEmpezar?.Invoke();
    }

    public void AtrasEmpezar () {
        OnAtrasEmpezar?.Invoke();
    }

    public void Historia()
    {
        OnHistoria?.Invoke();
        Debug.Log("Historia Activated");
    }

    public void SiguienteTextoHistoria () {
        OnSiguienteTextoHistoria?.Invoke();
    }

    public void AnteriorTextoHistoria () {
        OnAnteriorTextoHistoria?.Invoke();
    }

    public void Info()
    {
        OnInfo?.Invoke();
        Debug.Log("Info Activated");
    }

    public void Ayuda()
    {
        OnAyuda?.Invoke();
        Debug.Log("Ayuda Activated");
    }

    public void CerrarAPP()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
