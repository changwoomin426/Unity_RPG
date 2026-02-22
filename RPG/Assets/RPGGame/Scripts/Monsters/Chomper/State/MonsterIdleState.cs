using UnityEngine;

namespace RPG {
    public class MonsterIdleState : MonsterStateBase {
        private float _elapsedTime = 0f;

        protected override void OnEnable() {
            base.OnEnable();
            _elapsedTime = 0f;
        }

        protected override void Update() {
            base.Update();
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > _data.patrolWaitTime) {
                _elapsedTime = 0f;
                _manager.SetState(MonsterStateManager.EState.Patrol);
            }
        }

        public void PlayStep() {

        }

        public void Grunt() {

        }
    }
}