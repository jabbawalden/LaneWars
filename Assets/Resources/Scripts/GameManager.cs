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

    //UNIT COUNT
    [SerializeField] private List<LaneController> blueLanes;
    [SerializeField] private List<LaneController> orangeLanes;

    //SCRIPTS
    private AIController aiController;
     
    private void Awake()
    {
        aiController = FindObjectOfType<AIController>();

        defaultSpeed = 0.45f;
        upgradedSpeed = 0.7f;

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
        aiController.SetUpgradeBool(1, false);
        GameEvents.ReportChangeSpeeds();
    }

    public void UpgradeBlueSpeed()
    {
        OrangeSpeed = defaultSpeed;
        BlueSpeed = upgradedSpeed;
        aiController.SetUpgradeBool(1, true);
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
        aiController.SetUpgradeBool(2, false);
        GameEvents.ReportChangeHealth();
    }

    public void UpgradeBlueHealth()
    {
        OrangeHealth = defaultHealth;
        BlueHealth = upgradedHealth;
        aiController.SetUpgradeBool(2, true);
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
            laneC.additionalSpawn = 2;

        //orange lanes
        foreach (LaneController laneC in orangeLanes)
            laneC.additionalSpawn = 0;

        aiController.SetUpgradeBool(3, true);
    }

    public void UpgradeOrangeLanes()
    {
        //blue lanes
        foreach (LaneController laneC in blueLanes)
            laneC.additionalSpawn = 0;

        //orange lanes
        foreach (LaneController laneC in orangeLanes)
            laneC.additionalSpawn = 2;

        aiController.SetUpgradeBool(3, false);
    }

    public void ResetAllLanes()
    {
        //blue lanes
        foreach (LaneController laneC in blueLanes)
            laneC.additionalSpawn = 0;

        //orange lanes
        foreach (LaneController laneC in orangeLanes)
            laneC.additionalSpawn = 0;
    }

}
