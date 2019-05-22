using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {
	public static GameManagerScript instance;

	public class GameStateChangeHandler : MessageHandler<GameState.ShouldChange> {
		GameManagerScript gm;
		public GameStateChangeHandler(GameManagerScript gm) : base() {
			this.gm = gm;
		}

		public override void handleTypedMessage(GameState.ShouldChange m) {
			// handle setup between the states
			switch (m.state) {
			case GameState.State.EarlyGame1: {
                gm.resources = new Resources(faculty: 1, alumni: 0, students: 1, wealth: 10); //IMPORANT: ALUNI is 0 FOR A REASON DONT CHANGE IT DUMMY

                gm.state = GameState.State.EarlyGame1; //Set early game state, now disable all features not available in the early game

                //Initial upgrades that are available
                gm.upgradeList = new List<UpgradeBase>();
                UpgradeHireFaculty upgradeFaculty = new UpgradeHireFaculty(0);
                gm.AddUpgradable(upgradeFaculty); //Add Hire Administrators upgrade

                //Initial List of special students
                gm.specialStudentList = new List<SpecialStudent>();
                gm.specialStudentRNG = new RNGSpecialStudent();

                //initial purchasable agreements
                gm.agreements = new HighSchoolAgreement[3];
                gm.BuyHSA1.gameObject.SetActive(false);
                gm.BuyHSA2.gameObject.SetActive(false);
                gm.BuyHSA3.gameObject.SetActive(false);

                //Start timer thresholds
                gm.eventThreshold = Random.Range(2, 10);
                gm.agreementThreshold = Random.Range(15, 30);

                //starting dialogue
                gm.eventController.DoEvent(new Event("'YoU cOuLd ToTaLlY mAkE a BeTtEr UnIvErSiTy ThE sYsTeM iS bRoKeN', maybe you shouldn't have listened to that guy", Event.Type.Narrative));
            } break;
			case GameState.State.EarlyGame2: {
                gm.eventController.DoEvent(new Event("You've heard a rumor that some High Schools will take a bribe to push students to your school...", Event.Type.Narrative));
                gm.playing = !gm.playing;

                //Add the first agreement
                gm.resources.agreements.Add(new HighSchoolAgreement("Init HS", 15, 4, 0));

                //run generation function for HSA
                string[] name = RandomAgreements.instance.ChooseName(3);
                for (int i = 0; i < 3; i++) {
                    gm.agreements[i] = RandomAgreements.instance.generateAgreement(name[i]);
                }

                gm.agreementThreshold = Random.Range(4, 18); //use this to change time between new agreements
                gm.agreementTicker = 0;

                //enable every window if they were purchased before
                gm.BuyHSA1.gameObject.SetActive(true);
                gm.BuyHSA2.gameObject.SetActive(true);
                gm.BuyHSA3.gameObject.SetActive(true);
            } break;
			case GameState.State.EarlyGame3: break; // no-op
			case GameState.State.EarlyGame4: {
                //Add the Buy Campus and Buy License Upgrades
                UpgradeCampus campusUpgrade = new UpgradeCampus();
                gm.AddUpgradable(campusUpgrade);

                UpgradeLicense licenseUpgrade = new UpgradeLicense();
                gm.AddUpgradable(licenseUpgrade);

                gm.eventController.DoEvent(new Event("Students care about happiness and renown. Who knew? Maybe we need to start changing our policies to keep growing", Event.Type.Narrative));
                gm.playing = !gm.playing;

                UpgradeAdministrator upgradeAdmin = new UpgradeAdministrator();
                gm.AddUpgradable(upgradeAdmin); //Add Hire Administrators upgrade
            } break;
			case GameState.State.MidGame: {
                gm.eventController.DoEvent(new Event("A University is a business, and business is good...", Event.Type.Narrative));

                //disable early game features
                //buyMenu.options.RemoveAt(0); //remove HSA Buy Option

                //Create sliders and attach them to their content panel
                GameObject sliderCreation = Instantiate(gm.salarySliderPrefab, gm.sliderContentPanel);
                gm.salarySlider = sliderCreation.GetComponent<Slider>();
                GameObject sliderCreation2 = Instantiate(gm.facultyRatioSliderPrefab, gm.sliderContentPanel);
                gm.facultyRatioSlider = sliderCreation2.GetComponent<Slider>();
                gm.eventController.DoEvent(new Event("NEW POLICIES: Student-Faculty decides how many students a faculty can handle. Faculty Salary determines how much you pay faculty.", Event.Type.Feature));

                gm.facultyRatioSlider.minValue = gm.resources.minFaculty; //Make sure faculty to student ratio can be accomodated for
                gm.facultyRatioSlider.maxValue = gm.resources.maxFaculty;

                //Convert resources to MidGame Resources class
                gm.resources = new ResourcesMidGame(gm.resources);

                MessageBus.main.emit(new GameState.DidChange(from: GameState.State.EarlyGame4, to: GameState.State.MidGame));
            } break;
			case GameState.State.EndGame: throw new System.Exception("Not yet implemented");
			}

			// change the state
			GameState.State old = gm.state;
			gm.state = m.state;
			// Emit a change message so other things can handle it
			MessageBus.main.emit(new GameState.DidChange(from: old, to: gm.state));
		}
	}

	GameStateChangeHandler changeHandler;

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
    public TextMeshProUGUI yearText;
	public GameObject pauseOverlay;

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
	public GameState.State state;
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
	public bool upgradeAlumniBool = false;

	//Music and Sound Effects
	private AudioClip[] music = new AudioClip[5];
	public AudioClip m1; //Music sources
	public AudioClip m2;
	public AudioClip m3;
	public AudioClip m4;
	public AudioClip m5;
	public AudioSource musicAudioSource;
	public AudioSource soundFXSource;
	public AudioClip buttonSound;
	public AudioClip notificationSound;
	public AudioClip insufficientSound;
	public enum soundType { NOTIFICATION, BUTTON, INSUFFICIENT };

	[HideInInspector]
	public float turnTime = 1f;

	private void Awake() {
		if (GameManagerScript.instance == null) {
			GameManagerScript.instance = this;
		}
		else {
			Destroy(this);
		}

		eventController = GetComponent<EventController>();
		this.changeHandler = new GameStateChangeHandler(this);
	}

	// Start is called before the first frame update
	void Start() {
		MessageBus.main.emit(new GameState.ShouldChange(GameState.State.EarlyGame1));

		//setup music
		music[0] = m1;
		music[1] = m2;
		music[2] = m3;
		music[3] = m4;
		music[4] = m5;
		PlayNextSong();
	}

	// Update is called once per frame
	private float turnTimer = 0f;
	void Update() {
		CheckPause();
		pauseOverlay.SetActive(!this.playing);
		turnTimer += Time.deltaTime;
		if (turnTimer >= this.turnTime) {
			TakeTurn();
			turnTimer = 0f;
		}
	}

	void CheckPause() {
		//pause control by pressing key
		if (Input.GetKeyDown(KeyCode.P)) {
			this.playing = !this.playing;
			PlaySound(soundType.BUTTON);
		}
	}

	//take into account all policy changes and changes in resources, then update said resources
	void TakeTurn() {
		//Check whether game is paused or not
		if (playing) {
			//ticker values
			ticker++;
			eventTicker++;
			agreementTicker++;
			specialStudentTicker++;

            yearText.text = (1831 + ticker).ToString();

			//Do Narrative Events
			if (ticker == 6) {
				this.eventController.DoEvent(new Event("Hiring more faculty would help us get more students (Press P to pause or resume)", Event.Type.Narrative));
				this.playing = !this.playing;
			}
			else if (ticker == 12) {
				//this.eventController.DoEvent(new Event("This thing is picking up, Online College Classes sounds like a good idea", "Narrative"));
			}

			/*
			- First stage of the game you only have students and wealth. Buy faculty upgrades
			- Next stage at 50 students unlocks High School Agreements
			- After you hit 100 students you can unlock the ability to graduate students and create alumni. These will provide a permanent boost to wealth growth. Use this to buy HSA agreements. Unlock sliders
			- After 500 students, renown and happiness now affect student capacity. Unlock advanced statistics upgrade. 
			- After 1000 students you can now move onto the midgame.
			*/

			//Building calculations, MAKE SURE THIS IS ALWAYS CALCULATED FIRST, only run every time a new building is added
			if (state == GameState.State.MidGame && this.resources.buildings.Count == 0) { //add the first building
				this.resources.ApplyBuildingCalculations(new Building(Building.Type.Residential, capacity : 500, rating : 3, cost : 0));
			}

			//calculate HS Agreements, only done in early game
			if (state == GameState.State.EarlyGame2 || state == GameState.State.EarlyGame3 || state == GameState.State.EarlyGame4) {
				this.resources.calcHSAgreements();
			}

			//acceptance rate
			if (state == GameState.State.EarlyGame4 || state == GameState.State.MidGame) {
				this.resources.calcAcceptanceRate(acceptanceRateSlider.value);
			}

			//happiness. Optimal value is currently set to half the max value
			if (state == GameState.State.EarlyGame4 || state == GameState.State.MidGame) {
				this.resources.calcHappiness(tuitionSlider.value, tuitionSlider.maxValue, donationSlider.value, donationSlider.maxValue);
			}

			//renown, isn't calculated in Early Game because it's calculated by HSA in the earlygame
			if (state == GameState.State.MidGame) {
				this.resources.calcRenown(salarySlider.value);
				this.resources.calcSSProb();
				this.resources.calcGradRate(facultyRatioSlider.value, facultyRatioSlider.maxValue, facultyRatioSlider.minValue);
				this.resources.ranking = this.resources.calcRanking();
			}

			//Calculate R: The student growth rate
			this.resources.calcR();

			//Calculate K: The student capacity
			this.resources.calcK();

			//MAIN RESOURCES: Always calcualte these 4 resources LAST
			//Calculate wealth
			this.resourcesDelta.wealth = this.resources.calcWealth(donationSlider.value, tuitionSlider.value);

			//Calculate Faculty, only auto calculated in the midgame, you need to manually increase faculty in the early earlygame
			if (state == GameState.State.EarlyGame4 || state == GameState.State.MidGame) {
				this.resourcesDelta.faculty = this.resources.calcFaculty();
			}

			//Calculate Students
			this.resourcesDelta.students = this.resources.calcStudents(tuitionSlider.maxValue + donationSlider.maxValue);

			//Calculate Alumni, only unlocked by earlygame 3
			if (state == GameState.State.EarlyGame3 || state == GameState.State.EarlyGame4 || state == GameState.State.MidGame) {
				this.resourcesDelta.alumni = this.resources.calcAlumni();
			}

			//Update MidGame policy min and max values if values have changed
			if (state == GameState.State.MidGame) {
				if (facultyRatioSlider.minValue != this.resources.minFaculty) {
					facultyRatioSlider.minValue = this.resources.minFaculty;
				}
				if (facultyRatioSlider.maxValue != this.resources.maxFaculty) {
					facultyRatioSlider.maxValue = this.resources.maxFaculty;
				}
			}

			//Code for being in debt
			if (resources.wealth < 0) {
				negativeWealthTicker -= 1;
				this.eventController.DoEvent(new Event("!!! You are currently in debt. Recover your debt before the collectors shutdown the University.", Event.Type.Notification));
				PlaySound(soundType.NOTIFICATION);
			}
			if (negativeWealthTicker < 0) {
				this.eventController.DoEvent(new Event("You have been in debt for more than 5 turns and the collectors are at your door.", Event.Type.GameState));
				CancelInvoke();
			}

			//CODE FOR SPECIAL STUDENTS
			if (state == GameState.State.MidGame && this.resources.specialStudentThreshold == specialStudentTicker) {
				specialStudentTicker = 0;

				if (Random.Range(0.0f, 1.0f) <= this.resources.ssProb) {
					AddSpecialStudent();
				}
			}
		}
		// ALL CODE BELOW IS OUTSIDE OF THE TICKER AND WILL BE RUN EVERY SECOND

		//Events
		if (eventTicker == eventThreshold && state == GameState.State.MidGame) {
			//regenerate event threshold, reset time ot next event, and then do an event
			eventThreshold = Random.Range(5, 20); //use this to change time between events
			eventTicker = 0;
			eventController.DoEvent();
		}

		//Future Event Code Here: Checks for bad stats (if happiness is too low do an event letting you know that people are unhappy)

		//randomized agreements, made sure it's only for the early game
		if (state >= GameState.State.EarlyGame2 && state <= GameState.State.EarlyGame4) {
			if (agreementTicker == agreementThreshold) {
				this.eventController.DoEvent(new Event("!!!: New HS Agreements are available!", Event.Type.Notification));
				PlaySound(soundType.NOTIFICATION);

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
			this.eventController.DoEvent(new Event("You've run out of students. Don't be sad it happened be happy it's over.", Event.Type.GameState));
			CancelInvoke();
		}
		//move from earlygame stages. NEED TO ADD UPGRADES TO EACH STAGE AS A GATE TO PROGRESSION
		if (state == GameState.State.EarlyGame1 && this.resources.students >= 50) {
			state = GameState.State.EarlyGame2;
			MessageBus.main.emit(new GameState.ShouldChange(GameState.State.EarlyGame2));
			// MARK
		}
		else if (state == GameState.State.EarlyGame2 && this.resources.students >= 100 && upgradeAlumniBool == false) {
			//Don't change states immediately, instead add the upgrade which must be bought to move on
			UpgradeAlumni upgradeAlumni = new UpgradeAlumni();
			AddUpgradable(upgradeAlumni);
			this.eventController.DoEvent(new Event("With this many students maybe you can start giving out degrees", Event.Type.Narrative));
			this.playing = !this.playing;
			upgradeAlumniBool = true;
		}
		else if (state == GameState.State.EarlyGame3 && this.resources.students >= 500) {
			MessageBus.main.emit(new GameState.ShouldChange(GameState.State.EarlyGame4));
		}

		//check if early game is finished
		if (earlyGameRequirements == 2 && state == GameState.State.EarlyGame4) {
			MessageBus.main.emit(new GameState.ShouldChange(GameState.State.MidGame));
		}
	}

	//Add A Purchasable Upgrades
	public void AddUpgradable(UpgradeBase item) {
		//Create button prefab and attach it to the content panel
		GameObject buttonCreation = Instantiate(upgradeButton, contentPanel);
		UpgradeBuyButton buttonScript = buttonCreation.GetComponent<UpgradeBuyButton>();
		buttonScript.Setup(item); //pass upgrade object into the button

		upgradeList.Add(item);
	}

	//Add A Special Student
	void AddSpecialStudent() {
		SpecialStudent newStudent = specialStudentRNG.GenerateStudent();
		this.specialStudentList.Add(newStudent);
		this.resources.AddSpecialStudent(newStudent);
	}

	//Add Building
	public void AddBuilding(Building b) {
		this.resources.buildings.Add(b);
		this.resources.ApplyBuildingCalculations(b);
	}

	void PlayNextSong() {
		musicAudioSource.clip = music[Random.Range(0, 5)];
		musicAudioSource.Play();
		Invoke("PlayNextSong", musicAudioSource.clip.length);
	}

	public void PlaySound(soundType type) {
		//if already playing, skip 
		if (soundFXSource.isPlaying) {
			soundFXSource.Stop();
		}

		if (type == soundType.NOTIFICATION) {
			soundFXSource.clip = notificationSound;
		}
		else if (type == soundType.BUTTON) {
			soundFXSource.clip = buttonSound;
		}
		else if (type == soundType.INSUFFICIENT) {
			soundFXSource.clip = insufficientSound;
		}
		else {
			throw new System.Exception($"OOPSIE WOOPSIE WE MADE A FUCKY WUCKY: soundfx type not found");
		}

		soundFXSource.Play();
	}
}
