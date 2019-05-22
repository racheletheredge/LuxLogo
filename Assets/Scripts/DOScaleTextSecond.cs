using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening.Core;
using DG.Tweening;
using MirzaBeig.Scripting.Effects;

public class DOScaleTextSecond : MonoBehaviour {


	List<Vector3> initPos;
	public float gapBetweenWords = .5f;
	public float gapBetweenLetters = .1f;
	public float scaleUpSpeed = 1;
	public float scaleDownSpeed = 1;
	float count;
	int counter;
	public Ease easeIn;
	public Ease easeOut;

	public bool reset = true;

	float timeCounter = 0;


	public float goAwayDelay = 1;

	public Vector3 startPos;
	public Vector3 endPos;

	public Vector3 startScale;
	public Vector3 scaleUp = Vector3.one;
	public Vector3 endScale;

	public Vector3 startRotation;
	public Vector3 endRotation;



	void Start () {
		Init ();
		//Reinit ();
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

			}
		}
	}

	void Reinit(){
		for (int i = 0; i < this.transform.childCount; i++) {
			for (int j = 0; j < this.transform.GetChild(0).childCount; j++) {
				Transform t = this.transform.GetChild (0).GetChild (j);
				t.localEulerAngles = startRotation;
				t.localPosition += startPos;
				t.localScale = startScale;
			}
		}

	}

	void Animate(){
		
		
		count = 0;
		int p = 0;

		for (int i = 0; i < this.transform.childCount; i++) {
			for (int j = 0; j < this.transform.GetChild(0).childCount; j++) {
				
				Transform t = this.transform.GetChild (0).GetChild (j);

				t.DOLocalRotate (new Vector3(0,0,45), scaleUpSpeed).SetEase (easeIn).SetDelay (count);
				t.DOLocalRotate ( endRotation, scaleDownSpeed).SetEase (easeOut).SetDelay (count+goAwayDelay);

				t.DOLocalMove (initPos[p], scaleUpSpeed).SetEase (easeIn).SetDelay (count);
				t.DOLocalMove (initPos[p]+endPos, scaleDownSpeed).SetEase (easeOut).SetDelay (count+goAwayDelay);

				t.DOScale (scaleUp, scaleUpSpeed ).SetEase(easeIn).SetDelay (count);
				t.DOScale (endScale, scaleDownSpeed ).SetEase(easeOut).SetDelay (count + goAwayDelay);
			
				p+=1;
				count+=gapBetweenLetters;
			}
			count += gapBetweenWords;
		}
		count = 0;
	}
		
	void Update () {
		if (reset) {
			Reinit ();
			Animate ();
			reset = false;
		}
	}
}
