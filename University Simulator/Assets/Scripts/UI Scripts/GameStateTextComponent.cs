using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GameStateTextComponent: MonoBehaviour, MessageHandler {
	public GameStateStringDictionary strings;
	private TextMeshProUGUI text;

	private void Awake() {
		MessageBus.main.register<GameState.DidChange>(this);
		this.text = this.GetComponent<TextMeshProUGUI>();
	}

	public void handleMessage<T>(T msg) where T: Message.IMessage {
		GameState.State state = (msg as GameState.DidChange).to;
		this.text.text = this.strings.GetValueOrDefault(state, "Figure it out 😉");
	}
}
