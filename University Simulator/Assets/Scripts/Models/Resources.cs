using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Resources {
    public const float MAX_HAPPINESS = 8f;
    public const float MAX_RENOWN = 5f;

    //5 Main Resources
    public int faculty;
    public int alumni;
    public int students;
    public int wealth;

    //Hidden Resources
    public float r {
        get {
            //r_rate = ((students + faculty) / wealth) + renown;
            return (happiness / MAX_HAPPINESS) * renown;
        }
    } //student growth rate r

    public float K {
        get {
            //k_rate = (studentPool + alumni);
            return (studentPool * acceptanceRate);
        }
    } //carrying capacity (size limit) for student growth K

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
    public int maxFaculty;

    [SerializeField]
    public List<HighSchoolAgreement> agreements = new List<HighSchoolAgreement> {
        new HighSchoolAgreement("Starter's HS", 100, 3, 0)
    };

    public Resources(int faculty = 0, int alumni = 0, int students = 0, int wealth = 0) {
        this.faculty = faculty;
        this.alumni = alumni;
        this.students = students;
        this.wealth = wealth;

        //initial values for other variables
        happiness = 1;
        acceptanceRate = .8f;
        renown = 1f - (acceptanceRate * 2);
        studentPool = 100;
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
    public void calcHappiness(float tuition, float tuitionMax, float donation, float donationMax) {
        happiness = (int) ((tuitionMax / 2 - tuition) + (donationMax / 2 - donation));
    }

    //renown, earlygame calculations
    public virtual void calcRenown(float val) {
        //with the avg of hs value, renown below 3.0 will reduce the growth of students
        renown = val - (acceptanceRate * 2);
    }

    //acceptance rate slider calculations, should always be a float between 0.0 and 1.0
    public void calcAcceptanceRate(float val) {
        acceptanceRate = val;
    }

    //wealth. donation and tuition are sliders that change variables in gamemanagerscript, good luck balancing this pos
    public virtual int calcWealth(float donation, float tuition) {
        int students_penalty = 1;
        int faculty_penalty = 2 + (faculty / (faculty * 3));
        int temp = (int) ((((alumni * donation) + (students * tuition)) / 5) - (((faculty * faculty_penalty) + (students * students_penalty) + (3 * 5)) / 5));
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
        int temp = (int) (r * students * ((K - students) / K));
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
        else if (happiness < 4) {
            //alumni doesn't decrease
            temp = -1;
            alumni += temp;
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
}

[System.Serializable]
public class ResourcesMidGame : Resources {

    public ResourcesMidGame(Resources resc) : base(resc.faculty, resc.alumni, resc.students, resc.wealth) {
        buildings = new List<Building> (); //TODO: When starting out you should have 1 default building, need the tilemap to recognize that
        //buildings.Add(new EducationalBuilding(100));

        specialStudents = new List<SpecialStudent> ();
        specialStudentThreshold = 10;
        ranking = 1000;

        //Starting renown value is the avg rating of HSA you got
        float reTemp = 0;
        foreach(HighSchoolAgreement hs in agreements) {
            reTemp += hs.value;
        }

        //initial values that will be overwritten anyway
        graduationRate = 0.5f;
        studentPool = resc.studentPool + 500; //give a 500 student safety gap at the start, set studentPool to the studentPool from HSA which are irrelevant
        ssProb = 0.01f;
        maxFaculty = resc.faculty + 10;
    }

    //Buildings now affect multiple resources, calculate these here before any other calculation, run everytime a new building is added
    public override void ApplyBuildingCalculations(Building b) {
        if (b.type == "Residential") {
            studentPool += b.capacity;
        }
        else if (b.type == "Educational") {
            maxFaculty += b.capacity;
        }
        else if (b.type == "Institutional") {
            //NEeds to be figured out
        }
        else if (b.type == "Athletic") {
            //Needs to be figured out
        }
        else {
            //Debug.Log("ERROR: Checking building types in building array failed to compare type");
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
        //with the avg of hs value, renown below 3.0 will reduce the growth of students
        renown = val - (acceptanceRate * 2);
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

    public float calcSSProb() {
        float ret = 0.01f;

        //needs to be figured out once SS feature is in

        return ret;
    }

    public virtual void AddSpecialStudent(SpecialStudent obj) {
        specialStudents.Add(obj);
    }
}
