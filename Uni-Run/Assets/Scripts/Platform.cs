﻿using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour {
    public GameObject[] obstacles; // 장애물 오브젝트들
    private bool stepped = false; // 플레이어 캐릭터가 밟았었는가(점수 반복 상승 막음)

    // 컴포넌트가 활성화될때 마다 매번 실행되는 메서드
    private void OnEnable() {
        // 발판을 리셋하는 처리
        stepped = false;

        foreach (GameObject item in obstacles)
        {
            // Random.Range(0, 3) == 0 : 현재 순번의 장애물을 1/3의 확률로 활성화
            item.SetActive(Random.Range(0, 3) == 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        // 플레이어 캐릭터가 자신을 밟았을때 점수를 추가하는 처리
    }
}