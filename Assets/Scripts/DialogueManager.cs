using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    private bool isActive = true;

    public Text nameText;
    public Text sentenceText;

    public Animator animator;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue){
        isActive = true;
        animator.SetBool("isActive", isActive);
        nameText.text = dialogue.name;

        //Vaciamos la cola
        sentences.Clear();

        //Metemos en la cola los elementos que hay que meter
        foreach (string sentence in dialogue.sentences)
        {  
           sentences.Enqueue(sentence); 
        }

        //Displayeamos las sentences
        DisplayNexSentence();

    }

    private void FixedUpdate()
    {
        if (isActive && Input.GetButtonDown("Submit")){
            DisplayNexSentence();
        }
    }

    private void DisplayNexSentence(){
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence){
        sentenceText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            sentenceText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue(){
        isActive = false;
        animator.SetBool("isActive", isActive);
    }
}
