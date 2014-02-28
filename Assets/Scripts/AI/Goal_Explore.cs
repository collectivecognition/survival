using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExploreGoal : Goal {
	private float targetPos;
	private float arrivedDistance = 0.2f;
	private float moveSpeed = 50.0f;

	public ExploreGoal(GameObject _gameObject) : base(_gameObject){
		PickNewDestination();
	}

	public override void Achieve(){
		float direction = gameObject.transform.position.x > targetPos ? -1f : 1f;
		if(Mathf.Abs(gameObject.transform.position.x - targetPos) > 0.2f){
			gameObject.SendMessage("Walk", direction * 10f);
		}else{
			PickNewDestination();
		}
	}

	public void PickNewDestination(){
		targetPos = Random.Range (5f, 10f) * (Random.value < .5f ? -1 : 1);
	}
}