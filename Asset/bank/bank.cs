using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class bank : MonoBehaviour
{
    [SerializeField] int startingMoney = 150;
    [SerializeField] int currentCash;
    [SerializeField] TMP_Text gold;
    public int CurrentCash{ get { return currentCash; } }

    private void Awake()
    {
        currentCash = startingMoney;
        displayScore();
    }
    public void deposit(int amount)
    {
        currentCash += Mathf.Abs(amount);
        displayScore();
    }

    public void widhraw(int amount)
    {
        currentCash -= Mathf.Abs(amount);
        displayScore();
        if (currentCash < 0)
        {
            reloadScene();
        }

    }
    void displayScore()
    {
        gold.text = $"GOLD : {currentCash}";
    }
    void reloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
