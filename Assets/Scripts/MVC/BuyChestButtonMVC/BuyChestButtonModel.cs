public class BuyChestButtonModel
{
    public ChestButtonType ChestType { get; private set; }

    private BuyChestButtonController controller;

    public BuyChestButtonModel(ChestButtonType chestType)
    {
        ChestType = chestType;
    }

    public void SetController(BuyChestButtonController controller) => this.controller = controller;

}
