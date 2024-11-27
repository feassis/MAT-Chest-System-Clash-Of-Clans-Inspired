using UnityEngine;

public class BuyChestButtonController
{
    private BuyChestButtonModel model;
    private BuyChestButtonView view;

    private ChestService chestService;
    private WalletService walletService;
    private CommandInvolker commandInvolker;

    public BuyChestButtonController(BuyChestButtonModel model, BuyChestButtonView view, Transform uiRoot, 
        ChestService chestService, WalletService walletService, CommandInvolker commandInvolker)
    {
        this.model = model;
        this.model.SetController(this);
        this.view = GameObject.Instantiate<BuyChestButtonView>(view, uiRoot);
        this.view.SetController(this);
        this.chestService = chestService;
        this.view.SetupCurrencyIcon(model.ChestType);
        SetupChest();
        this.walletService = walletService;
        this.commandInvolker = commandInvolker;
    }

    private void SetupChest()
    {
        var chestConfig = chestService.GetRandomChest();
        model.SetChestConfig(chestConfig);

        var correctCost = model.ChestType == CurrencyType.Coins ? model.ChestConfig.GetRandomCoinCost() : model.ChestConfig.GetRandomCristalCost();
        model.SetCost(correctCost);
        view.SetCurrencyText(model.Cost);
        view.SetupIcon(model.ChestConfig.Sprite);
    }

    public void BuyButtonClicked()
    {
        AddChestCommand addChestCommand = new AddChestCommand(model.ChestConfig, walletService, chestService, model.ChestType, model.Cost);

        if(addChestCommand.Succeded)
        {
            SetupChest();
        }

        commandInvolker.RegisterCommand(addChestCommand);
        commandInvolker.ProcessCommand(addChestCommand);
    }
}
