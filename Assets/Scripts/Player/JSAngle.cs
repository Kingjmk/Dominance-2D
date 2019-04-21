using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JSAngle : MonoBehaviour {


  public float Angle;
  public float Length;
  public bool Active;

  public bool Abool = false;
  public bool Bbool = false;
  public bool Xbool = false;
  public bool Ybool = false;

  public GameObject JSBase;
  public GameObject JS;


  public float tempAngle;


  public float X;
  public float Y;

  public Slider HealthSlider;
  //public Slider StaminaSlider;
  public float HealthSliderValue;
  public float StaminaSliderValue;




  void Update () {

    Application.targetFrameRate = 60;

    HealthSlider.value = HealthSliderValue;
   // StaminaSlider.value = StaminaSliderValue;

    if (Active == true) {
      Vector3 A = JSBase.transform.position;
      Vector3 B = JS.transform.position;
      Length = Vector3.Distance(A, B);
      X = A.x - B.x;
      Y = A.y - B.y;
      Angle = Mathf.Atan2(X , Y) * Mathf.Rad2Deg;
      tempAngle = 0; //Angle;
    } else {
      Angle = tempAngle;
    }

  }

  public void PointerUP() {
    Active = false;
  }

  public void PointerDOWN() {
    Active = true;
  }

  public void AButtonUP() {
    Abool = false;
  }
  public void AButtonDown() {
    Abool = true;
  }
  public void BButtonUP() {
    Bbool = false;
  }
  public void BButtonDown() {
    Bbool = true;
  }
  /*
  public void XButtonUP(){
     Xbool = false;
  }
  public void XButtonDown(){
     Xbool = true;
  }
  public void YButtonUP(){
     Ybool = false;
  }
  public void YButtonDown(){
     Ybool = true;
  }
  */

}
