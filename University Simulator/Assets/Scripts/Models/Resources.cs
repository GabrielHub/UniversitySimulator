using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Resources {
    public const float MAX_HAPPINESS = 7f;
    public const float MAX_RENOWN = 5f;

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
        return new Resources(
            wealth: left.wealth + right.wealth,
            faculty: left.faculty + right.faculty,
            alumni: left.alumni + right.alumni,
            students: left.students + right.students
        );
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
    public virtual long calcWealth(float donation, float tuition) {
        long students_penalty = 1;
        long faculty_penalty = 2 + (faculty / (faculty * 3));
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
            temp = (long) ((((alumni * donation) + (students * tuition)) / 5) - ((faculty * faculty_penalty) + (students * students_penalty) / 3));
        }
        else {
            Debug.Log("OOPSIE WOOPSIE WE MADE A FUCKYWUCKY: calcwealth in earlygame is out of valid gamestate");
        }

        wealth += temp;

        return temp;
    }

    //faculty.
    public int calcFaculty() {
        int temp;
        if (faculty < wealth) {
            temp = (int) (3 * renown);
        }
        else {
            temp = 0;
        }

        faculty += temp;
        return temp;
    }

    //students
    public int calcStudents(float maxHappiness) {
        // K - Students accepted out of student pool
        // R - Growth Rate
        int temp = 0;

        if (GameManagerScript.instance.state == GameState.State.EarlyGame1) {
            if (students < K) {
                temp = 1;
            }
        }
        else if (GameManagerScript.instance.state == GameState.State.EarlyGame2) {
            if (students < K) {
                Debug.Log("EarlyGame2 r: " + r);
                temp = (int) (r * 2);
            }
            else {
                temp = -1;
            }
        }
        else if (GameManagerScript.instance.state == GameState.State.EarlyGame3) {
            temp = (int) (0.5 * students * ((K - students) / K));
        }
        else if (GameManagerScript.instance.state == GameState.State.EarlyGame4) {
            temp = (int) (r * students * ((K - students) / K));
        }
        else {
            Debug.Log("WOOPS: calcstudents is working outside of a valid gamestate in EarlyGame");
        }

        students += temp;
        return temp;
    }

    //alumni
    public int calcAlumni() {
        int temp;

        if (students <= 9) {
            alumni += students;
            students = 0;
            temp = students;
        }
        else if (happiness < 3) {
            //if happiness is too low, students won't graduate
            temp = 0;
        }
        else {
            int i = (int) (students / 20);
            students -= i;
            alumni += i;
            temp = i;
        }

        return temp;
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
        calcRenown(reTemp / agreements.Count());
        studentPool = stuTemp;

    }

    //These functions aren't used in the base class, just needed to be overriden by inherited classes
    public virtual void ApplyBuildingCalculations(Building b) { }

    public virtual void AddSpecialStudent(SpecialStudent obj) { }

    public virtual int calcRanking() { return 1000; }
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

        //initial values that will be overwritten anyway
        graduationRate = 0.5f;
        studentPool = resc.studentPool + 500; //give a 500 student safety gap at the start, set studentPool to the studentPool from HSA which are irrelevant
        ssProb = 0.1f;
        maxFaculty = resc.students; // Max nunber of students each faculty can be set to teach
        minFaculty = (int) Mathf.Round(resc.students / resc.faculty); //min number of students each faculty can be set to teach
    }

    //Buildings now affect multiple resources, calculate these here before any other calculation, run everytime a new building is added
    public override void ApplyBuildingCalculations(Building b) {
        if (b.type == Building.Type.Residential) {
            studentPool += b.capacity; //increase student pool
        }
        else if (b.type == Building.Type.Educational) {
            maxFaculty += b.capacity; //increase max amount of students a faculty can teach

            if (minFaculty >= 10) {
                minFaculty -= 5; //decrease the smallest amount of students a faculty can teach
            } 
        }
        else if (b.type == Building.Type.Instituional) {
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

    //no need to override because it takes different parameters. Faculty_penalty is the value for student-faculty ratio
    public int calcWealth(float donation, float tuition, float faculty_penalty) {
        int ret = (int) ((((alumni * donation) + (students * tuition)) / 5) - (faculty * faculty_penalty / 5));
        wealth += ret;

        return ret;
    }

    //renown, override earlygme calculation. val is faculty pay value from the policy slider
    public override void calcRenown(float val) {
        renown = (renownBase * val) / 10; //r already takes into account happiness with renown, so no need to add happiness to this equation
    }

    //happiness. Based on faculty pay slider (higher is good) and student to faculty ratio (higher is bad)
    public override void calcHappiness(float tuition, float fpay, float donation, float ratio) {
        happiness = (int) (fpay / ((tuition + donation) / 10 + ratio));
    }

    //stuFacRatio is the number of students a faculty can teach. The higher it is, the worst it is for graduation.
    public float calcGradRate(int studentFacultyRatio, int ratioMax, int ratioMin) {
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
            ret = 0.01f;
        }

        graduationRate = ret;
        return ret;
    }

    //Student growth shouldn't be using renown anymore, since renown is used in so many other calculations. Maybe use ranking instead
    public override float calcR() {
        return (1.0f - (ranking / 1000)) + 0.5f;
    }

    public override float calcK() {
        return studentPool;
    }

    //Base value that increases based on a combination of happiness and renown that is less than 1.0f
    public float calcSSProb() {
        float ret = 0.1f; //base 10% chance of a Special Student

        //use renown and happiness to affect this somehow, first need to see how big r gets

        return ret;
    }

    //calculates ranking based on renown and graduation rate. If a threshold is reached, increased ranking
    public override int calcRanking() {
        int ret = 1000;

        //needs to be calculated

        return ret;
    }

    public override void AddSpecialStudent(SpecialStudent obj) {
        specialStudents.Add(obj);

        //75% chance that adding a student increases ranking by one
        if (Random.Range(0.0f, 1.0f) <= 0.75f) {
            ranking++;
        }
    }
}
