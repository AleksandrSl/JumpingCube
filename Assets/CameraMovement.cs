using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    private Transform _target;            // The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.
    public Vector3 offset; 
                         // The initial offset from the target.

    void Start()
    { 
    }
    
    void Update()
    {
        //if (MainLogic._endGame)
        //{
        //    _target = GameObject.FindGameObjectWithTag("StaticObject").transform;
        //}
        //else
        //{
        if (GameObject.FindGameObjectWithTag("Alive") == null)
        {
            _target = GameObject.FindGameObjectWithTag("StaticObject").transform;

        }
        else { _target = GameObject.FindGameObjectWithTag("Alive").transform; }

            Vector3 targetCamPos = _target.position + offset;

            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        //}
    }
}