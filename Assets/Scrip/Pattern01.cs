using System.Collections;
using UnityEngine;

public class Pattern01 : MonoBehaviour
{
	[SerializeField]
	private GameObject enemyPrefab;     // 적 프리팹
	[SerializeField]
	private int maxEnemyCount;      // 적 생성 숫자
	[SerializeField]
	private float spawnCycle;           // 적 생성 주기

	private AudioSource audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		StartCoroutine(nameof(SpawnEnemys));
	}

	private void OnDisable()
	{
		StopCoroutine(nameof(SpawnEnemys));
	}

	private IEnumerator SpawnEnemys()
	{
		// 패턴 시작 전 잠시 대기하는 시간
		float waitTime = 1;
		yield return new WaitForSeconds(waitTime);

		int count = 0;
		//while ( true )
		while (count < maxEnemyCount)
		{
			// 음성 사운드는 재생이 종료되면 다시 재생
			if (audioSource.isPlaying == false)
			{
				audioSource.Play();
			}

			Vector3 position = new Vector3(Random.Range(Constants.min.x, Constants.max.x), Constants.max.y, 0);
			Instantiate(enemyPrefab, position, Quaternion.identity);

			yield return new WaitForSeconds(spawnCycle);

			count++;
		}

		// 패턴 오브젝트 비활성화
		gameObject.SetActive(false);
	}
}


