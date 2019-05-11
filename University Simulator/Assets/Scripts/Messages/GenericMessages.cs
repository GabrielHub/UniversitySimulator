
namespace Message {
	public abstract class IMessage {
        public virtual UpdateStage getUpdateStage() { return UpdateStage.Update; }
        override public string ToString() {
            return $"[On {this.getUpdateStage()}] {this.GetType()}";
        }
    }
	public class AllType: IMessage { }

    namespace Info {
        public abstract class InfoBase: IMessage {
            override public UpdateStage getUpdateStage() { return UpdateStage.Immediate; }
            public int level { get; protected set; }
            public object caller;
            public string message;
            public object associatedValue; 
            override public string ToString() {
                return $"{base.ToString()} '{this.message}': {this.associatedValue}\nFrom {this.caller}";
            }
        }
        public class Verbose: InfoBase { public Verbose(object caller, string message, object value = null) { this.level = 0; this.caller = caller; this.message = message; this.associatedValue = value; } }
        public class Debug: InfoBase { public Debug(object caller, string message, object value = null) { this.level = 1; this.caller = caller; this.message = message; this.associatedValue = value; } }
        public class Warning: InfoBase { public Warning(object caller, string message, object value = null) { this.level = 2; this.caller = caller; this.message = message; this.associatedValue = value; } }
        public class Error: InfoBase { public Error(object caller, string message, object value = null) { this.level = 3; this.caller = caller; this.message = message; this.associatedValue = value; } }
    }
}
