using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private UICropContainer _uiCropContainerPrefab;
    [SerializeField] private Transform _cropContainersParent;


    public void Configure(Inventory inventory)
    {
        InventoryItem[] items = inventory.GetInventoryItems();

        for (int i = 0; i < items.Length; i++)
        {
            UICropContainer cropContainerInstance = Instantiate(_uiCropContainerPrefab, _cropContainersParent);

            Sprite cropIcon = DataManager.instance.GetCropSpriteFromCropType(items[i].cropType);
            cropContainerInstance.Configure(cropIcon, items[i].amount);
        }
    }
    public void UpdateDisplay(Inventory inventory)
    {
        InventoryItem[] items = inventory.GetInventoryItems();
        UICropContainer containerInstance;
        for (int i = 0; i < items.Length; i++)
        {
            if (i < _cropContainersParent.childCount)
            {
                containerInstance = _cropContainersParent.GetChild(i).GetComponent<UICropContainer>();
                containerInstance.gameObject.SetActive(true);
                
            }
            else
            {
                containerInstance = Instantiate(_uiCropContainerPrefab, _cropContainersParent);
            }
            Sprite cropIcon = DataManager.instance.GetCropSpriteFromCropType(items[i].cropType);
            containerInstance.Configure(cropIcon, items[i].amount);
        }
        int remainingContainers = _cropContainersParent.childCount - items.Length;
        if (remainingContainers <= 0)
        {
            return;
        }
        for (int i = 0; i < remainingContainers; i++)
        {
            _cropContainersParent.GetChild(items.Length + i).gameObject.SetActive(false);

        }
    }
}
