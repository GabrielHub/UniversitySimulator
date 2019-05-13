[System.Serializable]
public struct HighSchoolAgreement {
	public string name;
	public int students;
	public int value;
	public int cost;

	public HighSchoolAgreement(string n, int s, int r, int c) {
		name = n;
		students = s;
		value = r; //value, how many stars is it ranked
		cost = c;
	}
}
