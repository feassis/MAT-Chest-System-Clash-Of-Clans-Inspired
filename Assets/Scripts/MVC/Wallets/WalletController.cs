using UnityEngine;

public class WalletController
{
    private WalletView view;
    private WalletModel model;

    private WalletService walletService;
    private EventService eventService;

    public WalletController(WalletView view, WalletModel model, Transform uiRoot, WalletService walletService, EventService eventService)
    {
        this.walletService = walletService;
        this.eventService = eventService;
        this.model = model;
        this.model.SetController(this);
        this.view = GameObject.Instantiate<WalletView>(view, uiRoot);
        this.view.SetController(this);
        this.view.SetCurrencyText(this.model.Type == CurrencyType.Coins ? walletService.Coins : walletService.Crystals);
        this.view.SetCurrencyIcon(this.model.Type);

        if(this.model.Type == CurrencyType.Coins)
        {
            eventService.OnCoinWalletUpdated.AddListener(UpdateCurrency);
        }
        else
        {
            eventService.OnCrystalsWalletUpdated.AddListener(UpdateCurrency);
        }
    }

    ~WalletController()
    {
        if (this.model.Type == CurrencyType.Coins)
        {
            eventService.OnCoinWalletUpdated.RemoveListener(UpdateCurrency);
        }
        else
        {
            eventService.OnCrystalsWalletUpdated.RemoveListener(UpdateCurrency);
        }
    }

    private void UpdateCurrency(int currencyAmount)
    {
        view.SetCurrencyText(currencyAmount);
    }
}
