using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface ClickableTileListener {
	void DidClickTile(Vector3 worldPosition, Tilemap tilemap, bool isValid);
}

public class ClickableTile: MonoBehaviour {
    public MonoBehaviour _target;
    ClickableTileListener target {
        get {
            return (ClickableTileListener) _target;
        }
    }
	public Tilemap map;

    void OnLeftClick(ClickManager.GetFrontmostRaycastHitResult result)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3Int cellPosition = this.map.WorldToCell(mousePosition);
        mousePosition = this.map.CellToWorld(cellPosition); // center on cell center
        bool isValid = false;
        if (this.map.HasTile(cellPosition)) {
            isValid = true;
        }
        this.target.DidClickTile(mousePosition, this.map, isValid);
    }
}
