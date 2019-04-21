using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class GUISelection : MonoBehaviour {
	private Button Btn;
	private Toggle Tgl;

	void Start() {
		Btn = this.gameObject.transform.parent.GetComponent<Button>();
		Tgl = this.gameObject.GetComponent<Toggle>();

	}
	void Update() {

		if (Btn.gameObject == EventSystem.current.currentSelectedGameObject)  {
			Tgl.isOn = true;
		} else {
            Tgl.isOn = false;
		}

	}


}