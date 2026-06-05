using TAG.Characters;
using UnityEngine;

namespace TAG.Content
{
    public static class ProceduralCreatureFactory
    {
        public static GameObject CreateCreature(CreatureDefinition definition, ProceduralMaterialPalette palette, bool player)
        {
            var root = new GameObject(definition != null ? definition.displayName : "TAG Creature");
            root.tag = player ? "Player" : "Untagged";
            var controller = root.AddComponent<CharacterController>();
            controller.radius = 0.45f;
            controller.height = 1.65f;
            controller.center = new Vector3(0f, 0.82f, 0f);

            if (player)
            {
                var mover = root.AddComponent<CreatureController>();
                AssignDefinition(mover, definition);
            }

            AddBody(root.transform, palette, definition, player);
            AddFace(root.transform, palette);
            AddEarsOrTail(root.transform, definition, palette);
            return root;
        }

        private static void AssignDefinition(CreatureController mover, CreatureDefinition definition)
        {
            var field = typeof(CreatureController).GetField("definition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field?.SetValue(mover, definition);
        }

        private static void AddBody(Transform parent, ProceduralMaterialPalette palette, CreatureDefinition definition, bool player)
        {
            var body = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            body.name = "Squishy Body";
            body.transform.SetParent(parent, false);
            body.transform.localScale = new Vector3(0.82f, 0.95f, 0.82f);
            body.transform.localPosition = new Vector3(0f, 0.82f, 0f);
            body.GetComponent<Renderer>().sharedMaterial = player ? palette.creaturePrimary : palette.enemy;

            var belly = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            belly.name = "Soft Belly Accent";
            belly.transform.SetParent(parent, false);
            belly.transform.localScale = new Vector3(0.58f, 0.42f, 0.18f);
            belly.transform.localPosition = new Vector3(0f, 0.72f, 0.42f);
            belly.GetComponent<Renderer>().sharedMaterial = palette.creatureAccent;
        }

        private static void AddFace(Transform parent, ProceduralMaterialPalette palette)
        {
            for (var i = -1; i <= 1; i += 2)
            {
                var eye = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                eye.name = i < 0 ? "Left Big Eye" : "Right Big Eye";
                eye.transform.SetParent(parent, false);
                eye.transform.localScale = Vector3.one * 0.13f;
                eye.transform.localPosition = new Vector3(i * 0.22f, 1.13f, 0.44f);
                eye.GetComponent<Renderer>().sharedMaterial = palette.neon;
            }
        }

        private static void AddEarsOrTail(Transform parent, CreatureDefinition definition, ProceduralMaterialPalette palette)
        {
            var id = definition != null ? definition.creatureId : string.Empty;
            if (id.Contains("fox") || id.Contains("monkey"))
            {
                var tail = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                tail.name = "Expressive Tail";
                tail.transform.SetParent(parent, false);
                tail.transform.localScale = new Vector3(0.22f, 0.7f, 0.22f);
                tail.transform.localPosition = new Vector3(0f, 0.72f, -0.58f);
                tail.transform.localRotation = Quaternion.Euler(62f, 0f, 0f);
                tail.GetComponent<Renderer>().sharedMaterial = palette.creatureAccent;
            }
            else
            {
                for (var i = -1; i <= 1; i += 2)
                {
                    var ear = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    ear.name = i < 0 ? "Left Leaf Ear" : "Right Leaf Ear";
                    ear.transform.SetParent(parent, false);
                    ear.transform.localScale = new Vector3(0.16f, 0.42f, 0.16f);
                    ear.transform.localPosition = new Vector3(i * 0.34f, 1.55f, 0f);
                    ear.transform.localRotation = Quaternion.Euler(0f, 0f, i * -28f);
                    ear.GetComponent<Renderer>().sharedMaterial = palette.jungleLeaf;
                }
            }
        }
    }
}
