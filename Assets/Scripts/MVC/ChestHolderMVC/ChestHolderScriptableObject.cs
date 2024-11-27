using UnityEngine;

[CreateAssetMenu(fileName = "new ChestHolderConfig", menuName = "Configs/ChestHolderConfig")]
public class ChestHolderScriptableObject : ScriptableObject
{
    public int InitialUnlockedSlots = 2;
    public int InitialUnlockCost = 25;
    public int IncrementUnlockCost = 15;
    public ChestSlotView chestSlotPrefab;
}