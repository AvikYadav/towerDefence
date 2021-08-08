using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    [SerializeField] int reward = 25;
    [SerializeField] int penalty = 25;
    // Start is called before the first frame update
    bank bank;
    void Start()
    {
        bank = FindObjectOfType<bank>();
    }

    public void GetReward()
    {
        if(bank == null) { return; }
        bank.widhraw(reward);
    }
    public void GetPenalty()
    {
        if(bank == null) { return; }
        bank.deposit(penalty);
    }
}
