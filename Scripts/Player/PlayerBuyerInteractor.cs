using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuyerInteractor : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private InventoryManager inventoryManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Buyer"))
        {
            SellCrops();
        }
    }
    private void SellCrops()
    {
        Inventory inventory = inventoryManager.GetInventory();
        InventoryItem[] items = inventory.GetInventoryItems();

        int coinsEarned = 0;
        for (int i = 0; i < items.Length; i++)
        {
            //Calculate the earnings
            int itemPrice = DataManager.instance.GetCropPriceFromCropType(items[i].cropType);
            coinsEarned += itemPrice * items[i].amount;
        }
        TransactionEffectManager.instance.PlayCoinParticles(coinsEarned);
        if (coinsEarned > 0)
            AudioManager.instance.Play("SaleCrops");
        //give coins to player
        //CashManager.instance.AddCoins(coinsEarned);
        //clear the inventory;
        inventoryManager.ClearInventory();
    }
}