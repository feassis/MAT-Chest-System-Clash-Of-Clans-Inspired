public class AddCurrencyCommand : ICommand
{
    private int currencyAmount;
    private CurrencyType currencyType;
    private WalletService walletService;

    public AddCurrencyCommand(int currencyAmount, CurrencyType currencyType, WalletService walletService)
    {
        this.currencyAmount = currencyAmount;
        this.currencyType = currencyType;
        this.walletService = walletService;
    }

    public void Execute()
    {
        switch (currencyType)
        {
            case CurrencyType.None:
                break;
            case CurrencyType.Coins:
                walletService.AddCoins(currencyAmount);
                break;
            case CurrencyType.Crystals:
                walletService.AddCrystals(currencyAmount);
                break;
        }
    }

    public void Undo()
    {
        switch (currencyType)
        {
            case CurrencyType.None:
                break;
            case CurrencyType.Coins:
                walletService.SubtractCoins(currencyAmount);
                break;
            case CurrencyType.Crystals:
                walletService.SubtractCrystals(currencyAmount);
                break;
        }
    }
}
