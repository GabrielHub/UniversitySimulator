using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
public class MenuItem: MonoBehaviour {
    public Tilemap map;
	public PopupMenu menu;
	public Tile item;

    // public void OnLeftClick(ClickManager.GetFrontmostRaycastHitResult result) {
	// 	// Transform box = this.transform.parent;
	// 	// PopupMenu menu = box.GetComponentInParent<PopupMenu>();
	// 	Debug.Log("onleftclick");
	// 	menu.OnClickMenuItem(this);
	// }
}
