# University Simulator: Build and grow a University to take over the world!

## Design Document
*Italicized things are to be changed or designed*

### Game Phases
- Early Game: 100k students
- Mid Game: Top Ranked College
- End Game: Only University in the world

- Lost State: 0 students

### Difficulty
Difficulty determines how outcomes are decided through percentages. A higher difficulty means when an event happens, the percentage something bad happens increases.
    `difficulty = ((students * sliderTuitionVal + alumni * 2 + faculty + buildings * 10) / (students + faculty + alumni + buildings * 200)) + (sliderTuitionVal / 5) + (sliderFacultyToVal / 25)`

	*Events happen every second, but whether they help or hurt you is determined by the difficulty value*
	*When you buy or interact with something, there is also a chance that something good or bad happens which is affected by difficulty*

### Events
Happens every turn (second) and can be negative or positive.

### UI/UX

- Tabs on the bottom of the screen for **Event Log** and one for each resource to manage them.
- The **Event Log** tab, will open or close a transparent Event Log Object in the top middle of the screen.
- **Event Log** will be a "simple messaging system which will allow items in our projects to subscribe to events, and have events trigger actions in our games. This will reduce dependencies and allow easier maintenance of our projects." (There's a Unity tutorial on how to do this on the unity website)
- **Play / Pause** button will be on the bottom right above the tabs
- **Resources** will be in the top left

### Resources:

Rules for resources (every turn):
	Look at Google Slides Chart

Types of Resources:

1. Students - Determining growth counter. Grows depending on certain resources (algorithm below). Provides materials in terms of yearly tuition.

2. Alumni - A counter for how many students have graduated. Each alumni grants a small amount materials as donations

3. Buildings - Determines how much Faculty you can accumulate. Costs donations to purchase more. Buildings can provide a building bonus, whether positive or negative

4. Faculty - Determines how many Students you can have at one time. Can be purchased.

5. Wealth - Resource used to spend used for actions

### Ideas:
- Add some factors that will change rate of graduation
- Adjustable tuition rates
- Amount of donations from alumni
- How many students per faculty member
- Maximum class capacity
- Notable Alumni which have special effects on the university

## TASKS TO DO and TASKS FINISHED

**A list of tasks to do / code to update:**

- [ ] **Redesign Core**
	- [ ] Resources and Balance
		- [x] Define Resources (are there different types of buildings?)
		- [x] Redesign how each resources affects the other (algorithm, relations)
		- [x] Win / Lose States
		- [ ] Change Buildings from numeric resource to a dictionary and a seperate tab to manage it
	- [ ] Gameplay Loop
		- [x] Change from Turn based click to Real Time
		- [ ] Add option to pause, either on events or something else
		- [ ] New Event System
		- [ ] Change difficulty algorithm
- [ ] **Redesign UX/UI**
	- [x] How the resources are displayed
	- [ ] Convert Event Log system to a simple messaging system.
- [ ] **Redesign Events**
- [ ] **Code Refactor**
	- [x] Implement the basics so it's working on a clean slate
	- [ ] Events aren't working because of the change from Turn Based to Real Time, needs to be rewritten
- [ ] **Add Graphics**
	- [ ] Create background
	- [ ] Create Basic Tile Map
- [ ] **Additional Features**
	- [ ] Game scales with resolution
		- [x] Text scales with resolution