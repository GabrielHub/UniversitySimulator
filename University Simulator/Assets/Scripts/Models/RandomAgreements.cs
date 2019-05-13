using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAgreements : MonoBehaviour
{
	//We might not want this to be a singleton, especially since it's only used in the early game and will be replaced by the RandomUniversity class
	public static RandomAgreements instance;
	public List<string> highSchoolNames = new List<string> {
		"SAD! High School",
		"VGHS",
		"Dr. Bob's Soap School",
		"Grass Is Greener On The Other Side HS",
		"Public Void High School",
		"Not So High School",
		"Happy Feet HS",
		"Free Real Estate School",
		"Happy Yeet High",
		"One Dance Tech School",
		"Morty&Rick Film School",
		"Pepsi Co Prep",
		"NaCL HS",
		"Charcuterie Board High School of Fine Arts",
		"CIA Van 7",
		"Pogwarts",
		"Avengers Academy",
		"Orphan Daycare",
		"Charmin UltraSchool",
		"Jack Black's Crack Home",
		"Lebron's LeSchool For LeHopeless",
		"Papa John's School For Deadbeat Daddies",
		"La Escuela de Papa Juan Para Padres Tristes",
		"Saving Private High School",
		"CollegeBoard CEO HS",
		"Foldable Fridge Tech",
		"Prep In Your Step Prepatory",
		"PlzHlpLckdInBsmntFrcd2RiteHSNames School",
		"Gone To The Store BRB School",
		"Bigger Baller Brandr School",
		"Prof. Javier School For The Gifted",
		"Tax Break Donation School",
		"101 Dalmations In This School",
		"Some Kind Of Suicide Squad High School",
		"Fingers Hurt From Typing Episcopal School",
		"Jimmy G Stands For Buckets Episcopal School",
		"Drinking Helps Me Sleep At Night Prepatory",
		"Mommy Needs Her Wine Daycare",
		"Make A Wish Foundation",
		"Bear Trap High",
		"James Corden Not Nice Upper School",
		"HanSolo Dies Film School",
		"Corbin Bleu Cooking School",
		"Rats! But Smart So School",
		"Boss Baby 7/10 Movie Prep",
		"Mitsubishi After High School",
		"Home Schooled Fuckers",
		"Scuffed Academy",
		"Bear Grylls Grilling Academy",
		"Grylls Bear Survival School",
		"Lower Esteem Upper School",
		"Big Shaq In The Paint Art School",
		"Garfield's Online School",
		"Miniclip.com Instutute of Science",
		"Seeing Stars Astronomy School",
		"School For Unfortunate Events",
		"Brendan Fraser Comeback Theatre School",
		"Jim Carrey's Gym",
		"Blue-eyes White High School",
		"Michael Bay's Chemistry Studio",
		"Helen Keller's Academy of the Arts",
		"Bill Cosby's Cocktail Courses",
		"Jim Crowe High School",
		"General Booty Police Academy",
		"Yugoslavia High School of Science",
		"Academic Magnetic High School",
		"High School Transylvania",
		"Jerry Mander Ringville High",
		"Mr. Doctor Early College Academy",
		"Kevin Spacey Young Boy's Prepatory",
		"King's Landing High School",
		"Talkaton School for Young Leaders",
		"Columbus High School",
		"Pitbull's Worldwide School",
		"IKEA Frontier College Prepatory",
		"Southside BRRRT SKRRT High",
		"Asian Export College Prepatory",
		"No Line Not Crossed Secondary School",
		"McLean High School",
		"Suite School On Deck",
		"Cashew Cash Checks College Prep",
		"Sunken Dreams High",
		"Stanley Steamers Student Academy",
		"Use Protection Next Time High School",
		"twitch.tv/crabsforgrabs High School",
		"Sid Meier School For Young Leaders",
		"Attendance Is 30% Secondary School",
		"PornHub Premium Prepatory",
		"Fornite School of Intepretive Dance",
		"Lone Pine School for Lonely Boys",
		"Antivaccinator Institute of Technology",
		"ITTTTT Tech",
		"Pacific Rim Institute",
		"Dr. Wood Oakwood Conservatory",
		"Country Road Home School",
		"Valley Charter School for Troubled Teachers",
		"Vince Charter School",
		"N. Cage School of Fine Arts",
		"USC First-Class Donation-First High School",
		"International Student Training School of UCLA",
		"R. L. Stine Instutute of Journalism",
		"Wa Ge Secondary School",
		"Liverty Academy for Young Bartenders",
		"Sea Me Now Naval Academy",
		"Burts Bees High School",
		"Harvard Tearslake",
		"Denny's High School of Culinary Arts",
		"Pacer Test School",
		"DadPlzNotTheBeltDadImSorryPlzStopDadStop High School",
		"One Almond Conservatory",
		"Magnetic Induction Magnet School",
		"Mesomorph Institute of Technology",
		"Ratatouille School of Culinary Arts for Young Rats",
		"Magneto's Magnet School for The Gifted",
		"Bread Makes You Fat High School",
		"Obscure Movie Reference School of Fine Arts",
		"Chipotle Foundation High School",
		"Optimus Prime High School of Science",
		"Chegg School for Altruistic Students",
		"St. Antetokounmpo's Episcopal School",
		"Lil Pump Mountainview Charter School",
		"Malibu Seabreeze High School",
		"Himachi Long Distance Connection HS",
		"'If at first you don't succeedle turn to the needle' School",
		"HotTub Toaster High",
		"Forestfire Hills High School",
		"Bencil Sharperino School of Journalism",
		"K. Lamar South L.A. High School",
		"GoT Season 8 Is A Travesty High School",
		"Stamford American School",
		"G.R. Wolfvalley High School",
		"Supreme HS",
		"Juul High School",
		"Big Wins High School",
		"Dwayne R. Johnson Prep",
		"Dangerous Coastline High",
		"Notch Mineville Academy of The Arts"
	};

	void Awake() {
		if (RandomAgreements.instance == null) {
			RandomAgreements.instance = this;
		} else {
			Destroy(this);
		}

		//I need to do this at Awake cuz it's not loading before the gamemanager that's calling it
		//fill out high school names. Feel free to come up with as many as you can think of :) We can reuse a lot of them for purchasing satellite campuses
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
            pool = Random.Range(85, 100);
            cost = 300;
        }
        else if (val == 2) {
            pool = Random.Range(75, 85);
            cost = Random.Range(400, 500);
        }
        else if (val == 3) {
            pool = Random.Range(55, 75);
            cost = Random.Range(600, 750);
        }
        else if (val == 4) {
            pool = Random.Range(35, 55);
            cost = Random.Range(850, 950);
        }
        else {
            pool = Random.Range(10, 35);
            cost = 1100;
        }
        return (new HighSchoolAgreement(name, pool, val, cost));
    }

}
