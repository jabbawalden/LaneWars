using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //SPEED
    private float orangeSpeed;
    public float OrangeSpeed { get { return orangeSpeed; } private set { orangeSpeed = value; } }
    private float blueSpeed;
    public float BlueSpeed { get { return blueSpeed; } private set { blueSpeed = value; } }
    private float defaultSpeed;
    private float upgradedSpeed;

    //HEALTH
    private float orangeHealth;
    public float OrangeHealth { get { return orangeHealth; } private set { orangeHealth = value; } }
    private float blueHealth; 
    public float BlueHealth { get { return blueHealth; } private set { blueHealth = value; } }
    private float defaultHealth;
    private float upgradedHealth;

    //UNITCOUNT
    [SerializeField] private List<LaneController> blueLanes;
    [SerializeField] private List<LaneController> orangeLanes;
     
    private void Awake()
    {
        defaultSpeed = 0.6f;
        upgradedSpeed = 1.1f;

        defaultHealth = 1;
        upgradedHealth = 2;

        OrangeSpeed = defaultSpeed;
        BlueSpeed = defaultSpeed;

        OrangeHealth = defaultHealth;
        BlueHealth = defaultHealth;
    }

    public void UpgradeOrangeSpeed()
    {
        OrangeSpeed = upgradedSpeed;
        BlueSpeed = defaultSpeed;
        GameEvents.ReportChangeSpeeds();
    }

    public void UpgradeBlueSpeed()
    {
        OrangeSpeed = defaultSpeed;
        BlueSpeed = upgradedSpeed;
        GameEvents.ReportChangeSpeeds();
    }

    public void ResetAllSpeeds()
    {
        OrangeSpeed = defaultSpeed;
        BlueSpeed = defaultSpeed;
        GameEvents.ReportChangeSpeeds();
    }

    public void UpgradeOrangeHealth()
    {
        OrangeHealth = upgradedHealth;
        BlueHealth = defaultHealth;
        GameEvents.ReportChangeHealth();
    }

    public void UpgradeBlueHealth()
    {
        OrangeHealth = defaultHealth;
        BlueHealth = upgradedHealth;
        GameEvents.ReportChangeHealth();
    }

    public void ResetAllHealth()
    {
        OrangeHealth = defaultHealth;
        BlueHealth = defaultHealth;
        GameEvents.ReportChangeHealth();
    }

    public void UpgradeBlueLanes()
    {
        //blue lanes
        foreach (LaneController laneC in blueLanes)
        {
            laneC.additionalSpawn = 1;
        }

        //orange lanes
        foreach (LaneController laneC in orangeLanes)
        {
            laneC.additionalSpawn = 0;
        }
    }

    public void UpgradeOrangeLanes()
    {
        //blue lanes
        foreach (LaneController laneC in blueLanes)
        {
            laneC.additionalSpawn = 0;
        }

        //orange lanes
        foreach (LaneController laneC in orangeLanes)
        {
            laneC.additionalSpawn = 1;
        }
    }

    public void ResetAllLanes()
    {
        //blue lanes
        foreach (LaneController laneC in blueLanes)
        {
            laneC.additionalSpawn = 0;
        }

        //orange lanes
        foreach (LaneController laneC in orangeLanes)
        {
            laneC.additionalSpawn = 0;
        }
    }

}
