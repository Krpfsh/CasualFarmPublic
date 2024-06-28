using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class CharacterHat : MonoBehaviour
{
    [SerializeField] private CharacterSkinItem[] skinItems;
    [SerializeField] private GameObject hatsParent;

    private void OnEnable()
    {
        ShopPanel.OnSkinChange += SetHat;
    }
    private void OnDisable()
    {
        ShopPanel.OnSkinChange -= SetHat;
    }
    private void Awake()
    {
        SetHat();
    }
    private void SetHat()
    {
        var children = new List<GameObject>();
        foreach (Transform child in hatsParent.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        foreach (CharacterSkinItem item in skinItems)
        {
            if (PlayerPrefs.GetString("Selected") == item.Name)
            {
                Instantiate(item.Prefab, hatsParent.transform);
            }
        }
    }
}
