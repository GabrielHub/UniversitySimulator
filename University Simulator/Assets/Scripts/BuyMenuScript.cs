using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyMenuScript : MonoBehaviour
{
	public TMPro.TMP_Dropdown dropdown;

	//For each drop down, there is a content gameobject. By default they are disabled, but enabled when update sees the value of dropdown is changed
	public GameObject content1;

    // Start is called before the first frame update
    void Start()
    {
        content1.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
    	//Visibility for dropdown, defines what content is shown
        if (dropdown.value == 0) {
        	content1.SetActive(true);
        }
        else if (dropdown.value == 1) {
        	content1.SetActive(false);
        }
    }
}