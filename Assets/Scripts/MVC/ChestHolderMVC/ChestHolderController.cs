using UnityEngine;

public class ChestHolderController
{
    private ChestHolderView view;
    private ChestHolderModel model;

    public ChestHolderController(ChestHolderView view, ChestHolderModel model, Transform uiRoot)
    {
        this.view = GameObject.Instantiate<ChestHolderView>(view, uiRoot);
        this.view.SetController(this);
        this.model = model;
        this.model.SetController(this);
    }
}
