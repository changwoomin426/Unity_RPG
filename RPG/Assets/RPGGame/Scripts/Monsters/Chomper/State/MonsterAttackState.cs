using UnityEngine;
using UnityEngine.Events;

namespace RPG {
    public class MonsterAttackState : MonsterStateBase {
        [SerializeField] private UnityEvent _onAttackBegin;
        [SerializeField] private UnityEvent _onAttackEnd;

        protected override void Update() {
            base.Update();
            Vector3 direction = _manager.PlayerTransform.position - _refTransform.position;
            direction.y = 0f;
            _refTransform.rotation = Quaternion.LookRotation(direction);

            if (Vector3.Distance(_refTransform.position, _manager.PlayerTransform.position) > _data.attackRange) {
                _manager.SetState(MonsterStateManager.EState.Chase);
            }
        }

        public void AttackBegin() {
            _onAttackBegin?.Invoke();
        }

        public void AttackEnd() {
            _onAttackEnd?.Invoke();
        }
    }
}