# UnityPrac

## 프로젝트를 진행하면서 배운 것들 정리

### Dodge 프로젝트 만들면서 배운 것들

#### 개념적

- Plane의 크기와 유닛 단위 : Plane의 크기는 가로세로 10유닛(Unit), 유니티에서 1유닛은 Cube 한 변의 길이, 즉, cube 가로길이의 10배

* 머티리얼(Material) : 셰이더와 텍스처가 합쳐진 에셋. 오브젝트의 픽셀 컬러를 결정

  - 셰이더 : 주어진 입력에 따라 픽셀의 최종 컬러를 결정하는 코드, 질감과 빛에 의한 반사와 굴절 등의 효과를 만들어 냄. => 물감
  - 텍스처 : 표면에 입히는 이미지 파일 => 스케치나 밑그림을 이해

* 알베도 : 반사율이라는 뜻, 물체가 어떤 색을 반사할지 결정 => 즉, 물체 표면의 기본색을 결정
* 트리거 콜라이더(Trigger Collider)[https://www.notion.so/Trigger-Collider-f05a6e6d6ada49ec8ea66207953a2815]
* 프리팹[https://www.notion.so/99ad603914194c838bfd39ec360131b9]

* <span style='background-color: #fcba03; color: black;'>리지드바디의 제약을 사용하면 힘이나 충돌 등 물리적인 상호작용으로 위치나 회전이 변경되는 것을 막을 수 있다. 그러나 트랜스폼 컴포넌트의 위치나 회전에 새로운 값을 할당하여 위치나 회전을 변경하는 것을 막을 수는 없다.</span>
* 충돌 이벤트 메서드[https://www.notion.so/661a5ab72b7b4ed7a93226bfb9c815c3]

#### 스크립트

- **스크립트 생성 후 유니티 에디터에서 스크립트 파일명 변경하면 스크립트 파일에 선언된 클래스명 자동으로 갱신안되니까 똑같이 수동으로 바꿔줘야 함. 스크립트 파일명이랑 클래스명이 같아야 올바르게 작동**

* MonoBehaviour 클래스를 상속받는 클래스들 유니티에서 컴포넌트로 사용 가능
* Update() : update 메서드는 한 프레임에 한 번, 매 프레임마다 반복 실행됨. => 60FPS이면 1초에 60번 실행됨.
* gameObject: gameObject 변수는 컴포넌트들의 기반 클래스인 MonoBehaviour에서 제공하는 GameObject 타입의 변수, 컴포넌트 입장에서 자신이 추가된 게임 오브젝트를 가리키는 변수
* GetComponent<>() : 자신의 게임 오브젝트에서 제네릭 부분에 입력한 타입의 컴포넌트를 찾아오는 메서드
* Input.GetAxios(string axisName) : 어떤 축에 대한 입력값을 숫자로 반환하는 메서드(https://www.notion.so/Input-GetAxios-0f3988aa25374898bd16e3a724b10ccc)

- transform : Transform 타입의 변수, 자신의 게임 오브젝트의 transform 컴포넌트로 바로 접근하는 변수.

#### 기타

- 오브젝트 복사 : Ctrl + D
