public class AddChestCommand : ICommand
{
    private ChestReward chestReward;
    private ChestConfigScriptableObject chestConfig;
    private WalletService walletService;
    private ChestService chestService;
    private CurrencyType paymentType;
    private int cost;

    public bool Succeded { get; private set; }

    public AddChestCommand(ChestConfigScriptableObject chestConfig, WalletService walletService,
        ChestService chestService, CurrencyType paymentType, int cost)
    {
        this.chestConfig = chestConfig;
        this.walletService = walletService;
        this.chestService = chestService;
        this.paymentType = paymentType;

        this.cost = cost;

        Succeded = CanAddChest();
    }

    public void Execute()
    {
        if(!Succeded)
        {
            return;
        }

        if(CurrencyType.Coins == this.paymentType)
        {
            walletService.SubtractCoins(cost);
        }
        else
        {
            walletService.SubtractCrystals(cost);
        }

        chestReward = new ChestReward(chestConfig.GetRandomCoinReward(), chestConfig.GetRandomCristalReward());
        chestService.AddChest(chestReward, chestConfig);
    }

    public void Undo()
    {
        if (!Succeded)
        {
            return;
        }

        if (CurrencyType.Coins == this.paymentType)
        {
            walletService.AddCoins(cost);
        }
        else
        {
            walletService.AddCrystals(cost);
        }

        chestService.RemoveChest(chestReward);
    }

    private bool CanAddChest()
    {
        int amountOnWallet = paymentType == CurrencyType.Coins ? walletService.Coins : walletService.Crystals;

        return amountOnWallet >= cost && chestService.CanAddAChest();
    }
}
