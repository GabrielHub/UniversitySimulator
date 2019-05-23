using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Resources {
    public static float MAX_HAPPINESS = 6.5f;
    public static float MAX_RENOWN = 4.952f;

    //5 Main Resources
    public int faculty;
    public int alumni;
    public int students;
    public long wealth;

    //Hidden Resources
    public float r; //student growth rate r

    public float K; //carrying capacity (size limit) for student growth K

    public float renown;
    public float happiness;
    public float acceptanceRate;
    public int studentPool;

    //MidGame Resources (Unused by earlygame, just used in the inherited class)
    public List<Building> buildings;
    public List<SpecialStudent> specialStudents;
    public int specialStudentThreshold; //Time between turns a special student can occur
    public int ranking; //out of 1000
    public float graduationRate;
    public float ssProb; //chance for a special student, between 0 and 1.0f
    public int maxFaculty; //MIN and MAX faculty decide the slider values for student to faculty ratio.
    public int minFaculty;
    public float renownBase; //base renown carried over from HSA agreements in the earlygame

    [SerializeField]
    public List<HighSchoolAgreement> agreements = new List<HighSchoolAgreement> ();

    public Resources(int faculty = 0, int alumni = 0, int students = 0, long wealth = 0) {
        this.faculty = faculty;
        this.alumni = alumni;
        this.students = students;
        this.wealth = wealth;

        //initial values for other variables
        happiness = 7.0f;
        acceptanceRate = 1.0f;
        renown = 1.0f;
        studentPool = 10;
    }

    public static Resources operator+(Resources left, Resources right) {
        left.wealth += right.wealth;
        left.alumni += right.alumni;
        left.students += right.students;
        left.faculty += right.faculty;
        return left;
    }

    //happiness. Optimal value is currently set to half the max value
    public virtual void calcHappiness(float tuition, float tuitionMax, float donation, float donationMax) {
        happiness = (int) ((tuitionMax / 2 - tuition) + (donationMax / 2 - donation));
    }

    //Calculate R the student growth factor.
    public virtual float calcR() {
        r = (happiness / MAX_HAPPINESS) * renown;
        return r;
    }

    //Calculate K, the student capacity (student pool)
    public virtual float calcK() {
        K = (studentPool + faculty * 5) * acceptanceRate;
        //Debug.Log(K.ToString());
        return K;
    }

    //renown, earlygame calculations
    public virtual void calcRenown(float val) {
        //with the avg of hs value, renown below 3.0 will reduce the growth of students
        renown = val - (acceptanceRate * 1.2f);
    }

    //acceptance rate slider calculations, should always be a float between 0.0 and 1.0
    public void calcAcceptanceRate(float val) {
        acceptanceRate = val;
    }

    //wealth. donation and tuition are sliders that change variables in gamemanagerscript, good luck balancing this pos
    public virtual long calcWealth(float donation, float tuition, float unused = 0) {
        long temp = 0;

        if (GameManagerScript.instance.state == GameState.State.EarlyGame1) {
            temp = (long) (students * 0.5);
        }
        else if (GameManagerScript.instance.state == GameState.State.EarlyGame2) {
            temp = students;
        }
        else if (GameManagerScript.instance.state == GameState.State.EarlyGame3) {
            temp = (long) (students + alumni);
        }
        else if (GameManagerScript.instance.state == GameState.State.EarlyGame4) {
            long students_penalty = 1;
            long faculty_penalty = 2 + (faculty / (faculty * 3));
            temp = (long) ((((alumni * donation) + (students * tuition)) / 5) - ((faculty * faculty_penalty) + (students * students_penalty) / 3));
        }
        else {
            Debug.Log("OOPSIE WOOPSIE WE MADE A FUCKYWUCKY: calcwealth in earlygame is out of valid gamestate");
        }

        wealth += temp;

        return temp;
    }

    //faculty.
    public virtual int calcFaculty() {
        int temp;
        if (faculty < wealth) {
            temp = (int) (renown / 2);
        }
        else {
            temp = -1;
        }

        faculty += temp;
        return temp;
    }

    //students
    public virtual int calcStudents() {
        // K - Students accepted out of student pool
        // R - Growth Rate
        int temp = 0;

        if (GameManagerScript.instance.state == GameState.State.EarlyGame1) {
            if (students < K) {
                temp = faculty;
            }
        }
        else if (GameManagerScript.instance.state == GameState.State.EarlyGame2) {
            if (students < K) {
                //Debug.Log("EarlyGame2 r: " + r);
                temp = (int) (r * 2.0);
            }
            else {
                temp = -1;
            }
        }
        else if (GameManagerScript.instance.state == GameState.State.EarlyGame3) {
            temp = (int) (0.25 * students * ((K - students) / K));
        }
        else if (GameManagerScript.instance.state == GameState.State.EarlyGame4) {
            temp = (int) (0.5*r * students * ((K - students) / K));
        }
        else {
            Debug.Log("WOOPS: calcstudents is working outside of a valid gamestate in EarlyGame");
        }

        students += temp;
        return temp;
    }

    //alumni
    public virtual int calcAlumni() {
        int graduates = (int) (students / 50);

        if (students <= graduates) {
            alumni += students;
            students = 0;
        }
        else if (happiness < 2) {
            //if happiness is too low, students won't graduate
            graduates = 0;
        }
        else {
            students -= graduates;
            alumni += graduates;
        }

        return graduates;
    }

    //Function to calculate values based on high school agreements (For EARLYGAME)
    public void calcHSAgreements() {
        int stuTemp = 0;
        float reTemp = 0;

        //Get the avg of hish school values
        foreach(HighSchoolAgreement hs in agreements) {
            stuTemp += hs.students;
            reTemp += hs.value;
        }

        if (agreements.Count() != 0)
        {
            calcRenown(reTemp / agreements.Count());
        }
        else {
            calcRenown(1);
        }
        Debug.Log("agreement count:" + agreements.Count());
        Debug.Log("reTemp:" + reTemp.ToString());
        studentPool = stuTemp;

    }

    //These functions aren't used in the base class, just needed to be overriden by inherited classes
    public virtual void ApplyBuildingCalculations(Building b) { }

    public virtual void AddSpecialStudent(SpecialStudent obj) { }

    public virtual int calcRanking() { return 1000; }

    public virtual float calcSSProb() { return 0.1f; }

    public virtual float calcGradRate(float studentFacultyRatio, float ratioMax, float ratioMin) { return 0.5f; }
}

