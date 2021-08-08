using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {


	private GameObject gun;
	private GameObject spawnPoint;
	private bool isShooting;


	void Start () {

		//for ios. will be skipped for android
		Application.targetFrameRate = 60;

		//create references to gun and bullet spawnPoint objects
		gun = gameObject.transform.GetChild (0).gameObject;
		spawnPoint = gun.transform.GetChild (0).gameObject;

		//set isShooting bool to default of false
		isShooting = false;
	}


	IEnumerator Shoot() {
		//set is shooting to true so we can't shoot continuosly
		isShooting = true;
		//create the bullet
		GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;
		//Get the bullet's rigid body component and set its position and rotation equal to that of the spawnPoint. very important
		Rigidbody rb = bullet.GetComponent<Rigidbody>();
		bullet.transform.rotation = spawnPoint.transform.rotation;
		bullet.transform.position = spawnPoint.transform.position;
		//add force to the bullet in the direction of the spawnPoint forward vector 500 coordinate travel given
		rb.AddForce(spawnPoint.transform.forward * 500f);
		//play the gun shot sound and gun animation
		GetComponent<AudioSource>().Play ();
		gun.GetComponent<Animation>().Play ();
		//destroy the bullet after 1 second
		Destroy (bullet, 1);
		//wait for 1 second to shoot again
		//can do the same for reloading
		
		yield return new WaitForSeconds (0.2f);
		isShooting = false;
	}


	void Update () {

		//does the actual shit
		RaycastHit hit;
		//draw the ray for debuging purposes use in scene view
		Debug.DrawRay(spawnPoint.transform.position, spawnPoint.transform.forward, Color.green);

		//cast a ray from the spawnpoint in the direction of its forward vector. will need to adjust raycast green color line before shooting
		if (Physics.Raycast(spawnPoint.transform.position, spawnPoint.transform.forward, out hit, 100)){

				if (!isShooting && Input.GetMouseButtonDown(0)) {
			 		StartCoroutine ("Shoot");				
				}
			//if the raycast hits any game object where its name contains "zombie" and not shooting start the shooting coroutine
			// if (hit.collider.name.Contains("zombie")) {
			// 	if (!isShooting) {
			// 		StartCoroutine ("Shoot");
			// 	}

			}

		}

	}
