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

Rules for resources:
	
- Every turn 20% of the total students graduate: Alumni Increases by 20% of Students, Students decrease by 20%.
- If Faculty is over 10% (default percent) of the Students number. Every building can accomodate 350 students. Student is allowed to increase. (Default is one faculty per 10 students, can be changed)
- Each student grants by default 2 material per turn (can be changed). Every 5 alumni grants permanent 1 material per turn. Material ++
- Each Faculty takes 1 material per turn. If you set buildings to give bonuses, they also detract material. Material --

Types of Resources:

1. Students - Determining growth counter. Grows depending on certain resources (algorithm below). Provides materials in terms of yearly tuition.
	- if students are smaller than the max allowed by faculty, and students are less than the max allowed by buildings, then:
		- Relation: students += the growth bonus * students * (1 - (students / (350 * # of buildings)))

2. Alumni - A counter for how many students have graduated. Each alumni grants a small amount materials as donations
	- Relation: students -= Mathf.FloorToInt(students / 5) and alumni += that same amount

3. Buildings - Determines how much Faculty you can accumulate. Costs donations to purchase more. Buildings can provide a building bonus, whether positive or negative
		must be bought

4. Faculty - Determines how many Students you can have at one time. Can be purchased.
	- Can only increase through material purchases or random events
		- otherwise is decreased when your materials aren't enough to pay for faculty, faculty -= Mathf.FloorToInt(((faculty - material) / 2))

5. Materials - Resource used to spend used for actions
	- Relation: material += (alumni / 5 (rounded down to an int))  + ((tuition of students) * students) + (buildings bonus)

### Modifiers / Algorithms?:


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