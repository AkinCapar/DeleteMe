using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPlace : MonoBehaviour
{
    [SerializeField] private GameObject passenger;

    [SerializeField] private int spawnAmount = 1;
    public List<GameObject> spawnedPassengers;

    private void OnEnable()
    {
        LevelManager.instance.OnLoadingPlace += LoadPassengers;
    }

    private void OnDisable()
    {
        LevelManager.instance.OnLoadingPlace -= LoadPassengers;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPassengers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPassengers()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject passengerClone = Instantiate(passenger,this.transform, this.transform);
            passengerClone.transform.position = new Vector3(this.transform.position.x + (i*1), 1, this.transform.position.z -5);
            spawnedPassengers.Add(passengerClone);
        }
    }

    private void LoadPassengers()
    {
        if(spawnedPassengers[0] != null)
        {
            LevelManager.instance.LoadingPassengers(spawnedPassengers[0]);
            spawnedPassengers.Remove(spawnedPassengers[0]);
        }

        else { return; }
    }
}
