using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Resources {
    //5 Main Resources
    public int faculty;
    public int alumni;
    public int students;
    public int wealth;
    public int buildingCount;
    //public float r_rate;
    //public float k_rate;

    //To check what phase of the game we're in
    public int gamePhase = 0;

    //Hidden Resources
    public float r {
        get {
            //r_rate = ((students + faculty) / wealth) + renown;
            return ((students + faculty) / wealth) + renown;
        }
    } //student growth rate r

    public float K {
        get {
            //k_rate = (studentPool + alumni);
            return (studentPool + alumni);
        }
    } //carrying capacity (size limit) for student growth K

    public float renown;
    public float happiness;
    public float acceptanceRate;

    //EarlyGame Resources
    public int studentPool;

    [SerializeField]
    public List<HighSchoolAgreement> agreements;

    public Resources(int faculty = 0, int alumni = 0, int students = 0, int wealth = 0, int buildingCount = 0) {
        this.faculty = faculty;
        this.alumni = alumni;
        this.students = students;
        this.wealth = wealth;
        this.buildingCount = buildingCount;

        this.agreements = new List<HighSchoolAgreement>();
        this.agreements.Add(new HighSchoolAgreement("Starter's HS", 100, 1, 0));

        //initial values for other variables
        gamePhase = 0;
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
            students: left.students + right.students,
            buildingCount: left.buildingCount + right.buildingCount
        );
    }

    //happiness. Optimal value is currently set to half the max value
    public void calcHappiness(float tuition, float tuitionMax, float donation, float donationMax) {
        happiness = (int) ((tuitionMax / 2 - tuition) + (donationMax / 2 - donation));
    }

    //renown, earlygame calculations
    public void calcRenown(float val) {
        //with the avg of hs value, renown below 3.0 will reduce the growth of students
        renown = val - (acceptanceRate * 2);
    }

    //acceptance rate slider calculations, should always be a float between 0.0 and 1.0
    public void calcAcceptanceRate(float val) {
        acceptanceRate = val;
    }

    //wealth. donation and tuition are sliders that change variables in gamemanagerscript
    public void calcWealth(float donation, float tuition) {
        wealth += (int) ((alumni * donation) + (students * tuition)) / 5;
        int students_penalty = 1 + (int) ((students / 50) * renown);
        int faculty_penalty = 2 + (faculty / (faculty * buildingCount));
        wealth -= ((faculty * faculty_penalty) + (students * students_penalty) + (buildingCount * 5)) / 5;

    }

    //faculty.
    public void calcFaculty() {
        if (faculty < wealth) {
            faculty += buildingCount;
        }
    }

    //students
    public void calcStudents(float maxHappiness) {
        // K - Students accepted out of student pool
        // R - Growth Rate

        students += (int) ((acceptanceRate) * (happiness / maxHappiness) * (r * students * ((K - students) / K)));
    }

    //alumni
    public void calcAlumni() {
        if (students <= 5) {
            alumni += students;
            students = 0;
        }
        else {
            int i = (int) (students / 10);
            students -= i;
            alumni += i;
        }
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
}
