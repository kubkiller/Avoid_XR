using System.Collections;
using UnityEngine;

public class Pattern09 : MonoBehaviour
{
	[SerializeField]
	private	GameObject		ground;			// 추가 지형
	[SerializeField]
	private	GameObject[]	warningImages;	// 경고 이미지 (상, 중, 하단)
	[SerializeField]
	private	GameObject[]	prefabs;		// 프리팹 배열
	[SerializeField]
	private	int				setCount;		// 상-중-하단을 한 셋트로 프리팹 등장 횟수

	private	AudioSource		audioSource;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		StartCoroutine(nameof(Process));
	}

	private void OnDisable()
	{
		StopCoroutine(nameof(Process));
	}

	private IEnumerator Process()
	{
		// 패턴 시작 전 잠시 대기하는 시간
		yield return new WaitForSeconds(1);

		// 발판을 활성화하고 1초 대기
		ground.SetActive(true);
		yield return new WaitForSeconds(1);

		// 하단 - 중단 - 상단 순차적으로 프리팹 생성
		int[] numbers = new int[3] { 0, 1, 2 };
		yield return StartCoroutine(SpawnPrefabSet(numbers, 0.5f, 1));

		// ? - ? - ? 임의의 순서대로 프리팹 생성 (setCount 횟수만큼 반복)
		int count = 0;
		while ( count < setCount )
		{
			// 0~2까지 숫자를 임의의 순서대로 numbers 배열에 저장
			numbers = Utils.RandomNumbers(3, 3);
			yield return StartCoroutine(SpawnPrefabSet(numbers, 0.5f, 1));

			count ++;
		}

		// 발판 비활성화
		ground.SetActive(false);

		// 패턴 오브젝트 비활성화
		gameObject.SetActive(false);
	}

	private IEnumerator SpawnPrefabWithWarning(int index, float waitTime)
	{
		// index번째 경고 이미지를 waitTime 시간동안 활성-비활성
		warningImages[index].SetActive(true);
		yield return new WaitForSeconds(waitTime);
		warningImages[index].SetActive(false);

		// 사운드 재생
		audioSource.Play();

		// 0 : 왼쪽에서 오른쪽으로, 1 : 오른쪽에서 왼쪽으로
		int spawnType = Random.Range(0, 2);
		// 여러 prefabs 오브젝트 중 임의의 오브젝트 선택
		int prefabIndex = Random.Range(0, prefabs.Length);
		// 오브젝트 생성 및 위치 설정
		Vector3 position = new Vector3(spawnType == 0 ? Constants.min.x : Constants.max.x, warningImages[index].transform.position.y, 0);
		GameObject clone = Instantiate(prefabs[prefabIndex], position, Quaternion.identity);
		// 오브젝트 이동 방향 설정
		clone.GetComponent<MovementTransform2D>().MoveTo(spawnType == 0 ? Vector3.right : Vector3.left);
	}

	private IEnumerator SpawnPrefabSet(int[] numbers, float delayTime, float endOfWaitTime=1)
	{
		int index = 0;
		while ( index < numbers.Length )
		{
			StartCoroutine(SpawnPrefabWithWarning(numbers[index], delayTime));

			yield return new WaitForSeconds(delayTime * 0.5f);
			
			index ++;
		}

		yield return new WaitForSeconds(endOfWaitTime);
	}
}

