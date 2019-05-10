using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController: MonoBehaviour {
    public interface Listener {
        void EventDidOccur(Event e);
    }
    private List<Listener> eventListeners = new List<Listener>();

    // Array of floats indicating the chances of the events below. The first element of array matches with next line of code and so on.
    public float[] probs = new float[7]{ .05f, .05f, .05f, .1f, .2f, .05f, .1f };

    //Format for event: "NAME: Description (Actual Effect)"    
    public Event lost_faculty = new Event(
        "Lost Hope: Faculty dislike working out of a shack (- Faculty)",
        "Random",
        new Resources(faculty: -3)
    );
    public Event lost_buildings = new Event(
        "Earthquake: Yeah this seems a little harsh (Nothing Happens)",
        "Random",
        new Resources(/*tbd*/)
    );
    public Event lost_student = new Event(
        "Dangerous Knowledge: Some Chemistry students have been eating their lab materials (- Students)",
        "Random",
        new Resources(students: -10)
    );

    //public const string renown_increase = "Renown and Students: Your math A-team aces the MATH national challenge competition and was broadcasted across all news channels.";
    public Event increase_wealth = new Event(
        "Tax Break 101: One of your accomplished alumni donates a lump sum of MOOLAH (+ Wealth)",
        "Random",
        new Resources(wealth: +2000)
    );
    public Event increase_student_and_wealth = new Event(
        "College Admission Scandal: A couple donations never hurt anybody (+ Wealth, + Students)",
        "Random",
        new Resources(wealth: +500, students: +5)
    );
    public Event increase_building = new Event(
        "Clumsy Builders: We're not doing building stuff yet (Nothing Happens)",
        "Random",
        new Resources(/*tbd*/)
    );
    public Event increase_faculty = new Event(
        "Networking: Sadness should be enjoyed in groups (++ Faculty)",
        "Random",
        new Resources(faculty: +3)
    );


    private EventLogScript eventLog;
    private GameManagerScript gameManager;

    public void RegisterListener(Listener listener) {
        this.eventListeners.Add(listener);
    }

    public void DeregisterListener(Listener listener) {
        this.eventListeners.Remove(listener);
    }
  

    // Start is called before the first frame update
    void Start() {
        eventLog = GetComponent<EventLogScript>();
        gameManager = GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    /// Does a specific event.
    public void DoEvent(Event e) {
        //GameManagerScript.instance.resources += e.modifiers; //This might cause an issue in the future for now uh idk
        foreach (Listener l in eventListeners) {
            l.EventDidOccur(e);
        }
    }

    /// Does a random event from the list
    public void DoEvent() {
        Event[] events = new Event[7]{ 
            lost_faculty, 
            lost_buildings, 
            lost_student, 
            increase_wealth, 
            increase_student_and_wealth, 
            increase_building, 
            increase_faculty
        };

        // Calculates which event should happen and executes the appropriate event.
        this.DoEvent(events[ChooseEvent(probs)]);
    }

    public int ChooseEvent(float[] probs) {

        float total = 0;
        foreach (float elem in probs) {
            total += elem;
        }

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++) {
            if (randomPoint < probs[i]) {
                return i;
            }
            else {
                randomPoint -= probs[i];
            }
        }
        return probs.Length - 1;
    }
}
