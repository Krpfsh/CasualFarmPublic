using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;
    [Header(" Settings ")]
    private int coins;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        LoadData();
        UpdateCoinsContainers();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateCoinsContainers();
        SaveData();
    }
    private void LoadData()
    {
        coins = PlayerPrefs.GetInt("Coins",10);
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("Coins", coins);
        if (PlayerPrefs.GetInt("RecordMain", 0) <= PlayerPrefs.GetInt("Coins", 0))
        {
            PlayerPrefs.SetInt("RecordMain", PlayerPrefs.GetInt("Coins", 0));
            YandexGame.NewLeaderboardScores("CashRecord", PlayerPrefs.GetInt("RecordMain", 0));
        }
    }

    private void UpdateCoinsContainers()
    {
        GameObject[] coinContainers = GameObject.FindGameObjectsWithTag("CoinAmount");

        foreach (GameObject coinContainer in coinContainers)
        {
            coinContainer.GetComponent<TextMeshProUGUI>().text = coins.ToString();
        }
    }
    public void UseCoins(int amount)
    {
        AddCoins(-amount);
    }
    public int GetCoins()
    {
        return coins;
    }
    [NaughtyAttributes.ButtonAttribute]
    private void Add500Coins()
    {
        AddCoins(500);
    }
}

