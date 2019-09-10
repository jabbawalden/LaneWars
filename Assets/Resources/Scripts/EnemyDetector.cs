using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    // Start is called before the first frame update
    public int layerToCheck;
    [SerializeField] UnitController ballController;

    private void Awake()
    {
        ballController = GetComponentInParent<UnitController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        
        if(collision.gameObject.layer == layerToCheck)
        {
            ballController.enemies.Add(collision.gameObject);
        }
        

    }
}
