using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    [SerializeField]public GameObject explosion;
    public Vector3 position = new Vector3(0.5f, 6.1f, 10);
    public Rigidbody rb;
    public AudioClip explosionSound;


    private AudioSource source;
    private RaycastHit hit;
    private Vector3 _jump = Vector3.zero;
    private Vector3 _zero = Vector3.zero;
    private Vector3 norm = new Vector3(0, 1, 0);
    private Quaternion _rotation = Quaternion.identity;

    private Ray ray;
    private bool _crashed;
    private bool _isJumped;
    private GameObject _explosion;

    // Use this for initialization

    void Awake()
    {

        source = GetComponent<AudioSource>();
    }

	IEnumerator WaitForNewTarget(float delay) {
		print(Time.time);
		yield return new WaitForSeconds (delay);
		gameObject.tag = "Dead";



		Destroy (rb);
		//print(Time.time);
	}


    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        
        _isJumped = false;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (rb != null)
        {
            if ((collision.gameObject.name == "Floor") && (!_crashed))
            {
               // if (MainLogic._score != 0)
                //{
                    
               //     _crashed = true;
                //}
                //else
                //{
                    MainLogic._score = Mathf.Max(Vector3.Angle(-transform.up, Vector3.ProjectOnPlane(-transform.up, norm)), MainLogic._score);
                    _crashed = true;
					Time.timeScale = 1.0f; 
                    gameObject.tag = "Dead";
					Destroy(rb);

                //}
            }

            if ((collision.gameObject.name == "Player") && (!_crashed))
            {

                Vector3 contactPoint = collision.contacts[0].point;
                _explosion = Instantiate(explosion, contactPoint, Quaternion.identity) as GameObject;
				ParticleSystem _explosionParticleSystem = _explosion.GetComponent<ParticleSystem>();
				_explosion.tag = "Explosion";
                source.PlayOneShot(explosionSound, 0.5f);
                MainLogic._score *= 2;
                print("Your score:" + MainLogic._score);
                _crashed = true;

				rb.constraints = RigidbodyConstraints.FreezeAll;
				rb.velocity = Vector3.zero;//Time.timeScale = 1.0f; 
				StartCoroutine(WaitForNewTarget(_explosionParticleSystem.duration));
                
				DestroyObject(_explosion, _explosionParticleSystem.duration);

            }

            
        }
    }
   

    void Update()
    {
        if (rb != null)
        {
            if ((Input.GetButton("Jump")) && (_jump.y <= 1200) && !_isJumped)
            {
                _jump.y += 30;
            }
            if (Input.GetButtonUp("Jump") && !_isJumped)
            {
                rb.AddForce(_jump);
                _jump.y = 0;
                _isJumped = true;
            }
            ray = new Ray(rb.position, -transform.up);
            if (Physics.Raycast(ray, out hit, 1.1f) == true)
            {
                if (hit.collider.name == "Platform")
                {
                    _isJumped = false;
                }else {
                    _isJumped = true;
                }
            }
            else { _isJumped = true; }



			if ((rb.position.y <= 3)&& (!_crashed))
            {
                Time.timeScale = 0.2f;
            }
            ///else { Time.timeScale = 1.0f; }

        }
    }
}