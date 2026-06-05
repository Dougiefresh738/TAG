using System.Collections.Generic;
using UnityEngine;

namespace TAG.Services
{
    public sealed class AnalyticsService : MonoBehaviour
    {
        [SerializeField] private bool analyticsEnabled = true;
        private readonly Queue<string> localEventMirror = new();

        public void Track(string eventName, IDictionary<string, object> parameters = null)
        {
            if (!analyticsEnabled || string.IsNullOrWhiteSpace(eventName)) return;
            var payload = parameters == null ? eventName : eventName + " " + MiniJson(parameters);
            localEventMirror.Enqueue(payload);
            while (localEventMirror.Count > 50) localEventMirror.Dequeue();
            Debug.Log("TAG Analytics: " + payload);
        }

        private static string MiniJson(IDictionary<string, object> parameters)
        {
            var parts = new List<string>();
            foreach (var pair in parameters) parts.Add($"\"{pair.Key}\":\"{pair.Value}\"");
            return "{" + string.Join(",", parts) + "}";
        }
    }
}
