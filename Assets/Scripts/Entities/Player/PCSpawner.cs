using UnityEngine;
using System.Collections;

public class PCSpawner : MonoBehaviour {

	public Transform player;
	private GameObject playerObject;
	private PlayerController pc;
	public bool canSpawn;  //At the moment, this isn't necessary


	// Use this for initialization
	void Start () {
		Instantiate(player, new Vector3(0f, -4.259603f, 0f), Quaternion.identity);
		playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
		pc = playerObject.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		//as long as the player isn't swimming, let user determine x position
		if (pc.Swimming == false && Application.loadedLevelName.Equals("Level01")) {
			Vector3 mPos = Input.mousePosition;
      		mPos = Camera.main.ScreenToWorldPoint(mPos);
      		playerObject.transform.position = new Vector3(mPos.x, playerObject.transform.position.y, 0f);

      		//the player hits the left mouse button to select the X position
      		if (Input.GetButtonDown("Fire1")){
        		pc.Swimming = true;
      		} 
		}
	}
}
