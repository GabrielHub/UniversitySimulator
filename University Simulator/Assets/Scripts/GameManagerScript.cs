﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    //UI text
    [SerializeField] TextMeshProUGUI eventLog;
    [SerializeField] TextMeshProUGUI studentsText;
    [SerializeField] TextMeshProUGUI facultyText;
    [SerializeField] TextMeshProUGUI alumniText;
    [SerializeField] TextMeshProUGUI buildingsText;
    [SerializeField] TextMeshProUGUI materialsText;

    //Other UI Elements
    public Button playButton;
    public Text playText;

	//resources
	private int students;
	private int faculty;
	private int alumni;
	private int buildings;
	private int material;

	//other hidden resources
	private int buildingBonus = 0;
	private float growthBonus = 1.0f;
	private float wealth = 0;
	private float difficulty = 1.0f;
	private int studentGrowth;
	private int popInitial; //initial student population

    //other variables
    private bool playing = true; //check if paused or not

    // Start is called before the first frame update
    void Start()
    {
    	//Resource list
        studentsText.text = "Students: " + students.ToString();
        facultyText.text = "Faculty: " + faculty.ToString();
        alumniText.text = "Alumni: " + alumni.ToString();
        buildingsText.text = "Buildings: " + buildings.ToString();
        materialsText.text = "Materials: " + material.ToString();

        //Button Setup //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        playButton.onClick.AddListener(PauseOnClick);

        //set up random ranges (possibly based on difficulty later)
        students = Mathf.FloorToInt(Random.Range(2.0f, 15.0f));
		faculty = Mathf.FloorToInt(Random.Range(1.0f, 5.0f));
		alumni = 1;
		buildings = 1;
		material = Mathf.FloorToInt(Random.Range(100.0f, 1000.0f));

		popInitial = students;

		eventLog.text += "\n BREAKING: SADU's only alum has taken over for the school!";

        //A turn is done every second, with a 0.5 second delay upon resuming
        InvokeRepeating("Turns", 0.5f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
    	//check for game over
    	if (students <= 0) {
    		eventLog.text = "\n You've run out of students and this University has failed.";
    	}
    	else if (alumni >= 500000) {
    		eventLog.text = "\n Congrats! You have as much alumni as NYU! \n \n what else do you want. a cookie?";
    	}
        

        //Resource List to be updated
        studentsText.text = "Students: " + students.ToString();
        facultyText.text = "Faculty: " + faculty.ToString();
        alumniText.text = "Alumni: " + alumni.ToString();
        buildingsText.text = "Buildings: " + buildings.ToString();
        materialsText.text = "Materials: " + material.ToString();
    }

    //take into account all policy changes and changes in resources, then update said resources
    void Turns() {
        //Check whether game is paused or not
        if (playing) {
            //calculate wealth and apply wealth difficulty
            wealth = students + faculty + alumni + buildings * 200;

            //calculate difficulty
            difficulty = ((students + alumni * 2 + faculty + buildings * 10) / wealth);

            //material changes
            material += Mathf.FloorToInt(alumni / 5) + students + buildingBonus;

            //student changes
            if ((students / 3 < faculty) && (students < 350 * buildings)) {
                //increase by growth rate
                int K = 350 * buildings;
                students += Mathf.FloorToInt(growthBonus * students * (1 - (students / K)));
            }

            //faculty changes
            if (material <= faculty) {
                faculty -= Mathf.FloorToInt(((faculty - material) / 2));
                material = 0;
            }
            else if (material <= 0) {
                students -= 2;
            }
            else {
                material -= faculty;
            }

            //alumni resource changes
            if (students <= 5) {
                alumni += students;
                students = 0;
            }
            else {
                int mod = Mathf.FloorToInt(students / 5);
                students -= mod;
                alumni += mod;
            }

            //events
            DoEvent();
        }
    }

    //Not currently working, view design doc
    void DoEvent() {
    	float rand = Random.Range(0.0f, difficulty);

    	//Random, dynamic difficulty events
    	if (rand <= 0.1) {
    		eventLog.text += "\n EVENT: A faculty member has published a great paper! \n New faculty have joined!";
    		faculty++;
    	}
    	else if (rand <= 0.4) {
    		if (rand <= 0.3) {
    			eventLog.text += "\n EVENT: Students have transferred from SADDERU \n Students have increased!";
    			students++;
    		}
    		else if (buildings > 1) {
    			eventLog.text += "\n EVENT: Two buildings bumped into each other, went on a date, and fell in love! \n A new building is born?";
    			buildings++;
    		}
    		else {
    			eventLog.text += "\n EVENT: A pipe has burst creating super athletes. \n Student growth has increased!";
    			growthBonus += 0.1f;
    		}
    	}
    	else if (rand <= 0.47) {
    		eventLog.text += "\n EVENT: A pipe has burst injuring and killing students!";
    		students -= 3;
    	}
    	else if (rand <= 0.69) {
    		eventLog.text += "\n EVENT: Students have used materials to buy equipment \n Materials lost!";
    		material -= Mathf.FloorToInt(material / 2.7f);
    	}
    	else if (rand <= 0.75) {
    		eventLog.text += "\n EVENT: Due to your great works, more students are joining! \n Student growth has increased!";
    		growthBonus += 0.1f;
    	}
    	else if (rand <= 0.85) {
    		eventLog.text += "\n EVENT: Due to rampant [REDACTED] bad things are happening! \n Faculty and Students lost!";
    		students -= 5;
    		faculty--;
    	}
    	else if (rand <= 1.0) {
    		eventLog.text += "\n EVENT: Ya just got taxed boi \n Materials lost!";
    		material -= Mathf.FloorToInt(material / 2.0f);
    	}
    	else if (rand <= 1.2) {
    		eventLog.text += "\n EVENT: Due to a bad Mathlete loss students grow disinterested \n Student growth decreased!";
    		growthBonus -= 0.2f;
    	}
    	else if (rand <= 1.4) {
    		eventLog.text += "\n EVENT: Increasing wealth of the school makes people mad \n Materials lost! \n Students lost!";
    		material -= 200;
    		students -= 10;
    	}
    	else if (rand <= 1.5) {
    		eventLog.text += "\n EVENT: Alumni have died on a trip to mars \n Alumni lost!";
    		alumni -= 50;
    	}
    	else {
    		eventLog.text += "\n BIG EVENT: 'Damn this school wealthy bruh lemme cop some of that' - President \n Materials halved!";
    		material = Mathf.FloorToInt(material / 2);
    	}
    }

    //Pause and Play button click function
    void PauseOnClick() {
        //change text when paused or playing
        if (playing) {
            playText.text = "Play";
            playing = !playing;
        }
        else {
            playText.text = "Pause";
            playing = !playing;
        }
    }

    //Different Buy Button Functions. Each thing you buy is coded below
    /*void ShackClick() {
    	if (material >= 350) {
    		material -= 350;

    		float rand = Random.Range(0.0f, difficulty);

    		if (rand <= 0.25f) {
    			eventLog.text += "\n BREAKING: Survival in new dorms is building comradery and school spirit! \n Student growth has increased!";
    			growthBonus += 0.5f;
    			buildings++;
    		}
    		else {
    			eventLog.text += "\n TRAGEDY: New small dorms lack hot water and ceilings. Not cool! \n Student growth has decreased! \n Materials have been lost due to repairs!";
    			growthBonus -= 0.3f;
    			material -= Mathf.FloorToInt(material / 2);
    		}
    	}

    	//autoscroll
		scrollRect.velocity = new Vector2(0.0f, 1000f);
    }

    void BuildingClick() {
    	if (material >= 500) {
    		material -= 500;

    		float rand = Random.Range(0.0f, difficulty);

    		if (rand <= 0.6f) {
    			eventLog.text += "\n BREAKING: Investors are happy, students are happy, SADU remains sad. \n Student growth has increased! \n Building bonus increased!";
    			growthBonus += 1.0f;
    			buildings++;
    			buildingBonus += 50;
    			faculty++;
    		}
    		else {
    			eventLog.text += "\n TRAGEDY: New buildings at SADU have introduced a new breed of rat. \n Student growth has decreased!";
    			growthBonus -= 0.8f;
    		}
    	}

    	//autoscroll
		scrollRect.velocity = new Vector2(0.0f, 1000f);
    }

    void BigBuildingClick() {
    	if (material >= 1300) {
    		material -= 1300;

    		float rand = Random.Range(0.0f, difficulty);

    		if (rand <= 0.88) {
    			eventLog.text += "\n BREAKING: Big Building Big Money Big Booty Big Lambo \n Student growth has increased! \n Building bonus increased!";
    			growthBonus += 2.0f;
    			buildings++;
    			buildingBonus += 150;
    			faculty += 2;
    		}
    		else {
    			eventLog.text += "\n TRAGEDY: Yeah you messed up bud. \n Student growth has decreased! \n Building bonus has decreased!";
    			growthBonus -= 1.5f;
    			buildingBonus -= 50;
    		}
    	}

    	//autoscroll
		scrollRect.velocity = new Vector2(0.0f, 1000f);
    }

    void HireClick() {
    	if (material >= 100) {
    		material -= 100;
    		float rand = Random.Range(0.0f, difficulty);

    		if (rand >= 0.1f && rand <= 0.9) {
    			eventLog.text += "\n SADU has hired a new professor! \n New faculty member!";
    			faculty++;
    		}
    		else if (rand < 1.0f) {
    			eventLog.text += "\n BREAKING: SADU new professor has somehow multiplied! \n Faculty amount has increased! \n Materials lost!";
    			faculty += 3;
    			material -= 30;
    		}
    		else {
    			eventLog.text += "\n SADU made an attempt to hire a new professor. Somehow they've hired a squirrel. \n Building bonus has decreased!";
    			buildingBonus -= 78;
    		}
    	}

    	//autoscroll
		scrollRect.velocity = new Vector2(0.0f, 1000f);
    }

    void RiskyBoostClick() {
    	if (material >= 650) {
    		material -= 650;
    		float rand = Random.Range(0.0f, difficulty);

    		if (rand <= 0.45f) {
    			eventLog.text += "\n Ads for SADU are a hit! We freaking got em lads \n Student growth increased!";
    			growthBonus += 0.1f;
    		}
    		else if (rand <= 0.6f) {
    			eventLog.text += "\n In a sad attempt to increase student numbers, SADU tripped on a power cord.";
    		}
    		else if (rand <= 0.8) {
    			eventLog.text += "\n BREAKING: A bump in name recognition has given building bonuses, but also an increase in aggresive Yelp reviews \n Building bonus has increased! \n Student growth decreased!";
    			growthBonus -= 0.1f;
    			buildingBonus += 50;
    		}
    		else {
    			eventLog.text += "\n TRAGEDY: 10 RIDICULOUS Facebook Ads YOU NEED TO SEE! \n Student growth has decreased!";
    			growthBonus -= 0.2f;
    		}
    	}

    	//autoscroll
		scrollRect.velocity = new Vector2(0.0f, 1000f);
    }

    void MedBoostClick() {
    	if (material >= 650) {
    		material -= 650;
    		float rand = Random.Range(0.0f, difficulty);

    		if (rand <= 0.35f) {
    			eventLog.text += "\n SADU has opened it's borders! (Higher transfer rate we mean) \n More students registered!";
    			students += Mathf.FloorToInt(popInitial / 2);
    		}
    		else if (rand <= 0.55f) {
    			eventLog.text += "\n BREAKING: Rumor swirling that SADU has faked some international student applications for diversity! \n More students registered! \n Student growth rate decreased!";
    			students += popInitial;
    			growthBonus -= 0.1f;
    		}
    		else if (rand <= 0.7f) {
    			eventLog.text += "\n SADU administration attempted to do something but failed!";
    		}
    		else {
    			eventLog.text += "\n Students feel new incoming students are just wealthier, meaner versions of themselves. \n Yearly materials increased! \n Student growth decreased! \n Student numbers increaased!";
    			buildingBonus += 50;
    			growthBonus -= 0.1f;
    			students += popInitial;
    		}
    	}

    	//autoscroll
		scrollRect.velocity = new Vector2(0.0f, 1000f);
    }

    void BigBoostClick() {
    	if (material >= 1000) {
    		material -= 1000;
    		float rand = Random.Range(0.0f, difficulty);

    		eventLog.text += "\n SADU releases Alumni support email! Will our ancestors pay for another StarBucks?";
    		if (rand <= 0.55f) {
    			eventLog.text += "\n SADU receives enough donations for new therapy dogs! Hopefully we don't lose any this time! \n Materials increased! \n Buildings bonus has increased!";
    			material += 200;
    			buildingBonus += 200;
    		}
    		else if (rand <= 0.88f) {
    			eventLog.text += "\n Alumni have removed SADU from their LinkedIns. Will daddy let me stay for Jenny's 21st? \n Alumni decreased \n Students lost!";
    			alumni -= Mathf.FloorToInt(alumni / 5);
    			students -= popInitial;
    		}
    		else {
    			eventLog.text += "\n TRAGEDY: Alumni have demolished he SADU distillery and requested refunds! SADDERU students seen rejoicing! \n Alumni decreased \n Students lost!";
    			alumni -= Mathf.FloorToInt(alumni / 3);
    			students -= popInitial;
    		}
    	}

    	//autoscroll
		scrollRect.velocity = new Vector2(0.0f, 1000f);
    }*/
}
