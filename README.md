# UnityPrac

레트로의 유니티 게임 프로그래밍 에센스 책 프로젝트를 진행하면서 배운 것들 정리

_내용이 긴 것들은 notion에 정리하고 링크 첨부_

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
* Quaternion.identity : 오일러각의 (0, 0, 0) 회전에 대응

### 기타

- 프리팹 갱신하기
  1. 하이어라키 창에서 수정된 프리팹 게임 오브젝트 선택
  2. 인스펙터 창에서 Overrides > Apply All 클릭

* 오디오 클립을 하이어라키 창으로 drag & drop 하면 해당 오디오 클립을 사용하는 오디오 소스 컴포넌트가 추가된 게임 오브젝트가 자동 생성됨.
</div>
</details>

<details open>
<summary> <h1> 좀비 서바이버 프로젝트 </h1> </summary>
<div>

### 개념적

- 매시 콜라이더 컴포넌트를 사용하면 3D 모델의 외형과 일치하는 콜라이더를 만들 수 있다. 하지만 메시 콜라이더는 복잡한 형태 때문에 처리량을 크게 증가시키므로 중요한 몇 가지 게임 오브젝트에만 선택적으로 사용하는 것이 좋다.

* 라이팅 연산 비용은 비싸다. 유니티는 라이팅 데이터 에셋을 사용하여 라이팅 효과의 실시간 연산량을 줄이며, 씬에 변화가 감지될 때마다 매번 새로운 라이팅 데이터 에셋을 생성한다.

