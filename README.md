# University Simulator: Build and grow a University to take over the world!

## Design Document
*Italicized things are to be changed or designed*

### Win / Lose State
- Lose State: *When you have 0 students*
- Win State:  *When hitting 500k Students*

### Game Phases
- Early Game: *(To be completed)*
- Mid Game: *(To be completed)*
- End Game: *We're in it now.*

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
	
- Students is decreased and Alumni is increased (based on NYU graduation ratio)
- Students increase determined by *student growth rate* and *carrying capacity*. (based on logistic population growth model)
	- *student growth rate* should be small and determined by Wealth and Renown.
	- *carrying capacity* is determined by Faculty and Buildings.
- Wealth is increased by Alumni and Students.
- Renown is determined by Faculty and Buildings.
- Faculty can be bought with Wealth

Types of Resources:

1. Students - Determining growth counter. Grows depending on certain resources (algorithm below). Provides materials in terms of yearly tuition.

2. Alumni - A counter for how many students have graduated. Each alumni grants a small amount materials as donations

3. Buildings - Determines how much Faculty you can accumulate. Costs donations to purchase more. Buildings can provide a building bonus, whether positive or negative

4. Faculty - Determines how many Students you can have at one time. Can be purchased.

5. Wealth - Resource used to spend used for actions

### Ideas:
- The option to expand with satellite campuses that are somewhat run on their own?
- Maybe we can choose to set the tuition rates to 0 and receive government accomodation when close to losing
- Some of the resources can be objects rather than just numbers, like buildings that have research labs or something
- Start with a different university. So rather than difficulty determined entirely by wealth, difficulty can be determined by your start. (start with a differnet number of students and buildings and so on)


## TASKS TO DO and TASKS FINISHED

**A list of tasks to do / code to update:**

- [ ] **Redesign Core**
	- [ ] Resources and Balance
		- [ ] Define Resources (are there different types of buildings?)
		- [ ] Redesign how each resources affects the other (algorithm, relations)
		- [ ] Win / Lose States
		- [ ] Change Buildings from numeric resource to an object and a seperate tab to manage it
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
- [ ] **Additional Features**
	- [ ] Game scales with resolution
		- [x] Text scales with resolution