using UnityEngine;
using System.Collections;

/*
** This script is for documentation of the street, its intersections points and anything layout related for our reference.
**
*/
public class LayoutDocumentation : MonoBehaviour {
}

/*

STREETS: 
(note: all streets have a slightly positive y value so they appear above the BasePlane)

CenterStreet: The main road that is X-oriented.

FirstStreet: The Z-oriented street with the negative X-value.

SecondStreet: The Z-oriented street with the 0 x-value.

ThirdStreet: The Z-oriented street with the positive x-value.



INTERSECTION POINTS:
(note: technically all of these points should have Y-value of 0.01, but that's just for the street)

First and Center: (-10, 0, 0)

Second and Center: (0, 0, 0)

Third and Center: (10, 0, 0)



CARS:

All cars are scale 0.3 and should always have Y-value of 0.15 to keep above road


*/