using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using YG.Example;


public class AdManager : MonoBehaviour
{
    [SerializeField] private int coinsToAdd;
    [SerializeField] private Button buttonAddGold;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }
    private void Start()
    {
        buttonAddGold.onClick.AddListener(delegate { ExampleOpenRewardAd(1); });
    }
    void ExampleOpenRewardAd(int id)
    {
        YandexGame.RewVideoShow(id);
    }
    void Rewarded(int id)
    {
        if(id == 1)
        {
            CashManager.instance.AddCoins(coinsToAdd);
        }
    }
}
