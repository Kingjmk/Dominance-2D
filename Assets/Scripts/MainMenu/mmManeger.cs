using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class mmManeger : MonoBehaviour {


  public Slider volumeSlider;
  public Dropdown Resmenu;
  public Toggle FullScr;
  public GameObject SettingsMenu;

  private int Height;
  private int Width;
  private bool fullscr;


  // Use this for initialization
  void Start () {
    Height = 1336;
    Width = 768;
    fullscr = true;


    if (PlayerPrefs.HasKey("Res") == false) {

      SPP();

    } else {
      volumeSlider.value = PlayerPrefs.GetFloat("Volume");
      Resmenu.value = PlayerPrefs.GetInt("Res");
      if (PlayerPrefs.GetInt("FullScr") == 1) { FullScr.isOn = true;}
      if (PlayerPrefs.GetInt("FullScr") == 0) { FullScr.isOn = false;}

    }


  }

  // Update is called once per frame
  void Update () {
    Application.targetFrameRate = 60;
    //Cursor.lockState = true;
    //Cursor.visible = false;

    if (Input.GetKeyDown(KeyCode.C)) {
      PlayerPrefs.DeleteAll();
    }

    AudioListener.volume = PlayerPrefs.GetFloat("Volume");

    float RM = PlayerPrefs.GetInt("Res");
    if (RM == 0) {Height = 1920; Width = 1080;}
    if (RM == 1) {Height = 1336; Width = 768;}
    if (RM == 2) {Height = 1024; Width = 768;}

    if (PlayerPrefs.GetInt("FullScr") == 1) { fullscr = true;}
    if (PlayerPrefs.GetInt("FullScr") == 0) { fullscr = false;}

    Screen.SetResolution (Height, Width, fullscr);

  }


  public void _StartButton() {
    SPP();
    SceneManager.LoadScene("1");

  }
  public void _SettingsButton() {
    SPP();

    SettingsMenu.SetActive(!SettingsMenu.activeSelf);


  }
  public void _ExitButton() {
    //SPP();
    Application.Quit();
  }

  void SPP () {
    PlayerPrefs.SetInt("Res", Resmenu.value);
    PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    if (FullScr.isOn == true) {
      PlayerPrefs.SetInt("FullScr", 1);
    } else {
      PlayerPrefs.SetInt("FullScr", 0);
    }
  }
}
