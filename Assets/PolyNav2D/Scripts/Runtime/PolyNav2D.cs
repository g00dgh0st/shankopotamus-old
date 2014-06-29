﻿#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(PolygonCollider2D))]
[AddComponentMenu("Navigation/Poly|Nav2D")]
///Singleton. Main class for map generation and navigation
public class PolyNav2D : MonoBehaviour {

	///If true map will recalculate whenever an obstacle changes position, rotation or scale.
	public bool generateOnUpdate = true;
	public List<PolyNavObstacle> navObstacles = new List<PolyNavObstacle>();
	///The layer that the obstacles will be assinged to.
	public int obstaclesLayer;
	///The radious from the edges to offset the agents.
	public float inflateRadius = 0.1f;

	[SerializeField]
	public PolygonCollider2D masterCollider;
	[SerializeField] [HideInInspector]
	private EdgeCollider2D edgeCollider;

	private PolyMap map;
	private List<PathNode> nodes = new List<PathNode>();

	static PolyNav2D _current;
	
	///The current instance of PolyNav2D
	public static PolyNav2D current{
		get
		{
			if (_current == null || !Application.isPlaying)
				_current = FindObjectOfType(typeof(PolyNav2D)) as PolyNav2D;
			
			return _current;
		}
	}

	public EdgeCollider2D borderCollider{
		get
		{
			if (edgeCollider == null){
				edgeCollider = new GameObject("PolyEdge").AddComponent<EdgeCollider2D>();
				edgeCollider.transform.parent = this.transform;
				edgeCollider.transform.localPosition = Vector3.zero;
			}
			return edgeCollider;			
		}
	}

	void Reset(){

		gameObject.name = "@PolyNav2D";
		masterCollider = GetComponent<PolygonCollider2D>();
		masterCollider.isTrigger = true;
		transform.position = new Vector3(transform.position.x, transform.position.y, 10);
	}

	void Awake(){

		if (_current != null && _current != this){
			//DestroyImmediate(gameObject, false);
			return;
		}

		_current = this;
		masterCollider.isTrigger = true;
		borderCollider.isTrigger = true;
		borderCollider.gameObject.layer = obstaclesLayer;
		GenerateMap();
	}

	///Adds a PolyNavObstacle to the map.
	public void AddObstacle( PolyNavObstacle navObstacle ){

		navObstacle.gameObject.layer = obstaclesLayer;
		
		if (!navObstacles.Contains(navObstacle)){
			navObstacles.Add(navObstacle);
			GenerateMap();
		}
	}

	///Removes a PolyNavObstacle to the map.
	public void RemoveObstacle ( PolyNavObstacle navObstacle ){
		
		navObstacles.Remove(navObstacle);
		GenerateMap();
	}


	///Find a path 'from' and 'to', providing a callback for when path is ready containing the path.
	public void FindPath(Vector2 start, Vector2 end, System.Action<List<Vector2>> callback){

		if (CheckLOS(start, end)){
			callback( new List<Vector2>( new Vector2[] {start, end} ));
			return;
		}

		//make a temp list with all existing nodes cloned
		List<PathNode> tempNodes= new List<PathNode>();
		foreach(var node in nodes)
			tempNodes.Add(node.Clone());

		//create start & end as nodes
		PathNode startNode = new PathNode(start);
		PathNode endNode = new PathNode(end);

		//add start and end nodes
		tempNodes.Add(startNode);
		tempNodes.Add(endNode);
		
		//link only those with the rest
		LinkNode(startNode, tempNodes);
		LinkNode(endNode, tempNodes);
		
		StartCoroutine( AStar.CalculatePath(startNode, endNode, tempNodes, callback) );
	}

	///Generate the map
	public void GenerateMap (){
		
		CreatePolyMap();
		CreateNodes();
		LinkNodes(nodes);
	}

	//helper function
	private Vector2[] TransformPoints ( Vector2[] points, Transform t ){
		for (int i = 0; i < points.Length; i++)
			points[i] = t.TransformPoint(points[i]);
		return points;
	}

