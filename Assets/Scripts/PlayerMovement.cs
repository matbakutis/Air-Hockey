using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public Transform BoundaryHolder;

	private Rigidbody2D rb;
	private Vector3 pos;

	bool wasJustClicked = true;
    bool canMove;
	Boundary playerBoundary;

	struct Boundary {
		public float Up, Down, Left, Right;
		public Boundary(float up, float down, float left, float right){
			Up = up; Down = down; Left = left; Right = right;
		}
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		playerBoundary = new Boundary(BoundaryHolder.GetChild(0).position.y,
									  BoundaryHolder.GetChild(1).position.y,
									  BoundaryHolder.GetChild(2).position.x,
									  BoundaryHolder.GetChild(3).position.x);
	}
	
	void Update(){
		
//        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved){
//			pos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 0));
//			rb.position = new Vector3 (pos.x, pos.y, pos.z);
//			Debug.Log(rb.position);
//			Debug.Log(pos.x);
//			Debug.Log(pos.y);
//			Debug.Log(pos.z);
//        }
		if (Input.GetMouseButton(0)){
			pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (wasJustClicked){
				wasJustClicked = false;
				if(rb.OverlapPoint(pos)){
					canMove = true;
				}else {
					canMove = false;
				}
			}
			if (canMove){
				
				Vector2 clampedInput = new Vector2(Mathf.Clamp(pos.x, playerBoundary.Left, playerBoundary.Right), Mathf.Clamp(pos.y, playerBoundary.Down, playerBoundary.Up));
				rb.MovePosition(clampedInput);
			}
		}else{
			wasJustClicked = true;
		}
    }
}
