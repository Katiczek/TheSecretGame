using UnityEngine;
using System.Collections;

public class PlayerMovementThirdPerson : MonoBehaviour
{

	public GameObject MovementTarget;


	NavMeshAgent agent;
	Ray movementray;
	RaycastHit movementhit;
	GameObject movementobject;
	
	Vector3 movement;
	Animator anim;





	void Awake()
	{
		anim = GetComponent<Animator> ();
	}

	void Start () 
	{
		agent = GetComponent<NavMeshAgent> ();
	}
	


	void FixedUpdate () 
	{

		Move();
		Animating();


	}

	void Move ()
	{
		if(Input.GetKey(KeyCode.Mouse0)){
			movementray = Camera.main.ScreenPointToRay(Input.mousePosition);
			DestroyObject(movementobject);
			
			if(Physics.Raycast(movementray, out movementhit)){
				movementobject = Instantiate(MovementTarget, new Vector3(movementhit.point.x, movementhit.point.y, movementhit.point.z), Quaternion.identity) as GameObject;
				agent.SetDestination(movementobject.transform.position);

			}
		}


		
	}


	void Animating ()
	{
		anim.SetBool ("IsRunning", agent.hasPath);
	}
}
