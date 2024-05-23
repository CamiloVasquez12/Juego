using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public enum GrowthStage { Seed, Sprout, Plant, Flower }
    public GrowthStage currentStage = GrowthStage.Seed;

    public Sprite[] growthStagesSprites;
    public float timeToGrow = 45f;
    public int waterNeeded = 3;
    private int currentWater;
    private float growthTimer = 0f;
    public bool isGrowing = true;

    private SpriteRenderer spriteRenderer;

    private void Update()
    {
        if (isGrowing)
        {
            growthTimer += Time.deltaTime;

            if (growthTimer >= timeToGrow)
            {
                Grow();
                growthTimer = 0f;
            }
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing.");
        }
        currentWater = 0;
        StartCoroutine(Grow());
    }

    private IEnumerator Grow()
    {
        while (currentStage != GrowthStage.Flower)
        {
            if (currentWater >= waterNeeded)
            {
                yield return new WaitForSeconds(timeToGrow); // Espera aquí el tiempo necesario para crecer
                AdvanceGrowthStage();
            }
            else
            {
                yield return new WaitForSeconds(1); // Espera corta para reintentar
            }
        }
    }

    private void AdvanceGrowthStage()
    {
        if (currentStage < GrowthStage.Flower)
        {
            currentStage++;
            spriteRenderer.sprite = growthStagesSprites[(int)currentStage];
            Debug.Log("Growth stage advanced to: " + currentStage);
            currentWater = 0; // Reiniciar el agua necesaria después de crecer
        }
    }

    public void WaterCrop()
    {
        waterNeeded--;

        if (waterNeeded <= 0)
        {
            isGrowing = true;
        }
    }

    public void Harvest(Player player)
    {
        if (currentStage == GrowthStage.Flower)
        {
            // Añadir 3 Flower_Seeds al inventario
            for (int i = 0; i < 3; i++)
            {
                Item seedItem = GameManager.instance.itemManager.GetItemByName("Flower_Seeds");
                if (seedItem != null)
                {
                    player.inventory.Add("Backpack", seedItem);
                }
            }

            // Añadir 2 Flower al inventario
            for (int i = 0; i < 2; i++)
            {
                Item flowerItem = GameManager.instance.itemManager.GetItemByName("Flower");
                if (flowerItem != null)
                {
                    player.inventory.Add("Backpack", flowerItem);
                }
            }

            Destroy(gameObject);
        }
    }
}
