using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
	
	// Singelton
	public static AudioManager Instance {get; private set;}
	
	public Audio[] sounds;
    public List<Audio> playingSounds;
	
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
        playingSounds = new List<Audio>();
			
		if(flag)Play("AmbientMusic");
	}
	
	void Update() {
		if(lastVolume != PersistentData.Instance.volume)
			AdjustVolume(PersistentData.Instance.volume);
        List<Audio> toRemove = new List<Audio>();
        foreach (Audio a in playingSounds)
        {
            if (!a.source.isPlaying)
            {
                toRemove.Add(a);
                if (!name.Equals("Hover") && !name.Equals("Click") && !name.Equals("AmbientMusic") && !name.Equals("Richtig") && !name.Equals("Falsch")
                    && speechBubble.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text.Equals(a.text)) StopDialog();
            }
        }
        foreach (Audio a in toRemove)
        {
            playingSounds.Remove(a);
        }
    }
	
   public void Play(string name) {
	   Audio a = Array.Find(sounds, sound => sound.name == name);
	   //Debug.Log("Play: "+name);
	   if (a != null)
	   {
           if (!name.Equals("Hover") && !name.Equals("Click") && !name.Equals("AmbientMusic") && !name.Equals("Richtig") && !name.Equals("Falsch")) Stop();

		   a.source.Play();
           playingSounds.Add(a);
		   
		   if (a.text.Length > 0)
		   {
			   speechBubble.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = a.text;
			   speechBubble.SetActive(true);
		   }         
	   }
	   else Debug.LogWarning("sound " + name + " not found!");
   }

    public void Stop() {
        foreach (Audio a in sounds) {
            if (!a.name.Equals("AmbientMusic")) a.source.Stop();
        }
        StopDialog();
    }



   public void StopDialog()
   {
	   speechBubble.SetActive(false);
   }

   private void AdjustVolume(float volume){
	   foreach(Audio a in sounds){
		   a.volume = volume;
		   a.source.volume = volume;
	   }
	   lastVolume = volume;
   }

    public void giveHint()
    {
        Play("Click");
        if (PersistentData.Instance.creditsRolled) Play("FlurErstesBetreten3");
        else if (!PersistentData.Instance.vacuumFixed) Play("Milestone12+3");
        else if (PersistentData.Instance.radioFixed) Play("Flush");
        else Play("Milestone22");
    }
}
