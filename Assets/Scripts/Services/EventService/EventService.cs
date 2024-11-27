public class EventService
{
    public EventController<int> OnCoinWalletUpdated { get; set; }
    public EventController<int> OnCrystalsWalletUpdated { get; set; }

    public EventService()
    {
        OnCoinWalletUpdated = new EventController<int>();
        OnCrystalsWalletUpdated = new EventController<int>();
    }
}
