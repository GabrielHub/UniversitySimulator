﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    //UI text
    public TextMeshProUGUI studentsText;
    public TextMeshProUGUI facultyText;
    public TextMeshProUGUI alumniText;
    public TextMeshProUGUI buildingsText;
	public TextMeshProUGUI wealthText;

    //Other UI Elements
    public Button playButton;
    public Text playText;

	//resources
    public Resources resources;
    //public static Dictionary<string, int> buildings = new Dictionary<string, int> ();

    //sliders
    public Slider tuitionSlider;
    public Slider donationSlider;
    public Slider acceptanceRateSlider;

    //Ticker/Time variables
    int ticker = 0; //unused atm
    int eventTicker = 0; //time between events, resets after every event

    //other variables
    private bool playing = true; //check if paused or not
    private int eventThreshold; //time until events, changes after every event
    [HideInInspector] // prevent this from being selectable in the inspector
    public EventController eventController; //script for events

    private void Awake() {
        if (GameManagerScript.instance == null) {
            GameManagerScript.instance = this;
        } else {
            Destroy(this);
        }

        eventController = GetComponent<EventController> ();
    }

    // Start is called before the first frame update
    void Start() {
        /*
            To add events, use eventController.DoEvent(new Event("Sample String"));
            Max lines can be changed in the editor
        */

        //Button Setup //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        playButton.onClick.AddListener(PauseOnClick);

        //set up  ranges (possibly based on difficulty later)
        this.resources.students = 45;
		this.resources.faculty = 10;
		this.resources.alumni = 1;
		this.resources.buildingCount = 3;
		this.resources.wealth = 50;

        this.resources.studentPool = 100; //start the game off with a limit of 100 students

        //for first event
        eventThreshold = Random.Range(3, 7);

        this.eventController.DoEvent(new Event("BREAKING: Crazy person declares themselves alumnus for non-existent University!"));

        //A turn is done every second, with a 0.5 second delay upon resuming
        InvokeRepeating("Turns", 0.5f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Resource List to be updated
        studentsText.text = "Students: " + this.resources.students.ToString();
        facultyText.text = "Faculty: " + this.resources.faculty.ToString();
        alumniText.text = "Alumni: " + this.resources.alumni.ToString();
        buildingsText.text = "Buildings: " + this.resources.buildingCount.ToString();
		wealthText.text = "Wealth: "+ this.resources.wealth.ToString();
    }

    //take into account all policy changes and changes in resources, then update said resources
    void Turns() {
        //Check whether game is paused or not
        if (playing) {
            //ticker values
            //eventController.DoEvent();
            ticker++;
            eventTicker++;

            //calculate hidden values
            //K = 350 * buildingCount + 10 * faculty; This algorithm will be used when buildings can be bought
            //K = studentPool;
            //renown is only increased when buying buildings (atm)
            //r = ((resources.students + resources.faculty) / resources.wealth) + renown;
            //acceptance rate
            this.resources.calcAcceptanceRate(acceptanceRateSlider.value);
            //happiness. Optimal value is currently set to half the max value
            this.resources.calcHappiness(tuitionSlider.value, tuitionSlider.maxValue, donationSlider.value, donationSlider.maxValue);

            //Calculate wealth
            this.resources.calcWealth(donationSlider.value, tuitionSlider.value);

            //Calculate Faculty
            this.resources.calcFaculty();

            //Calculate Students
            this.resources.calcStudents();

            //Calculate Alumni
            this.resources.calcAlumni();
        }

        //Events
        if (eventTicker == eventThreshold) {
            //regenerate event threshold, reset time ot next event, and then do an event
            eventThreshold = Random.Range(2, 10); //2 to 10 seconds
            eventTicker = 0;
            eventController.DoEvent();
        }

        //check for game over
        if (resources.students <= 0) {
            this.eventController.DoEvent(new Event("You've run out of students and this University has failed. \n Don't be sad it happened be happy it's over"));
            CancelInvoke();
        }
        else if (resources.alumni >= 500000) {
            this.eventController.DoEvent(new Event("Congrats! You have as many alumni as NYU!"));
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
}
