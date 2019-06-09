using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumBroken : MonoBehaviour
{
	
    // Start is called before the first frame update
    void Start() {
        if (PersistentData.Instance.vacuumFixed) {
			gameObject.SetActive(false);
		}
		else {
			gameObject.SetActive(true);
		}
    }
}
