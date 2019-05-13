using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

/*
	This is the code attached to buttons dynamically placed in the Upgrade Dropdown.
*/
public class UpgradeBuyButton : MonoBehaviour {
	public Text buttonText;
	public Button buttonComponent;
	private UpgradeBase upgradeItem;

	void Start() {
		buttonComponent.onClick.AddListener(HandleClick);
	}

    //Setup on instantiate
    public void Setup(UpgradeBase item) {
		buttonText.text = item.name + ": " + item.description + " | Cost: " + item.cost.ToAbbreviatedString();
		upgradeItem = item;
		Debug.Log(item.name + " is available to purchase!");
	}

	public void HandleClick() {
		//make sure you can afford to buy upgrade
		if (GameManagerScript.instance.resources.wealth > upgradeItem.cost) {
			GameManagerScript.instance.resources.wealth -= upgradeItem.cost;

			//Apply unique upgrade effect
			upgradeItem.ApplyEffect();

			//destroy button
			Destroy(gameObject);
			//gameObject.SetActive(false);
		}
		else {
			GameManagerScript.instance.eventController.DoEvent(new Event("Not enough wealth to buy this upgrade", Event.Type.Notification));
		}
	}
}
