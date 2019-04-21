using UnityEngine;
using System.Collections;

public class EnemyMan : MonoBehaviour {

	public LayerMask Ground;
	public Transform groundCheck;
	public bool isOnGround;


	public Rigidbody2D rbPlayer;
	public Transform myTransform;
	public float moveForce = 365f;
	public float maxSpeed = 5f;

	private Vector2 Move;

	private float Hor;
	private Vector3 PScale;

	private bool Jump;
	private bool a = false;
	private int Fall_State;

	public GameObject Player;
	public GameObject Weapon;

	private bool Wpf = true;

	public float Health = 30;

	private Animator anim;
	public AudioManager audioMan; // soundmap ,0 = walk , 1 = attack , 2 = death , 3 = Hit ,



    //redo a lot of stuff in this script , animation barely work (now almost), attack not so good , add jump y/n , add functional sound manager
	void Start () {

		rbPlayer = GetComponent<Rigidbody2D>();
		myTransform = GetComponent<Transform>();
		PScale = myTransform.localScale;
		Player = GameObject.Find("Player");
		anim = GetComponent<Animator>();
		audioMan = GetComponentInChildren<AudioManager>();

	}

	void Update () {

		float Distance = Vector2.Distance(myTransform.position , Player.transform.position);
		//Health Stuff

		if (Health > 100) Health = 100;
		if (Health < 0) {
			Health = 0;
			Death();

			

		}

		//Movement AI Stuff
		float H = Mathf.Sign(Player.transform.position.x - myTransform.position.x);
		Vector2 A = myTransform.position;
		Vector2 B = Player.transform.position;
		if (((Mathf.Abs(A.x - B.x) < 10) && Mathf.Abs(A.y - B.y) < 3) && !rbPlayer.isKinematic) {
			Hor = H; // enable this to enable movement
			myTransform.localScale = new Vector3 (H * PScale.x , PScale.y , PScale.z);

		} else Hor = 0;
		//----------------
		Jump = false;

		//Attack AI Stuff
		if (Input.GetKeyDown(KeyCode.M) || (Distance < 3 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) && Wpf ==  true && isOnGround && !rbPlayer.isKinematic) {

			anim.SetTrigger("Attack");
			audioMan.PlayOnce(1, 10);
			Weapon.gameObject.GetComponent<BoxCollider2D>().enabled = (true);
			Wpf = false;
			Invoke("WpF", 1.7f);
			Invoke ("WPF", 0.1f);
		}

		/*Ignore Colliders
		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), Player.GetComponent<Collider2D>(), true); // */

		// Movement
		if (isOnGround) {
			if (Hor == 0) {

				rbPlayer.velocity = rbPlayer.velocity - ((rbPlayer.velocity * 1.3f));
				//Idle anim-Sound Here
				anim.SetBool("Idle", true);
				anim.SetBool("Walk", false);
				audioMan.StopSound();
			} else {

				//Walk anim-sound Here
				anim.SetBool("Idle", false);
				anim.SetBool("Walk", true);
				audioMan.PlaySound(0);
			}
		}

		//falling anims
		if (rbPlayer.velocity.y < 0 && a == true) {
			anim.SetBool("Falling", true);
			Fall_State = 0;
			a = false;

		} if (rbPlayer.velocity.y < -0.1f) {
			Fall_State = 1;

		} if (isOnGround && a == false) {
			rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);
			anim.SetBool("Falling", false);
			Fall_State = 2;
			a = true;
		}

		anim.SetInteger("Falling_State", Fall_State);

	} //----------------------------------------------------------



	void FixedUpdate() {
		isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, Ground);

		if (Hor != 0) {
			if (Hor * rbPlayer.velocity.x < maxSpeed)
				rbPlayer.AddForce(Vector2.right * Hor * moveForce * 10);
			if (Mathf.Abs (rbPlayer.velocity.x) > maxSpeed)
				rbPlayer.velocity = new Vector2(Mathf.Sign(rbPlayer.velocity.x) * maxSpeed * 1.5f, rbPlayer.velocity.y);

		} else {
			if (isOnGround) rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
		}  //--------------

		//Jumping
		if (Jump && isOnGround) {
			Move.y = 300 ;
			rbPlayer.AddForce(new Vector2(0, Move.y), ForceMode2D.Impulse);
			//Jump Animation Here
			anim.SetBool("Idle", false);
			anim.SetBool("Walk", false);
			anim.SetTrigger("Jump");
			anim.SetBool("Falling", false);
			Fall_State = 0;


		} else {
			//Move.y = 0;
			anim.SetBool("Walk", false);
			anim.SetBool("Idle", false);
		}
		//---------

	} //--------------------------------------------

	void WpF() { Wpf = true;}
	void WPF() { Weapon.gameObject.GetComponent<BoxCollider2D>().enabled = (false);}


	void LossH(float H, bool h) {
		if (h == true) Health += H;
		if (h == false) Health -= H;
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "myWeapon") {
			float H = other.gameObject.GetComponent<Weapon>().Damage;
			StartCoroutine(KnockbackEffect(rbPlayer , 0.1f , 350.0f , other.transform));
			LossH(H, false);
			Debug.Log("Enemy Hit! " + H);
			audioMan.PlayOnce(3, 10);
			this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
			rbPlayer.isKinematic = true;
			Invoke("HitColor", 0.2f);


		}
	}

	public IEnumerator KnockbackEffect(Rigidbody2D rb , float Knockduration , float knockpower , Transform knockPos ) {
		float timer = 0;
		while (Knockduration > timer) {
			timer += Time.deltaTime;
			Vector3 Al = knockPos.position - rb.transform.position;
			rb.AddForce(new Vector3(Al.x * knockpower * -3 , -Al.y * 0.2f * knockpower , 0));
		} yield return 0;
	}

	void Death() {
		anim.SetBool("Walk", false);
		anim.SetBool("Death", true);
		Debug.Log("EnemyKilled");
		audioMan.PlayOnce(2, 10);
		rbPlayer.isKinematic = true;
		GetComponent<Collider2D>().enabled = false;
		Destroy (gameObject , 1.5f);

	}

	void HitColor() {
		this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		if (Health > 0) {
			rbPlayer.isKinematic = false;
		}
	}

}


