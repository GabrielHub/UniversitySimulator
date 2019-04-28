# University Simulator: Build and grow a University to take over the world!

## Design Document
*Italicized things are to be changed or designed*

### Game Phases
- Early Game: 100k students
- Mid Game: Top Ranked College
- End Game: Only University in the world

- Lost State: 0 students

### Difficulty
*TO BE REDESIGNED*

### Events
*TO BE REDESIGNED*

### UI/UX
(Based on rimworld UI?)
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

### Design Changes
- [ ] **Redesign Core**
	- [x] Resources and Balance v1
		- [x] Define Resources (are there different types of buildings?)
		- [x] Redesign how each resources affects the other (algorithm, relations)
		- [x] Win / Lose States
	- [ ] Resources and Balance v2
		- [ ] Redesign how difficulty works
	- [ ] Redesign Events
- [ ] **Redesign UX/UI**
	- [x] How the resources are displayed
	- [ ] How to buy buildings
- [x] **Graphics**
	- [x] Graphics decisions for v1
	- [ ] Drag and Drop features

### Code Changes
- [x] **Code Refactor**
	- [x] Implement the basics so it's working on a clean slate
- [ ] **Core**
	- [x] Resources and Balance
		- [ ] Implement Changes for V1 redesign
	- [ ] Gameplay Loop
		- [x] Change from Turn based click to Real Time
		- [x] Add option to pause, either on events or something else
		- [ ] New Event System
		- [ ] Change Buildings from numeric resource to a dictionary and a seperate tab to manage it
- [ ] **UX/UI**
	- [ ] Convert Event Log system to a simple messaging system.
	- [ ] Change resource display to images instead of text
- [ ] **Graphics**
	- [ ] Create background
	- [ ] Create Basic Tile Map
	- [ ] Drag and Drop features
	- [x] Game scales with resolution
		- [x] Text scales with resolution
	