using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*
	This is the code attached to buttons dynamically placed in the Special Students Dropdown.
*/
public class SpecialStudentButtonScript : MonoBehaviour {
	public Text buttonText;
	public Button buttonComponent;
	private SpecialStudent student;

	void Start() {
		buttonComponent.onClick.AddListener(HandleClick);
	}

	//Setup on instantiate
	public void Setup(SpecialStudent item) {
		buttonText.text = item.type + ": '" + item.name + "' | Scholarship Cost: " + item.cost.ToAbbreviatedString();
		student = item;
		Debug.Log(item.name + " is available to purchase!");
	}

	public void HandleClick() {
		//make sure you can afford to buy upgrade
		if (GameManagerScript.instance.resources.wealth > student.cost) {
			GameManagerScript.instance.resources.wealth -= student.cost;

			//85% chance that adding a student increases ranking by one
	        if (Random.Range(0.0f, 1.0f) <= 0.85f) {
	            //GameManagerScript.instance.resources.ranking++;
	            GameManagerScript.instance.resources.AddSpecialStudent(student);
	            GameManagerScript.instance.eventController.DoEvent(new Event(student.name + " accepted your offer!", Event.Type.Notification));
	        }
	        else {
	        	GameManagerScript.instance.eventController.DoEvent(new Event(student.name + " was a bust, their impact insignificant, your disappointment immeasurable and everyone's day ruined.", Event.Type.Notification));
	        }

	        GameManagerScript.instance.PlaySound(GameManagerScript.soundType.BUTTON);

			//destroy button
			Destroy(gameObject);
			//gameObject.SetActive(false);
		}
		else {
			GameManagerScript.instance.eventController.DoEvent(new Event("Not enough wealth to give this scholarship", Event.Type.Notification));
			GameManagerScript.instance.PlaySound(GameManagerScript.soundType.INSUFFICIENT);
		}
	}
}
