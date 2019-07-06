using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour {

    public int number;
    public string question { get; }
    public string audioClip { get; }
    public string answer { get; }
    public bool solved { get; set; }

    public Question(int number, string question, string audioClip, string answer)
    {
        this.number = number;
        this.question = question;
        this.audioClip = audioClip;
        this.answer = answer;
        this.solved = false;
    }
}
