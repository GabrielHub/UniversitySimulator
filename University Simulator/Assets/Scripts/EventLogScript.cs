using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class EventLogScript : MonoBehaviour, EventController.Listener {
	private List<string> eventLog = new List<string>();
	private string text = "";

	public TextMeshProUGUI eventLogText;

	public int maxLines;

	/// This function is called when the object becomes enabled and active.
	void Start() {
		GameManagerScript.instance.eventController.RegisterListener(this);
		this.gameObject.SetActive(false);
		// for (int i = 0; i < 100; ++i) {
		//     eventLog.Add("asdf\n");
		// }
	}

	/// This function is called when an event is emitted from EventController.
	public void EventDidOccur(Event e) {
		eventLog.Add(e.text);

		if (eventLog.Count >= maxLines)
			eventLog.RemoveAt(0);

		text = "";

		foreach (string log in eventLog) {
			text = log + "\n\n" + text;
		}

		eventLogText.text = text;
	}
}
