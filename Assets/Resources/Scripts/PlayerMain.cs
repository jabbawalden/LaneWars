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

    [System.NonSerialized] public int selectedLaneIndex;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        aiController = FindObjectOfType<AIController>();
        amountLeft = maxAmount;
    }

    private void Start()
    {
        ChangeLaneAmount(3, lane1, 1);
        ChangeLaneAmount(3, lane2, 2);
        ChangeLaneAmount(3, lane3, 3);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (selectedLaneIndex)
            {
                case 1:
                    ChangeLaneOne(true);
                    break;
                case 2:
                    ChangeLaneTwo(true);
                    break;
                case 3:
                    ChangeLaneThree(true);
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            switch (selectedLaneIndex)
            {
                case 1:
                    ChangeLaneOne(false);
                    break;
                case 2:
                    ChangeLaneTwo(false);
                    break;
                case 3:
                    ChangeLaneThree(false);
                    break;
            }
        }
    }

    public void ChangeLaneAmount(int amount, LaneController chosenLane, int uiIndex)
    {
        print(amount);
        amountLeft -= amount;
        chosenLane.amount += amount;
        uiManager.SetUnitRemainText(amountLeft);
        uiManager.SetLaneText(chosenLane.amount, uiIndex);
    }

    public void ChangeLaneOne(bool isAdding) 
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
        }
    }

    public void ChangeLaneTwo(bool isAdding)
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

    public void ChangeLaneThree(bool isAdding)
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
