using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening;
using MirzaBeig.Scripting.Effects;

public class DOScaleText : MonoBehaviour {


	List<Vector3> initPos;
	public float gapBetweenWords = .5f;
	public float gapBetweenLetters = .1f;
	public float scaleUpSpeed = 1;
	float count;
	public float noiseSpeed = .4f;
	public float noiseAmount = .025f;
	int counter;
	public Ease ease;

	public bool reset = true;

	public float startTime = 5;
	float timeCounter = 0;


	public Vector3 scaleTo;

	void Start () {
		Init ();
	}


	Vector3 GetNoisePosition(Vector3 v, Vector3 t){
		return new Vector3 (
			Noise2.perlin (t.x+v.x+.01f, t.y+v.y, t.z+v.z),
			Noise2.perlin (t.z+v.x, t.x+v.y+.01f, t.y+v.z),
			Noise2.perlin (t.y+v.x, t.z+v.y, t.x+v.z+.01f));
	}

	void Init(){
		initPos = new List<Vector3> ();

		for (int i = 0; i < this.transform.childCount; i++) {
			for (int j = 0; j < this.transform.GetChild(0).childCount; j++) {
				Transform t = this.transform.GetChild (0).GetChild (j);
				initPos.Add (t.localPosition);
				t.localPosition = new Vector3 (t.localPosition.x, t.localPosition.y + 10.15f, t.localPosition.z);
				t.localScale = Vector3.zero;
			}
		}
	}

	void Animate(){
		
		
		count = 0;
		int p = 0;

		for (int i = 0; i < this.transform.childCount; i++) {
			for (int j = 0; j < this.transform.GetChild(0).childCount; j++) {
				Transform t = this.transform.GetChild (0).GetChild (j);
				t.DOScale (scaleTo, scaleUpSpeed).SetEase (ease).SetDelay (count);
				t.DOLocalMove (initPos[p], scaleUpSpeed).SetEase (ease).SetDelay (count);
			
				p+=1;
				count+=gapBetweenLetters;
			}
			count += gapBetweenWords;
		}
		count = 0;
	}
		
	void Update () {
		if (reset) {
			Animate ();
			reset = false;
		}
	}
}
