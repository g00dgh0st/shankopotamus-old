using UnityEngine;
using System.Collections;
 
[RequireComponent(typeof(MeshFilter))]
public class ReverseNormals : MonoBehaviour {
 
	void Start () {
		MeshFilter filter = GetComponent(typeof (MeshFilter)) as MeshFilter;
		if( filter != null ) Game.ReverseNormals( filter.mesh );		
	}
}