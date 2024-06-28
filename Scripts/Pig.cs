using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private InventoryManager inventoryManager;
    public static Action<ProductType> OnProductExchanged;
    private void Start()
    {
        SeedParticles.OnSeedsCollidedPig += SeedsCollidedCallback;
    }
    private void OnDisable()
    {
        SeedParticles.OnSeedsCollidedPig -= SeedsCollidedCallback;
    }
    public void SeedsCollidedCallback()
    {
        if (HaveEat())
        {
            //������� ���������� �������� , � ��������� ���� ����
            Inventory inventory = inventoryManager.GetInventory();
            inventory.ExchangeProducts(ProductType.Carrot, ProductsCostManager.instance.GetCarrotCost());
            //�������� ���� � ���������
            OnProductExchanged?.Invoke(ProductType.Bacon);
            //������� ������� ����
            ParticleSpawnProduct();
        }
    }

    private bool HaveEat()
    {
        Inventory inventory = inventoryManager.GetInventory();
        //Debug.Log(inventory.GetCropAmount());
        //Debug.Log(ProductsCostManager.instance.GetEggCost());

        if (inventory.GetProductAmount(ProductType.Carrot) >= ProductsCostManager.instance.GetCarrotCost())
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