* 라이트맵[https://www.notion.so/1b735fee56b84a78adcbd49aefbed30c] : 오브젝트가 빛을 받았을 때 어떻게 보일지 **미리 그려둔(미리 계산해서 생성해둔) 텍스처**
* 글로벌 일루미네이션[https://www.notion.so/GI-9b4be2ab78144647a904f675ba419348] : 물체의 표면에 직접 들어오는 빛뿐만 아니라 다른 물체의 표면에서 반사되어 들어온 **간접광까지 표현**, 줄여서 **GI**라고 부름
  - _정적 게임 오브젝트에만 적용됨._
* <span style="background-color: yellow; color: black;">정적 게임 오브젝트 : Static이 체크된 게임 오브젝트, 이는 게임 도중에 위치가 변경될 수 없다.</span> 대신 유니티가 상대적으로 더 많은 성능 최적화를 적용함.

- Lighting > Realtime Lighting > Indirect Resolution : 텍스처 해상도를 유닛당 텍셀 조절
  - _텍셀은 텍스처의 화소이다. 화면의 1화소가 1픽셀이라면 텍스처의 1화소는 1텍셀이다._

* 3D 모델 에셋(FBX 파일)으로부터 생성된 게임 오브젝트는 애니메이터 컴포넌트를 가짐. 또한 3D 모델의 계층 구조나 본(Bone) 구조가 유지되어 함께 추가됨.
* **Angular Drag(각 항력) : 회전에 대한 마찰력**, 이 값을 높이면 물체가 잘 회전하지 않거나 회전해도 금방 멈추게 됨.
* Animator > Apply Root Motion : **게임 오브젝트의 위치와 회전을 애니메이션이 제어하도록 허용**
  - 예를 들어, 움직이는 캐릭터 애니메이션을 만들었다고 하는 경우, 루트 모션을 사용하면 걷는 애니메이션을 재생하는 동안 게임 오브젝트의 실제 위치가 이전보다 앞쪽으로 변경됨. 적용하지 않으면 제자리에서 걷는 애니메이션이 재생된다.
  * 루트 모션 적용을 사용하면 스크립트로 움직임을 제어하기 힘듬
* 유한 상태 머신을 병렬로 실행하는 방식으로 여러 상태가 동시에 현재 상태로 중첩되게 할 수 있다.
  - 같은 원리로 애니메이터 레이어를 여러개 사용함으로써 여러 애니메이션 상태가 게임 오브젝트 하나에 중첩되게 할 수 있다. => <span style="background-color: yellow; color: black;">애니메이터 컨트롤러에 레이어를 두 개 이상 만들면 각 레이어에서 재생하는 애니메이션은 위에서 아래 순서로 덮어쓰기 방식으로 적용됨.</span>
* 블렌드 트리 : 애니메이터의 상태에는 애니메이션 클립이 할당되는 데 평범한 애니메이션 클립이 아닌 특수한 종류의 모션(움직임을 나타내는 에셋)을 상태에 할당하는 것도 가능함. 그중 하나가 **애니메이션 클립을 혼합하는 블렌드 트리모션**이다.
  - 블렌드 트리를 가진 상태는 애니메이터 창 > 마우스 우클릭(상태도 있는 쪽) > Create State > From New Blend Tree 클릭 으로 만들 수 있다.
  * 자세한 설명은 https://www.notion.so/0c53a802528443538eb41ec3be164b11 블렌드 트리 섹션에 정리함.
* 아바타 마스크[https://www.notion.so/60a8ede5dcdf4599bcb127595847f276] : 애니메이터의 레이어별로 부위를 다르게 적용하려면 아바타 마스크를 설정해야 한다.
* 시네머신[https://www.notion.so/87810bb9af4e4393891e5692fdd5bc36] : 카메라의 움직임을 손쉽게 제어하는 유니티 공식 패키지
* 라인 렌더러 : 주어진 점들을 이은 _선을 그리는 컴포넌트_
  - position 필드에 설정 된 점 사이를 이어 선을 그림
* 파티글 시스템 컴포넌트 : 여러 작은 스프라이트 이미지를 랜덤하게 휘날리는 방식으로 동작
  - 유니티에서 연기 화염재 등의 시각 효과는 파티클 시스템 컴포넌트를 사용함.
* 코루틴[https://www.notion.so/d340c9ece62f44d98228637e12f97ae5] : 대기 시간을 가질 수 있는 메서드
  - 총을 발사하는 효과가 필요한 경우, 번쩍이는 탄알 궤적을 구현하려면 라인 렌더러를 켜서 선을 그린 다음 라인 렌더러를 다시 꺼야한다. 이때 매우 짧은 시간 동안 처리를 일시 정지함.
  - 따라서 라인 렌더러를 끄고 켜는 처리 사이에 대기 시간이 필요함. 이때 코루틴이 사용된다.
* 레이캐스트[https://www.notion.so/3cdeb97c2faf4702bbcfdf7097e9a82d] : 보이지 않는 광선을 쐈을 때 광선이 다른 콜라이더와 충돌하는지 검사하는 처리
* IK[https://www.notion.so/IK-84ead81c25794f5b9eaa8288ed2ec538] : 어떤 애니메이션을 사용하든 상관없이 캐릭터의 손의 위치가 항상 총의 손잡이에 위치하려면 애니메이터의 IK를 사용해야 함.
* UGUI의 캔버스 : 게임화면을 기준으로 UI를 배치함.
  - 렌더 모드를 전역 공간으로 변경하면 캔버스와 그 위의 UI 게임 오브젝트들은 3D 게임 월드에 배치되며, 캔버스 게임 오브젝트는 일반적인 게임 오브젝트처럼 게임 월드 상의 위치, 회전, 크기를 가지게 됨.
  * https://www.notion.so/UI-UGUI-d3065c41b3604167ac3952e137e76459에 UGUI 섹션에 정리해 둠
* UI > Slider[https://www.notion.so/Slider-2b07fd1705df4de299fe04d2f1fcba42]
* 내비게이션 시스템[https://www.notion.so/c486647c191f428fa24d59a8bafe0619]: 유니티는 한 위치에서 다른 위치로의 경로를 계산하고 실시간으로 장애물을 피하며 이동하는 인공지능을 만드는 내비게이션 시스템을 제공한다.
* 인공지능 추가하기 : Add Component > Navigation > Nav Mesh Agent
  - https://www.notion.so/c486647c191f428fa24d59a8bafe0619 인공지능 추가하기 섹션
* **레이어 마스크** : 특정 레이어를 가진 게임 오브젝트에 물리 또는 그래픽 처리 등을 적용시킬 때 사용함.
* 이벤트[https://www.notion.so/e5b2fd4a1142420f8558cc87cf48b5ab] : 연쇄적인 처리를 발생시키는 사건
* 포스트 프로세싱[https://www.notion.so/2dd6b392981344079dac8f560c69ddac]:후처리, 게임 화면이 최종 출력되기 전에 카메라의 이미지 버퍼에 삽입하는 추가 처리
* 매치메이킹 서버 : 리슨서버나 P2P 방식을 상용하더라도 참가할 클라이언트들이 서로를 찾아 방 하나에 모이는 과정에서 사용할 전용 서버가 필요함. 이러한 전용 서버를 매치메이킹 서버라고 부름.
* 포톤 룸 : 게임을 플레이하기 위해서는 네트워크를 통해 여러 클라이언트가 하나의 세션에 모여야 한다. 포톤은 이렇게 여러 클라이언트가 모인 네트워크상의 가상의 공간을 룸이라는 단위로 부름.
  - 포톤의 룸은 유니티의 씬이 아니다. 유니티의 씬과 다른 계층에서 동작함. 따라서 플레이어들이 같은 룸에 있지만 서로 다른 씬을 로드하는 것도 가능함.
  * 즉, 하나의 룸에 플레이어들이 모인 상태는 여러 사람이 각자 무전기를 든 채 같은 주파수를 공유하고있는 것으로 이해하면 된다. 주파수를 공유한 사람들끼리 정보를 공유하고 같은 장소로 이동할 수 있다.
  * 하지만, 주파수를 공유한다는 사실(같은 룸에 입장) 자체와 사람들이 위치한 물리적인 장소(씬) 사이에는 연관성이 없다.
* RPC[https://www.notion.so/RPC-afc46f44bb4140c385a4a398a4a835df] : 어떤 메서드나 처리를 네트워크를 넘어 다른 클라이언트에서 실행하는 것.
  - 대부분 멀티플레이어 API에는 RPC가 구현되어 있다.

### 스크립트

- FixedUpdate() : Update()처럼 유니티 이벤트 메서드로서 주기적으로 자동 실행된다. 화면 갱신 주기에 맞춰 실행되는 Update()와 달리 FixedUpdate()는 물리 정보 갱신 주기(기본값 0.02초)에 맞춰 실행됨.
  - 이동과 회전을 Update() 메서드에서 실행해도 되지만, 물리 주기에 맞춰 실행되는 이 메서드에서 실행할 경우 오차가 날 확률이 상대적으로 줄어듬.

* Time.fixedDeltaTime : 물리 정보의 갱신 주기, 즉 FixeUpdate() 메서드의 실행 간격을 표시한다.
  - 유니티는 개발자의 편의를 위해 FixedUpdate()내부에서 Time.deltaTime 값에 접근할 경우 자동으로 Time.fixedDeltaTime의 값을 출력한다.
* 리지드바디의 MovePosition() 메서드 : 이동할 Vector3 위치를 입력 받음. **MovePosition() 메서드는 상대 위치가 아닌 전역 위치를 사용한다.**

  - 해당 메서드에 (0, 0, 3)을 입력하면 현재 위치에서 (0, 0, 3)만큼 상대적으로 이동한 위치가 아니라 전역 좌표 (0, 0, 3)으로 이동함.

  * 리지드바디 컴포넌트를 사용하지 않고 트랜스폼 컴포넌트를 사용하여 같은 방식의 이동을 구현할 수도 있다.
    - 그렇지만 이렇게 하게 되면, _트랜스폼의 위칫값을 직접 변경하면 물리 처리를 무시하고 위치를 덮어쓰기 때문. => 막힌 벽 등을 무시하고 벽 반대쪽으로 이동할 수 도 있음._
    - <span style="background-color: yellow; color: black;">리지드바디의 MovePosition() 메서드를 사용하면 이동 경로에 다른 콜라이더가 존재하는 경우 밀어내거나 밀려나는 물리처리가 실행된다.</span> => 벽 반대쪽으로 '순간이동'하는 사고를 방지할 수 있다.

  ```C#
    rigidbody.MovePosition(rigidbody.position + moveDistance);

    //transform.position = transform.position + moveDistance;
  ```

* 오디오 소스 컴포넌트 Play() : 이미 재생 중인 오디오가 있다면 정지하고 처음부터 오디오를 다시 재생
* 오디오 소스 컴포넌트 PlayOneShot() : 이미 재생 중인 소리가 있어도 정지하지 않고, 재생할 오디오와 이미 재생중인 오디오를 **중첩하여 재생**
* out : out 키워드로 입력된 변수는 메서드 내부에서 변경된 사항이 반영된 채 되돌아 온다.
  - 메서드가 return 이외의 방법으로 추가 정보를 반환할 수 있게 만든다.
  - out 키워드로 입력된 변수는 메서드 내부에서 변경된 사항이 반영된 채 되돌아오기 때문.
* Raycast[https://www.notion.so/3cdeb97c2faf4702bbcfdf7097e9a82d](Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float maxDistance) : 보이지 않는 광선을 쐈을 때 광선이 다른 콜라이더와 충돌하는지 검사하는 처리
  - origin : 레이의 시작점
  - direction : 레이의 방향
  - RaycastHit hitInfo : 레이가 충돌한 경우 hitInfo에 자세한 충돌 정보가 채워진다.
  - maxDistance : 레이 충돌을 검사할 최대 거리
  * Raycast() 메서드는 자신의 내부에서 hitInfo에 충돌 정보를 채운다.
  * 종료되었을 때 변경 사항이 유지된 채로 hitInfo가 돌아옴.
* Action 타입 : 입력과 출력이 없는 메서드를 가리킬 수 있는 델리게이트
  ```C#
    Action onClean;
    void Start() {
      onClean += CleaningRoomA;
    }
    void CleaningRoomA() {
      Debug.Log("A방 청소");
    }
  ```
* event 키워드 : 어떤 델리게이트 변수를 event로 선언하면 클래스 외부에서는 해당 델리게이트를 **실행할 수 없게 된다.**
* GetComponentInChildren<T>() : 자식 게임오브젝트에서 컴포넌트 가져올 때 사용하는 메서드
* Physics.OverlapSphere() : 중심 위치와 반지름을 입력받아 가상의 구를 그리고, 구에 겹치는 모든 콜라이더를 반환한다.
  - 아무 필터링 없이 이 메서드를 실행하면 성능 낭비가 되므로, 세 번째 값으로 레이어 마스크를 입력하면 특정 레이어만 감지할 수 있게 됨.
* Quaternion.LookRotation() : 방향벡터를 입력받아 해당 방향을 바라보는 쿼터니언 회전값을 반환한다.
* 파티클 효과를 재생하기 전에 파티클 효과의 위치와 회전을 다음 값으로 변경해야 함.

  - 위치 : 공격받은 지점(피격 위치)
  - 회전 : 공격이 날아온 방향을 바라보는 방향(피격 방향)

  ```C#
   public ParticleSystem hitEffect;

   hitEffect.transform.position = hitPoint;
   hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);
  ```

* 콜라이더 컴포넌트의 ClosestPoint() : 콜라이더 표면 위의 점 중 특정 위치와 가장 가까운 점을 반환함.
* Lerp() : 두 지점 사이의 중간값을 반환하는 메서드
  ```C#
    // intensity 가 0에 가까울 수록 Min 값에 가까워지며, 1에 가까울 수록 Max에 가까워 진다.
     float health = Mathf.Lerp(healthMin, healthMax, intensity);
     Color skinColor = Color.Lerp(Color.white, strongEnemyColor, intensity);
  ```
* Random.insideUnitSphere : 반지름이 1인 구 안에서의 랜덤한 한 점을 반환하는 프로퍼티
  - 반환된 벡터는 원점 (0, 0, 0)을 중심으로 함

### 기타

- 하이어라키 창에서 Canvas로 예를 들면 옆에 펼치기 버튼을 [Alt + 클릭] -> Canvas의 모든 자식 오브젝트가 한 번에 표시됨.
  </div>
  </details>
<details open>
<summary> <h1> 좀비 서바이버 멀티플레이어  </h1> </summary>
<div>

### 개념적

- 게임 서버 방식
  - 전용 서버 방식에서는 고정된 호스트 서버가 존재함
  - 리슨 서버 방식에서는 플레이어 클라이언트 중 하나를 호스트로 삼음
  - P2P 방식에서는 모든 플레이어 클라이언트가 호스트 역할을 겸함.

* 매치메이킹 서버 : 리슨서버나 P2P 방식을 상용하더라도 참가할 클라이언트들이 서로를 찾아 방 하나에 모이는 과정에서 사용할 전용 서버가 필요함. 이러한 전용 서버를 매치메이킹 서버라고 부름.
* 포톤 룸 : 게임을 플레이하기 위해서는 네트워크를 통해 여러 클라이언트가 하나의 세션에 모여야 한다. 포톤은 이렇게 여러 클라이언트가 모인 네트워크상의 가상의 공간을 룸이라는 단위로 부름.
  - 포톤의 룸은 유니티의 씬이 아니다. 유니티의 씬과 다른 계층에서 동작함. 따라서 플레이어들이 같은 룸에 있지만 서로 다른 씬을 로드하는 것도 가능함.
  * 즉, 하나의 룸에 플레이어들이 모인 상태는 여러 사람이 각자 무전기를 든 채 같은 주파수를 공유하고있는 것으로 이해하면 된다. 주파수를 공유한 사람들끼리 정보를 공유하고 같은 장소로 이동할 수 있다.
  * 하지만, 주파수를 공유한다는 사실(같은 룸에 입장) 자체와 사람들이 위치한 물리적인 장소(씬) 사이에는 연관성이 없다.
* RPC[https://www.notion.so/RPC-afc46f44bb4140c385a4a398a4a835df] : 어떤 메서드나 처리를 네트워크를 넘어 다른 클라이언트에서 실행하는 것.
  - 대부분 멀티플레이어 API에는 RPC가 구현되어 있다.

- PUN(Photon Unity Network)[https://www.notion.so/PUN-Photon-Unity-Network-65c7a939fe4645398ccae2e90ff355f6] : 유니티용으로 제작된 포톤 네트워크 엔진

* <span style="background-color: yellow; color: black;"> Photon View 컴포넌트[https://www.notion.so/Photon-View-dee8d7d3687b40a88704e2b9fc223f51] : 네트워크를 통해 동기화 될(네트워크상에서 식별이 가능하도록) 모든 게임 오브젝트는 Photon View 컴포넌트를 가져야 함.</span>
  - 부모 게임 오브젝트에 Photon View 컴포넌트가 이미 추가되어 있다고 해도 자식 게임 오브젝트에서 독자적으로 실행할 네트워크 처리가 있다면 자식 게임 오브젝트에도 Photon View 컴포넌트를 추가하여 View ID를 부여해야 함.
* Photon Transform View 컴포넌트[https://www.notion.so/Photon-Transform-View-5a30231bd98f4ee8993f62cb40a911f2] : 자신의 게임 오브젝트에 추가된 트랜스폼 컴포넌트 값의 변화를 측정하고, Photon View 컴포넌트를 사용해 동기화 함.
* Photon Animator View 컴포넌트[https://www.notion.so/Photon-Animator-View-af105c771e57413bb342356dc09aeae0] : 네트워크를 넘어 로컬 게임 오브젝트와 리모트 게임 오브젝트 사이에서 애니메이터 컴포넌트의 파라미터를 동기화하여 서로 같은 애니메이션을 재생하도록 한다.
* IPunObservable 인터페이스와 OnPhotonSerializeView()[https://www.notion.so/IPunObservable-OnPhotonSerializeView-2b5102296f7f40b792338fab6ef16674] : Photon View 컴포넌트를 사용해 동기화를 구현할 모든 컴포넌트(스크립트)는 IPunObservable 인터페이스를 상속하고 OnPhotonSerializeView() 메서드를 구현해야 함.
  - OnPhotonSerializeView() : Photon View 컴포넌트를 사용해 로컬과 리모트 사이에서 어떤 값을 어떻게 주고받을지 결정한다. 해당 메서드는 Photon View 컴포넌트에 의해 자동으로 실행됨.
  - **IPunObservable 인터페이스를 상속한 컴포넌트는 Photon View 컴포넌트의 Observed Components에 등록되어 로컬과 리모트에서 동기화될 수 있다.**

### 스크립트

- 아래에 정리된 PUN 관련 클래스, 메서드, 함수들에 관한 설명은 https://www.notion.so/PUN-Photon-Unity-Network-65c7a939fe4645398ccae2e90ff355f6 에 좀 더 구체적으로 정리해두었음

  - MonoBehaviourPunCallbacks : MonoBehaviour를 확장한 클래스로, Photon.Pun에서 제공한다.
    - MonoBehaviour 기능을 유지한 채 컴포넌트가 포톤 서비스에 의해 발생하는 콜백(이벤트나 메시지)도 감지할 수 있게 함.
  - MonoBehaviourPun : MonoBehaviourPun에서 photonView 프로퍼티만 추가하여 단순 확장한 클래스임.

  * photonView : 자신의 게임 오브젝트에 추가된 Photon View 컴포넌트에 즉시 접근할 수 있는 지름길 프로퍼티

  - photonView.IsMine : 로컬인지

  * PhotonNetwork.ConnectUsingSettings(): 설정한 정보로 마스터 서버에 접속 시도
  * PhotonNetwork.CreateRoom(string 룸의 이름, 룸 옵션) : 새로운 방 생성
    - PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 }) : 최대 4명을 수용 가능한 빈 방 생성
    * 생성된 룸은 리슨 서버 방식으로 동작하며 룸을 생성한 클라이언트가 호스트 역할을 맡게 됨
  * - OnJoinedRoom() : 룸 참가에 성공한 경우 자동 실행
      - PhotonNetwork.CreateRoom()을 사용해 **자신이 룸을 직접 생성하고 참가한 경우에도 해당 메서드가 실행됨.**
  * PhotonNetwork.LoadLevel() : 어떤 씬을 로드하고, 해당 씬의 구성이 플레이어 사이에 동기화되도록 유지함.

  - PhotonNetwork.IsMasterClient : 호스트인지

  * PhotonNetwork.LeaveRoom() : 현재 네트워크 룸을 나가는 메서드
  * OnLeftRoom() : 로컬 플레이어가 현재 게임 룸을 나갈 때 자동 실행됨.

* [PunRPC][https://www.notion.so/PunRPC-9b224b1408f743cea6a21e21396c0027] : RPC를 구현하는 속성. [PunRPC]로 선언된 메서드는 다른 클라이언트에서 원격 실행할 수 있다.
* Invoke(지연 실행할 메서드명, 지연시간) : 특정 메서드를 지연 실행하는 메서드
* RpcTarget.MasterClient : 호스트 클라이언트를 나타내는 값
* PhotonNetwork.Destroy()[https://www.notion.so/PhotonNetwork-Destroy-825a256eecff45e3bb10cc4b3b8a8851] : 네트워크상의 모든 클라이언트에서 매개변수로 넘어온 게임오브젝트를 동일하게 파괴한다.
  - 지연시간을 받지 못함.
* PhotonNewtwork.Instantiate()[https://www.notion.so/PhotonNewtwork-Instantiate-be3da08b850b429e98ab327778b299c9] : 자신의 게임 월드에서 어떤 게임 오브젝트를 생성하고, 같은 게임 오브젝트를 타인의 게임 월드에도 생성되게 한다.
  - 입력으로 Photon View 컴포넌트가 추가된 프리팹을 받아 해당 프리팹의 복제본을 모든 클라이언트에서 생성함.
  * 생성된 게임 오브젝트의 소유권은 PhotonNewtwork.Instantiate()를 직접 실행한 측에 있다.
  * **PhotonNetwork.Instantiate()를 사용해 생성한 프리팹들은 Resources라는 이름의 폴더에 있어야함.**

### 기타

</div>
</details>
