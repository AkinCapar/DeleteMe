using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public delegate void OnLoadingPlaceDelegate();
    public event OnLoadingPlaceDelegate OnLoadingPlace;


    public delegate void OnLoadPassengersDelegate(GameObject passenger);
    public event OnLoadPassengersDelegate OnLoadingPassengers;

    public delegate void OnUnloadingPlaceDelegate(GameObject passenger);
    public event OnUnloadingPlaceDelegate OnUnloadingPlace;
    //
    //
    //public delegate void OnLoadingPlaceDelegate();
    //public event OnLoadingPlaceDelegate OnLoadingPlace;
    //

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadingPlaceInvoke()
    {
        OnLoadingPlace?.Invoke();
    }

    public void LoadingPassengers(GameObject passenger)
    {
        OnLoadingPassengers?.Invoke(passenger);
    }

    public void UnloadingPassengers(GameObject passenger)
    {
        OnUnloadingPlace?.Invoke(passenger);
    }
}
