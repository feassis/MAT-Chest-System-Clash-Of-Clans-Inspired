public class UnlockChestCommand : ICommand
{ 
    private ChestSlotController chestSlotController;
    private int crystalCost;
    private WalletService walletService;
    private float remainingTimer;

    private bool succeded;

    public UnlockChestCommand(ChestSlotController chestSlotController, int crystalCost, 
        WalletService walletService, float remainingTimer)
    {
        this.chestSlotController = chestSlotController;
        this.crystalCost = crystalCost;
        this.walletService = walletService;
        this.remainingTimer = remainingTimer;

        succeded = CanUnlockChest();
    }

    public void Execute()
    {
        if(!succeded)
        {
            return;
        }

        walletService.SubtractCrystals(crystalCost);
        chestSlotController.ChangeMode(ChestSlotMode.Ready);
    }

    public void Undo()
    {
        if (!succeded)
        {
            return;
        }

        walletService.AddCrystals(crystalCost);
        chestSlotController.SetTimer(remainingTimer);
        chestSlotController.ChangeMode(ChestSlotMode.Filled);
    }

    private bool CanUnlockChest()
    {
        return walletService.Crystals >= crystalCost;
    }
}