using UnityEngine;

namespace TAG.Mobile
{
    public sealed class MobilePlatformConfigurator : MonoBehaviour
    {
        [SerializeField] private bool enable120FpsIfAvailable = true;
        [SerializeField] private bool enableHaptics = true;

        private void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = enable120FpsIfAvailable && Screen.currentResolution.refreshRateRatio.value >= 119 ? 120 : 60;
        }

        public void LightImpact()
        {
#if UNITY_ANDROID || UNITY_IOS
            if (enableHaptics) Handheld.Vibrate();
#endif
        }
    }
}
