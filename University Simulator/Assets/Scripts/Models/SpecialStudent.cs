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