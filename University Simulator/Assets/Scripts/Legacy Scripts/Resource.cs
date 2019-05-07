using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Resource {
	public int value;
	public int delta;
	public bool enabled;

	public void increment() {
		this.value += this.delta;
	}
}
