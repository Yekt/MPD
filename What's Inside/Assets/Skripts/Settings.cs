using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
	
	public Slider slider;

	void Awake() {
		slider.SetValueWithoutNotify(PersistentData.Instance.volume);
	}
	
    public void AdjustVolume(float volume) {
		PersistentData.Instance.volume = volume;
	}
}
