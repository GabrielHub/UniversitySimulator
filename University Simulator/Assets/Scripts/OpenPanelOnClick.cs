using UnityEngine;

public class OpenPanelOnClick : MonoBehaviour {
	// public GameObject target;

	public void OnClick(GameObject target) {
		if (target.activeSelf) {
			target.SetActive(false);
		}
		else {
			target.SetActive(true);
		}
	}
}
