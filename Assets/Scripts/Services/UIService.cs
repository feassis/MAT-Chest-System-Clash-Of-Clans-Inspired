using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIService : MonoBehaviour
{
    [SerializeField] private Transform uiRoot;
    [SerializeField] private Transform buyButtonRoot;
    [SerializeField] private Transform walletRoot;

    [SerializeField] private ChestHolderView chestHolderViewPrefab;
    [SerializeField] private BuyChestButtonView buyChestViewPrefab;
    [SerializeField] private WalletView walletViewPrefab;

    [SerializeField] private Button gatherButton;
    [SerializeField] private int minCoins = 5;
    [SerializeField] private int minCrystals = 1;
    [SerializeField] private int maxCoins = 15;
    [SerializeField] private int maxCrystals = 3;
    [SerializeField, Range(0f, 1f)] private float crystalChance = 0.1f;

    [SerializeField] private Button undoButton;

    [SerializeField] private ChestHolderScriptableObject chestHolderScriptableObject;

    private ChestHolderController chestHolderController;

    private BuyChestButtonController coinBuyButtomController;
    private BuyChestButtonController crystalsBuyButtomController;

    private WalletController coinWallet;
    private WalletController crystalWallet;

    private ChestService chestService;
    private EventService eventService;
    private WalletService walletService;
    private CommandInvolker commandInvolker;

    public void Init(ChestService chestService, EventService eventService, WalletService walletService, CommandInvolker commandInvolker)
    {
        this.walletService = walletService;
        this.eventService = eventService;
        this.chestService = chestService;
        this.commandInvolker = commandInvolker;
        ChestHolderModel chestHolderModel = new ChestHolderModel(chestHolderScriptableObject.InitialUnlockedSlots, 
            chestHolderScriptableObject.InitialUnlockCost, chestHolderScriptableObject.IncrementUnlockCost, chestHolderScriptableObject.chestSlotPrefab);
        chestHolderController = new ChestHolderController(chestHolderViewPrefab, chestHolderModel, uiRoot);

        BuyChestButtonModel coinBuyButtomModel = new BuyChestButtonModel(CurrencyType.Coins);
        coinBuyButtomController = new BuyChestButtonController(coinBuyButtomModel, buyChestViewPrefab, buyButtonRoot,
            chestService, walletService, commandInvolker);

        BuyChestButtonModel crystalsBuyButtomModel = new BuyChestButtonModel(CurrencyType.Crystals);
        crystalsBuyButtomController = new BuyChestButtonController(crystalsBuyButtomModel, buyChestViewPrefab, buyButtonRoot, 
            chestService, walletService, commandInvolker);

        WalletModel coinModel = new WalletModel(CurrencyType.Coins);
        coinWallet = new WalletController(walletViewPrefab, coinModel, walletRoot, walletService, eventService);

        WalletModel crystalModel = new WalletModel(CurrencyType.Crystals);
        crystalWallet = new WalletController(walletViewPrefab, crystalModel, walletRoot, walletService, eventService);


        gatherButton.onClick.AddListener(OnGatherButtonClicked);
        undoButton.onClick.AddListener(OnUndoButton);
    }

    public ChestHolderController GetChestHolder() => chestHolderController;

    private void OnUndoButton()
    {
        commandInvolker.Undo();
    }

    private void OnGatherButtonClicked()
    {
        float sortedChance = Random.Range(0f, 1f);
        int reward = 0;
        AddCurrencyCommand command;

        if (sortedChance > crystalChance)
        {
            reward = Random.Range(minCoins, maxCoins + 1);
            command = new AddCurrencyCommand(reward, CurrencyType.Coins, walletService);
        }
        else
        {
            reward = Random.Range(minCrystals, minCrystals + 1);
            command = new AddCurrencyCommand(reward, CurrencyType.Crystals, walletService);
        }

        commandInvolker.RegisterCommand(command);
        commandInvolker.ProcessCommand(command);
    }
}
