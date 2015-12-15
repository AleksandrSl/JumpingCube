using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	[SerializeField]
	AnimationCurve DamperCurve;
	private GameObject _target; 
	private Vector3 _initialPosition = new Vector3 (6.2f, 4.7f, 9.5f);// The position that that camera will be following.
	private float _damperTime = 0;
	private float _smoothing = 5f;        // The speed with which the camera will be following.
	private float _speed;
	private AudioSource sound;
	private Camera _camera;

	public Vector3 offset;
	public float magnitude;
                         // The initial offset from the target.

    void Start()
    {
		sound = GetComponent<AudioSource> ();
		_camera = GetComponent<Camera> ();
    }
	//void FixedUpdate(){
//		Time.fixedDeltaTime = 0.1f;/
//	}//
    
	IEnumerator Shake() {
		

		
		Vector3 originalCamPos = transform.position;
		//print (originalCamPos.x);
		
		while (GameObject.FindGameObjectWithTag ("Explosion")) {
			
			        
			         
			float damper = DamperCurve.Evaluate(_damperTime);

			
			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude* damper;
			y *= magnitude* damper;

			transform.position = new Vector3(originalCamPos.x + x,originalCamPos.y + y, originalCamPos.z);
			
			yield return null;
		}
		

	}


	void LateUpdate()
    {
		//if (MainLogic._endGame)
		//{
		//    _target = GameObject.FindGameObjectWithTag("StaticObject").transform;
		//}
		//else
		//{

		sound.volume = _camera.velocity.magnitude/10; //rbCam.velocity.normalized.magnitude;


		if (GameObject.FindGameObjectWithTag ("Explosion")) {
			//print(transform.position.x);
			_damperTime = 0;
			_damperTime += Time.deltaTime;  
			StartCoroutine (Shake ());
		} else {
			if (GameObject.FindGameObjectWithTag ("Alive") == null) {
				//_target = GameObject.FindGameObjectWithTag ("StaticObject").transform;
				//_smoothing = 3f;
				//Vector3 targetCamPos  = _initialPosition;

				/////transform.position = Vector3.Lerp (transform.position, targetCamPos, _smoothing * Time.deltaTime);
				//print (_target);
				
			} else {
				_smoothing = 4;
				_target = GameObject.FindGameObjectWithTag ("Alive"); 
				
				Rigidbody rb = _target.GetComponent<Rigidbody>();
				//print ("Found");
				
				Vector3 targetCamPos = _target.transform.position + offset + rb.velocity * 10 * Time.deltaTime;
				transform.position = Vector3.Lerp (transform.position, targetCamPos, _smoothing * Time.deltaTime);

				//}
			}
		}

    }
}