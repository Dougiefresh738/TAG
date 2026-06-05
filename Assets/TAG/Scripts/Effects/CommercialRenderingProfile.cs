using UnityEngine;

namespace TAG.Effects
{
    [CreateAssetMenu(menuName = "TAG/Effects/Commercial Rendering Profile")]
    public sealed class CommercialRenderingProfile : ScriptableObject
    {
        public bool dynamicLighting = true;
        public bool softShadows = true;
        public bool cameraShake = true;
        public bool motionBlur = true;
        public bool depthOfField = true;
        public bool mapParticles = true;
        public bool highQualityShaders = true;
        public int targetFps = 60;
        public int premiumFps = 120;
        public Color rooftopGrade = new(1f, 0.68f, 0.42f);
        public Color jungleGrade = new(0.48f, 1f, 0.42f);
        public Color mineGrade = new(0.45f, 0.75f, 1f);
    }
}
