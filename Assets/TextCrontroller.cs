using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCrontroller : MonoBehaviour {

	// Use this for initialization
	public bool canScroll;
	private RectTransform transform ;
	public RectTransform canvasRect;
	private RectTransform childPoint;
	public RectTransform body;
	public float scrollSpeed;
	void Start () {
		transform = GetComponent<RectTransform>();
		//canvasRect = GetComponentInParent<RectTransform>();
		childPoint = body.GetComponentInChildren<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void FixedUpdate() {
		if(canScroll){
			transform.position += Vector3.up*scrollSpeed;
			if(childPoint.position.y > canvasRect.position.y){
				canScroll = false;
			}
			print(childPoint.position+ "??" + canvasRect.position) ;

		}
	}
}
