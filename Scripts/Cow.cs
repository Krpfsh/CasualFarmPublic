using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cow : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private InventoryManager inventoryManager;
    public static Action<ProductType> OnProductExchanged;
    private void Start()
    {
        SeedParticles.OnSeedsCollidedCow += SeedsCollidedCallback;
    }
    private void OnDisable()
    {
        SeedParticles.OnSeedsCollidedCow -= SeedsCollidedCallback;
    }
    public void SeedsCollidedCallback()
    {
        Debug.Log("1");
        if (HaveEat())
        {
            Debug.Log("2");
            //удалить количество кукурузы , в сравнимое цены €йца
            Inventory inventory = inventoryManager.GetInventory();
            inventory.ExchangeProducts(ProductType.Wheat, ProductsCostManager.instance.GetMilkCost());
            //добавить €йцо в инвентарь
            OnProductExchanged?.Invoke(ProductType.Milk);
            //создать партикл €йца
            ParticleSpawnProduct();
        }
    }

    private bool HaveEat()
    {
        Inventory inventory = inventoryManager.GetInventory();
        //Debug.Log(inventory.GetCropAmount());
        //Debug.Log(ProductsCostManager.instance.GetEggCost());

        if (inventory.GetProductAmount(ProductType.Wheat) >= ProductsCostManager.instance.GetMilkCost())
        {
            Debug.Log("3");
            return true;
        }

        return false;
    }

    public void ParticleSpawnProduct()
    {
        //Debug.Log("particle");
    }
}
