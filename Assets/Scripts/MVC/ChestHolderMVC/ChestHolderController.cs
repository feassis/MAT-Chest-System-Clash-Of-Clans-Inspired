using System;
using System.Collections.Generic;
using UnityEngine;

public class ChestHolderController
{
    private ChestHolderView view;
    private ChestHolderModel model;

    private List<ChestSlotController> slots = new List<ChestSlotController>();
    private WalletService walletService;
    private ChestService chestService;
    private CommandInvolker commandInvolker;
    private EventService eventService;

    public ChestHolderController(ChestHolderView view, ChestHolderModel model, Transform uiRoot, 
        WalletService walletService, ChestService chestService, CommandInvolker commandInvolker, EventService eventService)
    {
        this.view = GameObject.Instantiate<ChestHolderView>(view, uiRoot);
        this.view.SetController(this);
        this.model = model;
        this.model.SetController(this);
        
        this.walletService = walletService;
        this.chestService = chestService;
        this.commandInvolker = commandInvolker;
        this.eventService = eventService;

        this.eventService.OnSlotReady.AddListener(ActivateNextSlotSlot);

        InitializedChestSlots();
    }

    ~ChestHolderController()
    {
        this.eventService.OnSlotReady.RemoveListener(ActivateNextSlotSlot);
    }

    private void InitializedChestSlots()
    {
        for(int i = 0; i < model.InitialUnlockedSlots; i++)
        {
            ChestSlotModel modelSlot = new ChestSlotModel(ChestSlotMode.Empty, GetUpgradeCost());
            ChestSlotController controllerSlot = new ChestSlotController(model.chestSlotPrefab, modelSlot, 
                view.GetSlotHolder(), walletService, chestService, commandInvolker, eventService);
            slots.Add(controllerSlot);
        }

        ChestSlotModel lockedModelSlot = new ChestSlotModel(ChestSlotMode.Locked, GetUpgradeCost());
        ChestSlotController lockedControllerSlot = new ChestSlotController(model.chestSlotPrefab, lockedModelSlot, 
            view.GetSlotHolder(), walletService, chestService, commandInvolker, eventService);
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

    public ChestSlotController GetLastSlot() => slots[slots.Count - 1];

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

    public void ActivateNextSlotSlot()
    {
        foreach (var slot in slots)
        {
            if(slot.GetSlotMode() == ChestSlotMode.Filled)
            {
                slot.SetSlotActive(true);
                break;
            }
        }
    }

    public void IncrementUpgrade()
    {
        model.UpgradeAmount++;
    }

    public int GetUpgradeCost()
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

    public ChestSlotController CreateSlot(ChestSlotModel chestModel)
    {
        return new ChestSlotController(model.chestSlotPrefab, chestModel, view.GetSlotHolder(), walletService, chestService, commandInvolker, eventService);
    }

    public void AddSlot(ChestSlotController chestSlot)
    {
        slots.Add(chestSlot);
    }

    public void RemoveSlot(ChestSlotController chestSlot)
    {
        slots.Remove(chestSlot);
    }
}
