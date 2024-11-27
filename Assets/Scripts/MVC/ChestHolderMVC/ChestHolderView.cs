using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHolderView : MonoBehaviour
{
    [SerializeField] private Transform slotHolder;
    private ChestHolderController controller;
    
    public void SetController(ChestHolderController controller)
    {
        this.controller = controller;
    }

    public Transform GetSlotHolder()
    {
        return slotHolder;
    }
}
