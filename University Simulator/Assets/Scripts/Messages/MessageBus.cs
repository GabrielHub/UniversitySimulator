using System.Collections.Generic;
using System.Linq;

public enum UpdateStage {
	Immediate,
	Update,
	LateUpdate,
	FixedUpdate,
}

public interface MessageHandler {
	void handleMessage<T>(T m) where T: Message.IMessage;
}

public abstract class MessageHandler<T>: MessageHandler where T: Message.IMessage {
	public abstract void handleTypedMessage(T m);
	public virtual void handleMessage<X>(X m) where X: Message.IMessage {
		if (m is T) {
			this.handleTypedMessage(m as T);
		}
	}
}

public class MessageBus {
	static MessageBus _instance;
	public static MessageBus instance {
		get { 
			if (MessageBus._instance == null) {
				MessageBus._instance = new MessageBus();
			}
			return MessageBus._instance;
		}
	}

	public List<System.Action<System.Exception, Message.IMessage>> errorHandlers;
	
	public System.Type[] messages;

	MessageBus() {
		this.errorHandlers = new List<System.Action<System.Exception, Message.IMessage>>();
		this.messages = (from domainAssembly in System.AppDomain.CurrentDomain.GetAssemblies()
					from assemblyType in domainAssembly.GetTypes()
					where typeof(Message.IMessage).IsAssignableFrom(assemblyType)
					&& !(assemblyType.ToString().Contains("IMessage") || 
						assemblyType.ToString().Contains("Base") ||
						assemblyType.ToString().Contains("AllType"))
					select assemblyType).ToArray();
	}

	public Dictionary<System.Type, List<MessageHandler>> handlers = new Dictionary<System.Type, List<MessageHandler>>();

	public void register<T>(MessageHandler handler) where T: Message.IMessage {
		this.register(typeof(T), handler);
	}

	public void register(System.Type type, MessageHandler handler) {
		if (!(type.IsSubclassOf(typeof(Message.IMessage)))) {
			throw new System.Exception();
		}
		if (!this.handlers.ContainsKey(type)) {
			this.handlers[type] = new List<MessageHandler>();
		}
		this.handlers[type].Add(handler);
	}

	public void deregister<T>(MessageHandler handler) where T: Message.IMessage {
		this.deregister(typeof(T), handler);
	}

	public void deregister(System.Type type, MessageHandler handler) {
		if (!(type.IsSubclassOf(typeof(Message.IMessage)))) {
			throw new System.Exception();
		}
		if (this.handlers.ContainsKey(type)) {
			this.handlers[type].Remove(handler);
		}

	}

	public void emit(Message.IMessage m) {
		lock (this) {
			var stage = m.getUpdateStage();
			if (stage == UpdateStage.Immediate) {
				this.sendMessageToHandlers(m);
			} else {
				MessageBusUpdater.AddMessage(m);
			}
		}
	}

    public void sendMessageToHandlers(Message.IMessage m) {
		System.Action<MessageHandler> runHandler = (handler) => {
			try {
				handler.handleMessage(m);
			} catch (System.Exception e) {
				foreach (var errorHandler in this.errorHandlers) {
					errorHandler(e, m);
				}
			} // TODO: Catch errors
		};
        if (MessageBus.instance.handlers.ContainsKey(m.GetType())) {
            foreach (MessageHandler handler in MessageBus.instance.handlers[m.GetType()]) {
				runHandler(handler);
            }
        }
        if (MessageBus.instance.handlers.ContainsKey(typeof(Message.AllType))) {
            foreach (MessageHandler handler in MessageBus.instance.handlers[typeof(Message.AllType)]) {
				runHandler(handler);
            }
        }
    }
}
