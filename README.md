# University Simulator: Build and grow a University!
A Simulation game built in Unity 2019.1.0f2

## Early Game:

**Premise:**

	The university is brand new and doesn't have enough students to be recognized as a university yet! The school relies on local highschools to funnel students into SADU. Starting with generated high schools every couple of turns, SADU picks new agreements that can be upgrades or downgrades.

**Game Flow:**

- EarlyGame1: First stage of the game you only have students and wealth. Buy faculty upgrades
- EarlyGame2: Next stage at 50 students unlocks High School Agreements
- EarlyGame3: After you hit 100 students you can unlock the ability to graduate students and create alumni. These will provide a permanent boost to wealth growth. Use this to buy HSA agreements. 
- EarlyGame4: After 500 students, renown and happiness now affect student capacity. Unlock advanced statistics upgrade. Unlock policies
- (Happens during EarlyGame4): After 1000 students you can now move onto the midgame by buying two of the newly unlocked upgrades

**Focus:**
	
	* Tuition Rate Slider - Affects Happiness and Wealth. Higher Tuition rate decreases happiness and increases wealth per turn.
	* Acceptance Rate Slider - Determines how many of the applied students will enter the University. Higher acceptance rate increases students but decreases renown per turn.
	* Highschool Agreements - New agreements appear each turn and must be purchased. Agreements affect the max pool of students who can apply, as well as renown.
	* Alumni Donation Rate: Affect wealth gained by Alumni. Setting it too high when happiness is low will cause Alumni to denounce the university and alumni number decreases
	
**Details:**

- Student Body: Main Goal
- Wealth: Soft Goal
- Happiness: Affects number of students who apply to the university
- Renown: Affects growth rate of faculty
- Available Upgrades: Hire Faculty (repeatable purchase that increases in cost each time), Degrees (Allows you to graduate students), (Hire Administrators (Unlocks visibility of hidden resources), Official Campus (Needed to unlock next phase), Official University Certificate (needed to unlock next phase)
	
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
		- Buildings have different types (Residential, Educational, Institutional, and Athletic)
	* Faculty Pay Slider - Change how much faculty affects wealth per turn. A higher amount decreases wealth, but increases renown and happiness.
	* Ranking - A new modifier out of 1000. Based on renown, graduation rate. Also modified by Special Students and capped by avg rating of buildings.
	* Student To Faculty Ratio Slider - The number of students each faculty will take care of. Higher ratio increases graduation rate but decreases happiness.
	* Special Students - A chance of a special student occurs every x turns, depending on renown and happiness. Spend a certain amount of wealth to give scholarships to special students.

**Details:**

- Ranking: Main Goal
- Buildings: Soft Goal
- Graduation Rate: Now based on Students to Faculty Ratio.
- Renown: Now based on Faculty Pay and happiness. A static boost based on the avg Stars of High School Agreements from the previous stage carry over.
- High School Agreements: No longer available or a factor, converted to a static boost or detriment depending on the high schools you aquired.
- Random Events: Are now active. Random chance of an event happening every turn, that can be helpful or hurtful depending on how well you're doing.
- Available Upgrades: Marketing Campaign I (increases renown by a small amount), Marketing Campaign II (increasaes HSA static boost by a small amount), Financial Aid Program (Increases odds of special student), Advanced Analysis (upgrades the statistics page), *Design later when implemented*
- Student To Faculty: The max value can be increased by Educational buildings, while the min value will be decided by the amount of students / faculty. If the capacity of students is too much for the amount of faculty you've assigned, graduation rates will suffer.

**Lose Condition:**
- No wealth or students
- Fall out of national ranking (only a lose condition once you make it past 50)

**Win Condition:**
- Become number 1 ranked national university, certain amount of buildings

## End Game: NOT IMPLEMENTED

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
- Faculty Growth: Now a static linear function based on building upgrades.
- Alumni: Now a static amount every turn.
- Renown: Now based on building upgrades.
- University Count: All the universities left in the world.
- Available Upgrades: Campus Capacity (Allows you to remove limits on buying campuses), A.I. Administration (Unlocks full statistics tab), *Design others once implemented*

**Lose Condition:**
- There's a limited amount of endowment. If spread too thin you may not longer be able to progress.

**Win Condition:**
- Become the only University

### Events

**Types of Events:**

- Random Events: In the EventController script. A random event can happen between a range and can be helpful or damaging and manipulates resources
- Feature Events: When a new feature becomes available they send a message to the event log notifying the player.
- GameState Events: When in a lose state, or when moving to another game phase.
- Narrative Events: Acts as goals to achieve, notifies you whne there's a new goal, with a premise to string the player along a narrative
- Notification Events: When new buyables are available, or when a resource has been low for a while.

**Implementation:**

Uses a messaging system, push new events to the EventController using DoEvent() when you want a random event to happen.

Each event will flash a different color to show what kind of event it is.

### UI/UX

- Tabs on the bottom of the screen for **Event Log** and one for each resource to manage them.
- The **Event Log** button, will open or close an Event Log Object in the top middle of the screen to show you all past events.
- **Event Log** will be a "simple messaging system which will allow items in our projects to subscribe to events, and have events trigger actions in our games. This will reduce dependencies and allow easier maintenance of our project."
- **Play / Pause** button will be on the bottom right above the tabs
- **Resources** will be in the top left
- Tabs include a **Buy Menu**, an **Advanced Stats** tab, and an **Interactables** tab.
- We've extended .ToAbbreviatedString() to convert integer/floats to rounded strings (1000 == 1K, or 1M) (make sure each numerical string uses this function)

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
