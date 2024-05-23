using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventory> inventoryByName = new Dictionary<string, Inventory>();

    [Header("Backpack")]
    public Inventory backpack;
    public int backpackSlotCount;

    [Header("Toolbar")]
    public Inventory toolbar;
    public int toolbarSlotCount;

    private void Awake()
    {
        backpack = new Inventory(backpackSlotCount);
        toolbar = new Inventory(toolbarSlotCount);

        inventoryByName.Add("Backpack", backpack);
        inventoryByName.Add("Toolbar", toolbar);
    }

    public void Add(string inventoryName, Item item)
    {
        if(inventoryByName.ContainsKey(inventoryName))
        {
            inventoryByName[inventoryName].Add(item);
        }
    }

    public Inventory GetInventoryByName(string inventoryName)
    {
        if(inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName];
        }
        return null;
    }

    public Item GetItemFromSlot(string inventoryName, int slotIndex)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            Inventory inventory = inventoryByName[inventoryName];
            Inventory.Slot slot = inventory.slots[slotIndex];
            if (!slot.IsEmpty)
            {
                return GameManager.instance.itemManager.GetItemByName(slot.itemName);
            }
        }
        return null;
    }

    public void Remove(string inventoryName, Item item)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            Inventory inventory = inventoryByName[inventoryName];
            for (int i = 0; i < inventory.slots.Count; i++)
            {
                if (inventory.slots[i].itemName == item.data.itemName)
                {
                    inventory.Remove(i);
                    return;
                }
            }
        }
    }
}
