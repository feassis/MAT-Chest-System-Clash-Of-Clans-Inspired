using StatePattern.Sound;

public class WalletService
{
    private EventService eventService;
    private SoundService soundService;
    public WalletService(int coins, int crystals)
    {
        Coins = coins;
        Crystals = crystals;
        
    }

    public void Init(EventService eventService, SoundService soundService)
    {
        this.eventService = eventService;
        this.soundService = soundService;
    }

    public int Coins { get; private set; }
    public int Crystals { get; private set; }

    public void AddCoins(int coins)
    {
        Coins += coins;
        this.eventService.OnCoinWalletUpdated.InvokeEvent(Coins);
        soundService.PlaySoundEffects(SoundType.COIN_PICK_UP);
    }

    public void SubtractCoins(int coins)
    {
        Coins -= coins;
        this.eventService.OnCoinWalletUpdated.InvokeEvent(Coins);
        soundService.PlaySoundEffects(SoundType.COIN_PICK_UP);
    }

    public void AddCrystals(int crystals)
    {
        Crystals += crystals;
        this.eventService.OnCrystalsWalletUpdated.InvokeEvent(Crystals);
        soundService.PlaySoundEffects(SoundType.COIN_PICK_UP);
    }

    public void SubtractCrystals(int crystals)
    {
        Crystals -= crystals;
        this.eventService.OnCrystalsWalletUpdated.InvokeEvent(Crystals);
        soundService.PlaySoundEffects(SoundType.COIN_PICK_UP);
    }
}