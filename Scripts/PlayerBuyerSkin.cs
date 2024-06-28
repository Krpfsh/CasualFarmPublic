using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuyerSkin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BuyerSkin"))
        {
            other.GetComponent<BuyerSkinOpenPopup>().OpenShop();
        }
    }

}
