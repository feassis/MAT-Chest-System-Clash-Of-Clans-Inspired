public class BuyChestButtonModel
{
    public CurrencyType ChestType { get; private set; }
    public ChestConfigScriptableObject ChestConfig { get; private set; }

    public int Cost { get; private set; }

    private BuyChestButtonController controller;

    public BuyChestButtonModel(CurrencyType chestType)
    {
        ChestType = chestType;
    }

    public void SetController(BuyChestButtonController controller) => this.controller = controller;

    public void SetChestConfig(ChestConfigScriptableObject chestConfig) => ChestConfig = chestConfig;

    public int SetCost(int cost) => Cost = cost;
}
