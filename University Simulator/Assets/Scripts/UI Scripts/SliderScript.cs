using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {
	public Text valueText;
	private Slider slider;

	// Start is called before the first frame update
	void Start() {
		slider = GetComponent<Slider>();
	}

	void FixedUpdate() {
		if (slider.name == "AcceptanceSlider") {
			//Enabled Percentage view
			valueText.text = (slider.value * 100).ToString("F2") + "%";
		}
		else {
			//for int values that do not need to be converted
			valueText.text = ((int) slider.value).ToString();
		}
	}
}
