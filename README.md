# UnityPrac

## 프로젝트를 진행하면서 배운 것들 정리

<details open>
<summary> <h1> Dodge 프로젝트 </h1> </summary>
<div>

### 개념적

- Plane의 크기와 유닛 단위 : Plane의 크기는 가로세로 10유닛(Unit), 유니티에서 1유닛은 Cube 한 변의 길이, 즉, cube 가로길이의 10배

* 머티리얼(Material) : 셰이더와 텍스처가 합쳐진 에셋. 오브젝트의 픽셀 컬러를 결정

  - 셰이더 : 주어진 입력에 따라 픽셀의 최종 컬러를 결정하는 코드, 질감과 빛에 의한 반사와 굴절 등의 효과를 만들어 냄. => 물감
  - 텍스처 : 표면에 입히는 이미지 파일 => 스케치나 밑그림을 이해

* 알베도 : 반사율이라는 뜻, 물체가 어떤 색을 반사할지 결정 => 즉, 물체 표면의 기본색을 결정
* 트리거 콜라이더(Trigger Collider)[https://www.notion.so/Trigger-Collider-f05a6e6d6ada49ec8ea66207953a2815]
* 프리팹[https://www.notion.so/99ad603914194c838bfd39ec360131b9]

* <span style='background-color: #fcba03; color: black;'>리지드바디의 제약을 사용하면 힘이나 충돌 등 물리적인 상호작용으로 위치나 회전이 변경되는 것을 막을 수 있다. 그러나 트랜스폼 컴포넌트의 위치나 회전에 새로운 값을 할당하여 위치나 회전을 변경하는 것을 막을 수는 없다.</span>
* 충돌 이벤트 메서드[https://www.notion.so/661a5ab72b7b4ed7a93226bfb9c815c3]
* 유니티의 UI 시스템(**UGUI**) : 게임 월드와 UI를 별개의 공간으로 다루는 경우가 많았음 => 게임 월드(씬)에는 플레이어나 몬스터 등의 게임 오브젝트가 구성되고, 그것에 대한 정보를 표시하는 UI는 게임 오브젝트가 아닌 별개의 존재로 별개의 공간에서 다루는 경우가 많았다 => **유니티는 UI요소를 게임 월드 속의 게임 오브젝트 취급함**
* Quaternion(쿼터니언)[https://www.notion.so/Quaternion-885eb825a8db477282a0d857da7932b6]
  - 트랜스폼 컴포넌트의 rotation(회전)의 타입은 Vector3가 아닌 Quaternion임
  * 유니티 에디터의 인스펙터 창에서는 Quaternion이 비직관적이라서 rotation의 값읏 Vector3로 다루도록 배려한 것.

### 스크립트

- **스크립트 생성 후 유니티 에디터에서 스크립트 파일명 변경하면 스크립트 파일에 선언된 클래스명 자동으로 갱신안되니까 똑같이 수동으로 바꿔줘야 함. 스크립트 파일명이랑 클래스명이 같아야 올바르게 작동**

* MonoBehaviour 클래스를 상속받는 클래스들 유니티에서 컴포넌트로 사용 가능
* Update() : update 메서드는 한 프레임에 한 번, 매 프레임마다 반복 실행됨. => 60FPS이면 1초에 60번 실행됨.
* gameObject: gameObject 변수는 컴포넌트들의 기반 클래스인 MonoBehaviour에서 제공하는 GameObject 타입의 변수, 컴포넌트 입장에서 자신이 추가된 게임 오브젝트를 가리키는 변수
* GetComponent<~>() : 자신의 게임 오브젝트에서 제네릭 부분에 입력한 타입의 컴포넌트를 찾아오는 메서드
* Input.GetAxios(string axisName) : 어떤 축에 대한 입력값을 숫자로 반환하는 메서드(https://www.notion.so/Input-GetAxios-0f3988aa25374898bd16e3a724b10ccc)

- transform : Transform 타입의 변수, 자신의 게임 오브젝트의 transform 컴포넌트로 바로 접근하는 변수.

* FindObjectOfType<~>() : 씬에 존재하는 모든 오브젝트를 검색해서 원하는 타입의 오브젝트를 찾아냄.
  - 처리비용이 크기 때문에 start() 메서드처럼 초기에 한두 번 실행되는 메서드에서만 사용해야 함.
* Time.deltaTime : Update() 실행 사이의 시간 간격을 알기 위한 내장 변수
  - 1초에 60프레임의 속도로 화면을 갱신하는 컴퓨터에서는 1/60의 값, 마찬가지로 1초에 120프레임의 속도를 가진 컴퓨터이면 1/120의 값을 가짐
  * 1초당 60도 회전하도록 하려면 ? (rotationAngle = 60)
    ```C#
        void Update()
        {
            /*
            * 1초당 rotationAngle만큼 회전하도록 Time.deltaTime(초당 프레임에 역수를 취한 값)을 곱해준다.
            * 만약, 60FPS 컴퓨터라면
            * rotationAngle * (1/60) * (1초에 60번 update() 함수 실행)  = 총 60도 회전
            */
            transform.Rotate(0f, rotationAngle * Time.deltaTime , 0f);
        }
    ```
* Instantiate() : 게임 도중에 실시간으로 오브젝트를 생성할 때(즉, 복제) 해당 메서드 사용.
  ```
    Instantiate(원본, 위치, 회전)
  ```
* transform.LookAt(targetTransform) : 입력으로 다른 게임 오브젝트의 트랜스폼을 받는다. 입력받은 트랜스폼의 게임 오브젝트를 바라보도록 자신의 트랜스폼 회전을 변경함.
* using UnityEngine.UI : 유니티 UI 시스템과 관련된 코드 가져옴.
  using UnityEngine.SceneManagement : 씬 관리자(SceneManager) 등이 포함된 씬 관리 관련 코드를 가져옴.
* SceneManager.LoadScene("SampleScene") : 실행되면 직전까지의 씬을 파괴하고, 씬을 다시 로드함. 이것은 게임을 재시작하는 효과
  - SceneManager.LoadScene() : 해당 메서드로 로드할 씬은 빌드 설정의 빌드 목록에 등록되어 있어야 한다. 유니티 프로젝트를 생성할 때 자동 생성되는 SampleScene씬은 빌드 목록에 자동으로 등록되어 있으므로 따로 빌드 목록에 추가할 필요 없음.
    - 빌드 설정창과 빌드 목록 : 유니티 상단 메뉴의 File > Build Settings..으로 확인할 수 있음.
  * 씬 이름 이외에 빌드 순번을 사용해 씬 로드 가능. => SceneManager.LoadScene(0);
* PlayerPrefs[https://www.notion.so/PlayerPrefs-298998b10a08417b8ac6be28ad38e592]
* Text vs TextMeshProUGUI vs TextMeshPro : https://www.notion.so/Text-vs-TextMeshProUGUI-vs-TextMeshPro-81b0b32b5dc943bc9e4163ffd9d77a8d
* Vector3 연산(벡터 정규화, 크기,  내적, 외적)[https://www.notion.so/Vector3-d88974bc10ae4f05ab2910d00087bd29]
* Vector3 응용[https://www.notion.so/Vector3-69eb77db85a446f499ac0fb19e0d9e61]

### 기타

- **<span style='background-color: #fcba03; color: black; font-size: 15px;'>플레이 모드에서 수정한 사항은 저장이 안된다 !!!!! 필요한 수정을 할 경우 반드시 플레이 모드를 해제하고 하라!!!!</span>**

* 오브젝트 복사 : Ctrl + D
</div>
</details>

<details open>
<summary>  <h1>Move 오브젝트의 이동과 회전 연습 </h1> </summary>
<div>

### 개념적

- 유니티 공간[https://www.notion.so/f69c850d440a42849ec8d3ec541c471b]

### 스크립트

- Translate(vector3) : Transform 타입이 제공하는 평행이동을 위한 메서드
  - 기본 지역공간을 기준으로 이루어짐
  - 전역 공간을 기준으로 변경하고 싶으면 두번째 인자로 Space.World 값을 주면 됨.

* Rotate(vector3) : Transform 타입이 제공하는 현재 회전 상태에서 입력된 회전만큼 게임 오브젝트를 더 회전시키는 메서드

  - 지역 공간 기준
  - 전역 공간을 주고싶으면 위 Translate 메서드 처럼 두번째 인자값 주면 됨.

* 벡터의 속기[https://www.notion.so/c681fba458f34b7ca81720ff961dd1f8] : 자주 사용되는 Vector3 값을 즉시 생성할 수 있다.
* Transform 타입이 제공하는 방향 관련 변수(transform.forward 등)[https://www.notion.so/Transform-transform-forward-36094d657455497387383740b080f0cc] 로 게임 오브젝트의 방향을 쉽게 알 수 있다.
</div>
</details>

<details open>
<summary> <h1>유니런(2D) 프로젝트 </h1> </summary>
<div>

### 개념적

- 2D 프로젝트의 주요 특징(이 설정들을 각각 따로 변경하거나, 유니티 프로젝트 모드를 2D 또는 3D로 하여 일괄 변경할 수 있다.)
  - 이미지 파일을 스프라이트 타입으로 임포트함
  - 기본 생성 카메라가 직교모드를 사용함
  - 라이팅 설정 중 일부가 비활성화됨.
  - 씬 창이 2D 뷰로 보임.

* 유니티 2D 프로젝트와 3D 프로젝트는 유의미한 차이가 없다.유니티 프로젝트 생성 이후 언제든지 현재 프로젝트 설정을 2D와 3D 사이에서 변경할 수 있다.
* 프로젝트의 2D/3D 모드 설정과 사용할 컴포넌트의 종료는 서로 관련 없다. 게임 장르에 따라서 2D 프로젝트에서 2D가 아닌 일반 컴포넌트를 사용해도 문제 없음.
  - 2D 컴포넌트는 대부분 Vector2로 동작하거나 Vector3로 동작하되 z값을 무시함.
  - 하지만 2D 게임 오브젝트의 실제 위치값이 Vector2인 것은 아님. 프로젝트의 2D 게임 오브젝트도 실제로는 위치와 스케일 등을 Vector3로 저장한다. 다만, 원근감이 없으니 z값이 의미 없을 뿐.
* 스프라이트 : 2D 그래픽과 UI를 그릴 때 사용하는 텍스처 에셋(이미지 파일)이다.

- 유니티는 2D 프로젝트에서 이미지를 기본적으로 싱글 스프라이트 모드로 가져옴.
  - 싱글 스프라이트 : 하나의 스프라이트 에셋은 하나의 스프라이트를 표현
  - 멀티플 스프라이트[https://www.notion.so/Multiple-f4d9e19d15984b409fa11ddb64e57586] : 하나의 스프라이트 에셋을 여러 개의 개별 스프라이트로 잘라 사용할 수 있음.
  * 스프라이트 선택 > 인스펙터 창에서 Sprite Mode항목에서 single/multiple 변경 후 Apply 클릭

* 리지드바디 2D 컴포넌트 충돌 감지 방식
  1. Discrete(이산) : 충돌 감지를 일정 시간 간격으로 끊어서 실행한다.
  2. Continuous(연속) : 움직이기 이전 위치와 움직인 다음 위치 사이에서 예상되는 충돌까지 함께 감지
  - 연속이 이산보다 충돌 감지가 상대적으로 정확하지만 성능을 더 요구함.
* 해당 프로젝트에서 Player에 콜라이더 적용 시 박스 콜라이더 2D 대신 써클 콜라이더 2D를 사용한 이유 : Player 게임 오브젝트가 점프 후 각진 모소리에 안착했을 때 부드럽게 모서리를 타고 올라가도록 만들기 위함.
* 오디오 소스 컴포넌트[https://www.notion.so/db522d5b982b4aeaa99edf0522311ffa] : 게임 오브젝트에 소리를 낼 수 있는 능력을 부여
  - 오디오 소스 컴포넌트는 소리를 재생하는 부품이지, 소리를 담은 파일이 아님
  - 비유하자면 => 오디오 소스 컴포넌트(카세트 플레이어) , 오디오 클립(카세트테이프)
  * Play On Awake : 오디오 소스 컴포넌트가 활성화 되었을 때 최초 1회 오디오를 자동 재생하는 옵션
    - 해당 프로젝트에선 해당 설정이 활성화되어 있으면 게임 시작과 동시에 점프 소리가 1회 무조건 재생되므로 해제함.
* 애니메이션 만들기[https://www.notion.so/274cf1a0aac0410f9dcc7514303f6c46]
* 애니메이터 컨트롤러와 애니메이터[https://www.notion.so/0c53a802528443538eb41ec3be164b11]
* 정렬 레이어[https://www.notion.so/2e4bc49a7bdb424b843d68a79798490f] : 2D 게임 오브젝트가 그려지는 순서는 스프라이트 렌더러의 정렬 레이어가 결정
  - **가장 아래쪽 정렬 레이어가 가장 앞쪽에 그려진다.**
* 박스 콜라이더 2D 컴포넌트는 추가될 때 2D 게임 오브젝트의 스프라이트에 맞춰서 크기가 자동 설정됨. 따라서 박스 콜라이더 2D 컴포넌트의 size 필드의 x 값을 게임 오브젝트의 가로 길이로 볼 수 있다.
* 캔버스는 UI를 잡아두는 틀이다. 캔버스의 크기는 게임을 실행 중인 화면의 해당도로 결정됨. 캔버스 컴포넌트의 UI 스케일 모드의 **기본 설정은 고정 픽셀 크기** => 캔버스 크기가 변해도 배치된 UI 요소 크기가 변하지 않아서 화면 해상도에 따라 크기가 작아지는 문제 발생 =><span style='background-color: #fcba03; color: black; font-width: bold;'> **화면 크기에 따라 스케일 모드**는 다른 크기의 화면에 캔버스가 그려질 때 캔버스 자체를 확대/축소해서 해상도에 따라 UI 크게 달라지지 않음 </span>
  - 화면 크기에 따라 스케일 모드는 실제 화면과 기준 해상도 사이의 화면 비율이 다른 경우 캔버스 스케일러 컴포넌트 일치(Match)필드 값이 높은 방향의 길이를 유지하고 다른 방향의 길이를 조정함.
  - 그래서 <span style='background-color: #fcba03; color: black;'>UI 요소가 많이 나열된 방향의 일치 값을 높게 주는 것이 좋다.</span>
    - 예를 들어 세로 방향으로 버튼이 많이 나열되어 있다면 화면 비율이 변했을 때 가로보다 세로 방향의 레이아웃이 망가지기 쉽기 때문에 세로 일치값을 높이는게 좋음
* 게임 매니저[https://www.notion.so/ex-7db9587e822c48ffadf3875a66ef69fe] : 게임의 전반적인 상태를 관리하는 역할, 일반적으로 프로그램에 단 하나만 존재해야함(싱클톤 권장)
  - 해당 프로젝트에서의 역할
    - 점수 저장
    - 게임오버 상태 표현
    - 플레이어의 사망을 감지해 게임오버 처리 실행
    - 점수에 따라 점수 UI 텍스트 갱신
    - 게임오버되었을 때 게임오버 UI 활성화
* 오브젝트 풀링[https://www.notion.so/f2618a5d819f43d496f730182fe7c8f6] : 초기에 필요한 만큼 오브젝트를 미리 만들어 '풀'에 쌓아두는 방식 => 해당 프로젝트에서 발판을 무한 반복 생성하기 위해 사용함

### 스크립트

- Input.GetMouseButtonDown(int button) : 마우스 버튼을 누른 순간
  - 파라미터 : 0, 1, 2에 따라 마우스 왼쪽버튼, 오른쪽버튼, 휠버튼

* playerRigidbody.velocity = Vector2.zero : 점프 직전 속도를 제로로 변경하는 이유 => 직전까지의 힘(속도)가 상쇄되거나 합쳐져서 점프 높이가 비일관적으로 되는 현상을 막기위해
  1. 점프 사이에 충분한 시간 간격을 두고 이단 점프 실행(마우스 왼쪽 버튼을 여유 있게 두번 클릭)
  2. 매우 짧은 간격으로 이단 점프 실행(마우스 왼쪽 버튼을 빠르게 두번 클릭)
  - 2번의 경우 첫 번째 점프의 힘과 속력이 두 번째 점프의 힘과 속력에 그대로 합쳐진다. 따라서 2의 경우 두 번째 점프에 의한 상승 속도와 높이가 1의 경우에 비해 비약적으로 증가함
* playerRigidbody.velocity.y > 0 : 최고점에서 속도는 제로에 가깝다.
  - 이 시점에서 점프 속도가 아닌 낙하 속도를 절반으로 줄이는 문제가 발생할 수 있다.
  - 마우스 왼쪽 버튼을 너무 오래 누르고 있다가 캐릭터가 최고 높이에 도달한 후 낙하하기 시작한 시점에 손을 떼었다고 가정.
  - y 방향 속도 값이 0 이하일 때 속도를 절반으로 줄이면 상승 속도가 아니라 낙하 속도가 절반 줄어듬 그래서 해당 조건 추가
* OnCollisionEnter2D : 2D콜라이더를 사용하는 경우 OnTriggerEnter()의 2D버전인 OnCollisionEnter2D 메서드를 사용해야함.
* Collision 타입에서 충돌 지점의 정보를 담는 contacts라는 변수[https://www.notion.so/Collision-contacts-0feab03419f94d868bc88aed43c46fa9]
* Awake() : Start() 메서드처럼 초기 1회 자동 실행되는 유니티 이벤트 메서드지만, Start() 메서드보다 실행시점이 한 프레임 더 빠름
* OnEnable() : Awake()나 Start() 같은 유니티 이벤트 메서드. Start() 메서드처럼 컴포넌트가 활성화될 때 자동으로 한 번 실행됨. 하지만 처음 한 번만 실행되는 Start() 메서드와 달리 해당 메서드는 컴포넌트가 활성화 될 때마다 매번 다시 실행됨. => 컴포넌트를 끄고 다시 켜는 방식으로 재실행가능
  - 게임 오브젝트가 활성화될 때마다 상태를 리셋하는 기능을 구현할 때 주로 이용된다.
    - 해당 메서드에 초기화 코드를 넣어두고, 게임 오브젝트의 정보를 리셋해야 할 때마다 게임 오브젝트를 끄고 다시 켜는 방식으로 활용

### 기타

- 프리팹 갱신하기
  1. 하이어라키 창에서 수정된 프리팹 게임 오브젝트 선택
  2. 인스펙터 창에서 Overrides > Apply All 클릭

</div>
</details>
