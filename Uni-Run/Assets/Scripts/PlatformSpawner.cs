using UnityEngine;

// 발판을 생성하고 주기적으로 재배치하는 스크립트
public class PlatformSpawner : MonoBehaviour {
    public GameObject platformPrefab; // 생성할 발판의 원본 프리팹
    public int count = 5; // 생성할 발판의 개수

    public float timeBetSpawnMin = 0.25f; // 다음 배치까지의 시간 간격 최솟값
    public float timeBetSpawnMax = 0.75f; // 다음 배치까지의 시간 간격 최댓값
    private float timeBetSpawn; // 다음 배치까지의 시간 간격

    public float yMin = -3.5f; // 배치할 위치의 최소 y값
    public float yMax = 1.5f; // 배치할 위치의 최대 y값
    private const float xPos = 20f; // 배치할 위치의 x 값

    private GameObject[] platforms; // 미리 생성한 발판들
    private int currentIndex = 0; // 사용할 현재 순번의 발판

    private Vector2 poolPosition = new Vector2(0, -25); // 초반에 생성된 발판들을 화면 밖에 숨겨둘 위치
    private float lastSpawnTime; // 마지막 배치 시점


    void Start()
    {
        // 변수들을 초기화하고 사용할 발판들을 미리 생성
        platforms = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            // Quaternion.identity : 오일러각의 (0, 0, 0) 회전에 대응
            platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }
        lastSpawnTime = 0f;
        timeBetSpawn = 0f;
    }

    void Update() {
        // 순서를 돌아가며 주기적으로 발판을 배치
        bool isEnableSpawn = Time.time >= lastSpawnTime + timeBetSpawn;
        if (GameManager.instance.isGameover || !isEnableSpawn) return;

        lastSpawnTime = Time.time;
        timeBetSpawn += Random.Range(timeBetSpawnMin, timeBetSpawnMax);

        float yPos = Random.Range(yMin, yMax);

        // platform 컴포넌트의 OnEnable 메서드가 실행됨. => 해당 오브젝트의 상태를 리셋하기 위해
        platforms[currentIndex].SetActive(false);
        platforms[currentIndex].SetActive(true);

        platforms[currentIndex].transform.position = new Vector2(xPos, yPos);

        currentIndex = currentIndex >= count ? 0 : currentIndex + 1;

    }
}