[System.Serializable]
public class ResourcesMidGame : Resources {
    /*
    MidGame Resources (Unused by earlygame, just used in the inherited class)
    public List<Building> buildings;
    public List<SpecialStudent> specialStudents;
    public int specialStudentThreshold; //Time between turns a special student can occur
    public int ranking; //out of 1000
    public float graduationRate;
    public float ssProb; //chance for a special student, between 0 and 1.0f
    public int maxFaculty; //MIN and MAX faculty decide the slider values for student to faculty ratio.
    public int minFaculty;
    public float renownBase; //base renown carried over from HSA agreements in the earlygame
    */

    public ResourcesMidGame(Resources resc) : base(resc.faculty, resc.alumni, resc.students, resc.wealth) {
        buildings = new List<Building> (); //TODO: When starting out you should have 1 default building, need the tilemap to recognize that
        //buildings.Add(new EducationalBuilding(100));

        specialStudents = new List<SpecialStudent> ();
        specialStudentThreshold = 10;
        ranking = 1000;

        //Starting renown value is the avg rating of HSA you got
        float reTemp = 0;
        foreach(HighSchoolAgreement hs in resc.agreements) {
            reTemp += hs.value;
        }
        renownBase = reTemp;

        //new max values
        //MAX_RENOWN = renownBase;
        //MAX_HAPPINESS = 7.368f;

        //initial values that will be overwritten anyway
        graduationRate = 0.5f;
        studentPool = resc.studentPool + 500; //give a 500 student safety gap at the start, set studentPool to the studentPool from HSA which are irrelevant
        ssProb = 0.1f;
        maxFaculty = resc.students; // Max nunber of students each faculty can be set to teach
        minFaculty = (int) Mathf.Round(resc.students / resc.faculty); //min number of students each faculty can be set to teach
        calcK();
        calcR();
    }

    //Buildings now affect multiple resources, calculate these here before any other calculation, run everytime a new building is added
    public override void ApplyBuildingCalculations(Building b) {
        if (b.cost <= wealth) {
            if (b.type == Building.Type.Residential) {
                studentPool += b.capacity; //increase student pool
            }
            else if (b.type == Building.Type.Educational) {
                maxFaculty += b.capacity; //increase max amount of students a faculty can teach

                if (minFaculty >= 10) {
                    minFaculty -= 5; //decrease the smallest amount of students a faculty can teach
                }
            }
            else if (b.type == Building.Type.Institutional) {
                if (specialStudentThreshold > 1) {
                    specialStudentThreshold--;
                }
            }
            else if (b.type == Building.Type.Athletic) {
                ssProb += 0.1f; //increase student probability by 10%
            }
            else {
                throw new System.Exception($"precondition failure");
            }
        }
        else {
            GameManagerScript.instance.eventController.DoEvent(new Event("Not enough Wealth to purchase Building", Event.Type.Notification));
            GameManagerScript.instance.PlaySound(GameManagerScript.soundType.INSUFFICIENT);
        }
        
    }

