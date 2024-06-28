using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;
    // Start is called before the first frame update
    //private string _dataPath;
    private InventoryDisplay _inventoryDisplay;
    void Start()
    {
        //_dataPath = Application.dataPath + "/inventoryData.txt";
        //inventory = new Inventory();
        LoadInventory();

        ConfigureInventoryDisplay();

        CropTile.OnCropHarvested += CropHarvestedCallback;
        Chicken.OnProductExchanged += AddProductCallback;
        Cow.OnProductExchanged += AddProductCallback;
        Pig.OnProductExchanged += AddProductCallback;
    }
    private void OnDestroy()
    {
        CropTile.OnCropHarvested -= CropHarvestedCallback;
        Chicken.OnProductExchanged -= AddProductCallback;
        Cow.OnProductExchanged -= AddProductCallback;
        Pig.OnProductExchanged -= AddProductCallback;
    }
    [NaughtyAttributes.Button]
    public void ClearInventory()
    {
        inventory.Clear();
        _inventoryDisplay.UpdateDisplay(inventory);
        SaveInventory();
    }
    private void CropHarvestedCallback(ProductType cropType)
    {
        //Update our inventory
        inventory.CropHarvestedCallBack(cropType);

        _inventoryDisplay.UpdateDisplay(inventory);
        SaveInventory();
    }
    private void AddProductCallback(ProductType product)
    {
        //Update our inventory
        inventory.CropHarvestedCallBack(product);
        _inventoryDisplay.UpdateDisplay(inventory);
        SaveInventory();
    }
    private void ConfigureInventoryDisplay()
    {
        _inventoryDisplay = GetComponent<InventoryDisplay>();
        _inventoryDisplay.Configure(inventory);
    }
    private void LoadInventory()
    {

        string data = "";

        if (PlayerPrefs.GetString("InventoryManager") != null)
        {
            data = PlayerPrefs.GetString("InventoryManager");
            inventory = JsonUtility.FromJson<Inventory>(data);

            if (inventory == null)
            {
                inventory = new Inventory();
            }
        }
        else
        {
            inventory = new Inventory();
        }
    }
    private void SaveInventory()
    {
        string data = JsonUtility.ToJson(inventory, true);
        PlayerPrefs.SetString("InventoryManager", data);
    }
    public Inventory GetInventory()
    {
        return inventory;
    }
}
