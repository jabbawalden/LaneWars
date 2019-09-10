using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType {orangeBall, blueBall }
public class LaneController : MonoBehaviour
{
    public UnitType unitType;

    //private Vector2 pos1, pos2, pos3, pos4, pos5, pos6, pos7, pos8, pos9, pos10;
    public int amount;
    [SerializeField] private List<Vector2> positionList = new List<Vector2>();
    private Vector2 originalPos;
    public int additionalSpawn;

    private void OnEnable()
    {
        GameEvents.eventSpawnUnits += SpawnUnits;
    }

    private void Awake()
    {
        originalPos = new Vector2(transform.position.x, transform.position.y);
        additionalSpawn = 0;
    }

    public void SpawnUnits()
    {
        GameObject ball;

        if (unitType == UnitType.orangeBall)
        {
            ball = Resources.Load<GameObject>("Prefabs/OrangeBall");
        }
        else
        {
            ball = Resources.Load<GameObject>("Prefabs/BlueBall");
        }

        for (int i = 0; i < amount + additionalSpawn; i++)
        {
            Instantiate(ball, originalPos + positionList[i], Quaternion.identity);
        }
    }


}
