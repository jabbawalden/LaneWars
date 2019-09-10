using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{

    [Header("DecisionRate")]
    [SerializeField] private float decisionRateMin;
    [SerializeField] private float decisionRateMax;
    private float newDecisionTime;

    [Header("Controls")]
    [SerializeField] private List<LaneController> playerLanes;
    [SerializeField] private List<LaneController> ourLanes;

    [SerializeField] private List<LaneController> threat;
    [SerializeField] private List<LaneController> advantage;
    [SerializeField] private List<LaneController> even; 
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

    private void Awake()
    {
        amountLeft = maxAmount;
    }

    private void Start()
    {
        ChangeLaneAmount(2, ourLanes[0]);
        ChangeLaneAmount(2, ourLanes[1]);
        ChangeLaneAmount(2, ourLanes[2]);
    }

    private void Update()
    {
        LaneThreatCheck();
        DecisionCalculate();
    }

    private void ChangeLaneAmount(int amount, LaneController chosenLane)
    {
        amountLeft -= amount;
        chosenLane.amount += amount;
    }

    private void LaneThreatCheck()
    {
        threat.Clear();
        advantage.Clear();
        even.Clear();
        //checks this every 2 seconds
        //also needs to know where it has the advantage
        for (int i = 0; i < playerLanes.Count; i++)
        {
            if (playerLanes[i].amount > ourLanes[i].amount)
            {
                threat.Add(playerLanes[i]);
                laneToIncrease = ourLanes[i];
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
                even.Add(playerLanes[i]);
                //lane is even
            }
        }
    }

    private void CheckCount()
    {
        int mostAmount = 0;
        int leastAmount = 10;

        for (int i = 0; i < ourLanes.Count; i++)
        {
            if (ourLanes[i].amount > mostAmount)
            {
                mostAmount = ourLanes[i].amount;
                indexToDecrease = i;
            }
        }

        for (int i = 0; i < ourLanes.Count; i++)
        {
            if (ourLanes[i].amount < leastAmount)
            {
                leastAmount = ourLanes[i].amount;
                indexToPush = i;
            }
        }

        increaseDifference = mostAmount - leastAmount;
    }

    private void ControllerAction()
    {
        int doesNothing = Random.Range(0, 4);
        int decision = Random.Range(0, 5);
        int randomLaneToIncrease = Random.Range(0, 2);

        if (doesNothing <= 3)
        {
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
                //or choose amount TO swap
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
                    ChangeLaneAmount(2, ourLanes[randomLaneToIncrease]);
                }
            }
        }
        else if (doesNothing >= 2)
        {
            print("do nothing");
        }
        //if (neutralLane)

    }

    private void DecisionCalculate()
    {
        if (newDecisionTime <= Time.time)
        {
            float r = Random.Range(decisionRateMin, decisionRateMax);
            ControllerAction();
            newDecisionTime = Time.time + r;
        }
    }
}
