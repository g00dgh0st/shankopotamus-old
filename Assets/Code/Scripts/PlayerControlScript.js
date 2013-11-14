#pragma strict

public var navAgent : NavMeshAgent;

public var cursor : Transform;

function Update () {
  if( Input.GetMouseButton( 0 ) ) {
    var ray : Ray = Camera.main.ScreenPointToRay( Input.mousePosition );
    var hit : RaycastHit;
    var layerMask : int = 1 << 8;
    
    if( Physics.Raycast( ray, hit, Mathf.Infinity, layerMask ) ) {
    	dispatcher.eventNotify(dlEvents.PLAYERMOVE, navAgent);
      	navAgent.destination = hit.point;
      	cursor.gameObject.SetActive( true );
      	cursor.position = Vector3( hit.point.x, hit.point.y + 1, hit.point.z );
    } 
  }
  
  if( !navAgent.hasPath && cursor.gameObject.activeSelf ) cursor.gameObject.SetActive( false );
  else if( navAgent.velocity.x > 0 ) navAgent.gameObject.transform.localScale.x = 3;
  else if( navAgent.velocity.x < 0 ) navAgent.gameObject.transform.localScale.x = -3;
  
  
  // TODO: This needs to be a global var, only calculated on cam move/change. All 2D things need to use this.
  var planes : Plane[];
  planes = GeometryUtility.CalculateFrustumPlanes( Camera.main );
  transform.forward = planes[4].normal;
}
