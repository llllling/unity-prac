﻿using System.Collections.Generic;
using UnityEngine;

// 적 게임 오브젝트를 주기적으로 생성
public class EnemySpawner : MonoBehaviour {
    public Enemy enemyPrefab; // 생성할 적 AI

    public Transform[] spawnPoints; // 적 AI를 소환할 위치들

    public float damageMax = 40f; // 최대 공격력
    public float damageMin = 20f; // 최소 공격력

    public float healthMax = 200f; // 최대 체력
    public float healthMin = 100f; // 최소 체력

    public float speedMax = 3f; // 최대 속도
    public float speedMin = 1f; // 최소 속도

    public Color strongEnemyColor = Color.red; // 강한 적 AI가 가지게 될 피부색

    /* List : 배열과 달리 저장공간의 크기가 자유롭게 변하는 특징이 있다. 좀비 서바이버에서 적 수는 실시간으로 늘어나거나 줄어들기 때문에 
     * 생성한 적을 리스트로 저장*/
    private List<Enemy> enemies = new List<Enemy>(); // 생성된 적들을 담는 리스트
    private int wave = 0; // 현재 웨이브

    private void Update() {
        // 게임 오버 상태일때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.IsGameover)
        {
            return;
        }

        // 적을 모두 물리친 경우 다음 스폰 실행
        if (enemies.Count <= 0)
        {
            SpawnWave();
        }

        // UI 갱신
        UpdateUI();
    }

    // 웨이브 정보를 UI로 표시
    private void UpdateUI() {
        // 현재 웨이브와 남은 적의 수 표시
        UIManager.instance.UpdateWaveText(wave, enemies.Count);
    }

    // 현재 웨이브에 맞춰 적을 생성
    private void SpawnWave() {
        wave++;

        int spawnCount = Mathf.RoundToInt(wave * 1.5f);

        while(spawnCount > -1) {
            float enemyIntensity = Random.Range(0f, 1f);
            CreateEnemy(enemyIntensity);

            spawnCount--;
        }
    }

    // 적을 생성하고 생성한 적에게 추적할 대상을 할당
    private void CreateEnemy(float intensity) {
        // intensity 가 0에 가까울 수록 Min 값에 가까워지며, 1에 가까울 수록 Max에 가까워 진다.
        float health = Mathf.Lerp(healthMin, healthMax, intensity);
        float damage = Mathf.Lerp(damageMin, damageMax, intensity);
        float speed = Mathf.Lerp(speedMin, speedMax, intensity);
        // white와 strongEnemyColor를 섞은 컬러 반환
        Color skinColor = Color.Lerp(Color.white, strongEnemyColor, intensity);

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Enemy enemy = Instantiate(enemyPrefab, spawnPoint.position,spawnPoint.rotation);
        enemy.Setup(health, damage, speed, skinColor);

        enemies.Add(enemy);

        enemy.onDeath += () => enemies.Remove(enemy);
        enemy.onDeath += () => Destroy(enemy.gameObject, 10f); //10초 뒤에 파괴
        // 적 사망 시 점수 상승
        enemy.onDeath += () => GameManager.instance.AddScore(100);
    }
}