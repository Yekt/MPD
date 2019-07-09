using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadioMinigame : MonoBehaviour {

    private Text frage;
    private Text antwort1;
    private Text antwort2;
    private Text antwort3;
    private GameObject radioFixed;
    private GameObject radioBroken;

    private List<Question> questions;
    private List<string> answers;
    private int currentQuestion;
    private int solvedQuestions;

    private static System.Random rng = new System.Random();

    void Start()
    {
        frage = this.gameObject.transform.Find("UI Layer").Find("Quiz").Find("Frage").gameObject.GetComponent<Text>();
        antwort1 = this.gameObject.transform.Find("UI Layer").Find("Quiz").Find("Antwort1").Find("Text").gameObject.GetComponent<Text>();
        antwort2 = this.gameObject.transform.Find("UI Layer").Find("Quiz").Find("Antwort2").Find("Text").gameObject.GetComponent<Text>();
        antwort3 = this.gameObject.transform.Find("UI Layer").Find("Quiz").Find("Antwort3").Find("Text").gameObject.GetComponent<Text>();
        radioFixed = this.gameObject.transform.Find("Radio Layer").Find("FixedRadio").gameObject;
        radioBroken = this.gameObject.transform.Find("Radio Layer").Find("BrokenRadio").gameObject;

        questions = new List<Question>();
        questions.Add( new Question(1, "Wie nimmt das Radio die für Menschen nicht hörbaren Radiowellen auf?", "RadioFrage1", "Antenne") );
        questions.Add( new Question(2, "Welches Bauteil stellt sicher, dass nur der gewünschte Sender empfangen wird?", "RadioFrage2", "Eingangsfilter") );
        questions.Add( new Question(3, "Mit welchem Bauteil werden die Schallwellen von den Radiowellen getrennt?", "RadioFrage3", "Demodulator") );
        questions.Add( new Question(4, "Womit bekommt das Signal, das die Lautsprecher zum Schwingen bringt, ausreichend Energie zugeführt?", "RadioFrage4", "Verstärker") );
        questions.Add( new Question(5, "Wie werden die elektromagnetischen Wellen wieder in physische Schallwellen umgewandelt?", "RadioFrage5", "Lautsprecher") );

        answers = new List<string>();
        foreach (Question question in questions) {
            answers.Add(question.answer);
        }
        answers.Add("Netzteil");
        answers.Add("Batterie");
        answers.Add("Display");
        answers.Add("Kondensator");
        answers.Add("Oszillator");
        answers.Add("Regulator");
        answers.Add("Imperator");

        currentQuestion = 0;
        solvedQuestions = 0;

        askQuestion();
    }

    public void askQuestion() {
        Question question = questions[currentQuestion];
        int i = rng.Next(1, 4);
        string answer1;
        string answer2;
        string answer3;

        switch (i) {
            case 1:
                answer1 = question.answer;
                answer2 = randomAnswer(answer1, "");
                answer3 = randomAnswer(answer1, answer2);
                break;
            case 2:
                answer2 = question.answer;
                answer1 = randomAnswer(answer2, "");
                answer3 = randomAnswer(answer2, answer1);
                break;
            case 3:
                answer3 = question.answer;
                answer1 = randomAnswer(answer3, "");
                answer2 = randomAnswer(answer3, answer1);
                break;
            default:
                answer3 = question.answer;
                answer1 = randomAnswer(answer3, "");
                answer2 = randomAnswer(answer3, answer1);
                break;
        }
        frage.text = question.question;
        antwort1.text = answer1;
        antwort2.text = answer2;
        antwort3.text = answer3;
        
        AudioManager.Instance.Play(question.audioClip);
    }

    public void checkAnswer(Text text) {
        string answer = text.text;
        Question question = questions[currentQuestion];
        if (answer.Equals( question.answer ) && solvedQuestions < 5) {
            question.solved = true;
            solvedQuestions += 1;
            if (solvedQuestions >= 5) {
                PersistentData.Instance.radioFixed = true;
                radioBroken.SetActive(false);
                radioFixed.SetActive(true);
                frage.text = "Super, das Radio funktioniert wieder!";
                this.gameObject.transform.Find("UI Layer").Find("Quiz").Find("Antwort1").gameObject.SetActive(false);
                this.gameObject.transform.Find("UI Layer").Find("Quiz").Find("Antwort2").gameObject.SetActive(false);
                this.gameObject.transform.Find("UI Layer").Find("Quiz").Find("Antwort3").gameObject.SetActive(false);
                Inventory.Instance.deactivateItem("Antenne");
                Inventory.Instance.deactivateItem("Demodulator");
                Inventory.Instance.deactivateItem("Verstärker");
                Inventory.Instance.deactivateItem("Eingangsfilter");
                Inventory.Instance.deactivateItem("Lautsprecher");
                AudioManager.Instance.Play("RadioAbgeschlossen");
                AudioManager.Instance.Play("Richtig");
            }
            else {
                currentQuestion = (currentQuestion + 1) % 5;
                Question newQuestion = questions[currentQuestion];
                while (newQuestion.solved) {
                    currentQuestion = (currentQuestion + 1) % 5;
                    newQuestion = questions[currentQuestion];
                }
                askQuestion();
                AudioManager.Instance.Play("Richtig");
            }            
        }
        else if (solvedQuestions < 5) {
            currentQuestion = (currentQuestion + 1) % 5;
            Question newQuestion = questions[currentQuestion];
            while (newQuestion.solved)
            {
                currentQuestion = (currentQuestion + 1) % 5;
                newQuestion = questions[currentQuestion];
            }
            askQuestion();
            AudioManager.Instance.Play("Falsch");
        }
    }

    private string randomAnswer(string a1, string a2) {
        int rInt = rng.Next(0, answers.Count);
        string answer = answers[rInt];

        while (answer.Equals(a1) || answer.Equals(a2)) {
            rInt = rng.Next(0, answers.Count - 1);
            answer = answers[rInt];
        }
        return answer;
    }


    private class Question {

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
}
