using System;

namespace Fusion.Core.Types
{
    public class CharacterSkillInTraining : Skill
    {
        public long SkillInTraining;
        public int TrainingDestinationSkillPoints;
        public DateTime TrainingEndTime;
        public int TrainingStartSkillPoints;
        public DateTime TrainingStartTime;
        public int TrainingToLevel;
        public DateTime TrainingTypeId;
    }
}