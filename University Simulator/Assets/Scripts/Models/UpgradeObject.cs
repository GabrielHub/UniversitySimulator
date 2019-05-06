/*
	These are the objects for buying upgrades. Gamemanager script will generate only 3 at a time, at different points in the game
*/

public class UpgradeBase {
	public string name;
	public string description;
	public int cost; 
	//public int gamePhase; //what game phase this upgrade object is available in, might not be used we'll see

	public UpgradeBase(string _name, string _description, int _cost) {
		name = _name;
		description = _description;
		cost = _cost;
	}
}

public class UpgradeAdministrator : UpgradeBase {
	public UpgradeAdministrator() : base("Hire Administrators", "Unlocks detailed statistics for your University", 500) {
		//calls base constructor
	}
}

/*public class UpgradeCampus : UpgradeBase {
	public UpgradeCampus() : base() {

	}
}*/