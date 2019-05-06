using UnityEngine;

public class OpenPanelOnClick: MonoBehaviour {
	public GameObject target;

	public void OnClick() {
		if (this.target.activeSelf) {
			this.target.SetActive(false);
		} else {
			this.target.SetActive(true);
		}
	}
}
