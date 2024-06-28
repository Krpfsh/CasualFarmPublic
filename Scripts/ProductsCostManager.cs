using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsCostManager : MonoBehaviour
{
    public static ProductsCostManager instance;
    [SerializeField] private int eggCost;
    [SerializeField] private int milkCost;
    [SerializeField] private int baconCost;
    private void Awake()
    {
        if (instance == null)            instance = this;
        else
            Destroy(gameObject);
    }

    public int GetEggCost() => eggCost;
    public int GetMilkCost() => milkCost;
    public int GetCarrotCost() => baconCost;
}
