using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    const string strTagNameLoadingPlace = "Loading Place";
    const string strTagNameUnloadingPlace = "Unloading Place";

    [SerializeField] int carSize = 1;

    [SerializeField] public List<GameObject> loadedPassengers;

    [SerializeField] private float waitTimeBetweenPassengersLoading;
    [SerializeField] private float waitTimeBetweenPassengersUnloading;

    bool canLoad = false;
    bool stopLoading = false;
    bool canUnload = false;
    bool stopUnloading = false;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        LevelManager.instance.OnLoadingPassengers -= GetInPassengers;
    }

    private void Start()
    {
        LevelManager.instance.OnLoadingPassengers += GetInPassengers;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == strTagNameLoadingPlace && canLoad == false)
        {
            canLoad = true;
            canUnload = false;
            stopUnloading = false;
            StartCoroutine(InvokeOnLoadingPlace());
        }

        if(other.tag == strTagNameUnloadingPlace && canUnload == false)
        {
            canLoad = false;
            canUnload = true;
            stopLoading = false;
            StartCoroutine(InvokeOnUnloadingPlace());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == strTagNameLoadingPlace)
        {
            stopLoading = true;
        }

        if (other.tag == strTagNameUnloadingPlace)
        {
            stopLoading = true;
        }
    }


    private void GetInPassengers(GameObject passenger)
    {
        if(loadedPassengers.Count < carSize)
        {
            passenger.transform.SetParent(this.transform);
            loadedPassengers.Add(passenger);
        }

        else
        {
            return;
        }
    }

    IEnumerator InvokeOnLoadingPlace()
    {
        yield return new WaitForSeconds(waitTimeBetweenPassengersLoading);

        if (loadedPassengers.Count < carSize && stopLoading == false)
        {
            LevelManager.instance.LoadingPlaceInvoke();
            StartCoroutine(InvokeOnLoadingPlace());
        }
    
        else { StopCoroutine(InvokeOnLoadingPlace()); }
    }

    IEnumerator InvokeOnUnloadingPlace()
    {
        if(loadedPassengers.Count > 0 && stopLoading == false)
        {
            yield return new WaitForSeconds(waitTimeBetweenPassengersUnloading);
            LevelManager.instance.UnloadingPassengers(loadedPassengers[0]);
            loadedPassengers.Remove(loadedPassengers[0]);
            StartCoroutine(InvokeOnUnloadingPlace());
        }

        else { StopCoroutine(InvokeOnUnloadingPlace()); }
    }
}
