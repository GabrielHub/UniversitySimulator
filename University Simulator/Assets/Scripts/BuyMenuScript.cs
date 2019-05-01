﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyMenuScript : MonoBehaviour
{
	public TMPro.TMP_Dropdown dropdown;

	private List<HighSchoolAgreement> agreements;

	//For each drop down, there is a content gameobject. By default they are disabled, but enabled when update sees the value of dropdown is changed
	public GameObject content1;

    // Start is called before the first frame update
    void Start()
    {
        content1.SetActive(false);

        //Initial highschool you start out with
        agreements = new List<HighSchoolAgreement> ();
        HighSchoolAgreement temp = new HighSchoolAgreement("A Bad High School", 100, 1);
        agreements.Add(temp);
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

        int totStudent = 0;
        float totRenown = 0;
        //HighSchool Agreement Code
        foreach(HighSchoolAgreement hs in agreements) {
        	totStudent += hs.students;
        	totRenown += hs.ranking / 10;
        }
        GameManagerScript.studentPool = totStudent;
        GameManagerScript.hsRenown = totRenown;
    }
}

public class HighSchoolAgreement {
	public string name;
	public int students;
	public int ranking;

	public HighSchoolAgreement(string n, int s, int r) {
		name = n;
		students = s;
		ranking = r; //not a ranking out of like a 100 or anything, like a bad ranking is like 1 and a good ranking is like 100 ok that's just how it works
	}
}