using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAgreements : MonoBehaviour
{
	public static RandomAgreements instance;
	public List<string> highSchoolNames;

	void Awake() {
		if (RandomAgreements.instance == null) {
			RandomAgreements.instance = this;
		} else {
			Destroy(this);
		}

		//I need to do this at Awake cuz it's not loading before the gamemanager that's calling it
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
		highSchoolNames.Add("Charcuterie Board High School of Fine Arts");
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
		highSchoolNames.Add("Mitsubishi After High School");
		highSchoolNames.Add("Home Schooled Fuckers");
		highSchoolNames.Add("Scuffed Academy");
		highSchoolNames.Add("Bear Grylls Grilling Academy");
		highSchoolNames.Add("Grylls Bear Survival School");
		highSchoolNames.Add("Lower Esteem Upper School");
		highSchoolNames.Add("Big Shaq In The Paint Art School");
		highSchoolNames.Add("Garfield's Online School");
		highSchoolNames.Add("Miniclip.com Prepatory");
		highSchoolNames.Add("Seeing Stars Astronomy School");
		highSchoolNames.Add("School For Unfortunate Events");
		highSchoolNames.Add("Brendan Fraser Comeback Theatre School");
		highSchoolNames.Add("Jim Carrey's Gym");
		highSchoolNames.Add("$$$ Hulu Has Live Sports $$$");
		highSchoolNames.Add("Michael Bay's Chemistry Studio");
		highSchoolNames.Add("Helen Keller's Academy of the Arts");
		highSchoolNames.Add("Bill Cosby's Cocktail Courses");
		highSchoolNames.Add("Jim Crowe High School");
		highSchoolNames.Add("General Booty Police Academy");
		highSchoolNames.Add("Yugoslavia High School of Science");
		highSchoolNames.Add("Academic Magnetic High School");
		highSchoolNames.Add("High School Transylvania");
		highSchoolNames.Add("Jerry Mander Ringville High");
		highSchoolNames.Add("Mr. Doctor Early College Academy");
		highSchoolNames.Add("Kevin Spacey Young Boy's Prepatory");
		highSchoolNames.Add("King's Landing High School");
		highSchoolNames.Add("Talkaton School for Young Leaders");
		highSchoolNames.Add("Columbus High School");
		highSchoolNames.Add("Pitbull's Worldwide School");
		highSchoolNames.Add("IKEA Frontier College Prepatory");
		highSchoolNames.Add("Southside BRRRT SKRRT High");
		highSchoolNames.Add("Asian Export College Prepatory");
		highSchoolNames.Add("No Line Not Crossed Secondary School");
		highSchoolNames.Add("McLean High School");
		highSchoolNames.Add("Suite School On Deck");
		highSchoolNames.Add("Cashew Cash Checks College Prep");
		highSchoolNames.Add("Sunken Dreams High");
		highSchoolNames.Add("Stanley Steamers Student Academy");
		highSchoolNames.Add("Use Protection Next Time High School");
		highSchoolNames.Add("twitch.tv/crabsforgrabs High School");
		highSchoolNames.Add("Sid Meier School For Young Leaders");
		highSchoolNames.Add("Attendance Is 30% Secondary School");
		highSchoolNames.Add("PornHub Premium Prepatory");
		highSchoolNames.Add("Fornite School of Intepretive Dance");
		highSchoolNames.Add("Lone Pine School for Lonely Boys");
		highSchoolNames.Add("Antivaccinator Institute of Technology");
		highSchoolNames.Add("ITTTTT Tech");
		highSchoolNames.Add("Pacific Rim Institute");
		highSchoolNames.Add("Dr. Wood Oakwood Conservatory");
		highSchoolNames.Add("Country Road Home School");
		highSchoolNames.Add("Valley Charter School for Troubled Teachers");
		highSchoolNames.Add("Vince Charter School");
		highSchoolNames.Add("N. Cage School of Fine Arts");
		highSchoolNames.Add("USC First-Class Donation-First High School");
		highSchoolNames.Add("International Student Training School of UCLA");
		highSchoolNames.Add("R. L. Stine Instutute of Journalism");
		highSchoolNames.Add("Wa Ge Secondary School");
		highSchoolNames.Add("Liverty Academy for Young Bartenders");
		highSchoolNames.Add("Sea Me Now Naval Academy");
		highSchoolNames.Add("Burts Bees High School");
		highSchoolNames.Add("Harvard Tearslake");
		highSchoolNames.Add("Denny's High School of Culinary Arts");
		highSchoolNames.Add("Pacer Test School");
		highSchoolNames.Add("DadPlzNotTheBeltDadImSorryPlzStopDadStop High School");
		highSchoolNames.Add("One Almond Conservatory");
		highSchoolNames.Add("Magnetic Induction Magnet School");
		highSchoolNames.Add("Mesomorph Institute of Technology");
		highSchoolNames.Add("Ratatouille School of Culinary Arts for Young Rats");
		highSchoolNames.Add("Magneto's Magnet School for The Gifted");
		highSchoolNames.Add("Bread Makes You Fat High School");
		highSchoolNames.Add("Obscure Movie Reference School of Fine Arts");
		highSchoolNames.Add("Chipotle Foundation High School");
		highSchoolNames.Add("Optimus Prime High School of Science");
		highSchoolNames.Add("Chegg School for Altruistic Students");
		highSchoolNames.Add("St. Antetokounmpo's Episcopal School");
		highSchoolNames.Add("Lil Pump Mountainview Charter School");
		highSchoolNames.Add("Malibu Seabreeze High School");
		highSchoolNames.Add("Himachi Long Distance Connection HS");
		highSchoolNames.Add("'If at first you don't succeedle turn to the needle' School");
	}

	void Start() {
		
	}

	//n is the number of strings you want to choose
    public string[] ChooseName(int n) {
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

      //randomize HSAgreements after a certain time
    public HighSchoolAgreement generateAgreement(string name) {

        int val = Random.Range(1, 6);
        int pool;
        int cost;

        //val is the 'star' of HS out of 5. Lower rated HS will provide more students tho
        if (val == 1) {
            pool = Random.Range(300, 550);
            cost = 100;
        }
        else if (val == 2) {
            pool = Random.Range(250, 450);
            cost = Random.Range(200, 300);
        }
        else if (val == 3) {
            pool = Random.Range(100, 250);
            cost = Random.Range(250, 350);
        }
        else if (val == 4) {
            pool = Random.Range(50, 100);
            cost = Random.Range(300, 400);
        }
        else {
            pool = Random.Range(35, 75);
            cost = 500;
        }
        return (new HighSchoolAgreement(name, pool, val, cost));
    }

}
