using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BuyChestButtonView : MonoBehaviour
{
    private BuyChestButtonController controller;

    public void SetController(BuyChestButtonController controller) => this.controller = controller; 
}
