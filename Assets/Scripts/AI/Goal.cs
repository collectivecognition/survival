using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Goal {
	public bool active = false;
	public GameObject gameObject;

	public Goal(GameObject _gameObject){
		gameObject = _gameObject;
	}

	public virtual float Priority(){
		return 0.0f;
	}

	public virtual void Init(){}
	public virtual void Achieve(){}
}