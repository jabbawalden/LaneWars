using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LaneInputCheck : MonoBehaviour, IPointerClickHandler
{
    /*
    [SerializeField] private int index;
    bool checkHover;

    private PlayerMain playerMain;

    private void Awake()
    {
        playerMain = FindObjectOfType<PlayerMain>();
    }
    */

    [SerializeField] private Button button;

    //void Awake()
    //{
    //    button = GetComponent<Button>();
    //}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //call add based on index
            Debug.Log("Left click");
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            //call reduction based on index
            button.onClick.Invoke();
            Debug.Log("Right click");
        }
    }

    public void OnClick()
    {
        print("WHAAT");
    }


    //private void OnMouseOver()
    //{
    //    if (checkHover)
    //    {
    //        checkHover = false;
    //        playerMain.selectedLaneIndex = index;
    //        print("are hovering over " + index);
    //    }
    //}

    //private void OnMouseExit()
    //{
    //    if (!checkHover)
    //    {
    //        checkHover = true;
    //        playerMain.selectedLaneIndex = 0;
    //        print("no longer hovering over " + index);
    //    }
    //}

}
