using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 position = new Vector3(0.5f, 6.1f, 10);

    private RaycastHit hit;
    private Vector3 _jump = Vector3.zero;
    private Vector3 _zero = Vector3.zero;
    private Quaternion _rotation = Quaternion.identity;
    private bool _crashed;
    private bool _isJumped;
    
    
    // Use this for initialization

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _isJumped = false;
    }

   
    
    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.name == "Floor")&&(!_crashed))
        {
            Vector3 norm = new Vector3(0, 1, 0);
            print("Your score:" + Vector3.Angle(-transform.up, Vector3.ProjectOnPlane(-transform.up, norm)));

            _crashed = true;
        }
        if (_crashed)
        {
            rb.position = position;
            rb.rotation = _rotation;
            rb.velocity = _zero;
            rb.angularVelocity = _jump;
            _jump = _zero;
            _isJumped = false;
            _crashed = false;
        }
    }
   

    void Update()
    {

        if ((Input.GetButton("Jump"))&&(_jump.y <= 700)&&!_isJumped)
        {
            _jump.y += 10; 
        }
        if (Input.GetButtonUp("Jump")&&!_isJumped)
        {
            rb.AddForce(_jump);
            _jump.y = 0;
            _isJumped = true;
        }

        if (Physics.Raycast(rb.position, -transform.up, out hit, 1f))
        {
            if (hit.collider.name == "Platform")
            {
                _isJumped = false;
            }
            else { _isJumped = true; }
        }

        if (rb.position.y <= 3) {
            Time.timeScale = 0.2f; }
        else { Time.timeScale = 1.0f; }

        
    }
}