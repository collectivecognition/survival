using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// http://www.aaai.org/Papers/AIIDE/2005/AIIDE05-006.pdf

public class AI : MonoBehaviour {
	private class Goal {
		public bool active = false;

		public float Priority(){

		}
	}

	private List<Goal> goals = new List<Goal>();

	void Start () {
	
	}

	void Update () {
		// Decide which goal is the most important

		// Work toward that goal
	}
}
