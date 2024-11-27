using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Chest Config", menuName = "Configs/Chest")]
public class ChestConfigScriptableObject : ScriptableObject
{
    public int MinimunCoinCost;
    public int MaxCoinCost;
    public int MinimunCrystalCost;
    public int MaxCrystalCost;
    public int MinimunCoinReward;
    public int MaxCoinReward;
    public int MinimunCrystalReward;
    public int MaxCrystalReward;

    public float Timer;
    public float CrystalIncrementTime = 60;

    public Sprite Sprite;

    public int GetRandomCristalCost()
    {
        return Random.Range(MinimunCrystalCost, MaxCrystalCost + 1);
    }

    public int GetRandomCoinCost()
    {
        return Random.Range(MinimunCoinCost, MaxCoinCost + 1);
    }

    public int GetRandomCristalReward()
    {
        return Random.Range(MinimunCrystalReward, MaxCrystalReward + 1);
    }

    public int GetRandomCoinReward()
    {
        return Random.Range(MinimunCoinReward, MaxCoinReward + 1);
    }
}
