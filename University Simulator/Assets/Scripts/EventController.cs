using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventController : MonoBehaviour
{
    public float[] probs = new float[9]{ .05f, .05f, .05f, .1f, .2f, .05f, .1f, .20f, .20f };

    
    public const string lost_faculty = "Lost Hope: Faculty thinks SADU needs to provide more than a backyard shed to do their 'HiGh tEch' research. Lost 1 Faculty member.";
    public const string lost_buildings = "Earthquake: Unfortunately, an earthquake destroys and damages all of your buildings even if SADU isn't located in San Francisco. Lose Wealth and Buildings from damages and repairs.";
    public const string lost_student = "Dangerous Knowledge: Students in some very advanced chemistry class was accidentally exposed to organic mercury compound dimethylmercury. Many students died.";

    public const string renown_increase = "Renown and Students: Your math A-team aces the MATH national challenge competition and was broadcasted across all news channels. Increased Renown. ";
    public const string increase_wealth = "Bill Gates: One of your accomplished alumni donates a lump sum of MOLAH into the SADU. Wealth Increases by 10000";
    public const string increase_student_and_wealth = "College Admission Scandal: Very rich people decides to bribe their kids into SADU through the athletics department. SADU allows it but forget about winning any sports competition. Increase Wealth and Student Body. Decreased Renown.";
    public const string increase_building = "Clumsy Builders: Due to SADU's nonexistent renown, builders by accidently built a next-gen classroom in SADU's property that was originally for NYU.";
    public const string increase_faculty = "Networking: Some of your miserable faculty convinced their miserable friends to work at SADU";

    public const string neutral = "Nothing amazing: Nothing really happens. Students fail and students graduate. What more do you want?";

    private EventLogScript eventLog;
    
  

    // Start is called before the first frame update
    void Start()
    {
        eventLog = GetComponent<EventLogScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoEvent() {
        string[] events = new string[9]{ lost_faculty, lost_buildings, lost_student, renown_increase, increase_wealth, increase_student_and_wealth, increase_building, increase_faculty, neutral };
        int event_index = (int) Choose(probs);
        Debug.Log(event_index);
        switch (events[event_index]) {
            case lost_faculty:
                eventLog.AddEvent(lost_faculty);
                break;
            case lost_buildings:
                eventLog.AddEvent(lost_buildings);
                break;
            case lost_student:
                eventLog.AddEvent(lost_student);
                break;
            case renown_increase:
                eventLog.AddEvent(renown_increase);
                break;
            case increase_wealth:
                eventLog.AddEvent(increase_wealth);
                break;
            case increase_student_and_wealth:
                eventLog.AddEvent(increase_student_and_wealth);
                break;
            case increase_building:
                eventLog.AddEvent(increase_building);
                break;
            case increase_faculty:
                eventLog.AddEvent(increase_faculty);
                break;
            case null:
                eventLog.AddEvent(neutral);
                break;
        }

}


    public float Choose(float[] probs) {

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
