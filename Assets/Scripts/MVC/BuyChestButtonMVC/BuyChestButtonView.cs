using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyChestButtonView : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image currencyIcon;
    [SerializeField] private Sprite coinSprite;
    [SerializeField] private Sprite crystalSprite;
    [SerializeField] private TextMeshProUGUI currencyAmountText;

    [SerializeField] private Button buyButtonClicked;

    private BuyChestButtonController controller;



    public void SetController(BuyChestButtonController controller)
    {
        this.controller = controller;
        buyButtonClicked.onClick.AddListener(controller.BuyButtonClicked);
    }

        

    public void SetupIcon(Sprite icon)
    {
        iconImage.sprite = icon;
    }

    public void SetupCurrencyIcon(CurrencyType type)
    {
        switch (type)
        {
            case CurrencyType.None:
                break;
            case CurrencyType.Coins:
                currencyIcon.sprite = coinSprite;
                break;
            case CurrencyType.Crystals:
                currencyIcon.sprite = crystalSprite;
                break;
        }
    }

    public void SetCurrencyText(int amount)
    {
        currencyAmountText.text = amount.ToString();
    }
}
