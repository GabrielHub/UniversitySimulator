using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventController : MonoBehaviour
{
    // Array of floats indicating the chances of the events below. The first element of array matches with next line of code and so on.
    public float[] probs = new float[9]{ .05f, .05f, .05f, .1f, .2f, .05f, .1f, .20f, .20f };

    //Format for event: "NAME: Description (Actual Effect)"    
    public const string lost_faculty = "Lost Hope: Faculty dislike working out of a shack (- Faculty)";
    public const string lost_buildings = "Earthquake: Yeah this seems a little harsh (Nothing Happens)";
    public const string lost_student = "Dangerous Knowledge: Some Chemistry students have been eating their lab materials (- Students)";

    //public const string renown_increase = "Renown and Students: Your math A-team aces the MATH national challenge competition and was broadcasted across all news channels.";
    public const string increase_wealth = "Tax Break 101: One of your accomplished alumni donates a lump sum of MOOLAH (+ Wealth)";
    public const string increase_student_and_wealth = "College Admission Scandal: A couple donations never hurt anybody (+ Wealth, + Students)";
    public const string increase_building = "Clumsy Builders: We're not doing building stuff yet (Nothing Happens)";
    public const string increase_faculty = "Networking: Sadness should be enjoyed in groups (++ Faculty)";
    public const string renown_increase = "";
    public const string neutral = "SAD!: Nothing really happens. Students fail and students graduate. Life finds a way? (Nothing Happens)";

    private EventLogScript eventLog;
    private GameManagerScript gameManager;  
  

    // Start is called before the first frame update
    void Start()
    {
        eventLog = GetComponent<EventLogScript>();
        gameManager = GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoEvent() {
        string[] events = new string[9]{ lost_faculty, lost_buildings, lost_student, renown_increase, increase_wealth, increase_student_and_wealth, increase_building, increase_faculty, neutral };

        // Calculates which event should happen and returns the index of the event that is supposed to happen.
        int event_index = (int) ChooseEvent(probs);

        // Matches event string with given index. Here is where the effects of each element
        switch (events[event_index]) {
            case lost_faculty:
                eventLog.AddEvent(lost_faculty);
                GameManagerScript.faculty -= 3;
                break;
            case lost_buildings:
                eventLog.AddEvent(lost_buildings);
                // Not sure how bulidings is done
                break;
            case lost_student:
                eventLog.AddEvent(lost_student);
                GameManagerScript.students -= 10;
                break;
            case renown_increase:
                eventLog.AddEvent(renown_increase);
                // No renown yet
                break;
            case increase_wealth:
                eventLog.AddEvent(increase_wealth);
                GameManagerScript.wealth += 2000;
                break;
            case increase_student_and_wealth:
                eventLog.AddEvent(increase_student_and_wealth);
                GameManagerScript.wealth += 500;
                GameManagerScript.students += 5;
                // Reduce Renown?
                break;
            case increase_building:
                eventLog.AddEvent(increase_building);
                // Buildings Not Implemented Yet!
                break;
            case increase_faculty:
                eventLog.AddEvent(increase_faculty);
                GameManagerScript.faculty += 3;
                break;
            case null:
                eventLog.AddEvent(neutral);
                break;
        }

}


    public float ChooseEvent(float[] probs) {

        float total = 0;
        foreach (float elem in probs) {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++) {
            if (randomPoint < probs[i])
            {
                return i;
            }
            else {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }
}
