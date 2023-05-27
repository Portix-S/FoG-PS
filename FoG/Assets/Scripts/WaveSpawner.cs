using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };
	private GameManager gm;
	[System.Serializable]
	public class Wave
	{
		public string name;
		public Transform enemy;
		public int count;
		public float rate;
	}

	public Wave[] waves;
	private int nextWave = 0;
	public int NextWave
	{
		get { return nextWave + 1; }
	}

	public float timeBetweenWaves = 5f;
	private float waveCountdown;
	public float WaveCountdown
	{
		get { return waveCountdown; }
	}

	private float searchCountdown = 1f;
	Vector3 startPos;

	private SpawnState state = SpawnState.COUNTING;
	public SpawnState State
	{
		get { return state; }
	}

    public void Reset()
    {
		nextWave = 0;
		Destroy(GameObject.FindGameObjectWithTag("Boss"));
    }
    void Start()
	{
		waveCountdown = timeBetweenWaves;
        startPos = transform.position;
		gm = GetComponent<GameManager>();
	}

	void Update()
	{
		if (state == SpawnState.WAITING)
		{
			if (!EnemyIsAlive())
			{
				WaveCompleted();
			}
			else
			{
				return;
			}
		}

		if (waveCountdown <= 0)
		{
			if (state != SpawnState.SPAWNING)
			{
				StartCoroutine( SpawnWave ( waves[nextWave] ) );
			}
		}
		else if(!gm.onMenu)
		{
			waveCountdown -= Time.deltaTime;
		}
	}

	void WaveCompleted()
	{
		Debug.Log("Wave Completed!");

		state = SpawnState.COUNTING;
		waveCountdown = timeBetweenWaves;
		Debug.Log(gm.hasRequiredPoints + " " + nextWave + " " + waves.Length);
		if (nextWave + 1 > waves.Length - 1 || (nextWave == waves.Length - 2 && !gm.hasRequiredPoints))
		{
			nextWave = 0;
			if(gm.hasRequiredPoints)
            {
				gm.ResetWavePoints();
            }
			Debug.Log("ALL WAVES COMPLETE! Looping...");
		}
		else
		{
			if(gm.hasRequiredPoints)
            {
				nextWave = waves.Length - 1;
            }
            else 
				nextWave++;
		}
	}

	bool EnemyIsAlive()
	{
		searchCountdown -= Time.deltaTime;
		if (searchCountdown <= 0f)
		{
			searchCountdown = 1f;
			if (GameObject.FindGameObjectWithTag("Enemy") == null && GameObject.FindGameObjectWithTag("Boss") == null)
			{
				return false;
			}
		}
		return true;
	}

	IEnumerator SpawnWave(Wave _wave)
	{
		Debug.Log("Spawning Wave: " + _wave.name);
		state = SpawnState.SPAWNING;

		for (int i = 0; i < _wave.count; i++)
		{
			SpawnEnemy(_wave.enemy);
			yield return new WaitForSeconds( _wave.rate );
		}

		state = SpawnState.WAITING;

		yield break;
	}

	void SpawnEnemy(Transform _enemy)
	{
		Debug.Log("Spawning Enemy: " + _enemy.name);

		Transform enemy = Instantiate(_enemy);
		//float startX = Random.Range(startPos.x - 18f, startPos.x + 18f);

		//enemy.transform.position = new Vector3(startX, startPos.y, startPos.z);
	}
}
