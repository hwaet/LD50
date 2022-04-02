using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TextType : BaseMeshEffect
{

	public float wordsPerMinute = 150f;
	private float charsPerWord = 5f;
	public float indexToPrintTo = 999f;

	public AnimationCurve alphaCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));

	Graphic canvas;

	//	public AudioClip typeSound1;
	//	public AudioClip typeSound2;

	float typingProgress = 0f;
	public bool repeat = false;

	Text textComp;

	private void Update()
	{
		canvas.SetVerticesDirty();
	}

	protected new void OnValidate()
	{
		canvas = GetComponent<Graphic>();
		textComp = GetComponent<Text>();
	}

	protected override void Start()
	{
		startText();
	}

	[ContextMenu("Start Typing")]
	public void startText()
	{
		OnValidate();
		if (this.isActiveAndEnabled == true)
		{
			StartCoroutine(TypeText());
		}
	}

	public override void ModifyMesh(VertexHelper vh)
	{

		int vertCount = vh.currentVertCount;
		int previousCharactersInVerts = Mathf.FloorToInt(indexToPrintTo) * 4;
		int endIndex = alphaCurve.length != 0 ? (alphaCurve.length - 1) : 0;
		float curveLength = alphaCurve.keys[endIndex].time - alphaCurve.keys[0].time;

		var vert = new UIVertex();
		for (int v = 0; v < vertCount; v++)
		{
			vh.PopulateUIVertex(ref vert, v);

			//if (v < previousCharactersInVerts)
			//{
			//	vert.color.a = 255;
			//}
			//else if (v > previousCharactersInVerts + 3)
			//{
			//	vert.color.a = 0;
			//}
			//else
			//{
			//	vert.color.a = (byte) Mathf.CeilToInt(alphaValue);
			//}
			float remainder = v % 4;
			float vertToPointTo = (indexToPrintTo * 4);
			float curveTimeAxis = (vertToPointTo - (v - remainder)) / 4; //subtract the remainder to step alpha on every 4 groups of verts //Mathf.Repeat(indexToPrintTo, 1.0f);
			float alphaValue = alphaCurve.Evaluate(curveTimeAxis) * 255;

			vert.color.a = (byte)Mathf.CeilToInt(alphaValue);


			vh.SetUIVertex(vert, v);
		}

	}

	IEnumerator TypeText()
	{

		TextGenerator gen = textComp.cachedTextGenerator;
		indexToPrintTo = 0;

		while (indexToPrintTo <= gen.characterCount)
		{

			float numberOfLettersToPrintThisFrame = wordsPerMinute / (charsPerWord * 60f);
			indexToPrintTo += numberOfLettersToPrintThisFrame;

			int currentCharacterIndex = Mathf.FloorToInt(indexToPrintTo);

			typingProgress = indexToPrintTo / gen.characterCount;



			//if (repeat==true) {
			//	if (textComp.text.Length == message.Length) {
			//		textComp.text = textComp.text.Remove(0,Mathf.FloorToInt(message.Length * .5f));
			//		indexToPrintTo -= (message.Length * .5f);
			//		print ("looping text");
			//	}

			//}

			yield return 0;
		}

		if (repeat == true)
		{
			StartCoroutine(TypeText());
		}

		//		if (charsToPrint>1) {
		//			foreach (char letter in message.ToCharArray()) {
		//				textComp.text += letter;
		//	//			if (typeSound1 && typeSound2)
		//	//				SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
		//
		//	//			yield return new WaitForSeconds (letterPause);
		//			}

	}


}
