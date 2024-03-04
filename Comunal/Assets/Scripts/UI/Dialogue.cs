using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue {

	public string[] nomes;

	[TextArea(3, 10)]
	public string[] textos;

	public Image[] imagens;

}