using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    [Header(" Data ")]
    [SerializeField] private CropData[] cropData;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public Sprite GetCropSpriteFromCropType(ProductType cropType)
    {
        for (int i = 0; i < cropData.Length; i++)
        {
            if (cropData[i].cropType == cropType)
                return cropData[i].icon;
        }
        Debug.Log("No Cropdata found of that type");
        return null;    
    }

    public int GetCropPriceFromCropType(ProductType cropType)
    {
        for (int i = 0; i < cropData.Length; i++)
        {
            if (cropData[i].cropType == cropType)
                return cropData[i].price;
        }
        Debug.Log("No Cropdata found of that type");
        return 0;
    }
}
