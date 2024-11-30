using UnityEngine;

public class ChestSlotController
{
    private ChestSlotView view;
    private ChestSlotModel model;

    private float chestlockedTimer = 0;
    private WalletService walletService;
    private ChestService chestService;
    private CommandInvolker commandInvolker;
    private EventService eventService;

    public ChestSlotController(ChestSlotView view, ChestSlotModel model, Transform uiRoot, 
        WalletService walletService, ChestService chestService, CommandInvolker commandInvolker, EventService eventService)
    {
        this.view = GameObject.Instantiate<ChestSlotView>(view, uiRoot);
        this.view.SetController(this);
        this.model = model;
        this.model.SetController(this);
        ChangeMode(model.Mode);
        this.walletService = walletService;
        this.chestService = chestService;
        this.commandInvolker = commandInvolker;
        this.eventService = eventService;
    }

    public void ChangeMode(ChestSlotMode mode)
    {
        model.Mode = mode;

        switch (mode)
        {
            case ChestSlotMode.Empty:
                break;
            case ChestSlotMode.Locked:
                break;
            case ChestSlotMode.Filled:
                chestlockedTimer = model.ChestConfig.Timer;
                view.ShowTimerIsActive(model.GetIsActive());
                view.SetTimerText(chestlockedTimer);
                break;
            case ChestSlotMode.Ready:
                SetSlotActive(false);
                eventService.OnSlotReady.InvokeEvent();
                view.UpdateMode(ChestSlotMode.Ready, 0);
                break;
        }

        UpdateMode();
    }

    public ChestSlotMode GetSlotMode() => model.Mode;
    public ChestReward GetReward() => model.ChestReward;

    public void SetSlotActive(bool active)
    {
        model.SetIsActive(active);
        view.ShowTimerIsActive(active);
        view.UpdateCostText(GetPriceToOpenChest());
        view.ToggleCrystalAmount(active);
    }

    private int GetPriceToOpenChest()
    {
        return Mathf.CeilToInt(chestlockedTimer / model.ChestConfig.CrystalIncrementTime);
    }

    public void Destroy()
    {
        view.DestroyView();
    }

    public void UpdateTimer()
    {
        if(!IsSlotActive())
        {
            return;
        }

        chestlockedTimer = Mathf.Max(0, chestlockedTimer - Time.deltaTime);
        view.SetTimerText(chestlockedTimer);
        view.UpdateCostText(GetPriceToOpenChest());

        if(chestlockedTimer == 0)
        {
            ChangeMode(ChestSlotMode.Ready);
        }
    }

    public void UpdateMode()
    {
        switch (model.Mode)
        {
            case ChestSlotMode.Empty:
                view.UpdateMode(model.Mode, 0);
                break;
            case ChestSlotMode.Locked:
                view.UpdateMode(model.Mode, model.UpgradeCost);
                break;
            case ChestSlotMode.Filled:
                view.UpdateMode(model.Mode, GetPriceToOpenChest());
                break;
        }
    }

    public void AddChest(ChestReward chestReward, ChestConfigScriptableObject chestConfig)
    {
        model.ChestReward = chestReward;
        model.ChestConfig = chestConfig;
        view.SetChestIcon(model.ChestConfig.Sprite);

        ChangeMode(ChestSlotMode.Filled);
    }

    public bool IsSlotActive() => model.GetIsActive();

    public void EmptyChest()
    {
        model.ChestConfig = null;
        model.ChestReward = null;

        ChangeMode(ChestSlotMode.Empty);
    }

    public void SetChest(ChestConfigScriptableObject chestConfig, ChestReward reward)
    {
        model.ChestConfig = chestConfig;
        model.ChestReward = reward; 

        ChangeMode(ChestSlotMode.Filled);
    }

    public void SetTimer(float timer) => chestlockedTimer = timer;

    public void ChestSlotButtonClicked()
    {
        switch (model.Mode)
        {
            case ChestSlotMode.Empty:

                break;
            case ChestSlotMode.Locked:
                AddChestSlotCommand chestSlotCommand = new AddChestSlotCommand(chestService, walletService, model.UpgradeCost);

                commandInvolker.RegisterCommand(chestSlotCommand);
                commandInvolker.ProcessCommand(chestSlotCommand);
                break;
            case ChestSlotMode.Filled:
                if (!IsSlotActive())
                {
                    return;
                }

                UnlockChestCommand unlockChestCommand = new UnlockChestCommand(this, GetPriceToOpenChest(), walletService, chestlockedTimer);

                commandInvolker.RegisterCommand(unlockChestCommand);
                commandInvolker.ProcessCommand(unlockChestCommand);
                
                break;
            case ChestSlotMode.Ready:
                OpenChestCommand openChestCommand = new OpenChestCommand(model.ChestConfig, model.ChestReward, this, walletService);

                commandInvolker.RegisterCommand(openChestCommand);
                commandInvolker.ProcessCommand(openChestCommand);
                break;
        }
    }
}
