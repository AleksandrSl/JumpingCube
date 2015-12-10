using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainLogic : MonoBehaviour {
    [SerializeField] private Player playerPrefab;
    [SerializeField] private Text textScore;
    [SerializeField] private Text textLife;
    [SerializeField] private GameObject endGamePrefab;
	[SerializeField] private CanvasGroup menu;
    public static float _score = 0;
    public static bool _endGame;
    public static int _cubeQuantity = 5;

    private Player[] _players = new Player[_cubeQuantity];
    private Vector3 _position = new Vector3(0, 6.1f, 10);
    private int _cubeNumber = 0;
    private bool _isAdded;

    void Instantiate() {
        if (!_endGame)
        {

            if ((_cubeNumber == 0) || (_players[_cubeNumber - 1].rb == null))
            {
                textLife.text = "Life: " +( _cubeQuantity - _cubeNumber - 1);
                _players[_cubeNumber] = (Instantiate(playerPrefab) as Player);
                _players[_cubeNumber].name = "Player";
                _players[_cubeNumber].tag = "Alive";
                _cubeNumber += 1;
            }
        }
    }
	public void Hide() {
		menu.alpha = 0f; //this makes everything transparent
		menu.blocksRaycasts = false; //this prevents the UI element to receive input events
		Time.timeScale = 1f;
	}

	void Show() {
		menu.alpha = 1f;
		menu.blocksRaycasts = true;
		Time.timeScale = 0f;
	}

    void clearTheScene(float time = 0)
    {
        _cubeNumber = 0;
        foreach (Player Pl in _players)
        {
            if (Pl != null)
            {
                Destroy(Pl.gameObject, time);
            }
        }
    }

    public void newGame()
    {
        clearTheScene();
        _endGame = false;
        _score = 0;
    }


    // Use this for initialization
	void Start () {
		Hide ();
		Instantiate();
        _endGame = false;
	}
	
	// Update is called once per frame
	void Update () {
		textScore.text = "Score: " + _score;
		if (Input.GetButtonDown("Cancel")) {
			Show();
		}
	
        if ((_cubeNumber == _cubeQuantity) && (GameObject.FindGameObjectWithTag("Alive") == null))
        {
            _endGame = true;
            clearTheScene(2);
        }
        Instantiate();
       
        
    }
}
