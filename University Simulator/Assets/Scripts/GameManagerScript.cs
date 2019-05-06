using System.Collections;
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

    // For testing purposes
    //public TextMeshProUGUI r_rate;
    //public TextMeshProUGUI k_rate;

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
    public int ticker = 0;
    private int eventTicker = 0; //time between events, resets after every event
    private int agreementTicker = 0; //time between new purchasable HS agreements
    private int eventThreshold; //time until events, changes after every event
    private int agreementThreshold; //time until new purchasable HS agreements

    //other variables
    private bool playing = true; //check if paused or not
    [HideInInspector] // prevent this from being selectable in the inspector
    public EventController eventController; //script for events

    //buyables
    public Transform contentPanel; //The content object that we're attaching upgrade buttons to
    public GameObject upgradeButton; //Button prefab for the upgrade object
    public List<UpgradeBase> upgradeList;

    //EarlyGame Resources
    public HighSchoolAgreement[] agreements; //purchasable agreements
    public GameObject BuyHSA1;
    public GameObject BuyHSA2;
    public GameObject BuyHSA3;
    public bool enableStatistics = false;
    public int earlyGameRequirements = 0;

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

        resources = new Resources();

        //Initial upgrades that are available
        upgradeList = new List<UpgradeBase> ();
        UpgradeAdministrator upgradeAdmin = new UpgradeAdministrator();
        AddUpgradable(upgradeAdmin); //Add Hire Administrators upgrade


        //set up  ranges (possibly based on difficulty later)
        this.resources.students = 45;
		this.resources.faculty = 10;
		this.resources.alumni = 1;
		this.resources.buildingCount = 3;
		this.resources.wealth = 50;

        this.resources.studentPool = 100; //start the game off with a limit of 100 students

        //initial purchasable agreements
        agreements = new HighSchoolAgreement[3];

        //Start timer thresholds
        eventThreshold = Random.Range(2, 10);
        agreementThreshold = Random.Range(15, 30);

        //run generation function for initial agreements
        string[] name = RandomAgreements.instance.ChooseName(3);
        for (int i = 0; i < 3; i++) {
            agreements[i] = RandomAgreements.instance.generateAgreement(name[i]);
        }

        //starting dialogue
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

        //Debugging
        Debug.Log("R Value: " + this.resources.r);
        Debug.Log("K Value: " + this.resources.K);
        //r_rate.text = "R: " + this.resources.r_rate.ToString();
        //k_rate.text = "K: " + this.resources.k_rate.ToString();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            PauseOnClick();
        }
    }

    //take into account all policy changes and changes in resources, then update said resources
    void Turns() {
        //Check whether game is paused or not
        if (playing) {
            //ticker values
            //eventController.DoEvent();
            ticker++;
            eventTicker++;
            agreementTicker++;

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
            this.resources.calcStudents(tuitionSlider.maxValue + donationSlider.maxValue);

            //Calculate Alumni
            this.resources.calcAlumni();

            //calculate HS Agreements
            this.resources.calcHSAgreements();


            //CODE FOR UPGRADES
            //Unlocking Early Game Upgrades, make sure they aren't already added
            if (this.resources.students > 1000 && upgradeList.Count < 2) {
                //Add the Buy Campus and Buy License Upgrades
                UpgradeCampus campusUpgrade = new UpgradeCampus();
                AddUpgradable(campusUpgrade);

                UpgradeLicense licenseUpgrade = new UpgradeLicense();
                AddUpgradable(licenseUpgrade);
            }

        }
        // ALL CODE BELOW IS OUTSIDE OF THE TICKER AND WILL BE RUN EVERY SECOND

        //Events
        if (eventTicker == eventThreshold) {
            //regenerate event threshold, reset time ot next event, and then do an event
            eventThreshold = Random.Range(5, 20); //use this to change time between events
            eventTicker = 0;
            //eventController.DoEvent();
        }

        //Future Event Code Here: Checks for bad stats (if happiness is too low do an event letting you know that people are unhappy)

        //randomized agreements, made sure it's only for the early game
        if (resources.gamePhase == 0) {
            if (agreementTicker == agreementThreshold) {
                this.eventController.DoEvent(new Event("!!!: New HS Agreements are available!"));
                Debug.Log("New HS Agreements");

                //run generation function
                string[] name = RandomAgreements.instance.ChooseName(3);
                for (int i = 0; i < 3; i++) {
                    agreements[i] = RandomAgreements.instance.generateAgreement(name[i]);
                }

                agreementThreshold = Random.Range(10, 15); //use this to change time between new agreements
                agreementTicker = 0;

                //enable every window if they were purchased before
                BuyHSA1.SetActive(true);
                BuyHSA2.SetActive(true);
                BuyHSA3.SetActive(true);
            }
        }

        //check for game over, or game win
        if (resources.students <= 0) {
            this.eventController.DoEvent(new Event("You've run out of students and this University has failed. \n Don't be sad it happened be happy it's over"));
            CancelInvoke();
        }
        else if (earlyGameRequirements == 2) {
            this.resources.gamePhase = 1;
            //Unlock buildings, code required below

        }
    }

    //Add Purchasable Upgrades
    void AddUpgradable(UpgradeBase item) {
        //Create button prefab and attach it to the content panel
        GameObject buttonCreation = Instantiate(upgradeButton, contentPanel);
        UpgradeBuyButton buttonScript = buttonCreation.GetComponent<UpgradeBuyButton> ();
        buttonScript.Setup(item); //pass upgrade object into the button

        upgradeList.Add(item);
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
