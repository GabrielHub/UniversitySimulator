using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAgreements : MonoBehaviour
{
	public static List<string> highSchoolNames;

	void Start() {
		//fill out high school names. Feel free to come up with as many as you can think of :) We can reuse a lot of them for purchasing satellite campuses
		highSchoolNames = new List<string> ();

		highSchoolNames.Add("SAD! High School");
		highSchoolNames.Add("VGHS");
		highSchoolNames.Add("Dr. Bob's Soap School");
		highSchoolNames.Add("Grass Is Greener On The Other Side HS");
		highSchoolNames.Add("Public Void High School");
		highSchoolNames.Add("Not So High School");
		highSchoolNames.Add("Happy Feet HS");
		highSchoolNames.Add("Free Real Estate School");
		highSchoolNames.Add("Happy Yeet High");
		highSchoolNames.Add("One Dance Tech School");
		highSchoolNames.Add("Morty&Rick Film School");
		highSchoolNames.Add("Pepsi Co Prep");
		highSchoolNames.Add("NaCLHS");
		highSchoolNames.Add("RoomAndBoard Boarding School");
		highSchoolNames.Add("CIA Van 7");
		highSchoolNames.Add("Pogwarts");
		highSchoolNames.Add("Avengers Academy");
		highSchoolNames.Add("Orphan Daycare");
		highSchoolNames.Add("Charmin UltraSchool");
		highSchoolNames.Add("Jack Black's Crack Home");
		highSchoolNames.Add("Lebron's LeSchool For LeHopeless");
		highSchoolNames.Add("Papa John's School For Deadbeat Daddies");
		highSchoolNames.Add("La Escuela de Papa Juan Para Padres Tristes");
		highSchoolNames.Add("Saving Private High School");
		highSchoolNames.Add("CollegeBoard CEO HS");
		highSchoolNames.Add("Foldable Fridge Tech");
		highSchoolNames.Add("Prep In Your Step Prepatory");
		highSchoolNames.Add("PlzHlpLckdInBsmntFrcd2RiteHSNames School");
		highSchoolNames.Add("Gone To The Store BRB School");
		highSchoolNames.Add("Bigger Baller Brandr School");
		highSchoolNames.Add("Prof. Javier School For The Gifted");
		highSchoolNames.Add("Tax Break Donation School");
		highSchoolNames.Add("101 Dalmations In This School");
		highSchoolNames.Add("Some Kind Of Suicide Squad High School");
		highSchoolNames.Add("Fingers Hurt From Typing Episcopal");
		highSchoolNames.Add("Jimmy G Stands For Buckets Episcopal School");
		highSchoolNames.Add("Drinking Helps Me Sleep At Night Prepatory");
		highSchoolNames.Add("Mommy Needs Her Wine Daycare");
		highSchoolNames.Add("Make A Wish Foundation");
		highSchoolNames.Add("Astrology Real Tech");
		highSchoolNames.Add("James Corden Not Nice Upper School");
		highSchoolNames.Add("HanSolo Dies Film School");
		highSchoolNames.Add("Corbin Bleu Cooking School");
		highSchoolNames.Add("Rats! But Smart So School");
		highSchoolNames.Add("Boss Baby 7/10 Movie Prep");
		highSchoolNames.Add("Mitsubishi After School Program");
		highSchoolNames.Add("Home Schooled Fuckers");
		highSchoolNames.Add("Scuffed Academy");
		highSchoolNames.Add("Bear Grylls Grilling Academy");
		highSchoolNames.Add("Grylls Bear Survival School");
		highSchoolNames.Add("Lower Esteem Upper School");
		highSchoolNames.Add("Big Shaq In The Paint Art School");
		highSchoolNames.Add("Garfield's Online School");
		highSchoolNames.Add("Miniclip.com Prepatory");
		highSchoolNames.Add("Seeing Stars Astronomy School");
		highSchoolNames.Add("School For Unfortuante Events");
		highSchoolNames.Add("Brendan Fraser Comeback Theatre School");
		highSchoolNames.Add("Jim Carrey's Gym");
		highSchoolNames.Add("$$$ Hulu Has Live Sports $$$");
		highSchoolNames.Add("Michael Bay's Chemistry Studio");
		highSchoolNames.Add("Helen Keller's Academy of the Arts");
		highSchoolNames.Add("Bill Cosby's Cocktail Courses");
		highSchoolNames.Add("Jim Crowe High School");
	}

	//n is the number of strings you want to choose
    public static string[] ChooseName(int n) {
    	string[] result = new string[n];

    	int numToChoose = n;
    	for (int numLeft = highSchoolNames.Count; numLeft > 0; numLeft--) {
    		float prob = (float) numToChoose / (float) numLeft;

    		if (Random.value <= prob) {
    			numToChoose--;
    			result[numToChoose] = highSchoolNames[numLeft - 1];

    			if (numToChoose == 0) {
    				break;
    			}
    		}
    	}

    	return result;
    }
}
