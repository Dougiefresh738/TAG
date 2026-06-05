using System.Collections.Generic;
using UnityEngine;

namespace TAG.Maps
{
    [CreateAssetMenu(menuName = "TAG/Maps/Map Definition")]
    public sealed class MapDefinition : ScriptableObject
    {
        public string mapId = "rooftop";
        public string displayName = "Rooftop";
        public int unlockLevel = 1;
        public Sprite previewImage;
        public GameObject sceneRootPrefab;
        public AudioClip ambientLoop;
        public Material skyboxMaterial;

        [Header("Fantasy Dressing")]
        public List<string> signatureProps = new();
        public List<string> ambienceEvents = new();
        public Color gradeColor = Color.white;
        [TextArea] public string artDirection;
    }
}
