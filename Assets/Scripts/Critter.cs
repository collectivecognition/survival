using UnityEngine;
using System.Collections;

public class Critter : MonoBehaviour {
	
	public enum Needs {
		Food,
		Water,
		Mating,
		Sleep,
		Safety,
		Companionship,
		SafetyOfYoung,
		FoodForYoung,
		Num
		// Excretion
	}

	public float[] needs = new float[(int)Needs.Num];

	public float maxCalories = 100.0f;
	public float calories = 100.0f;
	public float calorieBurnRate = 10.0f; // Burn rate per second. 

	void EvaluateNeeds () {
		// Evaluate each need in the context of our current environment and prioritize accordingly
		// Each boils down to a score between 1 and 100
		// These scores are then weighted against each other on a per-critter basis. IE: Some types of critter will prioritize eating
	}

	// Use this for initialization
	void Start () {
	
	}

	void Update () {
		// Consume calories
		// TODO: Different burn rates for different activities

		calories -= calorieBurnRate * Time.deltaTime;

		// Die of starvation

		if (calories < 0) {
			Destroy (gameObject); // FIXME
		}
	}

	void FixedUpdate () {

	}
}
