public struct Resources {
    //5 Main Resources
    public int faculty;
    public int alumni;
    public int students;
    public int wealth;
    public int buildingCount;

    //Hidden Resources
    private float r; //student growth rate r
    private float K; //carrying capacity (size limit) for student growth K
    private float renown = 0.1f; //temporary starting value
    private float happiness = 1.0f;
    private float acceptanceRate;
    //EarlyGame Resources
    public static int studentPool;
    public static float hsRenown;

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
