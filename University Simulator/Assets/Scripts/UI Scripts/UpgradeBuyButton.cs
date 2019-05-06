using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
	This is the code attached to buttons dynamically placed in the Upgrade Dropdown.
*/
public class UpgradeBuyButton : MonoBehaviour
{
	public Text buttonText;
	public Button buttonComponent;
	private UpgradeBase upgradeItem;

	void Start() {
		buttonComponent.onClick.AddListener(HandleClick);
	}

    //Setup on instantiate
    public void Setup(UpgradeBase item) {
		buttonText.text = item.name + " | Description: " + item.description + " | Cost: " + item.cost;
		upgradeItem = item;
    }

    public void HandleClick() {

    }
}
