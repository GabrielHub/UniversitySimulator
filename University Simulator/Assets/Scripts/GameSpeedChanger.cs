using UnityEngine;
using UnityEngine.UI;

public class GameSpeedChanger: MonoBehaviour {
	public float percentage = 1f;

	private void Awake() {
		this.GetComponent<Button>().onClick.AddListener(this.OnClick);
	}

	public void OnClick() {
		float adjusted = 1f - percentage;
		GameManagerScript.instance.turnTime *= adjusted;
	}
}
