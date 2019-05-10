//Building base class, inherited by different types of buildings

[System.Serializable]
public class Building {
	public string type; //describes what kind of building it is (R]esidential, Educational, Athletic, Instituional (Hospital, administration, basically gives a boost))
	public int rating; //rating of buildings will be used in ranking calculations
	public int cost; //how much it costs to buy a building

	//variables needed for inherited classes
	public int capacity;
	public int specialStudentCap;
	public float renownCap;

	//variables to be used by the EndGame building upgrades system
	//public float boost;

	public Building(string _type, int _rating, int _cost) {
		type = _type;
		rating = _rating;
		cost = _cost;
	}
}

//Residential Buildings are dorms and stuff. Determines MidGame student pool
public class ResidentialBuilding : Building {
	//public int capacity;
	//student capacity of building

	public ResidentialBuilding(int _capacity, int _rating, int _cost) : base("Residential", _rating, _cost) {
		capacity = _capacity;
	}
}

//Educational Buildings determine student to faculty ratio cap.
public class EducationalBuilding : Building {
	//public int capacity;
	//might need a variable for increasing student to faculty, but i think we can just increase those max values in GameManagerScript

	public EducationalBuilding(int _capacity, int _rating, int _cost) : base("Educational", _rating, _cost) {
		capacity = _capacity;
	}
}

//Institutional Buildings decrease the turns until a special student cna happen and unlocks more upgrades
public class InstitutionalBuilding: Building {
	
	public InstitutionalBuilding(float _boost, int _rating, int _cost) : base("Institutional", _rating, _cost) {
		//figure out how we want boost to work here
	}
}

//Athletic Buildings determine the chance of special students and renown
public class AthleticBuilding: Building {
	//public int specialStudentCap;
	//public float renownCap;

	public AthleticBuilding(int stuCap, float rCap, int _rating, int _cost) : base("Athletic", _rating, _cost) {
		specialStudentCap = stuCap;
		renownCap = rCap;
	}
}