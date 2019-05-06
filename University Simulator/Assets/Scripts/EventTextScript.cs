using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EventTextScript: MonoBehaviour, EventController.Listener {
	private TextMeshProUGUI text;
	private Image background;
	// the time the event remains on screen for
	public float eventDisplayTime = 10f;
	// the time the background flashes when there's a new event
	public float flashTime = 1f;
	public Color flashColor = Color.red;

	private void Awake() {
		this.text = GetComponent<TextMeshProUGUI>();
		this.background = GetComponentInParent<Image>();
	}

	private float textTimer = 0f;
	private void Start() {
		GameManagerScript.instance.eventController.RegisterListener(this);
	}

	private void Update() {
		this.textTimer += Time.deltaTime;
		if (this.textTimer >= this.flashTime) {
			this.background.color = Color.white;
		}
		if (this.textTimer >= this.eventDisplayTime) {
			this.text.text = "No new events... (click to show/hide all events)";
		}
	}

	public void EventDidOccur(Event e) {
		this.text.text = e.text;
		this.textTimer = 0f;
		this.background.color = Color.red;
	}
}
