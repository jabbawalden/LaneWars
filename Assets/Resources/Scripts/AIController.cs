using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainState {aggressive, defensive }

public class AIController : MonoBehaviour
{
    private MainState mainState;

    [Header("DecisionRate")]
    [SerializeField] private float decisionRateMin;
    [SerializeField] private float decisionRateMax;
    private float newDecisionTime;

    [Header("Controls")]
    [SerializeField] private List<LaneController> playerLanes;
    [SerializeField] private List<LaneController> ourLanes;

    [SerializeField] private List<LaneController> threat;
    [SerializeField] private List<LaneController> advantage;
    [SerializeField] private List<LaneController> laneEven; 
    private int amountCheck;

    [SerializeField] private int maxAmount;
    [SerializeField] private int amountLeft;
    private int changeAmount;

    private LaneController laneToIncrease;
    private LaneController laneToDecrease;
    private LaneController neutralLane;

    public bool canChange;
    private int indexToPush;
    private int indexToDecrease;
    private int increaseDifference;

    [SerializeField] private bool hasSpeedUpgrade;
    [SerializeField] private bool hasHealthUpgrade;
    [SerializeField] private bool hasAmountUpgrade;

    private void Awake()
    {
        amountLeft = maxAmount;
    }

    private void Start()
    {
        ChangeLaneAmount(3, ourLanes[0]);
        ChangeLaneAmount(3, ourLanes[1]);
        ChangeLaneAmount(3, ourLanes[2]);
    }

    private void Update()
    {
        DecisionCalculate();
    }

    public void SetUpgradeBool (int lane, bool state)
    {
        switch(lane)
        {
            case 1:
                hasSpeedUpgrade = state;
                break;
            case 2:
                hasHealthUpgrade = state;
                break;
            case 3:
                hasAmountUpgrade = state;
                break;
        }
    }

    private void DecisionCalculate()
    {
        if (newDecisionTime <= Time.time)
        {
            float r = Random.Range(decisionRateMin, decisionRateMax);
            LaneThreatCheck();
            ControllerAction();
            newDecisionTime = Time.time + r;
        }
    }

    private void ChangeLaneAmount(int amount, LaneController chosenLane)
    {
        amountLeft -= amount;
        chosenLane.amount += amount;
    }

    private void LaneThreatCheck()
    {
        //clear lists so that they can update properly
        threat.Clear();
        advantage.Clear();
        laneEven.Clear();
        //checks every time a decision must be made
        //also needs to know where it has the advantage
        for (int i = 0; i < playerLanes.Count; i++)
        {
            if (playerLanes[i].amount > ourLanes[i].amount)
            {
                threat.Add(playerLanes[i]);
                laneToIncrease = ourLanes[i];
                //get amount required to match
                changeAmount = playerLanes[i].amount - ourLanes[i].amount;
                //this lane is a threat
            }
            else if (playerLanes[i].amount < ourLanes[i].amount)
            {
                advantage.Add(playerLanes[i]);
                laneToDecrease = ourLanes[i];
                //at an advantage
            }
            else if (playerLanes[i].amount == ourLanes[i].amount)
            {
                laneEven.Add(playerLanes[i]);
                //lane is even
            }
        }
    }

    private void CheckCount()
    {
        int mostAmount = 0;
        int leastAmount = 10;
        //check which enemy lane has the most units
        for (int i = 0; i < ourLanes.Count; i++)
        {
            if (ourLanes[i].amount > mostAmount)
            {
                mostAmount = ourLanes[i].amount;
                indexToDecrease = i;
            }
        }
        //check which enemy lane has the least units
        for (int i = 0; i < ourLanes.Count; i++)
        {
            if (ourLanes[i].amount < leastAmount)
            {
                leastAmount = ourLanes[i].amount;
                indexToPush = i;
            }
        }
        //the amount that they should swap by
        increaseDifference = mostAmount - leastAmount;
    }

    private void ControllerAction()
    {
        int doesNothing = Random.Range(0, 8);
        int decision = Random.Range(0, 6);
        int randomLaneToIncrease = Random.Range(0, 2);

        //print(doesNothing);

        //makes an actions
        if (doesNothing <= 3)
        {
            //print("make action");
            if (canChange && decision <= 2)
            {
                if (laneToIncrease)
                    ChangeLaneAmount(changeAmount, laneToIncrease);

                if (laneToDecrease)
                    ChangeLaneAmount(-changeAmount, laneToDecrease);

                canChange = false;
            }
            else if (decision >= 3)
            {
                /*
                CheckCount();
                ourLanes[indexToPush].amount += increaseDifference / 2;
                ourLanes[indexToDecrease].amount -= increaseDifference / 2;
                */
                //swap amounts between lanes
                CheckCount();
                ourLanes[indexToPush].amount += increaseDifference / 2;
                ourLanes[indexToDecrease].amount -= increaseDifference / 2;
                if (amountLeft > 0)
                {
                    ChangeLaneAmount(amountLeft, ourLanes[randomLaneToIncrease]);
                }
            }
        }//makes no action
        else if (doesNothing >= 4)
        {
            //print("reduce and wait");
            if (advantage.Count > 0)
            {
                foreach (LaneController laneController in advantage)
                {
                    laneController.amount -= 1;
                    amountLeft += 1;
                }
            }
            
        }
    }


    ///AI FUNCTIONALITY

    ///Parameters

    //Check threat, get most dangerous (we are at an disadvantage), advantage and even lanes (add to list)
    //Checks which of OUR lanes has the most and which has the least (if even, picks one of them)
    //Check how many advantages we have if any.
    //In both states, same actions can occur, just less chance of certain ones occurring

    ///action types
    //AI OVERVIEW STATE: Defensive - CONDITION = If enemy has 1 or more advantage 
    //No action (1 in 3 chance)
    //Swap biggest and smallest lane numbers AT WILL
    //Swap biggest and next smallest lane numbers AT WILL
    //Match player in numbers

    //AI OVERVIEW STATE: Aggressive - CONDITION = If player has 1 or more advantage
    //No action (1 in 5 chance)
    //Swap biggest and smallest lane numbers AT WILL
    //Swap biggest and next smallest lane numbers AT WILL
    //Pick lane with advantage, reduce to 1, and add remaining numbers to lane with no advantage OR lane that player has advantage of
    //Match player in numbers + 1

    //function for deciding if aggressive or defensive state
}
