using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.GamePlay.Characters
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Scriptable Objects/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        public CharacterName Name;
        public Sprite Sprite;
        public Color NameColor;
        public List<CharacterSkillData> Skills;
    }
}
