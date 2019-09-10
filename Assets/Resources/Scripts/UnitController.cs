using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {up, down }
public enum Type {orange, blue }
public class UnitController : MonoBehaviour
{
    [SerializeField] private Type type;
    [SerializeField] private Direction direction;
    [SerializeField] private float speed;
    public List<GameObject> enemies;
    [SerializeField] private GameObject target;
    private HealthComponent healthComp;

    private GameManager gameManager;

    private void OnEnable()
    {
        GameEvents.eventChangeSpeeds += SetSpeed;
        GameEvents.eventChangeHealth += SetHealth;
    }

    private void Awake()
    {
        healthComp = GetComponent<HealthComponent>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        SetSpeed();
        SetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        CheckList();
        Movement();
    }

    private void CheckList()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i])
                enemies.RemoveAt(i);
        }
    }

    private void Movement()
    {
        if (enemies.Count == 0)
        {
            if (direction == Direction.up)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
        }
        else
        {
            GetClosest();
            //If target exists, then go towards, else do as code above
            if (target)
            {
                Vector2 direction = target.transform.position - transform.position;
                direction = direction.normalized;
                transform.Translate(direction * speed * Time.deltaTime);
            }
            else
            {
                if (direction == Direction.up)
                {
                    transform.Translate(Vector2.up * speed * Time.deltaTime);
                }
                else
                {
                    transform.Translate(Vector2.down * speed * Time.deltaTime);
                }
            }
        }
    }

    private void GetClosest()
    {
        float distance = 15;

        foreach(GameObject obj in enemies)
        {
            if (obj)
            {
                float currentDistance = Vector2.Distance(transform.position, obj.transform.position);

                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    target = obj;
                }
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Orange is layer 8
        //Blue is layer 9
        
        if (type == Type.orange)
        {
            if (collision.collider.GetComponent<HealthComponent>() && collision.gameObject.layer == 9)
            {
                collision.collider.GetComponent<HealthComponent>().Damage(1);
            }
        }
        else if (type == Type.blue)
        {
            if (collision.collider.GetComponent<HealthComponent>() && collision.gameObject.layer == 8)
            {
                collision.collider.GetComponent<HealthComponent>().Damage(1);
            }
        }
    }

    private void SetSpeed()
    {
        if (type == Type.orange)
        {
            speed = gameManager.OrangeSpeed;
        }
        else if (type == Type.blue)
        {
            speed = gameManager.BlueSpeed;
        }
    }

    private void SetHealth()
    {
        if (type == Type.orange)
        {
            healthComp.health = gameManager.OrangeHealth;
        }
        else if (type == Type.blue)
        {
            healthComp.health = gameManager.BlueHealth;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
