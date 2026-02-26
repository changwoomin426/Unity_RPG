using System.Collections.Generic;
using UnityEngine;

namespace RPG {
    [CreateAssetMenu]
    public class NPCData : ScriptableObject {
        [System.Serializable]
        public class Attribute {
            public int id = 0;
            public string name = string.Empty;
            public float interactionSight = 0f;
        }

        public List<Attribute> _attributes = new List<Attribute>();
    }
}