public static class GameState {
	public enum State { EarlyGame1, EarlyGame2, EarlyGame3, EarlyGame4, MidGame, EndGame };
 public static State[] all = { State.EarlyGame1, State.EarlyGame2, State.EarlyGame3, State.EarlyGame4, State.MidGame, State.EndGame };

 public class ShouldChange : Message.IMessage {
		public override UpdateStage getUpdateStage() { return UpdateStage.Immediate; }
		public State state;
		public ShouldChange(State state) {
			this.state = state;
		}
		public override string ToString() {
			return $"{base.ToString()} to {this.state}";
		}
	}

	public class DidChange : Message.IMessage {
		public State from;
		public State to;
		public DidChange(State from, State to) {
			this.from = from;
			this.to = to;
		}

		public override string ToString() {
			return $"{base.ToString()} from {this.from} to {this.to}";
		}
	}
}
