using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Attack : MonoBehaviour {
	public Transform Weapon;
	private Movement1 Mov;

	public bool Wp = false;

	void Update () {

		JSAngle JS = GameObject.Find("Canvas_2").GetComponent<JSAngle>();
		Mov = Weapon.parent.GetComponent<Movement1>();

		if (Mov.Attack || JS.Abool == true) {
			Wp = true;
			Invoke("WpF", 0.01f);
		}

		Weapon.gameObject.GetComponent<BoxCollider2D>().enabled = (Wp);

		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene("mainmenuscene");

		}

	}

	void WpF () {
		Wp = false;
		Mov.Attack = false;
	}

}
