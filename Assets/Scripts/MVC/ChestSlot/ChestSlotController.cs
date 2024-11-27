using UnityEngine;

public class ChestSlotController
{
    private ChestSlotView view;
    private ChestSlotModel model;

    private float chestlockedTimer = 0;

    public ChestSlotController(ChestSlotView view, ChestSlotModel model, Transform uiRoot)
    {
        this.view = GameObject.Instantiate<ChestSlotView>(view, uiRoot);
        this.view.SetController(this);
        this.model = model;
        this.model.SetController(this);
        ChangeMode(model.Mode);
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
                break;
        }

        UpdateMode();
    }

    public ChestSlotMode GetSlotMode() => model.Mode;
    public ChestReward GetReward() => model.ChestReward;

    private int GetPriceToOpenChest()
    {
        return Mathf.CeilToInt(chestlockedTimer / model.ChestConfig.CrystalIncrementTime);
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

    public void EmptyChest()
    {
        model.ChestConfig = null;
        model.ChestReward = null;

        ChangeMode(ChestSlotMode.Empty);
    }


    public void ChestSlotButtonClicked()
    {

    }
}
