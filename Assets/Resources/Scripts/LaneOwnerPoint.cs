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

    [SerializeField] Color orange, blue;

    SpriteRenderer ourRenderPoint;

    private void Awake()
    {
        canChange = true;
        gameManager = FindObjectOfType<GameManager>();
        ourRenderPoint = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if (ownerType == OwnerType.orangeOwner)
        {
            ourRenderPoint.color = blue;
        }
        else
        {
            ourRenderPoint.color = orange;
        }
    }

    private void ChangeColors(Color color)
    {
        ourRenderPoint.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ownerType == OwnerType.blueOwner)
        {
            //If blue has passed over the line
            if (collision.CompareTag("Blue") && canChange)
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
                ChangeColors(blue);
            }
            //If orange has passed back over the line
            if (collision.CompareTag("Orange") && !canChange)
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
                ChangeColors(orange);
            }
        }

        if (ownerType == OwnerType.orangeOwner)
        {
            //If orange has passed over the line
            if (collision.CompareTag("Orange") && canChange)
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
                ChangeColors(orange);
            }

            //If blue has passed back over the line
            if (collision.CompareTag("Blue") && !canChange)
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
                ChangeColors(blue);
            }
        }
    }

}