	//takes all colliders points and convert them to usable stuff
	private void CreatePolyMap (){

		//create a polygon object for each obstacle
		List<Polygon> subPolygons = new List<Polygon>();
		foreach(PolyNavObstacle obstacle in navObstacles){
			List<Vector2> transformedPoints = TransformPoints(obstacle.points, obstacle.transform).ToList();
			List<Vector2> inflatedPoints = InflatePolygon(transformedPoints, Mathf.Max(0.001f, inflateRadius) );
			subPolygons.Add(new Polygon(inflatedPoints));
		}

		//invert the main polygon points so that we save checking for inward/outward later (for Inflate)
		Vector2[] reversedPoints = masterCollider.points;
		System.Array.Reverse(reversedPoints);
		System.Array.Reverse(masterCollider.points);

		//create the main polygon map (based on inverted) also containing the obstacle polygons
		List<Vector2> masterTransformedPoints = TransformPoints(reversedPoints, masterCollider.transform).ToList();
		List<Vector2> masterInflatedPoints = InflatePolygon(masterTransformedPoints, Mathf.Max(0.001f, inflateRadius) );
		map = new PolyMap(masterInflatedPoints, subPolygons);

		//set the points of the border collider same as master collider
		List<Vector2> borderPoints = new List<Vector2>();
		for (int i = 0; i < masterCollider.points.Length; i++)
			borderPoints.Add(masterCollider.points[i]);
		borderPoints.Add(masterCollider.points[masterCollider.points.Length-1]);
		borderCollider.points = borderPoints.ToArray();

		//
		//The colliders are never used again after this point.
		//
	}

	//Create Nodes at convex points (since master poly is inverted, it will be concave for it) if they are valid
	private void CreateNodes (){

		nodes.Clear();

		foreach(Polygon poly in map.allPolygons){

			//Inflate even more for nodes, by a marginal value to allow CheckLOS between them
			List<Vector2> inflatedPoints = InflatePolygon(poly.points, 0.005f);
			for (int i = 0; i < inflatedPoints.Count; i++){

				//if point is concave dont create a node
				if (PointIsConcave(inflatedPoints, i))
					continue;

				//if point is not in valid area dont create a node
				if (!PointIsValid(inflatedPoints[i]))
					continue;

				nodes.Add(new PathNode(inflatedPoints[i]));
			}
		}
	}

	//link the nodes provided
	private void LinkNodes(List<PathNode> nodeList){

		for (int a = 0; a < nodeList.Count; a++){

			nodeList[a].links.Clear();

			for (int b = 0; b < nodeList.Count; b++){
				
				if (nodeList[a] == nodeList[b])
					continue;

				if (b > a)
					continue;

				if (CheckLOS(nodeList[a].pos, nodeList[b].pos)){
					
					nodeList[a].links.Add(b);
					nodeList[b].links.Add(a);
				}
			}
		}
	}
	
	//link a single node with the rest. 'node' must already exist in 'toNodes'
	//used to link temporary start and end nodes. A bit repeating code, I know :/
	private void LinkNode(PathNode node, List<PathNode> toNodes){

		for (int i = 0; i < toNodes.Count; i++){

			if (node == toNodes[i])
				continue;

			if (CheckLOS(node.pos, toNodes[i].pos)){
				
				node.links.Add(i);
				toNodes[i].links.Add(toNodes.IndexOf(node));
			}			
		}
	}


	///Determine if 2 points see each other.
	public bool CheckLOS ( Vector2 posA, Vector2 posB ){

		/*LEGACY
		return (Physics2D.Linecast(posA, posB, 1<<obstaclesLayer).collider == null);
		*/

		if ( (posA - posB).magnitude < Mathf.Epsilon )
			return true;
		
		foreach(Polygon poly in map.allPolygons){
			for (int i = 0; i < poly.points.Count; i++){
				if (SegmentsCross(posA, posB, poly.points[i], poly.points[(i + 1) % poly.points.Count]))
					return false;
			}
		}
		return true;
	}

