using UnityEngine;

namespace RPG {
    public class MonsterChaseState : MonsterStateBase {
        protected override void Update() {
            base.Update();

            if (Vector3.Distance(_refTransform.position, _manager.PlayerTransform.position) <= _data.attackRange) {
                _manager.SetState(MonsterStateManager.EState.Attack);
            }

            Vector3 dircetion = _manager.PlayerTransform.position - _refTransform.position;
            dircetion.y = 0f;

            if (dircetion != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(dircetion);
                _refTransform.rotation = Quaternion.RotateTowards(
                    _refTransform.rotation,
                    targetRotation,
                    _data.chaseRotateSpeed * Time.deltaTime
                );
            }
        }
    }
}