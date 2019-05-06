# University Simulator: Build and grow a University!
A Simulation game built in Unity 2019.1.0f2

## Early Game:

**Premise:**

	The university is brand new and doesn't have enough students to be recognized as a university yet! The school relies on local highschools to funnel students into SADU. Starting with generated high schools every couple of turns, SADU picks new agreements that can be upgrades or downgrades.

**Focus:**
	
	* Tuition Rate Slider - Affects Happiness and Wealth. Higher Tuition rate decreases happiness and increases wealth per turn.
	* Acceptance Rate Slider - Determines how many of the applied students will enter the University. Higher acceptance rate increases students but decreases renown per turn.
	* Highschool Agreements - New agreements appear each turn and must be purchased. Agreements affect the max pool of students who can apply, as well as renown.
	* Alumni Donation Rate: Affect wealth gained by Alumni. Setting it too high when happiness is low will cause Alumni to denounce the university and alumni number decreases
	
**Details:**

- Student Body: Main Goal
- Wealth: Soft Goal
- Happiness: Affects number of students who apply to the university
- Buildings: Given 3 at the start, you cannot buy new buildings in this stage. Affects the starting student pool.
- Renown: Affects growth rate of faculty
- Available Upgrades: Hire Administrators (Unlocks visibility of hidden resources), Official Campus (Needed to unlock next phase), Official University Certificate (needed to unlock next phase)
	
**Lose Condition:**
- No wealth or students

**Win Condition:**
- 1k Students unlocks two required purchases.
- To move on, you need to purchase **Official Campus** costing 3K wealth and **Official University Certificate** costing 2K wealth

## Mid Game:

**Premise:**
	
	Now that the University is officially recognized, it can grow by buying buildings, hiring specialists, and no longer needs high school agreements. The university needs to rise in the rankings, so management of the renown and graduation rate of the University becomes vital.

**Focus:**

	* Building Management - Introduces clickable tilemap mechanic, building management tab, and StudentPool and Faculty is now capped by the buildings you have
		- Buildings have different types, that come with different bonuses
	* Faculty Pay Slider - Change how much faculty affects wealth per turn. A higher amount decreases wealth, but increases renown and happiness.
	* Ranking - A new modifier out of 100 that updates every 5 turns. Based on renown, graduation rate. Also modified by Building Bonuses and Special Students.
	* Student To Faculty Ratio Slider - The number of students each faculty can take care of. Higher ratio increases graduation rate but decreases happiness.
	* Special Students - A chance of a special student occurs every 4 turns, depending on renown and happiness. Spend a certain amount of wealth to give scholarships to special students.

**Details:**

- Ranking: Main Goal
- Buildings: Soft Goal
- Graduation Rate: Now based on Students to Faculty Ratio.
- Renown: Now based on Faculty Pay, Student To Faculty Ratio, and boosted by Building and Special Student bonuses. A static boost based on the avg Stars of High School Agreements from the previous stage carry over.
- High School Agreements: No longer available or a factor, converted to a static boost or detriment depending on the high schools you aquired.
- Random Events: Are now active. Random chance of an event happening every turn, that can be helpful or hurtful depending on how well you're doing.
- Available Upgrades: Marketing Campaign I (increases renown by a small amount), Marketing Campaign II (increasaes HSA static boost by a small amount), Financial Aid Program (Increases odds of special student), Advanced Analysis (upgrades the statistics page), *Design later when implemented*

**Lose Condition:**
- No wealth or students
- Fall out of national ranking (only a lose condition once you make it past 50)

**Win Condition:**
- Become number 1 ranked national university, certain amount of buildings

## End Game:

**Premise:**
	
	Who likes Monopoly? The University demands a universal takeover! By this stage of the game, the goal of the University is to become the single University for everyone! Start off by buying other campuses to serve as sattelite campuses until you can take over other full universities!

**Focus:**

	* Building Management - You must manage your buildings by upgrading them to specific types of buildings. (Buildings focused on housing, lecture halls, research, so on) Uses endowment and permanently reduces the amount of endowment you have.
	* Campus Management - While you no longer worry about student growth, students can now be spent on buying a new University/Campus when they become available.
	* Endowment - Ranking and Wealth are no longer factors, and instead the amount of wealth you have is converted into endowment. Endowment is a budget that is spent every turn.
	* Power - New resource that depends on renown and the number of campuses you have. Now used to unlock new Campus/Universities to buy and at higher amounts destroys competing Universities.
	* Special People - A chance of a special person occurs every 4 turns, chance can be increased using endowment. Special people can give you connections to other other Campuses that allow you to purchase them.

**Details:**

- Power: Main Goal
- Campuses: Soft Goal
- Student Growth: Now a static linear function based on building upgrades.
- Faculty Growth: Now a static linera function based on building upgrades.
- Alumni: Now a static amount every turn.
- Renown: Now based on building upgrades.
- University Count: All the universities left in the world.
- Available Upgrades: Campus Capacity (Allows you to remove limits on buying campuses), A.I. Administration (Unlocks full statistics tab), *Design others once implemented*

**Lose Condition:**
- There's a limited amount of endowment. If spread too thin you may not longer be able to progress.

**Win Condition:**
- Become the only University

### Events

	Done using the EventController script. Uses an event ticker that counts down the time since the last event.
	Picks a random value in a range. When the ticker hits that number an event happens.
	Events can be helpful or damaging. Currently based on a random value

### UI/UX

- Tabs on the bottom of the screen for **Event Log** and one for each resource to manage them.
- The **Event Log** tab, will open or close a transparent Event Log Object in the top middle of the screen.
- **Event Log** will be a "simple messaging system which will allow items in our projects to subscribe to events, and have events trigger actions in our games. This will reduce dependencies and allow easier maintenance of our projects."
- **Play / Pause** button will be on the bottom right above the tabs
- **Resources** will be in the top left
- Tabs include a **Buy Menu**, an **Advanced Stats** tab, and an **Interactables** tab.

### Balance and Difficulty:

Rules for resources (every turn):
	Look at Google Slides Chart

Types of Resources:

1. Students - Determining growth counter. Grows depending on certain resources (algorithm below). Provides materials in terms of yearly tuition. Decrease wealth for each student as well based on amount of students and renown.

2. Alumni - A counter for how many students have graduated. Each alumni grants a small amount of materials as donations

3. Buildings - Determines how much Faculty you can accumulate. Costs donations to purchase more. Buildings can provide a building bonus, whether positive or negative

4. Faculty - Determines how many Students you can have at one time. Can be purchased. Decreases wealth for each faculty

5. Wealth - Resource used to spend used for actions and used to maintain students/faculty.

Hidden Resources:

1. Student Growth (r) - Determines how many students you get per turn

2. Happiness - Factors into student growth

3. Renown - Determines how much each student costs and increases student growth

4. Student Pool - Determines max number of students you can get per turn

5. Acceptance Rate - Factors into student growth