	///determine if a point is within a valid (walkable) area.
	public bool PointIsValid ( Vector2 point ){

		/*LEGACY
		//the master collider is set to be further back so it's the last collider to be found
		return Physics2D.OverlapPoint(point) == masterCollider;
		*/

		foreach(Polygon poly in map.allPolygons){
			bool  isMaster = map.allPolygons.IndexOf(poly) == 0;
			if (isMaster? !PointInsidePolygon(poly.points, point) : PointInsidePolygon(poly.points, point))
				return false;
		}
		return true;
	}

	///Kind of scales a polygon based on it's vertices average normal.
	public static List<Vector2> InflatePolygon(List<Vector2> points, float dist){

		List<Vector2> inflatedPoints= new List<Vector2>();

		for (int i = 0; i < points.Count; i++){

			Vector2 ab = (points[(i + 1) % points.Count] - points[i]).normalized;
			Vector2 ac = (points[i == 0? points.Count - 1 : i - 1] - points[i]).normalized;
			Vector2 mid = (ab + ac).normalized;
			
			mid *= (!PointIsConcave(points, i)? -dist : dist);
			inflatedPoints.Add(points[i] + mid);
		}

		return inflatedPoints;
	}

	///Check if or not a point is concave to the polygon points provided
	public static bool PointIsConcave(List<Vector2> points, int point){

		Vector2 current = points[point];
		Vector2 next = points[(point + 1) % points.Count];
		Vector2 previous =  points[point == 0? points.Count - 1 : point - 1];

		Vector2 left = new Vector2(current.x - previous.x, current.y - previous.y);
		Vector2 right = new Vector2(next.x - current.x, next.y - current.y);

		float cross = (left.x * right.y) - (left.y * right.x);

		return cross > 0;
	}

	///Check intersection of two segments, each defined by two vectors.
	public static bool SegmentsCross ( Vector2 a ,   Vector2 b ,   Vector2 c ,   Vector2 d  ){

		 float denominator = ((b.x - a.x) * (d.y - c.y)) - ((b.y - a.y) * (d.x - c.x));

		if (denominator == 0)
			return false;

	    float numerator1 = ((a.y - c.y) * (d.x - c.x)) - ((a.x - c.x) * (d.y - c.y));
	    float numerator2 = ((a.y - c.y) * (b.x - a.x)) - ((a.x - c.x) * (b.y - a.y));

	    if (numerator1 == 0 || numerator2 == 0)
	    	return false;

	    float r = numerator1 / denominator;
	    float s = numerator2 / denominator;

	    return (r > 0 && r < 1) && (s > 0 && s < 1);
	}

	///Is a point inside a polygon?
	public static bool PointInsidePolygon(List<Vector2> polyPoints, Vector2 point){

		float xMin = 0;
		foreach(Vector2 p in polyPoints)
			xMin = Mathf.Min(xMin, p.x);

		Vector2 origin = new Vector2(xMin - 0.1f, point.y);
		int intersections = 0;

		for (int i = 0; i < polyPoints.Count; i++){

			Vector2 pA = polyPoints[i];
			Vector2 pB = polyPoints[(i + 1) % polyPoints.Count];

			if (SegmentsCross(origin, point, pA, pB))
				intersections ++;
		}

		return (intersections & 1) == 1;
	}

	///Finds the closer edge point to the navigation valid area
	public Vector2 GetCloserEdgePoint ( Vector2 point ){

		List<Vector2> possiblePoints= new List<Vector2>();
		Vector2 closerVertex = Vector2.zero;
		float closerVertexDist = Mathf.Infinity;

		foreach(var poly in map.allPolygons){

			//marginal inflate
			List<Vector2> inflatedPoints= InflatePolygon(poly.points, 0.001f);

			for (int i = 0; i < inflatedPoints.Count; i++){

				Vector2 a = inflatedPoints[i];
				Vector2 b = inflatedPoints[(i + 1) % inflatedPoints.Count];

				Vector2 originalA = poly.points[i];
				Vector2 originalB = poly.points[(i + 1) % poly.points.Count];
				
				Vector2 proj = (Vector2)Vector3.Project( (point - a), (b - a) ) + a;

				if (SegmentsCross(point, proj, originalA, originalB) && PointIsValid(proj))
					possiblePoints.Add(proj);

				float dist = (point - inflatedPoints[i]).magnitude;
				if ( dist < closerVertexDist && PointIsValid(inflatedPoints[i])){
					closerVertexDist = dist;
					closerVertex = inflatedPoints[i];
				}
			}
		}

		possiblePoints.Add(closerVertex);
		possiblePoints = possiblePoints.OrderBy(vector => (point - vector).magnitude).ToList();

		Debug.DrawLine(point, possiblePoints[0]);
		return possiblePoints[0];
	}


