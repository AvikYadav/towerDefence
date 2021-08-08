using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetLocator : MonoBehaviour
{
    [SerializeField] ParticleSystem projectiles;
    [SerializeField] float range = 15f;
    [SerializeField] Transform weaponHead;
    [SerializeField] GameObject pool;
    Transform target;
    bool tar;
    // Start is called before the first frame update

    // Update is called once per frame

    
    void Start()
    {
        pool = FindObjectOfType<objectPool>().gameObject;
        Debug.Log(pool);
        //enemy[] enemies = FindObjectsOfType<enemy>();
        //target = enemies[0].transform;
    }
    void Update()
    {

        //FindClosestTarget();
        //lockTarget();
        lockTargetMyFunction();
    }

    void lockTarget()
    {
        float targetDist = Vector3.Distance(transform.position, target.transform.position);
        if(targetDist < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }

        weaponHead.LookAt(target);
    }
    void lockTargetMyFunction()
    {
        tar = FindTarget();
        
        if (tar)
        {
            float targetDist = Vector3.Distance(transform.position, target.transform.position);
            if (targetDist < range)
            {
                Attack(true);
            }
            else
            {
                Attack(false);
            }
            weaponHead.LookAt(target);

        }
        else
        {
            Attack(false);

        }
    }

    bool FindTarget()
    {
        List<GameObject> enemyActive = new List<GameObject>();
        foreach (Transform enemyChild in pool.transform)
        {
            if (enemyChild.gameObject.activeInHierarchy)
            {
                enemyActive.Add(enemyChild.gameObject);
                //target = enemyChild.transform;
                //return true;
            }
        }
        Transform ClosestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemyActive)
        {
            float TargetDistance = Vector3.Distance(transform.position, enemy.transform.position);


            if (TargetDistance < maxDistance)
            {
                ClosestTarget = enemy.transform;
                maxDistance = TargetDistance;
            }

        }

        target = ClosestTarget;
        if (target == null)
        {
            return false;
        }
        return true;
        
    }
    void FindClosestTarget()
    {
        enemy[] enemies = FindObjectsOfType<enemy>();
        Transform ClosestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (enemy enemy in enemies)
        {
            float TargetDistance = Vector3.Distance(transform.position, enemy.transform.position);


            if(TargetDistance < maxDistance)
            {
                ClosestTarget = enemy.transform;
                maxDistance = TargetDistance;
            }

        }

        target = ClosestTarget;
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectiles.emission;
        emissionModule.enabled = isActive;
    }
}
