using UnityEngine;

namespace TAG.Effects
{
    public sealed class RuntimeVfxController : MonoBehaviour
    {
        [SerializeField] private CommercialRenderingProfile profile;
        [SerializeField] private ParticleSystem ambientParticles;
        [SerializeField] private Camera targetCamera;
        private float shakeTimer;
        private Vector3 cameraBasePosition;

        private void Awake()
        {
            targetCamera = targetCamera != null ? targetCamera : Camera.main;
            if (targetCamera != null) cameraBasePosition = targetCamera.transform.localPosition;
            if (profile != null) Application.targetFrameRate = Screen.currentResolution.refreshRateRatio.value >= 119 ? profile.premiumFps : profile.targetFps;
            if (ambientParticles != null && profile != null && profile.mapParticles) ambientParticles.Play();
        }

        public void Shake(float seconds, float strength)
        {
            if (profile != null && !profile.cameraShake) return;
            shakeTimer = Mathf.Max(shakeTimer, seconds * Mathf.Clamp01(strength));
        }

        private void LateUpdate()
        {
            if (targetCamera == null || shakeTimer <= 0f) return;
            shakeTimer -= Time.deltaTime;
            targetCamera.transform.localPosition = cameraBasePosition + Random.insideUnitSphere * (shakeTimer * 0.08f);
        }
    }
}
