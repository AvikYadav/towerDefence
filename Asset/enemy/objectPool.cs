using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPool : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] [Range(0.01f, 30f)] float delay = 2f;
    [SerializeField] [Range(0,30)] int poolSize = 5;
    //[SerializeField] bool spawing = true;

    GameObject[] pool;
    void Start()
    {
        StartCoroutine(spawnEnemy());
        //StartCoroutine(adjustDelay());
    }
    void Awake()
    {
        SpawnEnemyInPool();
    }

    void SpawnEnemyInPool()
    {
        pool = new GameObject[poolSize];

        for(int i = 0; i < pool.Length; i++)
        {
            Debug.Log("Instantiateing");
            pool[i] = Instantiate(Enemy, transform);
            pool[i].SetActive(false);
        }
    }
    void findEnemyDisabled()
    {
        foreach (GameObject enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return;
            }
        }
    }
    IEnumerator spawnEnemy()
    {
        while (true)
        {
            findEnemyDisabled();
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator adjustDelay()
    {
        while (delay>0.5f)
        {
            delay -= 0.01f;
            yield return new WaitForSeconds(0.5f);

        }
    }
}
