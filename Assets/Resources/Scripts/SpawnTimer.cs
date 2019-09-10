using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{

    [SerializeField] private float spawnRate;
    private float newTime;


    // Update is called once per frame
    void Update()
    {
        if (newTime <= Time.time)
        {
            GameEvents.ReportSpawnUnits();
            newTime = Time.time + spawnRate;
        } 
    }
}
