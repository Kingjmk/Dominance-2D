using UnityEngine;
using System.Collections;

public class HPack : MonoBehaviour {
    public float Health;



    void Update(){
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Player"){
            other.gameObject.GetComponent<PlayerStats>().Health += Health;
        }    
    }
}