	//Legacy
	public static List<Vector2> InflatePath(List<Vector2> points, float dist){

		List<Vector2> inflatedPath= new List<Vector2>();

		for (int i = 0; i < points.Count; i++){
			Vector2 ab = (points[(i + 1) % points.Count] - points[i]).normalized;
			Vector2 ac = (points[i == 0? points.Count - 1 : i - 1] - points[i]).normalized;
			Vector2 mid = (ab + ac).normalized * -dist;
			inflatedPath.Add(points[i] + mid);
		}

		//retain the first and last points at original positions
		inflatedPath[0] = points[0];
		inflatedPath[points.Count-1] = points[points.Count-1];
		return inflatedPath;
	}


	void OnDrawGizmos (){

		if (!Application.isPlaying)
			CreatePolyMap();

		DebugDrawPolygon(TransformPoints(masterCollider.points, masterCollider.transform).ToList(), Color.green);
		DebugDrawPolygon(map.points, new Color(1f,1f,1f,0.2f));
		
		foreach(PolyNavObstacle o in navObstacles)
			DebugDrawPolygon(TransformPoints(o.points, o.transform).ToList(), new Color(1, 0.7f, 0.7f));

		foreach(Polygon poly in map.subPolygons)
			DebugDrawPolygon(poly.points, new Color(1, 0.7f, 0.7f, 0.1f));

	}

	//helper debug function
	void DebugDrawPolygon(List<Vector2> points, Color color){
		for (int i = 0; i < points.Count; i++)
			Debug.DrawLine(points[i], points[(i + 1) % points.Count], color);
	}

#if UNITY_EDITOR

	[MenuItem("GameObject/Create Other/PolyNav2D")]
	public static void CreatePolyNav (){
		if (FindObjectOfType(typeof(PolyNav2D)) == null){
			PolyNav2D newNav = new GameObject("@PolyNav2D").AddComponent<PolyNav2D>();
			Selection.activeObject = newNav;
		}
	}

#endif

}


//defines a polygon
class Polygon{

	public List<Vector2> points = new List<Vector2>();

	public Polygon (){
	}

	public Polygon(List<Vector2> points){
		this.points = points;
	}
}

//defines the main navigation polygon and his sub obstacle polygons
class PolyMap : Polygon{

	public List<Polygon> subPolygons = new List<Polygon>();

	public List<Polygon> allPolygons{
		get
		{
			List<Polygon> polyList= new List<Polygon>();
			polyList.Add(this);
			polyList.AddRange(subPolygons);
			return polyList;
		}
	}

	public PolyMap(List<Vector2> points, List<Polygon> subs){

		this.points = points;
		subPolygons = subs;
	}
}

//defines a node for A*
class PathNode : System.IComparable{

	public Vector2 pos;
	public List<int> links= new List<int>();
	public float cost = 1;
	public float estimatedCost;
	public PathNode parent = null;

	public PathNode ( Vector2 pos  ){
		this.pos = pos;
	}

	public int CompareTo ( System.Object obj ){
		PathNode other = obj as PathNode;
		if (this.estimatedCost < other.estimatedCost){
			return -1;
		} else if (this.estimatedCost > other.estimatedCost){
			return 1;
		} else {
			return 0;
		}
	}

	public PathNode Clone (){

		PathNode newNode = new PathNode(pos);
		newNode.cost = this.cost;
		newNode.estimatedCost = this.estimatedCost;
		
		if (this.parent != null)
			newNode.parent = this.parent.Clone();

		foreach(int linkIndex in this.links)
			newNode.links.Add(linkIndex);

		return newNode;
	}
}
