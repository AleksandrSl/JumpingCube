using UnityEngine;
using System.Collections;

public class Ceiling : MonoBehaviour {
    public float upperBound = 80.0f;
    public float lowerBound = 10.0f;
    //public float rotationSpeed = 0.1f;
    public float _rotationX = 45.0f;
    
	private int _direction = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.rotation.x > upperBound) 
        {
			_direction = -1;
            
		} 
		if (transform.rotation.x < lowerBound) { _direction = 1;}
        
        transform.Rotate(_rotationX*_direction*Time.deltaTime, 0, 0);
	}
}
