using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

// TODO: this doesn't work, fix it
public class GamePhaseChanger : MonoBehaviour, MessageHandler {
	// Start is called before the first frame update
	private TMP_Dropdown dropdown;
	private int offset = 0;

	private void Awake() {
		MessageBus.main.register<GameState.DidChange>(this);
		this.dropdown = GetComponent<TMP_Dropdown>();
	}

	void Start() {
		dropdown.options.Clear();
		// populate the dropdown
		foreach (GameState.State state in GameState.all) {
			dropdown.options.Add(new TMP_Dropdown.OptionData(state.ToString()));
		}
	}

	public void handleMessage<T>(T m) where T : Message.IMessage {
		// remove earlier phases as they happen
		int value = (int) (m as GameState.DidChange).to;
		for (int i = 0; i <= value - offset; ++i) {
			this.dropdown.options.RemoveAt(i);
		}
		offset = value;
	}

	public void OnDidChange() {
		GameState.State to = (GameState.State) this.dropdown.value + this.offset;
		MessageBus.main.emit(new GameState.ShouldChange(to));
	}
}
