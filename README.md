# Portfolio_TGB
## 게임명: The Greatest BlackSmith
>장르 : 2D 플랫포머 어드밴처 로그라이크  
>플랫폼 : PC   
>제작의도 : Skul:the hero slayer에서 영감을 받아 제작한 플랫포머 로그라이크, 아이템 효과에 중점을 두고 개발   

<br /><br />

## 플로우차트

```mermaid
 flowchart TB

게임준비씬;
게임준비씬-.->|추후 연출 구간|타이틀씬;
타이틀씬-->세이브{임시 세이브 파일};

세이브-->|Y|새게임[새 게임];
세이브-->|N|이어하기;

이어하기 -.->세이브씬;

새게임-->튜토리얼{튜토리얼 진행 여부};
튜토리얼-->|N, 다음부턴 튜토리얼 스킵, 영구 세이브파일|연출씬-->튜토리얼씬;
튜토리얼-->|Y|시작마을;

시작마을-->Stage1;
튜토리얼씬-->Stage1;
Stage1-->Stage2-->Stage3-->중간마을-->Stage4-->Stage5-->|보스 연출|Stage보스;
Stage1-.->|인게임 중간 종료|세이브씬;
Stage보스-.->클리어{보스 클리어 여부};
클리어-->|Y|보스클리어;
클리어-->|N|사망시;
subgraph 마을
direction LR
상인([상인]);
대장간([대장간]);
축복([축복]);
end

subgraph 일시정지
direction TB
돌아가기([돌아가기]);
키설정([키설정]);
옵션([옵션]);
나가기([종료]);
end

subgraph 사망시
direction TB
사망연출-.->결과창;
end

subgraph 보스클리어
direction TB
보스클리어연출[보스 클리어 연출]-->크레딧씬;
end
보스클리어-->|임시세이브파일삭제|타이틀씬;
사망시-->|임시세이브파일삭제|타이틀씬;
```
## 타이틀씬
![TGB_Title](https://github.com/scom-01/TGB/assets/78716085/abb2be2b-864b-466b-a28f-8392af5d1a6a)
>새게임, 이어하기(세이브파일), 해금 아이템 리스트, 옵션, 나가기    
<br /><br />

## 마을    
![TGB_Village](https://github.com/scom-01/TGB/assets/78716085/1002ef15-af7f-4386-9be0-8045d7e0f496)
>NPC: 상인, 대장간, 축복   
>상인: 임시 재화를 사용, 해금되어있는 아이템 중 아이템 등급에 따라 차등된 가격으로 판매, 재입고 가능, 재입고 시 재입고 비용 증가   
>대장간: 임시 재화를 사용, 무기를 변경할 수 있음.   
>축복: 영구 재화를 사용, 영구적인 스탯 증가.

<br /><br />

## 스테이지
![TGB_Stage1_1](https://github.com/scom-01/TGB/assets/78716085/f6d11084-c0d1-44ce-8f60-70162445fbe4)
>좌측 상단: 플레이타임, 남은 적 수   
>좌측 하단: 체력, 스킬1, 스킬2
>중앙 하단: 버프, 디버프 표시
>우측 하단: 임시 재화, 영구 재화, 미니맵

<br /><br />

* * *
## 적용 기술

### 오브젝트 풀링   
오브젝트를 생성하고 파괴하는 작업은 메모리를 할당하고 파괴 시 가비지컬렉팅으로 인한 프레임 저하가 일어 날 수 있기에 오브젝트 풀링 기법으로 가비지 컬렉팅을 최소화하여 성능을 향상시켰습니다.   


### 스프라이트 아틀라스 에셋 사용   
드로우콜을 줄이기 위해 단일 텍스쳐를 호출함으로 써 하나의 드로우콜로 큰 성능 소모없이 패킹된 텍스쳐를 동시에 액세스 할 수 있음.   

### FSM   
유닛(플레이어, 적)들에 상태 제어를 FSM으로 함으로써 어떤 상태를 동작하는지 명확하게 파악 할 수 있고 구현 및 제어가 쉬운 장점이 있어 사용했습니다.   

### 컴포넌트 패턴   
유닛의 각 기능들을 컴포넌트화 하여 느슨한 구조로 추가 및 삭제가 용이하고 재사용할 수 있어 코드의 길이을 줄일 수 있었습니다.   

### 샌드박스 패턴
아이템 효과를 구현하기 위해 특정 상태에 따라 호출되는 함수가 상이하며 상위 클래스가 하위 클래스가 필요로 하는 기능을 제공할 수 있고, 하위 클래스들간 겹치는 행동이 많아 샌드박스 패턴을 사용하였습니다.
>중복 제공: 아이템 이펙트, 사운드, 실행   
>아이템 이펙트 호출: 아이템 효과과 발동 시에 호출 
>아이템 사운드 호출: 아이템 효과가 발동 시에 호출
>실행: 각 아이템 효과에 맞게 호출됨. 호출할 상태는 Enum으로 관리
><br /><br />
>
>![TGB_RewardItem](https://github.com/scom-01/TGB/assets/78716085/3bf6365c-dd10-4f0b-9e0b-f2414be30cfe)
>>예시:OnDamaged(피해입을 시 호출), OnMoveScene(씬[스테이지] 이동 시 호출), OnAction(공격 시도 시), OnHit(공격 히트 시), OnDodge(공격 회피 시)... 등등   
>>각 아이템 효과는 발동 확률 및 발동에 필요한 횟수, 쿨타임 등을 설정하여 다양한 아이템을 만들어 낼 수 있습니다.

* * *

### 레벨 디자인   
![TGB_LevelDesign](https://github.com/scom-01/TGB/assets/78716085/09761c36-9889-4835-9642-d4baa74fdac1)
>타일맵을 사용하여 맵 디자인   
>Rule Tile, Animation Tile 등을 사용하여 관리에 용이하도록 함.   
>2D 플랫포머에 맞게 대쉬, 이단 점프, 벽 점프 등을 활용하여 이동하도록 레벨 디자인    
>숨겨진 루트에서 숨겨진 아이템을 찾거나 빠르게 이동할 수 있음    


