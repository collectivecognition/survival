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

	public float maxHealth = 100.0f;
	public float health = 100.0f;
	public float starvationDamageRate = 10.0f;

	void EvaluateNeeds () {
		// Evaluate each need in the context of our current environment and prioritize accordingly
		// Each boils down to a score between 1 and 100
		// These scores are then weighted against each other on a per-critter basis. IE: Some types of critter will prioritize eating
	}

	// Use this for initialization
	void Start () {
	
	}

	void Update () {
		if (calories > 0) {
			// Consume calories
			// TODO: Different burn rates for different activities

			calories -= calorieBurnRate * Time.deltaTime;
			if(calories < 0){ calories = 0; }
		}else {
			// Take damage while starving

			health -= starvationDamageRate * Time.deltaTime; // TODO: Take damage in chunks, not constantly
			if(health < 0){ health = 0; }
		}

		if(health < 0){
			// Die of starvation

			Destroy(gameObject); // FIXME
		}
	}

	void FixedUpdate () {

	}

	void OnGUI () {
		GUI.Box(new Rect(10, 10, 200 * health / maxHealth,  10), "Health");
		GUI.Box(new Rect(10, 30, 200 * calories / maxCalories, 10), "Calories");
	}
}