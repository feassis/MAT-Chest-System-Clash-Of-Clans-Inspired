public class WalletModel
{
    public CurrencyType Type { get; private set; }
    private WalletController controller;

    public WalletModel(CurrencyType type)
    {
        Type = type;
    }

    public void SetController(WalletController controller) => this.controller = controller;
}
