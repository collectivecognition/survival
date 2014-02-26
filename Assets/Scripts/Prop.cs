using UnityEngine;
using System.Collections;

public class Prop : MonoBehaviour {

	public bool grabbable;
	public bool edible;
	public bool vegetable;
	public float calories;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Show the player that they can interact with this prop

	private Color originalColor;
	public Color highlightColor = Color.yellow;

	public void Highlight(bool on) {
		if(on){
			originalColor = renderer.material.color;
			renderer.material.color = highlightColor;
		}else{
			if(originalColor != null){
				renderer.material.color = originalColor;
			}
		}
	}
}
