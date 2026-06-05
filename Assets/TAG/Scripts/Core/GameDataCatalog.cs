using System.Collections.Generic;
using TAG.Characters;
using TAG.Maps;
using TAG.Progression;
using UnityEngine;

namespace TAG.Core
{
    [CreateAssetMenu(menuName = "TAG/Core/Game Data Catalog")]
    public sealed class GameDataCatalog : ScriptableObject
    {
        public List<CreatureDefinition> creatures = new();
        public List<MapDefinition> maps = new();
        public List<DifficultyDefinition> difficulties = new();
    }
}
