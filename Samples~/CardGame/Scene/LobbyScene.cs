using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework.Sample
{
    /// <summary>
    /// 로비 씬입니다.
    /// Scene을 상속해 OnSceneStart/OnSceneEnd 라이프사이클을 구현합니다.
    ///
    /// SceneManager가 씬 로드 완료 후 루트 오브젝트에서 IScene을 탐색해
    /// 자동으로 OnSceneStart()를 호출합니다.
    /// </summary>
    public class LobbyScene : Scene
    {
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private Button _battleButton;
        [SerializeField] private Text _timeText;
        [SerializeField] private Text _attackSumText;

        private TimeManager _timeManager;
        private PlayerManager _playerManager;

        public override async UniTask OnSceneStart()
        {
            _timeManager = Core.Get<TimeManager>();
            _playerManager = Core.Get<PlayerManager>();

            // 인벤토리 컨테이너 등록 (씬 진입 시점에 세팅)
            SetupInventory();

            // 1초마다 현재 시간 UI 갱신
            _timeManager.OnEvent1Sec += UpdateTimeUI;

            _inventoryButton.onClick.AddListener(OpenInventory);
            _battleButton.onClick.AddListener(GoToBattle);

            UpdateTimeUI();
            UpdateAttackSum();

            await UniTask.CompletedTask;
        }

        public override async UniTask OnSceneEnd()
        {
            _timeManager.OnEvent1Sec -= UpdateTimeUI;
            _inventoryButton.onClick.RemoveAllListeners();
            _battleButton.onClick.RemoveAllListeners();

            await UniTask.CompletedTask;
        }

        private void SetupInventory()
        {
            var inventory = new InventoryContainer();
            inventory.AddItem(new ItemData { Id = 1, Name = "파이어볼", AttackPower = 30, Cost = 3, Rarity = ItemRarity.Rare });
            inventory.AddItem(new ItemData { Id = 2, Name = "아이스 랜스", AttackPower = 20, Cost = 2, Rarity = ItemRarity.Common });
            inventory.AddItem(new ItemData { Id = 3, Name = "썬더 스트라이크", AttackPower = 60, Cost = 5, Rarity = ItemRarity.Legendary });

            _playerManager.AddContainer(inventory);

            // 아이템 추가 이벤트 발행
            using (var e = InventoryChangedEvent.Get())
            {
                e.ChangedItemId = 1;
                e.IsAdded = true;
            }
        }

        private void OpenInventory()
        {
            var inventory = _playerManager.GetContainer<InventoryContainer>();

            // 데이터를 넘겨서 팝업 오픈
            PopupManager.Instance.GetPopup<InventoryPopup>(inventory);
        }

        private void GoToBattle()
        {
            Core.Get<SceneManager>().LoadSceneAsync("BattleScene").Forget();
        }

        private void UpdateTimeUI()
        {
            _timeText.text = _timeManager.Now.ToString("HH:mm:ss");
        }

        private void UpdateAttackSum()
        {
            var total = _playerManager.GetContainer<InventoryContainer>().TotalAttackPower();
            _attackSumText.text = $"총 공격력: {total}";
        }
    }
}
