﻿using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(PolygonCollider2D))]
[AddComponentMenu("Navigation/PolyNavObstacle")]
///Place on a game object to act as an obstacle
public class PolyNavObstacle : MonoBehaviour {

	///Inverts the polygon (done automatically if collider already exists due to a sprite)
	public bool invertPolygon = false;

	private Vector3 lastPos;
	private Quaternion lastRot;
	private Vector3 lastScale;
	private Transform _transform;

	[SerializeField] [HideInInspector]
	private PolygonCollider2D polyCollider;

	///The polygon points of the obstacle
	public Vector2[] points{
		get
		{
			if (polyCollider == null)
				polyCollider = GetComponent<PolygonCollider2D>();

			Vector2[] tempPoints = polyCollider.points;

			if (invertPolygon)
				System.Array.Reverse(tempPoints);

			return tempPoints;			
		}
	}

	private PolyNav2D polyNav{
		get {return PolyNav2D.current;}
	}

	void Reset(){
		
		if (GetComponent<SpriteRenderer>() != null)
			invertPolygon = true;
	}

	void  OnEnable (){

		if (polyNav)
			polyNav.AddObstacle(this);
		
		polyCollider.isTrigger = true;
		lastPos = transform.position;
		lastRot = transform.rotation;
		lastScale = transform.localScale;
		_transform = transform;
	}

	void  OnDisable (){

		if (polyNav)
			polyNav.RemoveObstacle(this);
	}

	void  Update (){
		
		if (!polyNav || !polyNav.generateOnUpdate || !Application.isPlaying)
			return;

		if (_transform.position != lastPos || _transform.rotation != lastRot || _transform.localScale != lastScale)
			polyNav.GenerateMap();

		lastPos = _transform.position;
		lastRot = _transform.rotation;
		lastScale = _transform.localScale;
	}
}
