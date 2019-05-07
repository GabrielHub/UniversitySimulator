using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyMenuScript : MonoBehaviour
{
	public TMPro.TMP_Dropdown dropdown;

	//For each drop down, there is a content gameobject. By default they are disabled, but enabled when update sees the value of dropdown is changed
	public GameObject buyHS;
    public GameObject buyUpgrades;

    // Start is called before the first frame update
    void Start()
    {
        buyHS.SetActive(false);
        buyUpgrades.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //MAke sure you do gamephase checks for phase locked features

    	//Visibility for dropdown, defines what content is shown
        if (dropdown.value == 0) {
            //check for early gmae phase
            if (GameManagerScript.instance.resources.gamePhase == GamePhase.Early) {
                buyHS.SetActive(true);
                buyUpgrades.SetActive(false);
            }
        	else {
                buyHS.SetActive(false);
                buyUpgrades.SetActive(false);
            }
        }
        else if (dropdown.value == 1) {
        	buyHS.SetActive(false);
            buyUpgrades.SetActive(true);
        }
    }
}
