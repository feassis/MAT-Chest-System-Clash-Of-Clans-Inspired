public class OpenChestCommand : ICommand
{
    private ChestConfigScriptableObject chestConfig;
    private ChestReward chestReward;
    private ChestSlotController chestSlotController;
    private WalletService walletService;

    public OpenChestCommand(ChestConfigScriptableObject chestConfig, ChestReward chestReward, ChestSlotController chestSlotController, WalletService walletService)
    {
        this.chestConfig = chestConfig;
        this.chestReward = chestReward;
        this.chestSlotController = chestSlotController;
        this.walletService = walletService;
    }

    public void Execute()
    {
        chestSlotController.EmptyChest();
        walletService.AddCoins(chestReward.CoinAmount);
        walletService.AddCrystals(chestReward.CrystalAmount);
    }

    public void Undo()
    {
        walletService.SubtractCoins(chestReward.CoinAmount);
        walletService.SubtractCrystals(chestReward.CrystalAmount);
        chestSlotController.SetChest(chestConfig, chestReward);
    }
}