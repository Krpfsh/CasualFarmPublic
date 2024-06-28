using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkWalls : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject frontWall;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject backWall;
    [SerializeField] private GameObject leftWall;
    public void Configure(int configuration)
    {
        frontWall.SetActive(isKthBitSet(configuration, 0));
        rightWall.SetActive(isKthBitSet(configuration, 1));
        backWall.SetActive(isKthBitSet(configuration, 2));
        leftWall.SetActive(isKthBitSet(configuration, 3));
    }

    public bool isKthBitSet(int configuration, int k)
    {
        if ((configuration & (1 << k)) > 0)
            return false;
        else
            return true;
    }
}
