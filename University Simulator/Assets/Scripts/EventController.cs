using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HiddenResources {

}

public struct Resources {
    public int faculty;
    public int alumni;
    public int students;
    public int wealth;
    public int buildingCount;

    public Resources(int faculty = 0, int alumni = 0, int students = 0, int wealth = 0, int buildingCount = 0) {
        this.faculty = faculty;
        this.alumni = alumni;
        this.students = students;
        this.wealth = wealth;
        this.buildingCount = buildingCount;
    }

    public static Resources operator+(Resources left, Resources right) {
        return new Resources(
            wealth: left.wealth + right.wealth,
            faculty: left.faculty + right.faculty,
            alumni: left.alumni + right.alumni,
            students: left.students + right.students,
            buildingCount: left.buildingCount + right.buildingCount
        );
    }
}

public struct Event {
    public string text;
    public Resources modifiers;

    public Event(string text, Resources modifiers = new Resources()) {
        this.text = text;
        this.modifiers = modifiers;
    }
}

public class EventController : MonoBehaviour {
    public interface Listener {
        void EventDidOccur(string eventString);
    }
    private List<Listener> eventListeners;

    // Array of floats indicating the chances of the events below. The first element of array matches with next line of code and so on.
    public float[] probs = new float[9]{ .05f, .05f, .05f, .1f, .2f, .05f, .1f, .20f, .20f };

    //Format for event: "NAME: Description (Actual Effect)"    
    public Event lost_faculty = new Event(
        "Lost Hope: Faculty dislike working out of a shack (- Faculty)",
        new Resources(faculty: -3)
    );
    public Event lost_buildings = new Event(
        "Earthquake: Yeah this seems a little harsh (Nothing Happens)",
        new Resources(/*tbd*/)
    );
    public Event lost_student = new Event(
        "Dangerous Knowledge: Some Chemistry students have been eating their lab materials (- Students)",
        new Resources(students: -10)
    );

    //public const string renown_increase = "Renown and Students: Your math A-team aces the MATH national challenge competition and was broadcasted across all news channels.";
    public Event increase_wealth = new Event(
        "Tax Break 101: One of your accomplished alumni donates a lump sum of MOOLAH (+ Wealth)",
        new Resources(wealth: +2000)
    );
    public Event increase_student_and_wealth = new Event(
        "College Admission Scandal: A couple donations never hurt anybody (+ Wealth, + Students)",
        new Resources(wealth: +500, students: +5)
    );
    public Event increase_building = new Event(
        "Clumsy Builders: We're not doing building stuff yet (Nothing Happens)",
        new Resources(/*tbd*/)
    );
    public Event increase_faculty = new Event(
        "Networking: Sadness should be enjoyed in groups (++ Faculty)",
        new Resources(faculty: +3)
    );
    public Event renown_increase = new Event("Not Yet Implemented");
    public Event neutral = new Event("SAD!: Nothing really happens. Students fail and students graduate. Life finds a way? (Nothing Happens)");

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

    public void DoEvent() {
        Event[] events = new Event[9]{ 
            lost_faculty, 
            lost_buildings, 
            lost_student, 
            renown_increase, 
            increase_wealth, 
            increase_student_and_wealth, 
            increase_building, 
            increase_faculty, 
            neutral
        };
        // Calculates which event should happen and returns the index of the event that is supposed to happen.
        Event e = events[ChooseEvent(probs)];
        GameManagerScript.instance.resources += e.modifiers;
        eventLog.AddEvent(e);
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
