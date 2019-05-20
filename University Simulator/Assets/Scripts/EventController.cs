using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class EventController : MonoBehaviour {
	public interface Listener {
		void EventDidOccur(Event e);
	}
	private List<Listener> eventListeners = new List<Listener>();

	// Array of floats indicating the chances of the events below. The first element of array matches with next line of code and so on.
	public float[] probs = new float[21] { .25f, .10f, .05f, .25f, .10f, .05f, .25f, .10f, .05f, .25f, .10f, .05f, .25f, .10f, .05f, .25f, .10f, .05f, .15f, .15f, .03f };

	//Format for event: "NAME: Description (Actual Effect)"    
	public Event faculty_decrease_small = new Event(
		"Faculty Complaints: A faculty member took another's food from the break room, his disappointment is immeasurable and his day is ruined | Faculty-",
		Event.Type.Random,
		new Resources(faculty: -2)
	);
	public Event faculty_decrease_medium = new Event(
		"Lost Hope: A couple failures here and there are acceptable, but this takes the cake | Faculty--",
		Event.Type.Random,
		new Resources(faculty: -5)
	);
	public Event faculty_decrease_large = new Event(
		"Chemistry Conundrum: Someone put the Caesium in the wrong disposal but at least the explosion looked cool | Faculty--- Students-",
		Event.Type.Random,
		new Resources(faculty: -10, students: -35)
	);
	public Event faculty_increase_small = new Event(
		"Team Building Exercises: We took the remaining budget, funded a Laser-Tag faculty event, and these are the results | Faculty+ Wealth-",
		Event.Type.Random,
		new Resources(faculty: 2, wealth: -200)
	);
	public Event faculty_increase_medium = new Event(
		"Hot Dog Innovation: Researchers from your University have developed a self-cooking hotdog | Faculty++",
		Event.Type.Random,
		new Resources(faculty: 7)
	);
	public Event faculty_increase_large = new Event(
		"Hot Dog Time Machine: Researchers from the self-cooking hotdog team inadvertantly discovered time travel | Faculty+++",
		Event.Type.Random,
		new Resources(faculty: 15)
	);

	public Event student_decrease_small = new Event(
		"College Dropouts: 'Fuck them kids' - Michael JEFFREY Jordan | Students-",
		Event.Type.Random,
		new Resources(students: -250)
	);
	public Event student_decrease_medium = new Event(
		"Banned Frat: 1 Coked Out Zac Efron, 2 Molotov Cocktails, 3 Dead Cocker-Spaniels, 4 ok just ban them | Students--",
		Event.Type.Random,
		new Resources(students: -350)
	);
	public Event student_decrease_large = new Event(
		"Bad Rep: The only thing lower than your STD rate is your graduation rate | Students--- Faculty--",
		Event.Type.Random,
		new Resources(faculty: -10, students: -500)
	);
	public Event student_increase_small = new Event(
		"State Finals: The hockey team? Or was it the ultimate frisbee team? Whatever we got on National TV | Students+",
		Event.Type.Random,
		new Resources(students: 200)
	);
	public Event student_increase_medium = new Event(
		"Project Y: Rumors of a spectacular party have done wonders for advertising | Students++ Wealth--",
		Event.Type.Random,
		new Resources(students: 400, wealth: -500)
	);
	public Event student_increase_large = new Event(
		"Mr. Worldwide: Big boost in international applications | Students+++",
		Event.Type.Random,
		new Resources(students: 1000)
	);

	public Event wealth_decrease_small = new Event(
		"Financial Aid: The money's gotta come from somewhere | Wealth-",
		Event.Type.Random,
		new Resources(wealth: -800)
	);
	public Event wealth_decrease_medium = new Event(
		"HBO No: That's right you weasles free HBO for- since when is Game of Thrones is endi- the last season can't be that ba- | Wealth--",
		Event.Type.Random,
		new Resources(wealth: -1500)
	);
	public Event wealth_decrease_large = new Event(
		"Settlements: Your [REDACTED] may have [REDACTED] but at least she didn't [REDACTED] like [REDACTED| Wealth--- Faculty-",
		Event.Type.Random,
		new Resources(wealth: -3000, faculty: -2)
	);
	public Event wealth_increase_small = new Event(
		"Tax Break 101: Sometimes it do grow on trees | Wealth+",
		Event.Type.Random,
		new Resources(wealth: 650)
	);
	public Event wealth_increase_medium = new Event(
		"Sellouts: 'Hulu has live sports' | Wealth++",
		Event.Type.Random,
		new Resources(wealth: 2500)
	);
	public Event wealth_increase_large = new Event(
		"[REDACTED]: Just some donations nothing to see here | Wealth+++ Students+",
		Event.Type.Random,
		new Resources(students: 10, wealth: 5000)
	);

	public Event alumni_decrease = new Event(
		"Untimely Demise: Somebody catered the bad shrimp for the class of 1975 | Alumni--",
		Event.Type.Random,
		new Resources(alumni: -35)
	);
	public Event alumni_increase = new Event(
		"Honorary Degrees: omg im literally shaking we just gave a degree to two time oscar winner Brendan Fraser | Wealth+++ Students+",
		Event.Type.Random,
		new Resources(students: 10, wealth: 5000)
	);

	public Event admissions_scandal = new Event(
		"Admissions Scandal: pLaUsIbLe DeNiAbiLiTy | Wealth++ Students- Faculty-",
		Event.Type.Random,
		new Resources(students: -10, faculty: -3, wealth: 1000)
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

	/// Does a specific event.
	public void DoEvent(Event e) {
		GameManagerScript.instance.resources += e.modifiers;
		foreach (Listener l in eventListeners) {
			l.EventDidOccur(e);
		}
	}

	/// Does a random event from the list
	public void DoEvent() {
		Event[] events = new Event[21] {
			faculty_decrease_small,
			faculty_decrease_medium,
			faculty_decrease_large,
			faculty_increase_small,
			faculty_increase_medium,
			faculty_increase_large,
			student_decrease_small,
			student_decrease_medium,
			student_decrease_large,
			student_increase_small,
			student_increase_medium,
			student_increase_large,
			wealth_decrease_small,
			wealth_decrease_medium,
			wealth_decrease_large,
			wealth_increase_small,
			wealth_increase_medium,
			wealth_increase_large,
			alumni_decrease,
			alumni_increase,
			admissions_scandal
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
