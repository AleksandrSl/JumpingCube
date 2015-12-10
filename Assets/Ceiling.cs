using UnityEngine;
using System.Collections;

public class Ceiling : MonoBehaviour {
    public float upperBound = 80.0f;
    public float lowerBound = 10.0f;
    //public float rotationSpeed = 0.1f;
    public float _rotationX = 45.0f;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ((transform.rotation.x >= upperBound) || ((transform.rotation.x <= lowerBound)))
        {
            //print(transform.rotation.x);
            _rotationX = -_rotationX;
        }
        
        transform.Rotate(_rotationX*Time.deltaTime, 0, 0);
	}
}
