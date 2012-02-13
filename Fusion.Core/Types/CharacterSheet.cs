using System;
using System.Collections.Generic;
using Fusion.Core.Enums;

namespace Fusion.Core.Types
{
    public class CharacterSheet : Character
    {
        public string Ancestry;
        public IList<CharacterAttribute> Attributes;
        public decimal Balance;
        public string BloodLine; // TODO: Convert to bloodline enum
        public string CloneName;
        public long CloneSkillPoints;
        public DateTime DateOfBirth;
        public Gender Gender;
        public DateTime LastUpdated;
        public Race Race;
        public CharacterSkillInTraining SkillInTraining;
        public IList<CharacterSkill> Skills;
        public DateTime UpdateAvailable;

        public CharacterSheet()
        {
            Attributes = new List<CharacterAttribute>();
            Skills = new List<CharacterSkill>();
        }
    }
}