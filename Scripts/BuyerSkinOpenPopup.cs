using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerSkinOpenPopup : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject shopPopup;
    public void OpenShop()
    {
        shopPopup.SetActive(true);
    }
}
