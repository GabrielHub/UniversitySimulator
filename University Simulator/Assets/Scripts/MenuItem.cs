using UnityEngine;
using UnityEngine.Tilemaps;

public class MenuItem: MonoBehaviour {
    public Tilemap map;
	public PopupMenu menu;
	public Tile item;
	public Building building;

	public void OnClick() {
		Debug.Log("onleftclick");
		menu.OnClickMenuItem(this);
	}
}
