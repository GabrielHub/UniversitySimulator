using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/*
	These are the objects for buying Special Students.
*/

//For now special students just give a 75% chance of increasing ranking by 1
public struct SpecialStudent {
	public string type; //What field the special student affects, is from, belongs to.
	public string name; //RNG based name
	public int cost; //cost of scholarship to give

	public SpecialStudent(string _type, string _name, int _cost) {
		type = _type;
		name = _name;
		cost = _cost;
	}
}

//This class holds a pool of randomly generated stuff, and will return a randomly generated student when called on
[System.Serializable]
public class RNGSpecialStudent {
	public List<string> firstNames;
	public List<string> lastNames;
	public string[] types;
	public int cost;

	public RNGSpecialStudent() {
		firstNames = new List<string> {
			"Abby",
			"Apricot",
			"Ava",
			"Adam",
			"Alfonso",
			"Adolf",
			"Ally",
			"Alberto",
			"Adrian",
			"Atypical",
			"Akimbo",
			"Amoeba",
			"Absolut",
			"Captain",
			"Gray",
			"Grey",
			"Jack",
			"Jim",
			"Jill",
			"Bill",
			"Don",
			"Lee",
			"Curtis",
			"Grant",
			"Gabriel",
			"Sabrina",
			"Max",
			"Jahlil",
			"Michael",
			"Captain"
		};

		//lol let's make these all liquor names. Easter eggs amirite
		lastNames = new List<string> {
			"Goose",
			"Jameson",
			"Morgan",
			"Daniels",
			"Crow",
			"Julio",
			"Walker",
			"Blue",
			"Royal",
			"Regal",
			"Bacardi",
			"Beam",
			"Smirnoff",
			"Ketel",
			"Stag",
			"Turkey",
			"Malibu",
			"Bailey",
			"Ciroc",
			"Hennessy",
			"Meister",
			"Martin",
			"Cuervo",
			"Ball",
			"Grouse"
		};

		types = new string[] {
			"Gifted Athlete",
			"Programming Prodigy",
			"Mathematical Prodigy",
			"Mental Calculator",
			"Directorial Prodigy",
			"Future Superstar",
			"Prolific Artist",
			"Breakthrough Scientist"
		};

		cost = 0; // Random Range cannot be called here, so instead we'll return it in the GenerateStudent() function
	}

	public SpecialStudent GenerateStudent() {
		return new SpecialStudent(types[Random.Range(0, types.Length)], firstNames[Random.Range(0, firstNames.Count)] + " " + lastNames[Random.Range(0, lastNames.Count)], Random.Range(10000, 350000));
	}
}
