Lab 6 Readme
Matt Cramblett & Matias Grotewold

Implementation Ideas and Concepts:
4-way intersection, building on each corner. Pedestrians generate at building doors, turn left or right
	- the combination of the direction in which they are walking (N/E/S/W) and the side of the road (L/R) can determine how they handle the intersection (?)
Cars generate at road entry/exit points at edge of visible area. Need green/red lights to signify when cars should stop or continue.
	- cars will need speed and rotation
Maybe have intersections as their own object?

- If collision between car and person occurs, end simulation? We should avoid this happening