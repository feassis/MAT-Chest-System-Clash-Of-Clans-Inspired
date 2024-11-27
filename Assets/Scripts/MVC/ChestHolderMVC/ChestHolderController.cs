using System;
using System.Collections.Generic;
using UnityEngine;

public class ChestHolderController
{
    private ChestHolderView view;
    private ChestHolderModel model;

    private List<ChestSlotController> slots = new List<ChestSlotController>();

    public ChestHolderController(ChestHolderView view, ChestHolderModel model, Transform uiRoot)
    {
        this.view = GameObject.Instantiate<ChestHolderView>(view, uiRoot);
        this.view.SetController(this);
        this.model = model;
        this.model.SetController(this);
        InitializedChestSlots();
    }

    private void InitializedChestSlots()
    {
        for(int i = 0; i < model.InitialUnlockedSlots; i++)
        {
            ChestSlotModel modelSlot = new ChestSlotModel(ChestSlotMode.Empty, GetUpgradeCost());
            ChestSlotController controllerSlot = new ChestSlotController(model.chestSlotPrefab, modelSlot, view.GetSlotHolder());
            slots.Add(controllerSlot);
        }

        ChestSlotModel lockedModelSlot = new ChestSlotModel(ChestSlotMode.Locked, GetUpgradeCost());
        ChestSlotController lockedControllerSlot = new ChestSlotController(model.chestSlotPrefab, lockedModelSlot, view.GetSlotHolder());
        slots.Add(lockedControllerSlot);
    }

    public bool HasSlotAvailable()
    {
        foreach (var slot in slots)
        {
            if(slot.GetSlotMode() == ChestSlotMode.Empty)
            {
                return true;
            }
        }

        return false;
    }

    public void AddChest(ChestReward chestReward, ChestConfigScriptableObject config)
    {
        ChestSlotController availableSlot = slots.Find(s => s.GetSlotMode() == ChestSlotMode.Empty);

        if (availableSlot == null)
        {
            Debug.LogError("No Slot Available");
            return;
        }

        availableSlot.AddChest(chestReward, config);

        if (!HasSlotActive())
        {
            availableSlot.SetSlotActive(true);
        }

    }

    private bool HasSlotActive()
    {
        foreach (var slot in slots)
        {
            if (slot.IsSlotActive())
            {
                return true;
            }
        }

        return false;
    }

    private int GetUpgradeCost()
    {
        return model.InitialUnlockCost + model.IncrementUnlockCost * model.UpgradeAmount;
    }

    public void RemoveChest(ChestReward chestReward)
    {
        ChestSlotController availableSlot = slots.Find(s => s.GetReward() == chestReward);

        if (availableSlot == null)
        {
            Debug.LogError("No Slot Available To Remove");
            return;
        }

        availableSlot.EmptyChest();
    }
}
