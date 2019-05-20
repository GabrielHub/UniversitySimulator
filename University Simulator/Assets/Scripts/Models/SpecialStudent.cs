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
			"Captain",
			"Marc",
			"Mark",
			"Giannis",
			"Kawhi",
			"Stephen",
			"Kyle",
			"Jimmy",
			"Johnson",
			"Jeremy",
			"Jordan",
			"Leslie",
			"Karen",
			"Mia",
			"Alexis",
			"Beric",
			"Emelia",
			"Amelia",
			"Derek",
			"Matene",
			"Andrew",
			"Andy",
			"Drew",
			"Jon",
			"Clarence",
			"Joffrey",
			"Sandor",
			"Yennefer",
			"Ciri",
			"Geralt",
			"Macy",
			"Samuel",
			"Bud",
			"Blue",
			"Donald",
			"Don",
			"Olmeca",
			"Jose",
			"Del",
			"Tom",
			"Kristaps",
			"D'Angelo",
			"Tom",
			"Thomas",
			"Caesar",
			"Dick",
			"Ricky",
			"Arina",
			"Josh",
			"Joshua",
			"Michael",
			"Lebron",
			"Albert",
			"Wolfgang",
			"Enrico",
			"Pablo",
			"Arthur",
			"Clara",
			"Jean",
			"Fibonacci",
			"Blaise",
			"Pascal",
			"Maria",
			"Marie",
			"Curie",
			"Felix",
			"Jascha",
			"Salil",
			"Aditya",
			"Eshwar",
			"Ravi",
			"Spencer",
			"Salim",
			"Sarah",
			"Duncan",
			"Geico",
			"Trivago",
			"Caris",
			"Tian",
			"Rogers",
			"Tony",
			"Scarlett",
			"Charles",
			"Cersei",
			"Sansa",
			"Arya",
			"Damien",
			"Mario",
			"Luigi",
			"Linguini",
			"Chop",
			"Latte",
			"Dany",
			"Joey"
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
			"Grouse",
			"Jager",
			"Campari",
			"Cointreau",
			"Patron",
			"Ballantine",
			"Artois",
			"Stella",
			"Equis",
			"Adams",
			"Miller",
			"Moon",
			"Modelo",
			"Michelob",
			"Roses",
			"Bulleit",
			"Mark",
			"Maker",
			"Tito",
			"Stolichnaya",
			"Eddy",
			"Svedka",
			"Skyy",
			"Amsterdam",
			"Espolon",
			"Altos",
			"Jimador",
			"Maguey",
			"Sauza",
			"Gosling",
			"Jerry",
			"Q.",
			"Cruzan",
			"Myers",
			"Brugal",
			"Hendrick",
			"Tanqueray",
			"Bombay",
			"Beefeater",
			"Fords",
			"Plymouth",
			"Gordon",
			"Citadelle",
			"Haymans",
			"Aviation",
			"Guinness",
			"Ballast-Point",
			"Overholt",
			"Jinro",
			"Chum-Churum",
			"LaoJiao",
			"Miguel",
			"Hayward",
			"Iichiko",
			"Khortytsa",
			"Pitu",
			"Havana",
			"Bee",
			"Andong",
			"Chamisul",
			"Kokuryu",
			"Dassai",
			"Kirin",
			"Asahi",
			"Juyondai",
			"Hakkaisan",
			"Tiger",
			"Cabernet",
			"Sauvignon",
			"Chardonnay",
			"Merlot",
			"Pinot",
			"Noir",
			"Riesling",
			"Blanc",
			"Syrah",
			"Zinfandel",
			"Cellars",
			"Vino",
			"Prosecco",
			"Changyu",
			"Toro",
			"Mondavi",
			"Hardy"
		};

		types = new string[] {
			"Gifted Athlete",
			"Programming Prodigy",
			"Mathematical Prodigy",
			"Mental Calculator",
			"Directorial Prodigy",
			"Future Superstar",
			"Prolific Artist",
			"Breakthrough Scientist",
			"Renowned Writer",
			"Renowned Surgeon",
			"Famous Producer"
		};
	}

	public SpecialStudent GenerateStudent() {
		return new SpecialStudent(types[Random.Range(0, types.Length)], firstNames[Random.Range(0, firstNames.Count)] + " " + lastNames[Random.Range(0, lastNames.Count)], Random.Range(25000, 350000));
	}
}
