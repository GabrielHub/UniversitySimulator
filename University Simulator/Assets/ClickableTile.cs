using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface ClickableTileListener {
	void DidClickTile(Vector3 worldPosition, Tilemap tilemap, bool isValid);
}

public class ClickableTile: MonoBehaviour {
    public ClickableTileListener target;
	Tilemap map;

	private void Start() {
		this.map = this.GetComponent<Tilemap>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3Int cellPosition = this.map.WorldToCell(mousePosition);
			bool isValid = false;
            if (this.map.HasTile(cellPosition)) {
                isValid = true;
            }
			this.target.DidClickTile(mousePosition, this.map, isValid);
        }
    }
}
