using UnityEngine;
using System.Collections;

public class PlayerCam : MonoBehaviour {

	private Camera mainCam;
	public GameObject Player;

	public float ySmooth;
	public float xSmooth;

	private Vector2 velocity;

	void Start () {
		mainCam = Camera.main;
		Player = GameObject.Find("Player");
	}

	void FixedUpdate () {

		float xPos = Mathf.SmoothDamp(mainCam.transform.position.x , Player.transform.position.x , ref velocity.x , xSmooth);
		float yPos = Mathf.SmoothDamp(mainCam.transform.position.y , Player.transform.position.y , ref velocity.y , ySmooth);

		mainCam.transform.position = new Vector3(xPos, yPos + 0.5f, mainCam.transform.position.z);

	}
}
