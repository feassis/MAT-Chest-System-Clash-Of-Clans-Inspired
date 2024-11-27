using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentAmountText;
    [SerializeField] private Image Icon;
    [SerializeField] private Sprite CoinIcons;
    [SerializeField] private Sprite CrystalIcons;

    private WalletController controller;

    public void SetController(WalletController controller) => this.controller = controller; 

    public void SetCurrencyText(int currency)
    {
        currentAmountText.text = currency.ToString();
    }

    public void SetCurrencyIcon(CurrencyType type)
    {
        switch (type)
        {
            case CurrencyType.None:
                break;
            case CurrencyType.Coins:
                Icon.sprite = CoinIcons;
                break;
            case CurrencyType.Crystals:
                Icon.sprite = CrystalIcons;
                break;
        }
    }
}
