using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PopupMenu: MonoBehaviour, ClickableTileListener
{
    public Tilemap map;

    // Update is called once per frame
    
    public void DidClickTile(Vector3 worldPosition, Tilemap tilemap, bool isValid) {
        if (isValid) {
            this.showMenu(worldPosition);
        } else {
            this.hideMenu();
        }
    }

    void showMenu(Vector3 position) {
        this.gameObject.SetActive(true);
        this.transform.position = position;
    }

    void hideMenu() {
        this.gameObject.SetActive(false);
    }

    public void OnClickMenuItem(MenuItem menuItem) {
        this.hideMenu();
    }
}
