﻿using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
	
	// Singelton
	public static AudioManager Instance {get; private set;}
	
	public Audio[] sounds;
	
	private float lastVolume;
	public GameObject speechBubble;
	
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
	   Debug.Log("searches audio");
	   Audio a = Array.Find(sounds, sound => sound.name == name);
	   //Debug.Log(name);
	   if (a != null)
	   {
		   Debug.Log("plays audio");
		   a.source.Play();
		   
		   if (a.text.Length > 0)
		   {
			   Debug.Log("sets Text");
			   speechBubble.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = a.text;
			   speechBubble.SetActive(true);
		   }
	   }
	   else Debug.LogWarning("sound " + name + " not found!");
   }



   public void StopDialog()
   {
	   // todo: audio stoppen wenns läuft, Sprechblase ausblenden
	   speechBubble.SetActive(false);
   }

   private void AdjustVolume(float volume){
	   foreach(Audio a in sounds){
		   a.volume = volume;
		   a.source.volume = volume;
	   }
	   lastVolume = volume;
   }
}
