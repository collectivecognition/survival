using UnityEngine;
using System.Collections;

public class CollisionSetup : MonoBehaviour {

	void Awake () {
		var crittersLayer = LayerMask.NameToLayer ("Critters");
		Physics2D.IgnoreLayerCollision (crittersLayer, crittersLayer, true);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
