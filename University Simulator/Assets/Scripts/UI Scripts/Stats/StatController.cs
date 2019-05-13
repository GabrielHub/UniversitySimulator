using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class StatController : MonoBehaviour {
	public enum StatType {
		Wealth,
		Alumni,
		Students,
		Faculty,
		Capacity
	}
	public StatType type;
	public long value {
		get {
			Resources res = GameManagerScript.instance.resources;
			switch (this.type) {
			case StatType.Wealth:
				return res.wealth;
			case StatType.Alumni:
				return res.alumni;
			case StatType.Students:
				return res.students;
			case StatType.Faculty:
				return res.faculty;
			case StatType.Capacity:
				return res.studentPool;
			}
			return 0; // unreachable
		}
	}

	public long rate {
		get {
			Resources delta = GameManagerScript.instance.resourcesDelta;
			switch (this.type) {
			case StatType.Wealth:
				return delta.wealth;
			case StatType.Alumni:
				return delta.alumni;
			case StatType.Students:
				return delta.students;
			case StatType.Faculty:
				return delta.faculty;
			case StatType.Capacity:
				return delta.studentPool;
			}
			return 0; // unreachable
		}
	}

	public TextMeshProUGUI label;
	public TextMeshProUGUI deltaText;

	private void Awake() {
		this.label.text = this.type.ToString();
	}

	void Update() {
		//Debug.Log("here");
		this.deltaText.gameObject.SetActive(GameManagerScript.instance.enableStatistics);
	}
}
