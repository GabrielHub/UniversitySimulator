using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour {
    public static GameManagerScript instance;

    // For testing purposes
    //public TextMeshProUGUI r_rate;
    //public TextMeshProUGUI k_rate;

	//resources
    public Resources resources;

    //Per Turn Display Stats
    public Resources resourcesDelta;

    //sliders
    public Slider tuitionSlider;
    public Slider donationSlider;
    public Slider acceptanceRateSlider;
    public Transform sliderContentPanel; //These sliders need to be added dynamically once the right game phase is active
    public GameObject salarySliderPrefab;
    private Slider salarySlider;
    public GameObject facultyRatioSliderPrefab;
    private Slider facultyRatioSlider;

    //Ticker/Time variabless
    public int ticker = 0;
    private int eventTicker = 0; //time between events, resets after every event
    private int agreementTicker = 0; //time between new purchasable HS agreements
    private int eventThreshold; //time until events, changes after every event
    private int agreementThreshold; //time until new purchasable HS agreements
    private int negativeWealthTicker = 5;
    private int specialStudentTicker = 0; //threshold for this ticker is found in ResourcesMidGame

    //other variables
    public bool playing = true; //check if paused or not
    public enum GameState {EarlyGame1, EarlyGame2, EarlyGame3, EarlyGame4, MidGame, EndGame};
    public GameState state;
    [HideInInspector] // prevent this from being selectable in the inspector
    public EventController eventController; //script for events

    //Buy Menu
    public TMP_Dropdown buyMenu;
    //For UUpgrades in the Buy Menu
    public Transform contentPanel; //The content object that we're attaching upgrade buttons to
    public GameObject upgradeButton; //Button prefab for the upgrade object
    public List<UpgradeBase> upgradeList; //list of all the upgrades that have been available
    //For SpecialStudents in the Buy Menu
    private RNGSpecialStudent specialStudentRNG; //Pool of randomly generated special students, use to get a special student object
    public TMP_Text specialSDescriptionText; // Change the text when special students are enabled or disabled
    public Transform specialStudentContentPanel; //The content object that we're attaching special student buttons to
    public GameObject specialStudentButton; //Button prefab for the special student object
    public List<SpecialStudent> specialStudentList; //list of all the special students that have been available

    //EarlyGame Resources
    public HighSchoolAgreement[] agreements; //purchasable agreements
    public BuyAgreementScript BuyHSA1;
    public BuyAgreementScript BuyHSA2;
    public BuyAgreementScript BuyHSA3;
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

        resources = new Resources();
        state = GameState.EarlyGame1; //Set early game state, now disable all features not available in the early game

        //Initial upgrades that are available
        upgradeList = new List<UpgradeBase> ();
        UpgradeAdministrator upgradeAdmin = new UpgradeAdministrator();
        //AddUpgradable(upgradeAdmin); //Add Hire Administrators upgrade

        //Initial List of special students
        specialStudentList = new List<SpecialStudent> ();
        specialStudentRNG = new RNGSpecialStudent();

        //set up  ranges (possibly based on difficulty later)
        this.resources.students = 1;
		this.resources.faculty = 1;
		this.resources.alumni = 1;
		this.resources.wealth = 1;

        //initial purchasable agreements
        agreements = new HighSchoolAgreement[3];
        BuyHSA1.gameObject.SetActive(false);
        BuyHSA2.gameObject.SetActive(false);
        BuyHSA3.gameObject.SetActive(false);

        //Start timer thresholds
        eventThreshold = Random.Range(2, 10);
        agreementThreshold = Random.Range(15, 30);

        //starting dialogue
        this.eventController.DoEvent(new Event("The 'Online University' is unrecognized. Let's begin (Press P to pause or resume)", "Narrative"));

        //A turn is done every second, with a 0.5 second delay upon resuming
        InvokeRepeating("Turns", 0.5f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //pause control by pressing key
        if (Input.GetKeyDown(KeyCode.P)) {
            this.playing = !this.playing;
        }
    }

    //take into account all policy changes and changes in resources, then update said resources
    void Turns() {
        //Check whether game is paused or not
        if (playing) {
            //ticker values
            ticker++;
            eventTicker++;
            agreementTicker++;
            specialStudentTicker++;

            /*
            - First stage of the game you only have students and wealth. Buy faculty upgrades
            - Next stage at 50 students unlocks High School Agreements
            - After you hit 100 students you can unlock the ability to graduate students and create alumni. These will provide a permanent boost to wealth growth. Use this to buy HSA agreements. Unlock sliders
            - After 500 students, renown and happiness now affect student capacity. Unlock advanced statistics upgrade. 
            - After 1000 students you can now move onto the midgame.
            */

            //Building calculations, MAKE SURE THIS IS ALWAYS CALCULATED FIRST, only run every time a new building is added
            if (state == GameState.MidGame && this.resources.buildings.Count == 0) { //add the first building
                this.resources.ApplyBuildingCalculations(new ResidentialBuilding(500, 3, 0));
            }

            //calculate HS Agreements, only done in early game
            if (state == GameState.EarlyGame2 || state == GameState.EarlyGame3) {
                this.resources.calcHSAgreements();
            }

            //acceptance rate
            if (state == GameState.EarlyGame4 || state == GameState.MidGame) {
                this.resources.calcAcceptanceRate(acceptanceRateSlider.value);
            }

            //happiness. Optimal value is currently set to half the max value
            if (state == GameState.EarlyGame4 || state == GameState.MidGame) {
                this.resources.calcHappiness(tuitionSlider.value, tuitionSlider.maxValue, donationSlider.value, donationSlider.maxValue);
            }

            //renown, isn't calculated in Early Game because it's calculated by HSA in the earlygame
            if (state == GameState.MidGame) {
                this.resources.calcRenown(salarySlider.value);
            }

            //Calculate R: The student growth rate
            this.resources.calcR();

            //Calculate K: The student capacity
            this.resources.calcK();

            //MAIN RESOURCES: Always calcualte these 4 resources LAST
            //Calculate wealth
            this.resourcesDelta.wealth = this.resources.calcWealth(donationSlider.value, tuitionSlider.value);

            //Calculate Faculty, only auto calculated in the midgame, you need to manually increase faculty in the early earlygame
            if (state == GameState.EarlyGame4 || state == GameState.MidGame) {
                this.resourcesDelta.faculty = this.resources.calcFaculty();
            }
            
            //Calculate Students
            this.resourcesDelta.students = this.resources.calcStudents(tuitionSlider.maxValue + donationSlider.maxValue);

            //Calculate Alumni
            if (state == GameState.EarlyGame3 || state == GameState.EarlyGame4 || state == GameState.MidGame) {
                this.resourcesDelta.alumni = this.resources.calcAlumni();
            }

            //Update MidGame policy min and max values if values have changed
            if (state == GameState.MidGame) {
                if (facultyRatioSlider.minValue != this.resources.minFaculty) {
                   facultyRatioSlider.minValue = this.resources.minFaculty; 
                }
                if (facultyRatioSlider.maxValue != this.resources.maxFaculty) {
                   facultyRatioSlider.maxValue = this.resources.maxFaculty; 
                }
            }

            //CODE FOR UPGRADES
            //For unlocking Early Game Upgrades, make sure they aren't already added
            if (this.resources.students > 1000 && upgradeList.Count < 2) {
                //Add the Buy Campus and Buy License Upgrades
                UpgradeCampus campusUpgrade = new UpgradeCampus();
                AddUpgradable(campusUpgrade);

                UpgradeLicense licenseUpgrade = new UpgradeLicense();
                AddUpgradable(licenseUpgrade);
            }
            if (resources.wealth < 0)
            {
                negativeWealthTicker -= 1;
                this.eventController.DoEvent(new Event("!!! You are currently in debt. Recover your debt before the collectors shutdown the University.", "Notification"));
            }
            if (negativeWealthTicker < 0)
            {
                this.eventController.DoEvent(new Event("You have been in debt for more than 5 turns and the collectors are at your door.", "GameState"));
                CancelInvoke();
            }

            //CODE FOR SPECIAL STUDENTS
            if (state == GameState.MidGame && this.resources.specialStudentThreshold == specialStudentTicker) {
                specialStudentTicker = 0;

                if (Random.Range(0.0f, 1.0f) <= this.resources.ssProb) {
                    AddSpecialStudent();
                }
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
        if (state == GameState.EarlyGame2 || state == GameState.EarlyGame3 || state == GameState.EarlyGame4) {
            if (agreementTicker == agreementThreshold) {
                this.eventController.DoEvent(new Event("!!!: New HS Agreements are available!", "Notification"));

                //run generation function
                string[] name = RandomAgreements.instance.ChooseName(3);
                for (int i = 0; i < 3; i++) {
                    agreements[i] = RandomAgreements.instance.generateAgreement(name[i]);
                }

                agreementThreshold = Random.Range(4, 18); //use this to change time between new agreements
                agreementTicker = 0;

                //enable every window if they were purchased before
                BuyHSA1.gameObject.SetActive(true);
                BuyHSA2.gameObject.SetActive(true);
                BuyHSA3.gameObject.SetActive(true);
            }
        }

        //check for game over, or game win, or gamestate changes
        if (resources.students <= 0) {
            this.eventController.DoEvent(new Event("You've run out of students. Don't be sad it happened be happy it's over.", "GameState"));
            CancelInvoke();
        }
        //move from earlygame stages. NEED TO ADD UPGRADES TO EACH STAGE AS A GATE TO PROGRESSION
        if (state == GameState.EarlyGame1 && this.resources.students >= 50) {
            state = GameState.EarlyGame2;
            this.eventController.DoEvent(new Event("You've heard a rumor that some High Schools will take a bribe to push students to your school...", "Narrative"));
            this.playing = !this.playing;
        }
        else if (state == GameState.EarlyGame2 && this.resources.students >= 100) {
            state = GameState.EarlyGame3;
            this.eventController.DoEvent(new Event("With this many students maybe you can start giving out degrees", "Narrative"));
            this.playing = !this.playing;
        }
        else if (state == GameState.EarlyGame3 && this.resources.students >= 500) {
            state = GameState.EarlyGame4;
            this.eventController.DoEvent(new Event("Students care about happiness and renown. Who knew? Maybe we need to start changing our policies to keep growing", "Narrative"));
            this.playing = !this.playing;
        }
        //check if early game is finished
        if (earlyGameRequirements == 2 && state == GameState.EarlyGame4) {
            this.eventController.DoEvent(new Event("A University is a business, and business is good...", "Narrative"));
            state = GameState.MidGame;
            MoveToMidGame();
        }
    }

    //Add A Purchasable Upgrades
    void AddUpgradable(UpgradeBase item) {
        //Create button prefab and attach it to the content panel
        GameObject buttonCreation = Instantiate(upgradeButton, contentPanel);
        UpgradeBuyButton buttonScript = buttonCreation.GetComponent<UpgradeBuyButton> ();
        buttonScript.Setup(item); //pass upgrade object into the button

        upgradeList.Add(item);
    }

    //Add A Special Student
    void AddSpecialStudent() {
        SpecialStudent newStudent = specialStudentRNG.GenerateStudent();
        specialStudentList.Add(newStudent);
        this.resources.AddSpecialStudent(newStudent);
    }

    //Add Building
    void AddBuilding(Building b) {
        this.resources.buildings.Add(b);
        this.resources.ApplyBuildingCalculations(b);
    }

    //Code when moving to the MIDGAME
    void MoveToMidGame() {
        //disable early game features
        //buyMenu.options.RemoveAt(0); //remove HSA Buy Option

        //Create sliders and attach them to their content panel
        GameObject sliderCreation = Instantiate(salarySliderPrefab, sliderContentPanel);
        salarySlider = sliderCreation.GetComponent<Slider> ();
        GameObject sliderCreation2 = Instantiate(facultyRatioSliderPrefab, sliderContentPanel);
        facultyRatioSlider = sliderCreation2.GetComponent<Slider> ();
        this.eventController.DoEvent(new Event("NEW POLICIES: Student-Faculty decides how many students a faculty can handle. Faculty Salary determines how much you pay faculty.", "Feature"));

        facultyRatioSlider.minValue = this.resources.minFaculty; //Make sure faculty to student ratio can be accomodated for
        facultyRatioSlider.maxValue = this.resources.maxFaculty;

        //Convert resources to MidGame Resources class
        resources = new ResourcesMidGame(resources);
    }
}
