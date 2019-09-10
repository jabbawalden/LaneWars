using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OwnerType {blueOwner, orangeOwner }

public enum UpgradeType { increaseSpeed, increaseHealth, increaseAmount }
//increase speed
//double health
//add extra 1 unit to all lanes

public class LaneOwnerPoint : MonoBehaviour
{
    [SerializeField] private UpgradeType upgradeType;
    [SerializeField] private OwnerType ownerType;
    [SerializeField] private LaneOwnerPoint otherLaneOwnerPoint;
    public bool canChange;

    private GameManager gameManager;

    private void Awake()
    {
        canChange = true;
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ownerType == OwnerType.blueOwner)
        {
            if (collision.gameObject.layer == 9 && canChange)
            {
                print("blue has taken lane");
                if (upgradeType == UpgradeType.increaseSpeed)
                {
                    gameManager.UpgradeBlueSpeed();
                }
                else if (upgradeType == UpgradeType.increaseHealth)
                {
                    gameManager.UpgradeBlueHealth();
                }
                else if (upgradeType == UpgradeType.increaseAmount)
                {
                    gameManager.UpgradeBlueLanes();
                }
                canChange = false;
                otherLaneOwnerPoint.canChange = true;
            }

            if (collision.gameObject.layer == 8 && !canChange)
            {
                print("orange has reverted lane");
                if (upgradeType == UpgradeType.increaseSpeed)
                {
                    gameManager.ResetAllSpeeds();
                }
                else if (upgradeType == UpgradeType.increaseHealth)
                {
                    gameManager.ResetAllHealth();
                }
                else if (upgradeType == UpgradeType.increaseAmount)
                {
                    gameManager.ResetAllLanes();
                }
                canChange = true;
                otherLaneOwnerPoint.canChange = true;
            }
        }

        if (ownerType == OwnerType.orangeOwner)
        {
            if (collision.gameObject.layer == 8 && canChange)
            {
                print("orange has taken lane");
                if (upgradeType == UpgradeType.increaseSpeed)
                {
                    gameManager.UpgradeOrangeSpeed();
                }
                else if (upgradeType == UpgradeType.increaseHealth)
                {
                    gameManager.UpgradeOrangeHealth();
                }
                else if (upgradeType == UpgradeType.increaseAmount)
                {
                    gameManager.UpgradeOrangeLanes();
                }
                canChange = false;
                otherLaneOwnerPoint.canChange = true;
            }

            if (collision.gameObject.layer == 9 && !canChange)
            {
                print("blue has reverted lane");
                if (upgradeType == UpgradeType.increaseSpeed)
                {
                    gameManager.ResetAllSpeeds();
                }
                else if (upgradeType == UpgradeType.increaseHealth)
                {
                    gameManager.ResetAllHealth();
                }
                else if (upgradeType == UpgradeType.increaseAmount)
                {
                    gameManager.ResetAllLanes();
                }
                canChange = true;
                otherLaneOwnerPoint.canChange = true;
            }
        }

    }

}
