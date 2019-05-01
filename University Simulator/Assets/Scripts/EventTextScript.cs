using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EventTextScript: MonoBehaviour, EventController.Listener {
	private TextMeshProUGUI text;

	private void Awake() {
		this.text = GetComponent<TextMeshProUGUI>();
	}

	private void Start() {
		GameManagerScript.instance.eventController.RegisterListener(this);
	}

	public void EventDidOccur(Event e) {
		this.text.text = e.text;
	}
}
