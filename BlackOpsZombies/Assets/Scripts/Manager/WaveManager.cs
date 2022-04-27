using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnManager))]
public class WaveManager : MonoBehaviour {

	private int _wave = 0;
	private int _amountToSpawn = 10;
	private bool _canWave = true;
	private SpawnManager _spawnManager;
	private Transform _player; 

	[SerializeField] private float _batchSpawnAmount;
	[SerializeField] private float _batchWaitSeconds;
	[SerializeField] private Transform[] _spawnNodes;

	// Use this for initialization
	void Start () {
		_spawnManager = GetComponent<SpawnManager>();
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		StartCoroutine(WaveSpawn());
		StartCoroutine(CheckEnemyLeft());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator WaveSpawn()
    {
		while (true)
        {
			if (_canWave)
            {
				for(int i = 0; i < _spawnNodes.Length; ++i)
                {
					if(Vector3.Distance(_player.position, _spawnNodes[i].position) > 22)
                    {
						for(int j = 0; j < _amountToSpawn / _spawnNodes.Length; ++j)
                        {
							_spawnManager.SpawnObject(_spawnNodes[i].position);

							if (j == _batchSpawnAmount)
								yield return new WaitForSeconds(_batchWaitSeconds);
                        }
                    }
                }

				_amountToSpawn = Mathf.RoundToInt((float)_amountToSpawn * 1.5f);
            }

			yield return null;
        }
    }

	IEnumerator CheckEnemyLeft()
    {
		var list = GameObject.FindGameObjectsWithTag("Enemy");
		while (true)
        {
			for (int i = 0; i < 60; ++i)
				yield return new WaitForEndOfFrame();

			if (list.Length <= 0)
            {
				_wave++;
				_canWave = true;
            }

			list = GameObject.FindGameObjectsWithTag("Enemy");

			yield return null; 
        }
    }
}
