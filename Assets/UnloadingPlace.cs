using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnloadingPlace : MonoBehaviour
{
    public List<GameObject> unloadedPassengers;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        LevelManager.instance.OnUnloadingPlace -= UnloadingPassengers;
    }
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.instance.OnUnloadingPlace += UnloadingPassengers;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UnloadingPassengers(GameObject passenger)
    {
        unloadedPassengers.Add(passenger);
        passenger.transform.SetParent(this.transform);
    }
}
