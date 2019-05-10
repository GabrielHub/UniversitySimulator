using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BuyAgreementScript : MonoBehaviour {
	public enum ScriptNumber { One, Two, Three }
	public ScriptNumber scriptNumber;
	//UI text
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI poolText;
    public TextMeshProUGUI valueText;

    public Button buyButton;
    public  Text buttonText;

	private HighSchoolAgreement agreement {
		get {
			return GameManagerScript.instance.agreements[(int) this.scriptNumber];
		}
	}

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
			nameText.text = this.agreement.name;
			poolText.text = this.agreement.students.ToString();
			valueText.text = this.agreement.value.ToString();
			buttonText.text = this.agreement.cost.ToString();
    }

    public void BuyOnClick() {
			Resources res = GameManagerScript.instance.resources;
			// todo: fix this logic
			if (res.wealth >= this.agreement.cost) {
				res.agreements.Add(this.agreement);
				res.wealth -= this.agreement.cost;
				GameManagerScript.instance.eventController.DoEvent(new Event("Purchased HS Agreement: " + nameText.text, "Notification"));

				this.gameObject.SetActive(false);
			} else {
				// todo: this shouldn't be an event
				GameManagerScript.instance.eventController.DoEvent(new Event("Not Enough $$$ To Make This Purchase!", "Notification"));
			}
    }
}
