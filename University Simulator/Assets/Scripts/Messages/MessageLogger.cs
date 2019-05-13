using UnityEngine;

[ExecuteInEditMode]
public class MessageLogger : MonoBehaviour, MessageSink {
    MessageSinkHandler sinkHandler;
    MessageErrorSinkHandler errorHandler;

    private void Awake() {
        this.sinkHandler = new MessageSinkHandler(this);
        this.errorHandler = new MessageErrorSinkHandler(this);
    }
    
    // Keep this, it makes it disableable.
    private void Update() { }

    public void outputMessage(string message) {
        if (this.isActiveAndEnabled) {
            Debug.Log(message);
        }
    }
}
