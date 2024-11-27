using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlotView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject timerBG;
    [SerializeField] private GameObject timerHolder;
    [SerializeField] private TextMeshProUGUI crystalCostText;
    [SerializeField] private Image icon;
    [SerializeField] private Image crystalIcon;
    [SerializeField] private GameObject lockedIcon;
    [SerializeField] private Button button;

    private ChestSlotController controller;

    private void Update()
    {
        controller.UpdateTimer();
    }

    public void SetController(ChestSlotController controller)
    {
        this.controller = controller;
        button.onClick.AddListener(controller.ChestSlotButtonClicked);
    } 

    public void UpdateMode(ChestSlotMode mode, int crystalAmount)
    {
        crystalCostText.text = crystalAmount.ToString();

        switch (mode)
        {
            case ChestSlotMode.Empty:
                icon.gameObject.SetActive(false);
                timerHolder.gameObject.SetActive(false);
                lockedIcon.gameObject.SetActive(false);
                ToggleCrystalAmount(false);
                break;
            case ChestSlotMode.Locked:
                icon.gameObject.SetActive(false);
                timerHolder.gameObject.SetActive(false);
                lockedIcon.gameObject.SetActive(true);
                ToggleCrystalAmount(true);
                break;
            case ChestSlotMode.Filled:
                icon.gameObject.SetActive(true);
                timerHolder.gameObject.SetActive(true);
                break;
        }
    }

    public void ShowTimerIsActive(bool isActive)
    {
        timerBG.gameObject.SetActive(isActive);
    }

    public void ToggleCrystalAmount(bool isActive)
    {
        crystalCostText.gameObject.SetActive(isActive);
        crystalIcon.gameObject.SetActive(isActive);
    }

    public void SetChestIcon(Sprite chestIcon)
    {
        icon.sprite = chestIcon;
    }

    public void UpdateCostText(int cost)
    {
        crystalCostText.text = cost.ToString();
    }

    public void SetTimerText(float timer)
    {
        if(timer > 0)
        {
            timerText.text = TimerFormatter.FormatTime(timer);
        }
        else
        {
            timerHolder.gameObject.SetActive(false);
        }
    }
}
