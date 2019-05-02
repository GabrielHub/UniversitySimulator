using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class BuyAgreementScript : MonoBehaviour
{
	//UI text
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI poolText;
    public TextMeshProUGUI valueText;

    public Button buyButton;
    private Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        //Button Setup //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        buyButton.onClick.AddListener(BuyOnClick);

        buttonText = buyButton.GetComponentInChildren<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
    	if (this.tag == "0") {
    		nameText.text = GameManagerScript.instance.agreements[0].name;
       		poolText.text = GameManagerScript.instance.agreements[0].students.ToString();
        	valueText.text = GameManagerScript.instance.agreements[0].value.ToString();

        	buttonText.text = (GameManagerScript.instance.agreements[0].cost.ToString());
    	}
        else if (this.tag == "1") {
        	nameText.text = GameManagerScript.instance.agreements[1].name;
       		poolText.text = GameManagerScript.instance.agreements[1].students.ToString();
        	valueText.text = GameManagerScript.instance.agreements[1].value.ToString();

        	buttonText.text = (GameManagerScript.instance.agreements[1].cost.ToString());
        }
        else {
        	nameText.text = GameManagerScript.instance.agreements[2].name;
       		poolText.text = GameManagerScript.instance.agreements[2].students.ToString();
        	valueText.text = GameManagerScript.instance.agreements[2].value.ToString();

        	buttonText.text = (GameManagerScript.instance.agreements[2].cost.ToString());
        }
    }

    void BuyOnClick() {
    	if (this.tag == "0") {
    		//I CAN'T FIGURE OUT WHY THIS WONT WORK SOMEONE HELP ME WTF EVERYTHING ELSE IS WORKIGN ONCE YOU GET THIS WORKING JUST DUPLICATE THE AGREEMENT OBJECT IN UNITY TWICE AND MOVE IT OVER
    		GameManagerScript.instance.resources.agreements.Add(GameManagerScript.instance.agreements[0]);
    		GameManagerScript.instance.resources.wealth -= GameManagerScript.instance.resources.agreements[0].cost;
    		GameManagerScript.instance.eventController.DoEvent(new Event("Purchased HS Agreement: " + nameText.text));
    	}
        else if (this.tag == "1") {
        	GameManagerScript.instance.resources.agreements.Add(GameManagerScript.instance.agreements[1]);
        }
        else {
        	GameManagerScript.instance.resources.agreements.Add(GameManagerScript.instance.agreements[2]);
        }
    }
}
