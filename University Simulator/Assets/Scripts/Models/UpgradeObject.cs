/*
	These are the objects for buying upgrades. Gamemanager script will generate only 3 at a time, at different points in the game
*/

public class UpgradeBase {
	public string name;
	public string description;
	public int cost; 
	public bool bought;
	//public int gamePhase; //what game phase this upgrade object is available in, might not be used we'll see

	public UpgradeBase(string _name, string _description, int _cost) {
		name = _name;
		description = _description;
		cost = _cost;
		bought = false;
	}

	public virtual void ApplyEffect() {
		//function to be overloaded
		bought = true;
	}
}

//unlocks Advanced Statistics
public class UpgradeAdministrator : UpgradeBase {
	public UpgradeAdministrator() : base("Hire Administrators", "unlocks advanced statistics tab", 500) {
		//calls base constructor
	}

	public override void ApplyEffect() {
		bought = true;
		GameManagerScript.instance.enableStatistics = true;
		GameManagerScript.instance.resources.faculty += 5;
		GameManagerScript.instance.eventController.DoEvent(new Event("Hired Administrators: hiding the paper trail has never been easier"));
	}
}

//First of 2 requirements for Finishing Early Game. Unlocks Buildings
public class UpgradeCampus : UpgradeBase {
	public UpgradeCampus() : base("Buy A Campus", "REQUIRED to unlock buildings", 3500) {
		//calls base constructor
	}

	public override void ApplyEffect() {
		bought = true;
		GameManagerScript.instance.earlyGameRequirements++;
		GameManagerScript.instance.eventController.DoEvent(new Event("Bought A Campus: sometimes you gotta fly before you can walk"));
	}
}

//Second of 2 requirements for Finishing Early Game. Unlocks Next stage
public class UpgradeLicense : UpgradeBase {
	public UpgradeLicense() : base("Education License", "REQUIRED to unlock the next stage", 1000) {
		//calls base constructor
	}

	public override void ApplyEffect() {
		bought = true;
		GameManagerScript.instance.earlyGameRequirements++;
		GameManagerScript.instance.eventController.DoEvent(new Event("Education License Approved: a recognized degree for me"));
	}
}
