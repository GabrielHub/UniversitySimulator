using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    //UI text
    public TextMeshProUGUI studentsText;
    public TextMeshProUGUI facultyText;
    public TextMeshProUGUI alumniText;
    public TextMeshProUGUI buildingsText;
	public TextMeshProUGUI wealthText;
    private EventLogScript eventLog; //used to call a function to add lines to the event log

    //Other UI Elements
    public Button playButton;
    public Text playText;

	//resources
	public static int students;
	public static int faculty;
	public static int alumni;
	public static int buildingCount;
    public static Dictionary<string, int> buildings = new Dictionary<string, int> ();
	public static float wealth;

	//other hidden resources
	private float r; //student growth rate r
    private float K; //carrying capacity (size limit) for student growth K
    private float renown = 0.1f; //temporary starting value

    //Ticker/Time variables
    int ticker = 0;
    int eventTicker = 0;

    //other variables
    private bool playing = true; //check if paused or not
    private int eventThreshold;
    private EventController eventController;

    // Start is called before the first frame update
    void Start()
    {
    	//Resource list
        studentsText.text = "Students: " + students.ToString();
        facultyText.text = "Faculty: " + faculty.ToString();
        alumniText.text = "Alumni: " + alumni.ToString();
        buildingsText.text = "Buildings: " + buildingCount.ToString();
		wealthText.text = "Wealth: "+ wealth.ToString();

        //Get event log script component
        eventLog = GetComponent<EventLogScript> ();
        /*
            To add events, use eventLog.AddEvent("Sample String");
            Max lines can be changed in the editor
        */
        eventController = GetComponent<EventController> ();

        //Button Setup //Calls the TaskOnClick/TaskWithParameters/ButtonClicked method when you click the Button
        playButton.onClick.AddListener(PauseOnClick);

        //set up random ranges (possibly based on difficulty later)
        students = 55;
		faculty = 10;
		alumni = 1;
		buildingCount = 1;
		wealth = 50;
        //for first event
        eventThreshold = Random.Range(3, 7);

		eventLog.AddEvent("BREAKING: SADU's only alum has taken over for the school!");

        //A turn is done every second, with a 0.5 second delay upon resuming
        InvokeRepeating("Turns", 0.5f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //Resource List to be updated
        studentsText.text = "Students: " + students.ToString();
        facultyText.text = "Faculty: " + faculty.ToString();
        alumniText.text = "Alumni: " + alumni.ToString();
        buildingsText.text = "Buildings: " + buildingCount.ToString();
		wealthText.text = "Wealth: "+ wealth.ToString();
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
            K = 350 * buildingCount + 10 * faculty;
            //renown is only increased when buying buildings (atm)
            if ((wealth / (students + faculty)) > 0) {
                r = ((students + faculty) / wealth) + renown;
            }
            
            //Calculate wealth
            wealth += alumni + (students * 2);

            //Calculate Faculty
            if (faculty < wealth) {
                faculty += buildingCount;
            }

            //Calculate Students
            students += (int) (r * students * ((K - students) / K));

            //Calculate Alumni
            if (students <= 5) {
                alumni += students;
                students = 0;
            }
            else {
                int i = (int)(students / 5);
                students -= i;
                alumni += i;
            }
        }

        //Events
        if (eventTicker == eventThreshold) {
            //regenerate event threshold
            eventThreshold = Random.Range(3, 7);
            eventTicker = 0;
            eventController.DoEvent();
        }

        //check for game over
        if (students <= 0) {
            eventLog.AddEvent("You've run out of students and this University has failed. \n Don't be sad it happened be happy it's over");
            CancelInvoke();
        }
        else if (alumni >= 500000) {
            eventLog.AddEvent("Congrats! You have as much alumni as NYU! What else do you want, a cookie?");
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
