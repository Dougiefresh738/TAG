using UnityEngine;
using UnityEngine.SceneManagement;

namespace TAG.Runtime
{
    public sealed class BootstrapSceneLoader : MonoBehaviour
    {
        [SerializeField] private string firstScene = "MainMenu";

        private void Start()
        {
            if (!string.IsNullOrWhiteSpace(firstScene)) SceneManager.LoadScene(firstScene);
        }
    }
}
