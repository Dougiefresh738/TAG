using System.Collections.Generic;
using UnityEngine;

namespace TAG.AI
{
    public sealed class AIDirector : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private List<EnemyAgent> enemies = new();
        [SerializeField] private AnimationCurve aggressionOverTime = AnimationCurve.EaseInOut(0f, 1f, 300f, 2.5f);
        [SerializeField] private float flankRadius = 8f;
        private float matchTime;

        private void Update()
        {
            if (player == null) return;
            matchTime += Time.deltaTime;
            var aggression = aggressionOverTime.Evaluate(matchTime);
            for (var i = 0; i < enemies.Count; i++)
            {
                var angle = (360f / Mathf.Max(1, enemies.Count)) * i;
                var offset = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * flankRadius;
                enemies[i].Pursue(player, offset, aggression);
            }
        }
    }
}
