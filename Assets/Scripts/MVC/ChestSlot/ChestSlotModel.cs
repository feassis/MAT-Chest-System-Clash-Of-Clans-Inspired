public class ChestSlotModel
{
    public ChestSlotMode Mode;
    public int UpgradeCost;
    public ChestConfigScriptableObject ChestConfig;
    public ChestReward ChestReward;
    private ChestSlotController controller;

    public ChestSlotModel(ChestSlotMode mode, int upgradeCost)
    {
        Mode = mode;
        UpgradeCost = upgradeCost;
    }

    public void SetController(ChestSlotController controller)
    {
        this.controller = controller;
    }
}
