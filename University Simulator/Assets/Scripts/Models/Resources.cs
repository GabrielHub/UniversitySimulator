public struct Resources {
    //5 Main Resources
    public int faculty;
    public int alumni;
    public int students;
    public int wealth;
    public int buildingCount;

    //Hidden Resources
    public float r {
        get {
            return ((students + faculty) / wealth) + renown;
        }
    } //student growth rate r

    public float K {
        get {
            return studentPool * acceptanceRate;
        }
    } //carrying capacity (size limit) for student growth K
    public float renown;
    public float happiness;
    public float acceptanceRate;
    //EarlyGame Resources
    public int studentPool;
    public float hsRenown;
    private HighSchoolAgreement[] agreements;

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

    //happiness. Optimal value is currently set to half the max value
    public void calcHappiness(float tuition, float tuitionMax, float donation, float donationMax) {
        happiness = (int)((tuitionMax / 2 - tuition) + (donationMax / 2 - donation));
    }

    public void calcWealth() {

    }
}
