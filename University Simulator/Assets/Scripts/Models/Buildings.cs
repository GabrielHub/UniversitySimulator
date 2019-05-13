//Building base class, inherited by different types of buildings

[System.Serializable]
public class Building {
	public enum Type { Residential, Educational, Athletic, Institutional }
	public Type type; //describes what kind of building it is (R]esidential, Educational, Athletic, Instituional (Hospital, administration, basically gives a boost))
	public int rating; //rating of buildings will be used in ranking calculations
	public int cost; //how much it costs to buy a building

	//variables needed for inherited classes
	public int capacity;
	public int specialStudentCap;
	public float renownCap;

	//variables to be used by the EndGame building upgrades system
	//public float boost;

	public Building(Type type, int rating = 0, int cost = 0, int capacity = 0, int specialStudentCap = 0, int renownCap = 0) {
		this.type = type;
		this.rating = rating;
		this.cost = cost;
	}
}
