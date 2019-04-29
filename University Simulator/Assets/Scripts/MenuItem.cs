using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
public class MenuItem: MonoBehaviour {
    public Tilemap map;

    public void OnLeftClick(ClickManager.GetFrontmostRaycastHitResult result) {
		Transform box = this.transform.parent;
		PopupMenu menu = box.GetComponentInParent<PopupMenu>();
		menu.OnClickMenuItem(this);
	}
}
