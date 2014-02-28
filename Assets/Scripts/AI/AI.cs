using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// http://www.aaai.org/Papers/AIIDE/2005/AIIDE05-006.pdf

public class AI : MonoBehaviour {
	private List<Goal> goals = new List<Goal>();

	void Start () {
		goals.Add (new ExploreGoal (gameObject));
	}

	void Update () {
		// Decide which goal is the most important

		// Work toward that goal
		foreach(Goal goal in goals){
			goal.Achieve ();
		}
	}
}
