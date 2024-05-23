using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanting : MonoBehaviour
{
    public GameObject flowerCropPrefab;

    public void PlantSeed(Vector3Int tilePosition)
    {
        if (flowerCropPrefab == null)
        {
            Debug.LogError("Flower crop prefab is not assigned.");
            return;
        }

        // Convertir la posición del tile a una posición centrada en el tile
        Vector3 positionToPlant = new Vector3(tilePosition.x + 0.5f, tilePosition.y + 0.5f, 0);
        GameObject crop = Instantiate(flowerCropPrefab, positionToPlant, Quaternion.identity);
        Debug.Log("Seed planted at: " + positionToPlant);
    }
}
