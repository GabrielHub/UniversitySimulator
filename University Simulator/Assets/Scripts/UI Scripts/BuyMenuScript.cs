using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyMenuScript : MonoBehaviour
{
	public TMP_Dropdown dropdown;

	//For each drop down, there is a content gameobject. By default they are disabled, but enabled when update sees the value of dropdown is changed
	public GameObject buyHS; //High School Agreements dropdown content
    public GameObject buyUpgrades; //Univeristy Upgrades dropdown content
    public GameObject buySS; //Special Students dropdown content, ENABLED IN MIDGAME

    // Start is called before the first frame update
    void Start()
    {
        DropdownCheck();
    }

    // Update is called once per frame
    void Update()
    {
        //**Make sure you do gamephase checks for phase locked features
        DropdownCheck();
    }

    void DropdownCheck() {
        //Visibility for dropdown, defines what content is shown
        if (dropdown.value == 0) {
            //check for early gmae phase. If in the early game, HSA is index 0. If not, Upgrades is.
            if (GameManagerScript.instance.state == GameManagerScript.GameState.EarlyGame) {
                buyHS.SetActive(true);
                buyUpgrades.SetActive(false);
                buySS.SetActive(false);
            }
            else {
                buyHS.SetActive(false);
                buyUpgrades.SetActive(true);
                buySS.SetActive(false);
            }
        }
        else if (dropdown.value == 1) {
            //if game is in EarlyGame, index 1 is Upgrades. If not it is Special Students
            if (GameManagerScript.instance.state == GameManagerScript.GameState.EarlyGame) {
                buyHS.SetActive(false);
                buyUpgrades.SetActive(true);
                buySS.SetActive(false);
            }
            else {
                buyHS.SetActive(false);
                buyUpgrades.SetActive(false);
                buySS.SetActive(true);
            }
        }
        else if (dropdown.value == 2) {
            //if game is in EarlyGame, index 2 is Special Students. If not (FOR NOW) it is empty
            if (GameManagerScript.instance.state == GameManagerScript.GameState.EarlyGame) {
                buyHS.SetActive(false);
                buyUpgrades.SetActive(false);
                buySS.SetActive(true);
            }
        }
    }
}