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

		if (e.type == "GameState") {
			this.background.color = Color.red;
		}
		else if (e.type == "Notification") {
			this.background.color = Color.green;
		}
		else if (e.type == "Narrative") {
			this.background.color = Color.blue;
		}
		else if (e.type == "Random") {
			this.background.color = Color.yellow;
		}
		else if (e.type == "Feature") {
			this.background.color = Color.magenta;
		}
		else {
			Debug.Log("ERROR: mislabled event type, event type does not exist: " + e.text);
		}
	}
}