    //no need to override because it takes different parameters. Faculty_penalty is the value for student-faculty ratio
    public override long calcWealth(float donation, float tuition, float faculty_penalty) {
        long ret = Convert.ToInt64(((((alumni * donation) + (students * tuition)) / 5) - (faculty_penalty / 5)));
        wealth += ret;

        return ret;
    }

    public override int calcStudents() {
        int temp = (int) (r * students * ((K - students) / K));
        students += temp;
        //Debug.Log("Student's midgame being changed:" + temp);
        return temp;
    }

    //renown, override earlygme calculation. val is faculty pay value from the policy slider
    public override void calcRenown(float val) {
        //Debug.Log("What");
        renown = (renownBase * val) / 10; //r already takes into account happiness with renown, so no need to add happiness to this equation
    }

    public override int calcFaculty() {
        int temp;
        if (faculty < wealth) {
            temp = (int) (renown / 1.5 + 0.75);
        }
        else {
            temp = -1;
        }

        faculty += temp;
        return temp;
    }

    public override int calcAlumni() {
        if (float.IsNaN(graduationRate)) {
            graduationRate = 0.5f;
        }
        int graduates = (int) ((students / 5) * graduationRate);
        //Debug.Log("Graduation Rate: " + graduationRate);
        if (students <= graduates) {
            alumni += students;
            students = 0;
        }
        else if (happiness < 2) {
            //if happiness is too low, students won't graduate
            graduates = 0;
        }
        else {
            students -= graduates;
            alumni += graduates;
        }

        return graduates;
    }

    //happiness. Based on faculty pay slider (higher is good) and student to faculty ratio (higher is bad)
    public override void calcHappiness(float tuition, float fpay, float donation, float ratio) {
        happiness = (int) (fpay / ((tuition + donation) / 10 + ratio));
    }

    //stuFacRatio is the number of students a faculty can teach. The higher it is, the worst it is for graduation.
    public override float calcGradRate(float studentFacultyRatio, float ratioMax, float ratioMin) {
        //Might just need the first part, but the second added value helps reduce penalties for having higher ratio
        float ret = ((ratioMax - studentFacultyRatio) / ratioMax);

        if (studentFacultyRatio < Mathf.Round(students / faculty)) {
            ret -= 0.1f;
        }

        //Maxed optimal use of graduation
        if (ret >= 0.99f) {
            ret = 0.99f;
        }
        else if (ret <= 0.0f) {
            ret = 0.05f;
        }

        if (float.IsNaN(graduationRate)) {
            graduationRate = 0.5f;
        }

        graduationRate = ret;
        return ret;
    }

    //Student growth shouldn't be using renown anymore, since renown is used in so many other calculations. Maybe use ranking instead
    public override float calcR() {
        r = (1.0f - (ranking / 1000)) + 0.5f;
        return (1.0f - (ranking / 1000)) + 0.5f;
    }

    public override float calcK() {
        K = studentPool + faculty;
        return studentPool;
    }

    //Base value that increases based on a combination of happiness and renown that is less than 1.0f
    public override float calcSSProb() {
        float ret = 0.5f; //base 10% chance of a Special Student
        ret += happiness / renown; //needs some balancing

        if (ret > 0.99f) {
            ret = 0.99f;
        }
        ssProb = ret;

        return ret;
    }

    //calculates ranking based on renown and graduation rate. If a threshold is reached, increased ranking
    public override int calcRanking() {
        if (ranking > 850) {
            ranking -= (int) (renown - (renown * (1 - graduationRate)));
        }
        else if (ranking > 100) {
            ranking -= (int) (renown - (renown * (1 - graduationRate))) % 10;
        }
        else if (ranking > 50) {
            ranking -= (int) (renown - (renown * (1 - graduationRate))) % 5;
        }
        else if (ranking > 1) {
            ranking -= 1;
        }
        else {
            ranking = 1;
        }

        if (ranking <= 1) {
            ranking = 1;
        }
        
        return ranking;
    }

    public override void AddSpecialStudent(SpecialStudent obj) {
        specialStudents.Add(obj);
        ranking++;
    }
}
