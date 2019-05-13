using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class StatDeltaController : MonoBehaviour {
	public StatController stat;
	private TextMeshProUGUI text;
	// Start is called before the first frame update
	void Start() {
		this.text = this.GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update() {
		// if (!GameManagerScript.instance.enableStatistics) { this.gameObject.SetActive(false); return; }
		string text = this.stat.rate.ToString();
		if (this.stat.rate > 0) {
			text = "+" + text;
		}
		this.text.text = text;
	}
}
