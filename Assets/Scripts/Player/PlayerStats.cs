using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public Transform Player;
	public float Health = 75;
	public float Stamina = 75;

	void Start() {
		Time.timeScale = 1.00f;
		Player = GetComponent<Transform>();
	}

	void Update () {

		JSAngle JS = GameObject.Find("Canvas_2").GetComponent<JSAngle>();
		JS.HealthSliderValue = Health;

		//if (Health > 150) Health = 150;
		if (Health <= 0) {
			Health = 0;
			Player.GetComponent<Movement1>().Death();
			
		}
	}


	void LossH(float H, bool h) {
		if (h == true) Health += H;
		if (h == false) Health -= H;


	}
	/*

	void LossS(float S, bool s){
		if(s == true) Stamina += S;
		if(s == false) Stamina -= S;
	} */

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "EnemyWeapon") {
			float H = other.gameObject.GetComponent<Weapon>().Damage;
			//Pushing Playerback
			StartCoroutine(KnockbackEffect(Player.gameObject.GetComponent<Movement1>().rbPlayer , 0.1f , 350.0f , other.transform.position));
			LossH(H, false);
			Player.GetComponent<Movement1>().audioMan.PlayOnce(3, 10);
			Debug.Log("Ahh! " + H);

		}

		//Healthpack pickup collider
		if (other.gameObject.tag == "HealthPack") {
			float H = other.gameObject.GetComponent<HPack>().Health;
			other.gameObject.SetActive (false);
			LossH(H, true);
			Debug.Log("Nice" + H);

		}
	}

	public void KnockbackEff(Rigidbody2D rb , float Knockduration , float knockpower , Vector3 knockPos ) {
		StartCoroutine(KnockbackEffect(rb , Knockduration , knockpower , knockPos ));
	}

	public IEnumerator KnockbackEffect(Rigidbody2D rb , float Knockduration , float knockpower , Vector3 knockPos ) {

		float timer = 0;

		while (Knockduration > timer) {
			timer += Time.deltaTime;
			Vector3 Al = knockPos - rb.transform.position;
			rb.AddForce(new Vector3(Al.x * knockpower * -3 , -Al.y * 0 * knockpower , 0), ForceMode2D.Impulse);
			//Debug.Log("Bash");
			this.GetComponent<SpriteRenderer>().color = Color.red;
			Invoke("ResetColor", 0.3f);

		}
		yield return 0;

	}

	void ResetColor() {
		
		this.GetComponent<SpriteRenderer>().color = Color.white;
		}





}
