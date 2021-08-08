using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(enemy))]
public class enemyHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] float HitPoints = 10f;
    [SerializeField] float DifficultyHealth = 5;
    float currentHealth;

    enemy enemy;
    void OnEnable()
    {
        currentHealth = health;    
    }

    void Start()
    {
        enemy = GetComponent<enemy>();
    }
    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        Debug.Log("collided");
        currentHealth -= HitPoints;
        if (currentHealth < Mathf.Epsilon)
        {
            gameObject.SetActive(false);
            if (currentHealth < 1000)
            {
                addDificulty();
                health += DifficultyHealth;
            }
            enemy.GetPenalty();
        }
    }

    void addDificulty()
    {
        DifficultyHealth += 5;
    }
}
