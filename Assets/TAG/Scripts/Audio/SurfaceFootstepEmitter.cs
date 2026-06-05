using System.Collections.Generic;
using UnityEngine;

namespace TAG.Audio
{
    public sealed class SurfaceFootstepEmitter : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private List<SurfaceFootstepSet> surfaces = new();

        public void PlayFootstep(string surfaceId)
        {
            var set = surfaces.Find(s => s.surfaceId == surfaceId) ?? (surfaces.Count > 0 ? surfaces[0] : null);
            if (source == null || set == null || set.clips.Count == 0) return;
            source.PlayOneShot(set.clips[Random.Range(0, set.clips.Count)], set.volume);
        }
    }

    [System.Serializable]
    public sealed class SurfaceFootstepSet
    {
        public string surfaceId = "concrete";
        public List<AudioClip> clips = new();
        public float volume = 1f;
    }
}
