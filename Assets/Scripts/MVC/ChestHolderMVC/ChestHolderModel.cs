public class ChestHolderModel
{
    private ChestHolderController controller;

    public int InitialUnlockedSlots = 2;
    public int InitialUnlockCost = 25;
    public int IncrementUnlockCost = 15;
    public int UpgradeAmount = 0;
    public ChestSlotView chestSlotPrefab;
   

    public ChestHolderModel(int initialUnlockedSlots, int initialUnlockCost, int incrementUnlockCost, ChestSlotView chestSlotPrefab)
    {
        InitialUnlockedSlots = initialUnlockedSlots;
        InitialUnlockCost = initialUnlockCost;
        IncrementUnlockCost = incrementUnlockCost;
        this.chestSlotPrefab = chestSlotPrefab;
    }

    public void SetController (ChestHolderController controller)
    {
        this.controller = controller;
    }
}
