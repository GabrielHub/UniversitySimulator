using UnityEngine;
using UnityEngine.UI;

public class MetricsController: MonoBehaviour {
	public Slider renownSlider;
	public Slider happinessSlider;

	private Resources res {
		get { return GameManagerScript.instance.resources; }
	}

	private void Update() {
		this.UpdateValue(this.renownSlider, res.renown / Resources.MAX_RENOWN);
		this.UpdateValue(this.happinessSlider, res.happiness / Resources.MAX_HAPPINESS);
	}

	private void UpdateValue(Slider slider, float value) {
		slider.value = value;
		Color c;
		if (value < 0.5) {
			c = Color.Lerp(Color.red, Color.yellow, value*2);
		} else {
			c = Color.Lerp(Color.yellow, Color.green, value*2-1);
		}
		slider.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = c;
	}
}
