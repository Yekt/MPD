using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {
	
	// Singelton
	public static AudioManager Instance {get; private set;}
	
	public Audio[] sounds;
	
	private float lastVolume;
	
	void Start(){
		bool flag = false;
		if(Instance == null){
			Instance = this;
			DontDestroyOnLoad(gameObject);
			flag = true;
		}
			
		foreach(Audio a in sounds){
			a.source = gameObject.AddComponent<AudioSource>();
			a.source.clip = a.clip;
			a.volume = PersistentData.Instance.volume;
			a.source.volume = PersistentData.Instance.volume;
			a.source.loop = a.loop;
		}
		lastVolume = PersistentData.Instance.volume;
			
		if(flag)Play("AmbientMusic");
	}
	
	void Update() {
		if(lastVolume != PersistentData.Instance.volume)
			AdjustVolume(PersistentData.Instance.volume);
	}
    
	
	
   public void Play(string name){
	   Audio a = Array.Find(sounds, sound => sound.name == name);
	   if(a != null) a.source.Play();
	   else Debug.LogWarning("sound " + name + " not found!");
   }
   
   private void AdjustVolume(float volume){
	   foreach(Audio a in sounds){
		   a.volume = volume;
		   a.source.volume = volume;
	   }
	   lastVolume = volume;
   }
}
