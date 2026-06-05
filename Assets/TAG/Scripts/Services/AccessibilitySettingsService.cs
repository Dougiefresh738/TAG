using UnityEngine;

namespace TAG.Services
{
    public sealed class AccessibilitySettingsService : MonoBehaviour
    {
        [SerializeField] private bool reduceCameraShake;
        [SerializeField] private bool reduceMotionBlur;
        [SerializeField] private bool highContrastUi;
        [SerializeField, Range(0.5f, 1.5f)] private float uiScale = 1f;
        [SerializeField, Range(0f, 1f)] private float hapticStrength = 0.75f;

        public bool ReduceCameraShake => reduceCameraShake;
        public bool ReduceMotionBlur => reduceMotionBlur;
        public bool HighContrastUi => highContrastUi;
        public float UiScale => uiScale;
        public float HapticStrength => hapticStrength;

        public void Apply(bool reduceShake, bool reduceBlur, bool highContrast, float scale, float haptics)
        {
            reduceCameraShake = reduceShake;
            reduceMotionBlur = reduceBlur;
            highContrastUi = highContrast;
            uiScale = Mathf.Clamp(scale, 0.5f, 1.5f);
            hapticStrength = Mathf.Clamp01(haptics);
            PlayerPrefs.SetInt("tag_reduce_shake", reduceCameraShake ? 1 : 0);
            PlayerPrefs.SetInt("tag_reduce_blur", reduceMotionBlur ? 1 : 0);
            PlayerPrefs.SetInt("tag_high_contrast", highContrastUi ? 1 : 0);
            PlayerPrefs.SetFloat("tag_ui_scale", uiScale);
            PlayerPrefs.SetFloat("tag_haptics", hapticStrength);
        }

        private void Awake()
        {
            reduceCameraShake = PlayerPrefs.GetInt("tag_reduce_shake", 0) == 1;
            reduceMotionBlur = PlayerPrefs.GetInt("tag_reduce_blur", 0) == 1;
            highContrastUi = PlayerPrefs.GetInt("tag_high_contrast", 0) == 1;
            uiScale = PlayerPrefs.GetFloat("tag_ui_scale", 1f);
            hapticStrength = PlayerPrefs.GetFloat("tag_haptics", 0.75f);
        }
    }
}
