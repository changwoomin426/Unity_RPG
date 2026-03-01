using UnityEngine;

namespace RPG {
    public class GrenadierIdleState : GrenadierStateBase {
        protected override void Update() {
            base.Update();
            float distanceToPlayer = (_manager.PlayerTransform.position - _refTransform.position).sqrMagnitude;

            if (distanceToPlayer <= _manager.Data.sightRange * _manager.Data.sightRange &&
                distanceToPlayer > _manager.Data.rangeAttackRange * _manager.Data.rangeAttackRange) {
                _manager.SetState(GrenadierStateManager.EState.Rotate);
                return;
            }

            if (Util.IsInSight(_refTransform, _manager.PlayerTransform, _manager.Data.sightAngle, _manager.Data.rangeAttackRange)) {
                _manager.ChangeToAttack();
                return;
            }
        }
    }
}