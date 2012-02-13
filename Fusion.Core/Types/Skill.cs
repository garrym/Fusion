using System.Collections.Generic;

namespace Fusion.Core.Types
{
    public class Skill
    {
        public string Description;
        public long GroupId;
        public string Name;
        public Attribute PrimaryAttribute;
        public int Rank;
        public IList<Skill> RequiredSkills;
        public Attribute SecondaryAttribute;
        public long TypeId;
    }
}