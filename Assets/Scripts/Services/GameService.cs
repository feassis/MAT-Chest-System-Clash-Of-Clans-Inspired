using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameService : MonoBehaviour
{
    [SerializeField] private UIService uiService;

    [SerializeField] private List<ChestConfigScriptableObject> chestConfigs = new List<ChestConfigScriptableObject>();

    private ChestService chestService;
    private CommandInvolker commandInvolker;
    private EventService eventService;
    private WalletService walletService;

    private void Start()
    {
        CreateServices();
        InitializeServices();
    }

    private void CreateServices()
    {
        chestService = new ChestService();
        commandInvolker = new CommandInvolker();
        eventService = new EventService();
        walletService = new WalletService(0, 0, eventService);
    }

    private void InitializeServices()
    {
        chestService.Init(chestConfigs, uiService);

        uiService.Init(chestService, eventService, walletService, commandInvolker);
    }
}
