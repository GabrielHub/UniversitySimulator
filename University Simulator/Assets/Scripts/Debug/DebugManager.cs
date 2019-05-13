using System.Collections.Generic;

using UnityEngine;

[ExecuteInEditMode]
public class DebugManager : MonoBehaviour {
	public class DidChange : Message.IMessage {
		public override UpdateStage getUpdateStage() { return UpdateStage.Immediate; }
	}

	public static DebugManager instance;
	public bool debug = false;

	public GameObject[] targets;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			if (Application.isPlaying) {
				Destroy(this);
			}
			else {
				DestroyImmediate(this);
			}
			throw new System.Exception($"Only 1 Debug Manager can exist!");
		}
	}

	private bool lastDebug = false;
	private void Update() {
		if (debug != lastDebug) {
			setVisibility();
		}
		lastDebug = debug;
	}

	void setVisibility() {
		foreach (GameObject g in this.targets) {
			g.SetActive(this.debug);
		}
	}
}
