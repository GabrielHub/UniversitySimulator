using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class StatArrowController : MonoBehaviour {
	private static string upArrow = "⬆";
	private static string downArrow = "⬇";
	private static string staticArrow = "=";

	public StatController stat;

	private TextMeshProUGUI text;
	// Start is called before the first frame update
	void Start() {
		this.text = this.GetComponent<TextMeshProUGUI>();
	}

	// Update is called once per frame
	void Update() {
		if (this.stat.rate > 0) {
			this.text.color = Color.green;
			this.text.text = upArrow;
		}
		else if (this.stat.rate < 0) {
			this.text.color = Color.red;
			this.text.text = downArrow;
		}
		else {
			this.text.color = Color.grey;
			this.text.text = staticArrow;
		}
	}
}
