using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InventoryManager inventory;
    private TileManager tileManager;
    public SeedPlanting seedPlanting;
    private void Start()
    {
        tileManager = GameManager.instance.tileManager;
        if (seedPlanting == null)
        {
            seedPlanting = FindObjectOfType<SeedPlanting>();
        }
    }
    private void Awake()
    {
        inventory = GetComponent<InventoryManager>();        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (tileManager != null)
            {
                Vector3Int tilePosition = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
                string tileName = tileManager.GetTileName(tilePosition);

                if (!string.IsNullOrWhiteSpace(tileName))
                {
                    if (tileName == "Interactable" && inventory.toolbar.selectedSlot.itemName == "Hoe")
                    {
                        tileManager.SetInteracted(tilePosition);
                    }
                    else if (tileName == "Summer_Plowed" && inventory.toolbar.selectedSlot.itemName == "Flower_Seeds")
                    {
                        PlantCrop(tilePosition);  // Aquí pasas directamente el Vector3Int
                        Debug.Log("Crop has been planted");
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (inventory.toolbar.selectedSlot.itemName == "Sprinkler")
            {
                Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
                WaterCrop(position);
                Debug.Log("Crop has been watered");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
            HarvestCrop(position);
        }
    }

    public void DropItem(Item item)
    {
        Vector2 spawnLocation = transform.position;
        Vector2 spawnOffset = Random.insideUnitCircle * 1.25f;
        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);
        droppedItem.rb2d.AddForce(spawnOffset * 2f, ForceMode2D.Impulse);
    }

    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }

    public void PlantCrop(Vector3Int tilePosition)
    {
        if (seedPlanting != null)
        {
            seedPlanting.PlantSeed(tilePosition);
        }
        else
        {
            Debug.LogError("SeedPlanting script is not assigned.");
        }
    }

    public void WaterCrop(Vector3Int position)
    {
        Vector2 tileCenter = new Vector2(position.x + 0.5f, position.y + 0.5f);
        Collider2D collider = Physics2D.OverlapPoint(tileCenter);

        if (collider != null)
        {
            Crop crop = collider.GetComponent<Crop>();
            if (crop != null && crop.isGrowing && crop.waterNeeded > 0)
            {
                crop.WaterCrop();
                Debug.Log("Watered the crop at position: " + position);
            }
        }
    }

    public void HarvestCrop(Vector3Int position)
    {
        Crop crop = tileManager.GetCropAtPosition(position);
        if (crop != null)
        {
            crop.Harvest(this);
        }
    }
}
