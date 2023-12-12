# 유니런

### 목표

2D 러너 게임 유니런을 제작함.

### 미션

계속 뒤면서 발판(플랫폼) 사이를 점프해 낭떠러지로 떨어지지 않고 살아남아라!

### 조작법

- 점프 : 마우스 왼쪽 버튼
- (사망 후) 게임 재시작 : 마우스 왼쪽 버튼

### 기능

1. 발판은 무한 생성됨. 발판의 생성 간격과 높이는 랜덤. 각각의 발판 위에는 1~3개의 장애물이 일정 확률로 배치됨.
2. 캐릭터가 점프 후 새로운 발판에 착지할 때마다 점수가 추가
3. 플레이어는 마우스 왼쪽 버튼으로 점프. 이단 점프도 가능
4. 마우스 왼쪽 버튼을 누르는 시간으로 점프 높이를 조정할 수 있음. 버튼을 오래 누르면 상대적으로 높이 점프함
5. 플레이어 캐릭터에 애니메이션이 적용됨. 상황에 따라서 뛰거나, 점프하거나, 죽는 애니메이션이 재생됨

### 예제 프로젝트를 진행하면서 리팩토링

- if-else 구문은 대부분 if { return;}으로 구현하여 else 블록을 지움.

#### PlayerController.cs

- 변하지 않는 값은 상수로 표현

  - before

  ```C#
    // 마우스 왼쪽 버튼 누르고 최대 점프 횟수(2)에 도달하지 않았으면
  if (Input.GetMouseButtonDown(0) && jumpCount < 2)
  {

  }
  ```

  - after

  ```C#
    // 마우스 왼쪽 버튼 누르고 최대 점프 횟수(2)에 도달하지 않았으면
  private const int maxJumpCount = 2;

  if (Input.GetMouseButtonDown(0) && jumpCount < maxJumpCount2)
  {

  }
  ```

* if 조건문에 사용되는 수식을 명확하게 어떤 조건문인지 표현하기 위해 변수로 선언(게임 개발할 때는 메모리가 중요해서 이렇게 변수를 사용하는 것이 성능에 많이 영향을 미치는걸 까..? )

  - gpt 에 문의 결과 : 메모리 측면에서는 두 코드 모두 큰 차이가 없다. 변수를 추가로 만들더라도 메모리 영향은 미미하다. 따라서 메모리 성능에 큰 영향을 미치지 않고, 가독성과 유지보수성을 향상시킬 수 있는 코드 작성이 더 중요하다고 함. 리팩토링한 게 더 권장한다고 함!

  * before

    ```C#
     // 마우스 왼쪽 버튼 누르고 최대 점프 횟수에 도달하지 않았으면
    if (Input.GetMouseButtonDown(0) && jumpCount < maxJumpCount)
    {

    } else if (Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0) {
    //마우스 왼쪽 버튼에서 손을 떼는 순간 && 속도의 y 값이 양수라면(위로 상승 중)
    }
    ```

  * after

    ```C#
     // 마우스 왼쪽 버튼 누르고 최대 점프 횟수에 도달하지 않았으면
    bool isJumpOn = Input.GetMouseButtonDown(0) && jumpCount < maxJumpCount;
    //마우스 왼쪽 버튼에서 손을 떼는 순간 && 속도의 y 값이 양수라면(위로 상승 중)
    bool isJumpOff = Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0;
    if (isJumpOn)
    {

    } else if (isJumpOff) {

    }
    ```

#### Platform.cs

- 불필요한 if-else 코드 제거
  - before
  ```C#
  for (int i = 0; i < obstacles.Length; i++)
  {
    // Random.Range(0, 3) == 0 : 현재 순번의 장애물을 1/3의 확률로 활성화
    if (Random.Range(0, 3) == 0) {
        obstacles[i].SetActive(true);
    } else {
         obstacles[i].SetActive(false);
    }
  }
  ```
  - after
  ```C#
  foreach (GameObject item in obstacles)
  {
      item.SetActive(Random.Range(0, 3) == 0);
  }
  ```
