using UnityEngine;

namespace TAG.Content
{
    [CreateAssetMenu(menuName = "TAG/Content/Procedural Material Palette")]
    public sealed class ProceduralMaterialPalette : ScriptableObject
    {
        public Material rooftopConcrete;
        public Material rooftopGarden;
        public Material neon;
        public Material jungleGround;
        public Material jungleLeaf;
        public Material templeStone;
        public Material mineRock;
        public Material crystal;
        public Material lava;
        public Material creaturePrimary;
        public Material creatureAccent;
        public Material enemy;
    }
}
