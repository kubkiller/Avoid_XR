using System.Collections;
using UnityEngine;

public class Pattern10 : MonoBehaviour
{
	[SerializeField]
	private	GameObject			warningObject;	// 경고 오브젝트
	[SerializeField]
	private	MovementTransform2D	unityLogo;		// 유니티 로고 오브젝트
	[SerializeField]
	private	float				moveTime;		// 이동 시간
	[SerializeField]
	private	float				minX = -2.7f;	// 왼쪽 x 위치
	[SerializeField]
	private	float				maxX = 2.7f;	// 오른쪽 x 위치

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

		// 경고 오브젝트 활성-비활성
		warningObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		warningObject.SetActive(false);

		// 로고 오브젝트 활성화
		unityLogo.gameObject.SetActive(true);

		// 처음 이동 방향을 오른쪽으로 설정
		unityLogo.MoveTo(Vector3.right);

		// 로고 오브젝트 좌/우 이동
		float time = 0;
		while ( time < moveTime )
		{
			time += Time.deltaTime;

			// 로고의 위치가 왼쪽 최소 범위를 넘어가면 이동 방향을 오른쪽으로 설정
			if ( unityLogo.transform.position.x <= minX )
			{
				unityLogo.MoveTo(Vector3.right);
			}
			// 로고의 위치가 오른쪽 최대 범위를 넘어가면 이동 방향을 왼쪽으로 설정
			else if ( unityLogo.transform.position.x >= maxX )
			{
				unityLogo.MoveTo(Vector3.left);
			}

			yield return null;
		}

		// 로고 오브젝트 비활성화
		unityLogo.gameObject.SetActive(false);

		// 패턴 오브젝트 비활성화
		gameObject.SetActive(false);
	}
}

