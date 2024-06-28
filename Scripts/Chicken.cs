using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private InventoryManager inventoryManager;
    public static Action<ProductType> OnProductExchanged;
    private void Start()
    {
        SeedParticles.OnSeedsCollidedChicken += SeedsCollidedCallback;
    }
    private void OnDisable()
    {
        SeedParticles.OnSeedsCollidedChicken -= SeedsCollidedCallback;
    }
    public void SeedsCollidedCallback()
    {
        if (HaveEat())
        {
            //������� ���������� �������� , � ��������� ���� ����
            Inventory inventory = inventoryManager.GetInventory();
            inventory.ExchangeProducts(ProductType.Corn, ProductsCostManager.instance.GetEggCost());
            //�������� ���� � ���������
            OnProductExchanged?.Invoke(ProductType.Egg);
            //������� ������� ����
            ParticleSpawnProduct();
        }
    }

    private bool HaveEat()
    {
        Inventory inventory = inventoryManager.GetInventory();
        //Debug.Log(inventory.GetCropAmount());
        //Debug.Log(ProductsCostManager.instance.GetEggCost());

        if (inventory.GetProductAmount(ProductType.Corn) >= ProductsCostManager.instance.GetEggCost())
        {
            return true;
        }
            
        return false;
    }

    public void ParticleSpawnProduct()
    {
        //Debug.Log("particle");
    }
}
