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

	Done using the EventController script. Uses an event ticker that counts down the time since the last event.
	Picks a random value in a range. When the ticker hits that number an event happens.
	Events can be helpful or damaging. Currently based on a random value

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
