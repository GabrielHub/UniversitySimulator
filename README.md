# University Simulator: Build and grow a University to take over the world!

## Design Document

Lose State: When you have 0 students and cannot keep growing.
Win State: (Currently) When hitting 100k Students or something

Early Game: *(To be completed)*
Mid Game: *(To be completed)*
End Game: *We're in it now.*


### NOTE: ALL THESE DESIGN DECISIONS DESCRIBE HOW THE GAME WORKS NOW (TO BE CHANGED IN THE FUTURE)
	Currently all under a single script, and game manager.

			Rules for resources:
			DONE - Every turn 20% of the total students graduate. Flat increase -> Alumni ++
			If Faculty is over 10% (default percent) of the Students number. Every building can accomodate 350 students. Student is allowed to increase. (Default is one faculty per 10 students, can be changed)
			Each student grants by default 2 material per turn (can be changed). Every 5 alumni grants permanent 1 material per turn. Material ++
			Each Faculty takes 1 material per turn. If you set buildings to give bonuses, they also detract material. Material --

			Buy Menu:
			You can buy buildings with material.
			You can hire faculty with material.

Difficulty determines how outcomes are done through percentages. A higher difficulty means when an event happens, the percentage something bad happens increases.
    `difficulty = ((students * sliderTuitionVal + alumni * 2 + faculty + buildings * 10) / wealth) + (sliderTuitionVal / 5) + (sliderFacultyToVal / 25)`

**Resources:**

	Wealth - Hidden resource defined by the accumulation of all other resources. Used to determine difficulty
		Wealth = students + faculty + alumni + buildings * 200

	Students - Determining growth counter. Grows depending on certain resources (algorithm below). Provides materials in terms of yearly tuition.
		if students are smaller than the max allowed by faculty, and students are less than the max allowed by buildings, then:
			students += the growth bonus * students * (1 - (students / (350 * # of buildings)))

	Alumni - A counter for how many students have graduated. Each alumni grants a small amount materials as donations
		students -= Mathf.FloorToInt(students / 5) and alumni += that same amount

	Buildings - Determines how much Faculty you can accumulate. Costs donations to purchase more. Buildings can provide a building bonus, whether positive or negative
		must be bought

	Faculty - Determines how many Students you can have at one time. Can be purchased.
		Can only increase through material purchases or random events
		otherwise is decreased when your materials aren't enough to pay for faculty, faculty -= Mathf.FloorToInt(((faculty - material) / 2))

	Materials - Resource used to spend used for actions
		material += (alumni / 5 (rounded down to an int))  + ((tuition of students) * students) + (buildings bonus)

**Ideas:**

	- Maybe we can choose to set the tuition rates to 0 and receive government accomodation when close to losing
	- Some of the resources can be objects rather than just numbers, like buildings that have research labs or something
	- Start with a different university. So rather than difficulty determined entirely by wealth, difficulty can be determined by your start. (start with a differnet number of students and buildings and so on)# UniversitySimulator


## TASKS TO DO and TASKS FINISHED

**A list of tasks to do / code to update:**

	1. Redesign Resources
	2. Redesign UX/UI
	3. Redesign Events
	4. Code Refactor
		- ~~Implement the basics so it's working on a clean slate~~ **DONE**
		- Events aren't working because of the change from Turn Based to Real Time, needs to be rewritten