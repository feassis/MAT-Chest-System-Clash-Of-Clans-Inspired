public class AddChestSlotCommand : ICommand
{
    private ChestService chestService;
    private WalletService walletService;
    private int cost;

    private bool succeded;
    private ChestSlotController chestSlotController;

    public AddChestSlotCommand(ChestService chestService, WalletService walletService, int cost)
    {
        this.chestService = chestService;
        this.walletService = walletService;
        this.cost = cost;

        succeded = CanAddChestSlot();
    }

    public void Execute()
    {
        if (!succeded)
        {
            return;
        }

        var chestHolder = chestService.GetChestHolder();
        chestHolder.IncrementUpgrade();
        walletService.SubtractCrystals(cost);

        var chestSlotModel = new ChestSlotModel(ChestSlotMode.Locked, chestHolder.GetUpgradeCost());
        chestHolder.GetLastSlot().ChangeMode(ChestSlotMode.Empty);
        chestSlotController = chestService.GetChestHolder().CreateSlot(chestSlotModel);

        chestHolder.AddSlot(chestSlotController);
    }

    public void Undo()
    {
        if (!succeded)
        {
            return;
        }

        chestService.GetChestHolder().RemoveSlot(chestSlotController);

        chestSlotController.Destroy();
        walletService.AddCrystals(cost);
    }

    private bool CanAddChestSlot()
    {
        if(walletService.Crystals >= cost)
        {
            return true;
        }

        return false;
    }
}