using UnityEngine;
using System.Collections;

public class AnimationCurvePlayer : MonoBehaviour {
	[SerializeField]
	AnimationCurve timeScaleCurve;

	private float _CurveTime = 0;
	//private Transform _Player;
	// Use this for initialization
	void Start () {
		//_Player = this.gameObject
	}
	
	// Update is called once per frame
	void Update () {
		_CurveTime += Time.deltaTime;
		//Vector3 position = m_MyTransform.position;

		Time.timeScale = timeScaleCurve.Evaluate (_CurveTime);
		//position.z = curveIntroSwoopZ.Evaluate(m_CurveTime);
		
		//m_MyTransform.position = position;
	}
}
