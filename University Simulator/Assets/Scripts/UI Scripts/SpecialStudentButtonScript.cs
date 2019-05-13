using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
	This is the code attached to buttons dynamically placed in the Special Students Dropdown.
*/
public class SpecialStudentButtonScript : MonoBehaviour
{
    public Text buttonText;
	public Button buttonComponent;
	private SpecialStudent student;

	void Start() {
		buttonComponent.onClick.AddListener(HandleClick);
	}

    //Setup on instantiate
    public void Setup(SpecialStudent item) {
		buttonText.text = item.type + ": '" + item.name + "' | Scholarship Cost: " + item.cost;
		student = item;
		Debug.Log(item.name + " is available to purchase!");
    }

    public void HandleClick() {
    	//make sure you can afford to buy upgrade
    	if (GameManagerScript.instance.resources.wealth > student.cost) {
    		GameManagerScript.instance.resources.wealth -= student.cost;

    		GameManagerScript.instance.eventController.DoEvent(new Event("This isn't implemented yet", Event.Type.Notification));

    		//destroy button
    		Destroy(gameObject);
    		//gameObject.SetActive(false);
    	}
    	else {
    		GameManagerScript.instance.eventController.DoEvent(new Event("Not enough wealth to give this scholarship", Event.Type.Notification));
    	}
    }
}
