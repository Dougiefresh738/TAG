using TAG.Maps;
using UnityEngine;

namespace TAG.Content
{
    public static class ProceduralMapBuilder
    {
        public static GameObject Build(MapDefinition map, ProceduralMaterialPalette palette)
        {
            var id = map != null ? map.mapId : "rooftop";
            var root = new GameObject($"Procedural {id} Map");
            if (id.Contains("jungle")) BuildJungle(root.transform, palette, id.Contains("temple"));
            else if (id.Contains("mine")) BuildMine(root.transform, palette, id.Contains("crystal"));
            else BuildRooftop(root.transform, palette, id.Contains("night"));
            return root;
        }

        private static void BuildRooftop(Transform root, ProceduralMaterialPalette p, bool night)
        {
            AddCube(root, "Main Rooftop", new Vector3(0f, -0.05f, 0f), new Vector3(42f, 0.1f, 42f), p.rooftopConcrete);
            AddCube(root, "Rooftop Garden", new Vector3(-10f, 0.05f, 8f), new Vector3(7f, 0.2f, 6f), p.rooftopGarden);
            AddCylinder(root, "Water Tower", new Vector3(12f, 2.2f, 12f), new Vector3(1.8f, 2.2f, 1.8f), p.rooftopConcrete);
            AddCube(root, "AC Unit Cluster", new Vector3(5f, 0.55f, -11f), new Vector3(6f, 1.1f, 3f), p.rooftopConcrete);
            AddCube(root, "Billboard", new Vector3(-16f, 4f, -13f), new Vector3(7f, 3f, 0.35f), p.neon);
            AddCube(root, "Rooftop Bridge", new Vector3(0f, 1.2f, 20f), new Vector3(12f, 0.45f, 3f), p.rooftopConcrete);
            AddCube(root, "Construction Zone", new Vector3(14f, 0.6f, -5f), new Vector3(5f, 1.2f, 7f), p.rooftopGarden);
            AddCube(root, "Helipad", new Vector3(-13f, 0.04f, -9f), new Vector3(8f, 0.12f, 8f), p.neon);
            for (var i = 0; i < 16; i++) AddCube(root, "Skyline Tower", new Vector3(-38f + i * 5f, -1f, 32f + (i % 4) * 2f), new Vector3(3f, 8f + i % 5, 3f), p.rooftopConcrete);
            AddLight(root, night ? "Neon Moon Key" : "Sunset Key", new Vector3(-18f, 18f, -12f), night ? new Color(0.45f, 0.7f, 1f) : new Color(1f, 0.55f, 0.25f), 2.2f, LightType.Directional);
        }

        private static void BuildJungle(Transform root, ProceduralMaterialPalette p, bool temple)
        {
            AddCube(root, "Jungle Ground", new Vector3(0f, -0.08f, 0f), new Vector3(46f, 0.16f, 46f), p.jungleGround);
            for (var i = 0; i < 22; i++)
            {
                var x = Mathf.Sin(i * 2.17f) * 19f;
                var z = Mathf.Cos(i * 1.41f) * 19f;
                AddCylinder(root, "Huge Tree Trunk", new Vector3(x, 2.5f, z), new Vector3(0.8f, 2.5f, 0.8f), p.templeStone);
                AddSphere(root, "Dense Canopy", new Vector3(x, 5.4f, z), new Vector3(3.3f, 2.2f, 3.3f), p.jungleLeaf);
            }
            AddCube(root, temple ? "Ancient Temple" : "Ruins", new Vector3(0f, 1.6f, 12f), new Vector3(10f, 3.2f, 7f), p.templeStone);
            AddCube(root, "Hidden Shortcut Tunnel", new Vector3(-12f, 0.7f, -8f), new Vector3(7f, 1.4f, 3f), p.templeStone);
            AddCube(root, "Waterfall Sheet", new Vector3(17f, 2f, 0f), new Vector3(0.35f, 4f, 10f), p.neon);
            AddLight(root, "Leaf Shaft Sun", new Vector3(0f, 18f, -8f), new Color(0.8f, 1f, 0.55f), 1.8f, LightType.Directional);
        }

        private static void BuildMine(Transform root, ProceduralMaterialPalette p, bool crystal)
        {
            AddCube(root, "Mine Floor", new Vector3(0f, -0.08f, 0f), new Vector3(44f, 0.16f, 44f), p.mineRock);
            for (var i = -3; i <= 3; i++) AddCube(root, "Rail Tie", new Vector3(i * 4f, 0.08f, -3f), new Vector3(2.5f, 0.15f, 0.4f), p.rooftopConcrete);
            AddCube(root, "Mine Rails", new Vector3(0f, 0.18f, -2.2f), new Vector3(30f, 0.12f, 0.18f), p.neon);
            AddCube(root, "Mine Rails", new Vector3(0f, 0.18f, -3.8f), new Vector3(30f, 0.12f, 0.18f), p.neon);
            AddCube(root, "Mine Cart", new Vector3(-9f, 0.65f, -3f), new Vector3(2.6f, 1.3f, 2f), p.rooftopConcrete);
            AddCube(root, "Lava Section", new Vector3(11f, 0.02f, 11f), new Vector3(9f, 0.1f, 7f), p.lava);
            AddCylinder(root, "Elevator Shaft", new Vector3(-15f, 1.7f, 12f), new Vector3(2.5f, 1.7f, 2.5f), p.templeStone);
            for (var i = 0; i < 18; i++) AddCrystal(root, new Vector3(Mathf.Sin(i) * 18f, 1f, Mathf.Cos(i * 1.7f) * 18f), p.crystal, crystal ? 1.7f : 1f);
            AddLight(root, "Crystal Cavern Glow", new Vector3(0f, 6f, 0f), new Color(0.45f, 0.8f, 1f), 3f, LightType.Point);
        }

        private static GameObject AddCube(Transform root, string name, Vector3 pos, Vector3 scale, Material mat) => AddPrimitive(root, PrimitiveType.Cube, name, pos, scale, mat);
        private static GameObject AddSphere(Transform root, string name, Vector3 pos, Vector3 scale, Material mat) => AddPrimitive(root, PrimitiveType.Sphere, name, pos, scale, mat);
        private static GameObject AddCylinder(Transform root, string name, Vector3 pos, Vector3 scale, Material mat) => AddPrimitive(root, PrimitiveType.Cylinder, name, pos, scale, mat);

        private static GameObject AddPrimitive(Transform root, PrimitiveType type, string name, Vector3 pos, Vector3 scale, Material mat)
        {
            var go = GameObject.CreatePrimitive(type);
            go.name = name;
            go.transform.SetParent(root, false);
            go.transform.localPosition = pos;
            go.transform.localScale = scale;
            if (mat != null) go.GetComponent<Renderer>().sharedMaterial = mat;
            return go;
        }

        private static void AddCrystal(Transform root, Vector3 pos, Material mat, float boost)
        {
            var crystal = AddPrimitive(root, PrimitiveType.Cylinder, "Crystal Cluster", pos, new Vector3(0.45f * boost, 1.2f * boost, 0.45f * boost), mat);
            crystal.transform.localRotation = Quaternion.Euler(0f, Random.Range(0, 180f), 12f);
        }

        private static void AddLight(Transform root, string name, Vector3 pos, Color color, float intensity, LightType type)
        {
            var go = new GameObject(name);
            go.transform.SetParent(root, false);
            go.transform.localPosition = pos;
            var light = go.AddComponent<Light>();
            light.type = type;
            light.color = color;
            light.intensity = intensity;
            light.shadows = LightShadows.Soft;
        }
    }
}
