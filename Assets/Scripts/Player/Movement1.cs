using UnityEngine;
using System.Collections;

public class Movement1 : MonoBehaviour {

  public Transform Player;
  public Rigidbody2D rbPlayer;

  public LayerMask Ground;
  public Transform groundCheck;
  public bool isOnGround;

  public float Angle;
  public float moveForce = 365f;
  public float maxSpeed = 10f;
  public bool Bbool;
  public bool Attack;
  private bool jumpRe = true;

  private Vector3 PScale;
  private float D;

  private bool Dragging;
  private DistanceJoint2D mJoint;
  private Collision2D col2D;

  private int Fall_State;
  private bool a = true;
  private bool CanMove = true;

  public Animator anim;
  public AudioManager audioMan; //soundmap , 0 = walk , 1 = Jump , 2 = Attack , 3 = Hit , 4 = Death ,

  public pmManager pmMan;

  void Start() {

    rbPlayer = GetComponent<Rigidbody2D>();
    PScale = Player.localScale;
    audioMan = GetComponentInChildren<AudioManager>();
  }

  void Update() {
    //Application.targetFrameRate = 1;
    isOnGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, Ground);
    JSAngle JS = GameObject.Find("Canvas_2").GetComponent<JSAngle>();
    Angle = JS.Angle;
    Bbool = JS.Bbool;

    /*
    if (Angle != 0) {
      D =  Mathf.Sign(Angle);
    } else */

    anim.SetFloat("Speed" , Mathf.Abs(Input.GetAxis("Horizontal")));

    //Drag
    if (Input.GetKeyDown(KeyCode.E)) DragOn();
    if (Input.GetKeyUp(KeyCode.E)) DragOff();
    
    if(Player.position.y < -5)Death();
    
    if(Player.position.x > 217) winEvent();


    //Trigger Attack
    if (Input.GetKeyDown(KeyCode.X) && !Player.gameObject.GetComponent<Attack>().Wp && isOnGround) {
      Attack = true;
      D = 0;
      Invoke("atReset", 0.1f);
      anim.SetTrigger("Attack");
      audioMan.PlayOnce(2, 10);

    }

    //Walk Animation Controllers
    if (isOnGround) {
      if (D == 0) {
        rbPlayer.velocity = rbPlayer.velocity - ((rbPlayer.velocity * 1.5f));
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

    } if (isOnGround && anim.GetBool("Falling")) {
      rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, 0);
      anim.SetBool("Falling", false);
      Fall_State = 2;
      a = true;
      Invoke("rReset", 0.4f);
    }

    anim.SetInteger("Falling_State", Fall_State);
    //--------------------

  }

  void FixedUpdate() {

    //Input + Movement
    if(CanMove == true){
    if (Input.GetAxis("Horizontal") != 0) {
      D = -Mathf.Sign(Input.GetAxis("Horizontal"));
      Player.localScale = new Vector3(-D * PScale.x , PScale.y, PScale.z);

      if (-D * rbPlayer.velocity.x < maxSpeed)
        rbPlayer.AddForce(Vector2.right * -D * moveForce * 10);
      if (Mathf.Abs (rbPlayer.velocity.x) > maxSpeed)
        rbPlayer.velocity = new Vector2(Mathf.Sign(rbPlayer.velocity.x) * maxSpeed * 1.5f, rbPlayer.velocity.y);

    } else {
      D = 0;
      if (isOnGround) rbPlayer.velocity = new Vector2(0, rbPlayer.velocity.y);
    }
  }
    //--------------


    //Jumping
    if ((Input.GetKey(KeyCode.Z) || Bbool) && isOnGround && Dragging == false && jumpRe) {
      float ju = 300 ;
      rbPlayer.AddForce(new Vector2(0, ju), ForceMode2D.Impulse);
      jumpRe = false;


      //Jump Animation Here
      audioMan.PlayOnce(1, 10);
      anim.SetBool("Idle", false);
      anim.SetBool("Walk", false);
      anim.SetTrigger("Jump");
      anim.SetBool("Falling", false);
      Fall_State = 0;


    } else {
      anim.SetBool("Walk", false);
      anim.SetBool("Idle", false);
    }
    //---------

  }
  //Death anim
  public void Death() {
    
    Debug.Log("Died lol");
    CanMove = false;
    anim.SetTrigger("Death");
    audioMan.PlayOnce(4, 10);
    //Trigger Death menu and stuff
    Invoke("CallonDeath",0f);
  }

  public void CallonDeath(){
    Time.timeScale = 0;
    //Trigger Death menu and stuff
    pmMan.onDeath();

  }

  void winEvent(){
    Time.timeScale = 0;
    //Trigger Death menu and stuff
    pmMan.onWin();

  }


  //Drag Objects

  void OnCollisionEnter2D(Collision2D col) {
    if (col.gameObject.tag == "Draggable") {
      Debug.Log("Hit");
      col2D = col;
    } else {
      col2D = null;
    }

  }

  void DragOn() {
    if (col2D != null && col2D.gameObject.tag == "Draggable" && col2D.contacts[0].normal.y != 1) {
      Dragging = true;
      mJoint = col2D.gameObject.AddComponent<DistanceJoint2D>();
      mJoint.connectedBody = Player.GetComponent<Rigidbody2D>();
      mJoint.distance = 2.2f;
      mJoint.autoConfigureDistance = false;
      mJoint.maxDistanceOnly = true;
      mJoint.enableCollision = true;
      Debug.Log("Drag");
      //Drag , Push Animation Here


    }
  }

  void DragOff() {
    if (mJoint != null) {
      Dragging = false;
      Destroy(mJoint);
      Debug.Log("Drag off");
    }
  }

  void rReset() { jumpRe = true; }
  void atReset() { rbPlayer.isKinematic = false;}

}
