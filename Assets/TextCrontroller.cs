using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCrontroller : MonoBehaviour {

	// Use this for initialization
	public bool canScroll;
	private RectTransform transform ;
	public float scrollSpeed;
	void Start () {
		transform = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void FixedUpdate() {
		if(canScroll){
			transform.position += Vector3.up*scrollSpeed;
		}
	}
}
