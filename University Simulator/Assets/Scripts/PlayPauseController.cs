using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class PlayPauseController : MonoBehaviour {
	private TextMeshProUGUI text;

	private void Start() {
		this.text = this.GetComponentInChildren<TextMeshProUGUI>();
	}

	private void Update() {
		if (GameManagerScript.instance.playing) {
			this.text.text = "pause";
		}
		else {
			this.text.text = "play";
		}
	}

	public void OnButtonClick() {
		GameManagerScript.instance.PlaySound(GameManagerScript.soundType.BUTTON);
		GameManagerScript.instance.playing = !GameManagerScript.instance.playing;
	}
}
