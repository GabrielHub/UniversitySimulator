using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsController : MonoBehaviour
{
	public Text mainStats;
	public Text mood;
	public Text advancedStats;
	public Text enabledText;

    void Update()
    {
    	//disable all stats text
    	mainStats.text = "";
        mood.text = "";
        advancedStats.text = "";

        if (GameManagerScript.instance.enableStatistics) {
        	enabledText.text = ""; //essentially disabling text

        	//main resources per turn 
        	string students = "Students: " + GameManagerScript.instance.resources.students + "\n";
        	string faculty = "Faculty: " + GameManagerScript.instance.resources.faculty + "\n";
        	string alumni = "Alumni: " + GameManagerScript.instance.resources.alumni + "\n";
        	string wealth = "Wealth: " + GameManagerScript.instance.resources.wealth + "\n";
        	string buildingCount = "Buildings: " + GameManagerScript.instance.resources.buildingCount + "\n";
        	mainStats.text += "Resources Per Turn (TO BE FIXED ONCE BALANCED):\n\n" + students + faculty + alumni + wealth + buildingCount;

        	//University Moods
        	string happiness = "Happiness: " + GameManagerScript.instance.resources.happiness + "\n";
        	string renown = "Renown: " + GameManagerScript.instance.resources.renown + "\n";
        	mood.text += "University Mood:\n\n" + happiness + renown;

        	//Advanced stastics
        	string maxStudents = "Student Capacity: " + GameManagerScript.instance.resources.studentPool + "\n";
        	string studentGrowth = "Student Application Multiplier: " + GameManagerScript.instance.resources.r + "\n";
        	string yearsPassed = "Years: " + GameManagerScript.instance.ticker + "\n";
        	advancedStats.text += "Advanced Statistics:\n\n" + maxStudents + studentGrowth + yearsPassed;
        }
    }
}
