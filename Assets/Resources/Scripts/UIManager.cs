using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unitsRemaining;
    [SerializeField] private TextMeshProUGUI unitsL1, unitsL2, unitsL3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUnitRemainText(int amount)
    {
        unitsRemaining.text = amount.ToString();
    }

    public void SetLaneText(int amount, int uiIndex)
    {
        switch (uiIndex)
        {
            case 1:
                unitsL1.text = amount.ToString();
                break;
            case 2:
                unitsL2.text = amount.ToString();
                break;
            case 3:
                unitsL3.text = amount.ToString();
                break;
        }

    }

}
