using System;
using PoeHUD.Poe.RemoteMemoryObjects;

namespace PoeHUD.Models
{
    public sealed class AreaInstance
    {
        public int RealLevel { get; }
        public int NominalLevel { get; }
        public string Name { get; }
        public int Act { get; }
        public bool IsTown { get; }
        public bool IsHideout { get; }
        public bool HasWaypoint { get; }
        public int Hash { get; }

        public DateTime TimeEntered = DateTime.Now;

        public AreaInstance(AreaTemplate area, int hash, int realLevel)
        {
            Hash = hash;
            RealLevel = realLevel;
            NominalLevel = area.NominalLevel;
            Name = area.Name;
            Act = area.Act;
            IsTown = area.IsTown;
            HasWaypoint = area.HasWaypoint;
            IsHideout = Name.Contains("Hideout");
        }

        public override string ToString()
        {
            return $"{Name} ({RealLevel}) #{Hash}";
        }
        public string DisplayName => String.Concat(Name, " (", RealLevel, ")");
        public static string GetTimeString(TimeSpan timeSpent)
        {
            int allsec = (int)timeSpent.TotalSeconds;
            int secs = allsec % 60;
            int mins = allsec / 60;
            int hours = mins / 60;
            mins = mins % 60;
            return String.Format(hours > 0 ? "{0}:{1:00}:{2:00}" : "{1}:{2:00}", hours, mins, secs);
        }
    }
}