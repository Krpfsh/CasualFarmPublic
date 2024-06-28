using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(ChunkWalls))]
public class Chunk : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject unlockedElements;
    [SerializeField] private GameObject lockedElements;
    [SerializeField] private TextMeshPro priceText;
    private ChunkWalls chunkWalls;
    [SerializeField] private MeshFilter chunkFilter;
    [Header(" Settings ")]
    [SerializeField] private int initialPrice;

    private int _currentPrice;
    private bool _unlocked;
    private int configuration;

    [Header(" Actions ")]
    public static Action OnUnclocked;
    public static Action OnPriceChanged;

    private void Awake()
    {
        chunkWalls = GetComponent<ChunkWalls>();
    }
    private void Start()
    {
        _currentPrice = initialPrice;
        priceText.text = _currentPrice.ToString();
    }
    public void Initialize(int loadedPrice)
    {
        _currentPrice = loadedPrice;
        priceText.text = _currentPrice.ToString();
        if (_currentPrice <= 0)
            Unlock(false);
    }

    public void TryUnlock()
    {
        if (CashManager.instance.GetCoins() <= 0)
        {
            return;
        }
        _currentPrice--;
        CashManager.instance.UseCoins(1);

        OnPriceChanged?.Invoke();

        priceText.text = _currentPrice.ToString();

        if (_currentPrice <= 0)
        {
            Unlock();
        }
    }

    private void Unlock(bool triggerAction = true)
    {
        unlockedElements.SetActive(true);
        lockedElements.SetActive(false);
        _unlocked = true;

        if (triggerAction)
            OnUnclocked?.Invoke();
    }
    public void UpdateWalls(int configuration)
    {
        this.configuration = configuration;
        chunkWalls.Configure(configuration);
    }
    public bool IsUnlocked()
    {
        return _unlocked;
    }
    public int GetInitialPrice()
    {
        return initialPrice;
    }
    public int GetCurrentPrice()
    {
        return _currentPrice;
    }

    public void DisplayLockedElements()
    {
        lockedElements.SetActive(true);
    }

    public void SetRenderer(Mesh chunkMesh)
    {
        chunkFilter.mesh = chunkMesh;
    }
}
