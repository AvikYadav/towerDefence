using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    // Start is called before the first frame update
    public bool createTower(Tower towerPrefab,Vector3 pos)
    {
        bank bank = FindObjectOfType<bank>();
        if (bank == null) { return false; }
        

        if (bank.CurrentCash >= cost)
        {
            Instantiate(towerPrefab, pos, Quaternion.identity);
            bank.widhraw(cost);
            return true;

        }
        return false;
    }
}
