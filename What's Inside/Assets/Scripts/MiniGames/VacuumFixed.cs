using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumFixed : MonoBehaviour
{
	
    // Start is called before the first frame update
    void Start() {
		Debug.Log("start");
        if (PersistentData.Instance.vacuumFixed) {
			gameObject.SetActive(true);
		}
		else {
			gameObject.SetActive(false);
		}
    }
}
