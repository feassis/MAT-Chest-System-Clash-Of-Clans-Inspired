using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestService
{
    private List<ChestConfigScriptableObject> chestConfigs = new List<ChestConfigScriptableObject>();
    private UIService uiService;

    public void Init(List<ChestConfigScriptableObject> chestConfigs, UIService uiService)
    {
        this.chestConfigs.AddRange(chestConfigs);
        this.uiService = uiService;
    }

    public ChestConfigScriptableObject GetRandomChest()
    {
        int sortedIndex = Random.Range(0, chestConfigs.Count);
        return chestConfigs[sortedIndex];
    }

    public bool CanAddAChest()
    {
        return uiService.GetChestHolder().HasSlotAvailable();
    }

    public void AddChest(ChestReward chestReward, ChestConfigScriptableObject chestConfig)
    {
        uiService.GetChestHolder().AddChest(chestReward, chestConfig);
    }

    public void RemoveChest(ChestReward chestReward)
    {
        uiService.GetChestHolder().RemoveChest(chestReward);
    }

    public ChestHolderController GetChestHolder()
    {
        return uiService.GetChestHolder();
    }
}

public class ChestReward
{
    public int CoinAmount;
    public int CrystalAmount;

    public ChestReward(int coinAmount, int crystalAmount)
    {
        CoinAmount = coinAmount;
        CrystalAmount = crystalAmount;
    }
}