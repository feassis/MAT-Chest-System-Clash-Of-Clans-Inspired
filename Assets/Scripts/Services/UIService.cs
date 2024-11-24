using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIService : MonoBehaviour
{
    [SerializeField] private Transform uiRoot;
    [SerializeField] private Transform buyButtonRoot;

    [SerializeField] private ChestHolderView chestHolderViewPrefab;
    [SerializeField] private BuyChestButtonView buyChestViewPrefab;

    private ChestHolderController chestHolderController;

    private BuyChestButtonController coinBuyButtomController;
    private BuyChestButtonController crystalsBuyButtomController;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Init();
    }


    public void Init()
    {
        ChestHolderModel chestHolderModel = new ChestHolderModel();
        chestHolderController = new ChestHolderController(chestHolderViewPrefab, chestHolderModel, uiRoot);

        BuyChestButtonModel coinBuyButtomModel = new BuyChestButtonModel(ChestButtonType.Coins);
        coinBuyButtomController = new BuyChestButtonController(coinBuyButtomModel, buyChestViewPrefab, buyButtonRoot);

        BuyChestButtonModel crystalsBuyButtomModel = new BuyChestButtonModel(ChestButtonType.Crystals);
        crystalsBuyButtomController = new BuyChestButtonController(crystalsBuyButtomModel, buyChestViewPrefab, buyButtonRoot);
    }



}
