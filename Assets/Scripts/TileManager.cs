using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;

    [SerializeField] private Tile hiddenInteravtableTile;
    [SerializeField] private Tile plowedTile;

    private Dictionary<Vector3Int, Crop> crops = new Dictionary<Vector3Int, Crop>();

    void Start()
    {
        foreach(var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);

            if(tile != null && tile.name == "Interactable_visible")
            {
                interactableMap.SetTile(position, hiddenInteravtableTile);
            }            
        }
    }

    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, plowedTile);
    }

    public string GetTileName(Vector3Int position)
    {
        if(interactableMap != null)
        {
            TileBase tile = interactableMap.GetTile(position);

            if(tile != null)
            {
                return tile.name;
            }
        }

        return null;
    }

    public void PlaceCrop(Vector3Int position, Crop crop)
    {
        if (!crops.ContainsKey(position))
        {
            crops.Add(position, crop);
        }
    }

    public Crop GetCropAtPosition(Vector3Int position)
    {
        crops.TryGetValue(position, out Crop crop);
        return crop;
    }
}
