using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDVisibilityManager : MonoBehaviour, MessageHandler {
    public GameStateDictionary states;

    // Start is called before the first frame update
    void Start() {
        MessageBus.instance.register<GameState.DidChange>(this);
    }

    public void handleMessage<T>(T m) where T: Message.IMessage {
        if (!(m is GameState.DidChange)) return;
        GameState.DidChange msg = m as GameState.DidChange;
        this.gameObject.SetActive(this.states.GetValueOrDefault(msg.to, false));
    }
}
