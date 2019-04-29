using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PopupMenu: MonoBehaviour, ClickableTileListener
{
    public Tilemap map;
    public GameObject menuObject;

    // Update is called once per frame
    
    public void DidClickTile(Vector3 worldPosition, Tilemap tilemap, bool isValid) {
        if (isValid) {
            this.menuObject.SetActive(true);
            this.menuObject.transform.position = worldPosition;
        } else {
            this.menuObject.SetActive(false);
        }
    }

    void OnClickMenuItem() {

    }
}
