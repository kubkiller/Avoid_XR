using UnityEngine;

public class PatternController : MonoBehaviour
{
	[SerializeField]
	private	GameController	gameController;
	[SerializeField]
	private	GameObject[]	patterns;			// �����ϰ� �ִ� ��� ����
	[SerializeField]
	private	GameObject		hpPotion;           // ü�� ȸ�� ����
	public int cnt = 4;

	private	GameObject		currentPattern;		// ���� ������� ����
	private	int[]			patternIndexs;		// ��ġ�� �ʴ� patterns.Length ������ ����
	private	int				current = 0;        // patternIndexs �迭�� ����

	private void Awake()
	{
		// �����ϰ� �ִ� ����(patterns) ������ �����ϰ� �޸� �Ҵ�
		patternIndexs = new int[patterns.Length];

		// ó������ ������ ���������� �����ϵ��� ����
		for ( int i = 0; i < patternIndexs.Length; ++ i )
		{
			patternIndexs = Utils.RandomNumbers(patternIndexs.Length, patternIndexs.Length);
		}
	}

	private void Update()
	{
		if ( gameController.IsGamePlay == false ) return;

		// ���� ������� ������ ����Ǿ� ������Ʈ�� ��Ȱ��ȭ�Ǹ�
		if ( currentPattern.activeSelf == false || cnt == 0)
		{
			// ���� ���� ����
			ChangePattern();
			if (cnt <= 3)
            {
				cnt++;
				if(cnt == 3)
                {
					cnt = 4;
					GameObject.Find("Pattern06").SetActive(false);
                }
			}

		}
	}

	public void GameStart()
	{
		ChangePattern();
	}

	public void GameOver()
	{
		// ���� ������� ���ϸ� ��Ȱ��ȭ
		currentPattern.SetActive(false);
	}

	public void ChangePattern()
	{
		// ���� ����(currentPattern) ����
		currentPattern = patterns[patternIndexs[current]];

		// ���� ���� Ȱ��ȭ
		currentPattern.SetActive(true);

		current ++;

		//4, 3, 3���� ������ Ŭ�������� �� ü�� ȸ�� ���� ����
		if ( current == 4 || current == 7 || current == 10 )
		{
			hpPotion.SetActive(true);
		}

		// ������ �ѹ��� ��� �����ߴٸ� ���� ������ ��ġ�� �ʴ� ������ ���ڷ� ����
		if ( current >= patternIndexs.Length )
		{
			patternIndexs = Utils.RandomNumbers(patternIndexs.Length, patternIndexs.Length);
			current = 0;
		}
	}
}

