/*
	These are the objects for buying upgrades. Gamemanager script will generate ..., at different points in the game
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

//repeatable hire faculty to increase K
public class UpgradeHireFaculty : UpgradeBase {
	public UpgradeHireFaculty(int prevCost) : base("Hire New Faculty", "Hiring more faculty to help out increases student capacity", (int) (prevCost * 1.8 + 10)) {

	}

	public override void ApplyEffect() {
		bought = true;
		GameManagerScript.instance.resources.faculty += 2;
		GameManagerScript.instance.eventController.DoEvent(new Event("Hired New Faculty: more meat for the machine", Event.Type.Notification));

		UpgradeHireFaculty upgradeFaculty = new UpgradeHireFaculty(cost);
        GameManagerScript.instance.AddUpgradable(upgradeFaculty); //Add new repeatable
	}
}

//For unlocking alumni
public class UpgradeAlumni : UpgradeBase {
	public UpgradeAlumni() : base("Undergrad Diplomas", "Official degrees that unlock alumni", 2500) { }

	public override void ApplyEffect() {
		bought = true;
		GameManagerScript.instance.eventController.DoEvent(new Event("College Diplomas For Everyone*!: *For the low low price of 4** years @ 70k a year **No guarantees or refunds", Event.Type.Notification));
		GameManagerScript.instance.resources.alumni++;
		MessageBus.instance.emit(new GameState.ShouldChange(GameState.State.EarlyGame3));
	}
}

//unlocks Advanced Statistics
public class UpgradeAdministrator : UpgradeBase {
	public UpgradeAdministrator() : base("Hire Administrators", "unlocks advanced statistics tab", 5000) {
		//calls base constructor
	}

	public override void ApplyEffect() {
		bought = true;
		GameManagerScript.instance.enableStatistics = true;
		GameManagerScript.instance.resources.faculty += 5;
		GameManagerScript.instance.eventController.DoEvent(new Event("Hired Administrators: hiding the paper trail has never been easier", Event.Type.Notification));
	}
}

//First of 2 requirements for Finishing Early Game. Unlocks Buildings
public class UpgradeCampus : UpgradeBase {
	public UpgradeCampus() : base("Buy A Campus", "REQUIRED to unlock buildings", 25000) {
		//calls base constructor
	}

	public override void ApplyEffect() {
		bought = true;
		GameManagerScript.instance.earlyGameRequirements++;
		GameManagerScript.instance.eventController.DoEvent(new Event("Bought A Campus: sometimes you gotta fly before you can walk", Event.Type.Notification));
	}
}

//Second of 2 requirements for Finishing Early Game. Unlocks Next stage
public class UpgradeLicense : UpgradeBase {
	public UpgradeLicense() : base("Education License", "REQUIRED to unlock the next stage", 10000) {
		//calls base constructor
	}

	public override void ApplyEffect() {
		bought = true;
		GameManagerScript.instance.earlyGameRequirements++;
		GameManagerScript.instance.eventController.DoEvent(new Event("Education License Approved: a recognized degree for me", Event.Type.Notification));
	}
}
