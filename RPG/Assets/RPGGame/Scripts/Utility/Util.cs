using UnityEngine;
using UnityEngine.AI;

namespace RPG {
    public class Util {
        public static void Log(object message) {
#if UNITY_EDITOR
            Debug.Log($"{message}");
#endif
        }

        public static void LogRed(object message) {
#if UNITY_EDITOR
            Debug.Log($"<color=red>{message}</color>");
#endif
        }

        public static void LogGreen(object message) {
#if UNITY_EDITOR
            Debug.Log($"<color=green>{message}</color>");
#endif
        }

        public static void LogBlue(object message) {
#if UNITY_EDITOR
            Debug.Log($"<color=blue>{message}</color>");
#endif
        }

        public static bool IsArrived(Transform selfTransform, Vector3 destination, float offset = 0.1f) {
            return Vector3.Distance(selfTransform.position, destination) < offset;
        }

        public static bool RandomPoint(Vector3 center, float range, out Vector3 result) {
            for (int ix = 0; ix < 30; ++ix) {
                Vector3 randomPoint = center + Random.insideUnitSphere * range;

                if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas)) {
                    result = hit.position;
                    return true;
                }
            }

            result = Vector3.zero;
            return false;
        }
    }
}