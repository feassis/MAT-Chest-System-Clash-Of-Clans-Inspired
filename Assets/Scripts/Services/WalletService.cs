public class WalletService
{
    private EventService eventService;
    public WalletService(int coins, int crystals, EventService eventService)
    {
        Coins = coins;
        Crystals = crystals;
        this.eventService = eventService;
    }

    public int Coins { get; private set; }
    public int Crystals { get; private set; }

    public void AddCoins(int coins)
    {
        Coins += coins;
        this.eventService.OnCoinWalletUpdated.InvokeEvent(Coins);
    }

    public void SubtractCoins(int coins)
    {
        Coins -= coins;
        this.eventService.OnCoinWalletUpdated.InvokeEvent(Coins);
    }

    public void AddCrystals(int crystals)
    {
        Crystals += crystals;
        this.eventService.OnCrystalsWalletUpdated.InvokeEvent(Crystals);
    }

    public void SubtractCrystals(int crystals)
    {
        Crystals -= crystals;
        this.eventService.OnCrystalsWalletUpdated.InvokeEvent(Crystals);
    }
}