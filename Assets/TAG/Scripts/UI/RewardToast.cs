using TMPro;
using UnityEngine;

namespace TAG.UI
{
    public sealed class RewardToast : MonoBehaviour
    {
        [SerializeField] private TMP_Text label;
        [SerializeField] private CanvasGroup group;

        public void Show(string message)
        {
            if (label != null) label.text = message;
            if (group != null)
            {
                group.alpha = 1f;
                group.interactable = false;
                group.blocksRaycasts = false;
            }
        }
    }
}
