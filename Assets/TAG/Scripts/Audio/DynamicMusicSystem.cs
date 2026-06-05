using System.Collections.Generic;
using UnityEngine;

namespace TAG.Audio
{
    public sealed class DynamicMusicSystem : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> layers = new();
        [SerializeField, Range(0f, 1f)] private float intensity;
        [SerializeField] private float fadeSpeed = 2f;

        public void SetIntensity(float value)
        {
            intensity = Mathf.Clamp01(value);
        }

        private void Update()
        {
            for (var i = 0; i < layers.Count; i++)
            {
                var target = intensity >= i / Mathf.Max(1f, layers.Count - 1f) ? 1f : 0f;
                layers[i].volume = Mathf.MoveTowards(layers[i].volume, target, fadeSpeed * Time.deltaTime);
            }
        }
    }
}
