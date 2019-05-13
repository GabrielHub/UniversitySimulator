using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MessageBusUpdater: MonoBehaviour {
	static MessageBusUpdater instance;

	public Dictionary<UpdateStage, Queue<Message.IMessage>> messageQueues = new Dictionary<UpdateStage, Queue<Message.IMessage>> {
		[UpdateStage.FixedUpdate] = new Queue<Message.IMessage>(),
		[UpdateStage.Update] = new Queue<Message.IMessage>(),
		[UpdateStage.LateUpdate] = new Queue<Message.IMessage>()
	};

	private void Awake() {
		if (MessageBusUpdater.instance == null) {
			MessageBusUpdater.instance = this;
		} else if (MessageBusUpdater.instance != this) {
			if (Application.isPlaying) {
				Destroy(this);
			} else {
				DestroyImmediate(this);
			}
		}
	}

	private void FixedUpdate() {
		this.sendMessageForStage(UpdateStage.FixedUpdate);
	}

	// Update is called once per frame
	private void Update () {
		this.sendMessageForStage(UpdateStage.Update);
	}

	private void LateUpdate() {
		this.sendMessageForStage(UpdateStage.LateUpdate);
	}

	void sendMessageForStage(UpdateStage stage) {
		var queue = this.messageQueues[stage];
		while (queue.Count > 0) {
			MessageBus.instance._sendMessageToHandlers(queue.Dequeue());
		}
	}

	public static void AddMessage(Message.IMessage m) {
		MessageBusUpdater.instance.messageQueues[m.getUpdateStage()].Enqueue(m);
	} 
}
