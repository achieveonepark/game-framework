using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameFramework.Sample
{
    /// <summary>
    /// 배틀 씬입니다.
    /// 인벤토리에서 카드를 꺼내 CardDraggable로 스폰하고,
    /// CardSlot에 드래그해서 덱을 구성하는 흐름을 보여줍니다.
    /// </summary>
    public class BattleScene : Scene
    {
        [SerializeField] private Transform _handArea;
        [SerializeField] private CardDraggable _cardPrefab;
        [SerializeField] private Button _backButton;
        [SerializeField] private Text _statusText;

        private readonly List<CardDraggable> _spawnedCards = new List<CardDraggable>();

        public override async UniTask OnSceneStart()
        {
            _backButton.onClick.AddListener(GoToLobby);

            // 인벤토리에서 카드 스폰
            var inventory = Core.Get<PlayerManager>().GetContainer<InventoryContainer>();
            foreach (var kv in inventory.GetAll())
            {
                SpawnCard(kv.Value);
            }

            _statusText.text = "카드를 슬롯에 드래그하세요";

            // 인벤토리 변경 이벤트 수신
            InventoryChangedEvent.RegisterListener(OnInventoryChanged);

            await UniTask.CompletedTask;
        }

        public override async UniTask OnSceneEnd()
        {
            _backButton.onClick.RemoveAllListeners();
            InventoryChangedEvent.UnregisterListener(OnInventoryChanged);

            await UniTask.CompletedTask;
        }

        private void SpawnCard(ItemData data)
        {
            var card = Instantiate(_cardPrefab, _handArea);
            card.Setup(data);
            _spawnedCards.Add(card);
        }

        private void GoToLobby()
        {
            Core.Get<SceneManager>().LoadSceneAsync("LobbyScene").Forget();
        }

        private void OnInventoryChanged(InventoryChangedEvent e)
        {
            _statusText.text = e.IsAdded
                ? $"아이템 {e.ChangedItemId} 추가됨"
                : $"아이템 {e.ChangedItemId} 제거됨";
        }
    }
}
