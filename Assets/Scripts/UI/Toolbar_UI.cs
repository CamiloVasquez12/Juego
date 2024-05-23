using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar_UI : MonoBehaviour
{
    [SerializeField] private List<Slots_UI> toolbarSlots = new List<Slots_UI>();

    private Slots_UI selectedSlot;

    private void Start()
    {
        SelectSlots(0);
    }

    private void Update()
    {
        CheckAlphaNumericKeys();
    }

    public void SelectSlots(Slots_UI slot)
    {
        SelectSlots(slot.slotID);
    }

    public void SelectSlots(int index)
    {
        if(toolbarSlots.Count == 9)
        {
            if(selectedSlot != null)
            {
                selectedSlot.SetHighlight(false);
            }
            selectedSlot = toolbarSlots[index];
            selectedSlot.SetHighlight(true);

            GameManager.instance.player.inventory.toolbar.SelectSlot(index);
        }
    }

    private void CheckAlphaNumericKeys()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectSlots(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectSlots(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectSlots(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectSlots(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectSlots(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectSlots(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectSlots(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            SelectSlots(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SelectSlots(8);
        }
    }
}
