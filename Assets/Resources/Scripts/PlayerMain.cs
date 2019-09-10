using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [SerializeField] private int maxAmount;
    [SerializeField] private int amountLeft;
    [SerializeField] private LaneController lane1, lane2, lane3;
    private UIManager uiManager;
    private AIController aiController;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        aiController = FindObjectOfType<AIController>();
        amountLeft = maxAmount;
    }

    private void Start()
    {
        ChangeLaneAmount(2, lane1, 1);
        ChangeLaneAmount(2, lane2, 2);
        ChangeLaneAmount(2, lane3, 3);
    }

    public void ChangeLaneAmount(int amount, LaneController chosenLane, int uiIndex)
    {
        amountLeft -= amount;
        chosenLane.amount += amount;
        uiManager.SetUnitRemainText(amountLeft);
        uiManager.SetLaneText(chosenLane.amount, uiIndex);
    }

    public void changeLaneOne(bool isAdding) 
    {
        aiController.canChange = true;
        if (isAdding && amountLeft != 0)
        {
            ChangeLaneAmount(1, lane1, 1);
        }
        else if (!isAdding)
        {
            if (lane1.amount > 0)
                ChangeLaneAmount(-1, lane1, 1);
            else
                print("do not deduct");
        }
    }

    public void changeLaneTwo(bool isAdding)
    {
        aiController.canChange = true;
        if (isAdding && amountLeft != 0)
        {
            ChangeLaneAmount(1, lane2, 2);
        }
        else if (!isAdding)
        {
            if (lane2.amount > 0)
                ChangeLaneAmount(-1, lane2, 2);
        }
    }

    public void changeLaneThree(bool isAdding)
    {
        aiController.canChange = true;
        if (isAdding && amountLeft != 0)
        {
            ChangeLaneAmount(1, lane3, 3);
        }
        else if (!isAdding)
        {
            if (lane3.amount > 0)
                ChangeLaneAmount(-1, lane3, 3);
        }
    }
}
