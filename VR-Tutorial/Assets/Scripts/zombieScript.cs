using UnityEngine;
using System.Collections;

public class zombieScript : MonoBehaviour {
	
	private Transform goal;
	private UnityEngine.AI.NavMeshAgent agent;


	void Start () {


		goal = Camera.main.transform;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		//set the navmesh agent's desination equal to the main camera's position (our first person character)
		agent.destination = goal.position;
		//start the walking animation
		GetComponent<Animation>().Play ("Walk01");
	}



	void OnTriggerEnter (Collider col)
	{
		//disable zombie collider so multiple collisions dont happen
		GetComponent<CapsuleCollider>().enabled = false;
		//destroy bullet
		Destroy(col.gameObject);
		//stop the zombie from moving forward by setting its destination to it's current position
		agent.destination = gameObject.transform.position;

		GetComponent<Animation>().Stop ();
		GetComponent<Animation>().Play ("Death");
		//destroy this zombie in 2 seconds.
		Destroy (gameObject, 0.5f);
		//create new zombie. put zombie prefab in Resources folder
		//GameObject zombie = Instantiate(Resources.Load("zombie", typeof(GameObject))) as GameObject;

		//set the zombies position equal to these new coordinates
		//float randomX = UnityEngine.Random.Range (-12f,12f);
		//float constantY = .01f;
		//float randomZ = UnityEngine.Random.Range (-13f,13f);

		//zombie.transform.position = new Vector3 (randomX, constantY, randomZ);

		//if the zombie gets positioned less than or equal to 3 scene units away from the camera we won't be able to shoot it
		//so keep repositioning recurssively the zombie until it is greater than 3 scene units away. 
		//while (Vector3.Distance (zombie.transform.position, Camera.main.transform.position) <= 3) {

			//randomX = UnityEngine.Random.Range (-12f,12f);
			//randomZ = UnityEngine.Random.Range (-13f,13f);

			//zombie.transform.position = new Vector3 (randomX, constantY, randomZ);
		}

	void Update ()
	{ goal = Camera.main.transform;
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.destination = goal.position;
		//start the walking animation
		GetComponent<Animation>().Play ("Walk01");
	}
}

