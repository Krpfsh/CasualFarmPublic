using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Inventory
{
    [SerializeField] private List<InventoryItem> items = new List<InventoryItem>();

    public void CropHarvestedCallBack(ProductType cropType)
    {
        bool cropFound = false;

        for (int i = 0; i < items.Count; i++)
        {
            InventoryItem item = items[i];

            if (item.cropType == cropType)
            {
                item.amount++;
                cropFound = true;
                break;
            }
        }
        //DebugInventory();
        if (cropFound)
        {
            return;
        }

        items.Add(new InventoryItem(cropType, 1));


    }
    public InventoryItem[] GetInventoryItems()
    {
        return items.ToArray();
    }
    public int GetProductAmount(ProductType productType)
    {
        foreach (InventoryItem item in items)
        {
            if (item.cropType == productType)
            {
                return item.amount;
            }
        }
        return 0;
    }
    public void DebugInventory()
    {
        foreach (InventoryItem item in items)
        {
            Debug.Log("we have  " + item.amount + " items in our " + item.cropType + " List");
        }
    }
    public void Clear()
    {
        items.Clear();
    }

    public void ExchangeProducts(ProductType cropType, int productCost)
    {
        foreach (InventoryItem item in items)
        {
            if (item.cropType == cropType)
            {
                item.amount -= productCost;
            }
        }
    }
}
