using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance {get; private set;}

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay = false;
    [SerializeField] float timeBtwChars = 0.1f;
	public TMP_Text nameText;
	public TMP_Text dialogueText;
    public Image image;
	public GameObject textFrame;
	private Queue<string> sentences;
	private Queue<string> names;
	private Queue<Image> images;

	public event Action onShowDialogue;
	public event Action onCloseDialogue;
	Action onDialogueFinished;
	public bool isShowing{get; private set;}

	void Start() {
		Instance = this;
		sentences = new Queue<string>();
		names = new Queue<string>();
		images = new Queue<Image>();
	}

	public void HandleUpdate(){
		if(Input.GetKeyDown(KeyCode.Z)){
			DisplayNextSentence();
		}
	}

	public IEnumerator StartDialogue (Dialogue dialogue, Action onFinished =null)
	{
		yield return new WaitForEndOfFrame();
		onShowDialogue?.Invoke();
		textFrame.SetActive(true);
		onDialogueFinished = onFinished;
		sentences.Clear();
		names.Clear();
		images.Clear();

		foreach (string sentence in dialogue.textos)
		{
			sentences.Enqueue(sentence);
		}

		foreach (string name in dialogue.nomes)
		{
			names.Enqueue(name);
		}

		foreach (Image image in dialogue.imagens)
		{
			images.Enqueue(image);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence ()
	{
		if (sentences.Count == 0)
		{
			textFrame.SetActive(false);
			onDialogueFinished?.Invoke();
			onCloseDialogue?.Invoke();
			return;
		}
	
		string sentence = sentences.Dequeue();
		nameText.text = names.Dequeue();
		image = images.Dequeue();

		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
		
    }

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = leadingCharBeforeDelay ? leadingChar : "";
        yield return new WaitForSeconds(delayBeforeStart);

		foreach (char letter in sentence.ToCharArray())
		{
			if (dialogueText.text.Length > 0)
			{
				dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - leadingChar.Length);
			}
            dialogueText.text += letter;
			dialogueText.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

        
		if (leadingChar != "")
		{
			dialogueText.text = dialogueText.text.Substring(0, dialogueText.text.Length - leadingChar.Length);
		}
	}
}
