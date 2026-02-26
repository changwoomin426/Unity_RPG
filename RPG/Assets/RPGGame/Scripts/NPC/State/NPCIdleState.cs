using UnityEngine;

namespace RPG {
    public class NPCIdleState : NPCStateBase {
        protected override void Update() {
            base.Update();

            if (CanTalk()) {
                _manager.SetState(NPCStateManager.EState.Talk);
            }
        }
    }
}