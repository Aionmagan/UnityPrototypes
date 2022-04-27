using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour {

	[SerializeField] private ACharacter _player;
	[SerializeField] private Image _image;

	private Color _currentColor; 

	// Use this for initialization
	void Start () {
		_image = GetComponentInChildren<Image>();
		_currentColor = _image.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (_player != null)
        {
			_currentColor.a = 1f - _player.Health * 0.01f;
			_image.color = _currentColor;
        }
	}
}
