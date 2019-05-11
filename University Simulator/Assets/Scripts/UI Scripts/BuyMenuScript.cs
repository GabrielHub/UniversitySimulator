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

    //Text that says whether this is enabled or disabled or whatever
    public TMP_Text hsaText;

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

        //Changes the text of other options, idk how to do this outside of selecting a specific dropdown
        if (GameManagerScript.instance.state == GameManagerScript.GameState.EarlyGame1) {
            dropdown.options[1].text = "Keep growing to unlock"; //lock HSA agreements
            dropdown.options[2].text = "Keep growing to unlock";//lock special students
        }
        else if (GameManagerScript.instance.state == GameManagerScript.GameState.EarlyGame2) {
            dropdown.options[1].text = "High School Agreements"; //unlock HSA agreements
        }
        else if (GameManagerScript.instance.state == GameManagerScript.GameState.MidGame) {
            dropdown.options[2].text = "Special Students"; //unlock HSA agreements
            dropdown.options[1].text = "Locked"; //relock HSA agreements
        }
    }

    void DropdownCheck() {
        //Visibility for dropdown, defines what content is shown
        if (dropdown.value == 0) {
            //check for early game phase.
            buyHS.SetActive(false);
            buyUpgrades.SetActive(true);
            buySS.SetActive(false); 
        }
        else if (dropdown.value == 1) {
            if (GameManagerScript.instance.state == GameManagerScript.GameState.EarlyGame1) {
                buyHS.SetActive(false);
                buyUpgrades.SetActive(false);
                buySS.SetActive(false);
                //hsaText.text = "Keep growing to unlock agreements";
            }
            else if (GameManagerScript.instance.state == GameManagerScript.GameState.MidGame) {
                //hide this permanently once it's in the midgame
                buyHS.SetActive(false);
                buyUpgrades.SetActive(false);
                buySS.SetActive(false);

                //hsaText.text = "High School Agreements are now irrelevant";
            }
            else {
                //else it can be shown
                buyHS.SetActive(true);
                buyUpgrades.SetActive(false);
                buySS.SetActive(false);

                hsaText.text = "New agreements appear randomly and increase the capacity of students";
            }
        }
        else if (dropdown.value == 2) {
            if (GameManagerScript.instance.state == GameManagerScript.GameState.MidGame) {
                buyHS.SetActive(false);
                buyUpgrades.SetActive(false);
                buySS.SetActive(true);
            }
            else {
                buyHS.SetActive(false);
                buyUpgrades.SetActive(false);
                buySS.SetActive(false);
            }
        }
    }
}