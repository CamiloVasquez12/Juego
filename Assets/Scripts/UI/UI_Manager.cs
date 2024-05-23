using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public Dictionary<string, Inventory_UI> inventoryUIByName = new Dictionary<string, Inventory_UI>();

    public GameObject inventoryPanel;

    public List<Inventory_UI> inventoryUIs;

    public static Slots_UI draggedSlot;
    public static Image draggedIcon;
    public static bool dragSingle;


    private void Awake()
    {
        Initialize();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventoryUI();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3Int position = new Vector3Int((int)GameManager.instance.player.transform.position.x, (int)GameManager.instance.player.transform.position.y, 0);
            GameManager.instance.player.WaterCrop(position);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3Int position = new Vector3Int((int)GameManager.instance.player.transform.position.x, (int)GameManager.instance.player.transform.position.y, 0);
            GameManager.instance.player.HarvestCrop(position);
        }
    }
    public void ToggleInventoryUI()
    {
        if (inventoryPanel != null)
        {
            if (!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
                RefreshInventoryUI("Backpack");
            }
            else
            {
                inventoryPanel.SetActive(false);
            }
        }
    }

    public void RefreshInventoryUI(string inventoryName)
    {
        if(inventoryUIByName.ContainsKey(inventoryName))
        {
            inventoryUIByName[inventoryName].Refresh();
        }
    }

    public void RefreshAll()
    {
        foreach(KeyValuePair<string, Inventory_UI> keyValuePair in inventoryUIByName)
        {
            keyValuePair.Value.Refresh();
        }
    }

    public Inventory_UI GetInventoryUI(string inventoryName)
    {
        if(inventoryUIByName.ContainsKey(inventoryName))
        {
            return inventoryUIByName[inventoryName];
        }

        Debug.LogWarning("There is not inventory ui for " + inventoryName);
        return null;
    }

    void Initialize()
    {
        foreach(Inventory_UI ui in inventoryUIs)
        {
            inventoryUIByName.Add(ui.inventoryName, ui);
        }
    }
}
