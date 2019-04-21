using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class pmManager : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject DeathMenu;
	public GameObject WinMenu;
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P)){
			_pmEnable(true);

		}

	}


	public void _pmEnable(bool bo) {
		if (bo == true) {
			pauseMenu.SetActive(bo);
			Time.timeScale = 0;

		} else {
			pauseMenu.SetActive(false);
			Time.timeScale = 1.0f;

		}

	}

	public void _LoadScene(int Scene) {
      SceneManager.LoadScene("mainmenuscene");
      Time.timeScale = 1.0f;
	}

	public void onDeath(){
		DeathMenu.SetActive(true);
		Debug.Log("Death Menu Appeared");
	}
	public void onWin(){
		WinMenu.SetActive(true);
		Debug.Log("Win Menu Appeared");
	}


}
