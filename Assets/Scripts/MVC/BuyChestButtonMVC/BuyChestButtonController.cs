using UnityEngine;

public class BuyChestButtonController
{
    private BuyChestButtonModel model;
    private BuyChestButtonView view;


    public BuyChestButtonController(BuyChestButtonModel model, BuyChestButtonView view, Transform uiRoot)
    {
        this.model = model;
        this.model.SetController(this);
        this.view = GameObject.Instantiate<BuyChestButtonView>(view, uiRoot);
        this.view.SetController(this);

    }
}
