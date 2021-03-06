using System.Collections.Generic;
using System.Linq;
using PoeHUD.Models;
using PoeHUD.Models.Enums;
using PoeHUD.Poe.RemoteMemoryObjects;

namespace PoeHUD.Poe.Components
{
    public class Mods : Component
    {
        public ItemRarity ItemRarity => Address != 0 ? (ItemRarity)M.ReadInt(Address + 0x68) : ItemRarity.Normal;
        public int ItemLevel => Address != 0 ? M.ReadInt(Address + 0x11C) : 1;
        public int RequiredLevel => Address != 0 ? M.ReadInt(Address + 0x120) : 1;
        public string UniqueName => Address != 0 ? M.ReadStringU(M.ReadInt(Address + 0x1C, 4, 4)) : string.Empty;
        public bool Identified => Address != 0 && M.ReadInt(Address + 0x64) == 1;
        public ItemStats ItemStats => new ItemStats(Owner);

        public List<ItemMod> ItemMods
        {
            get
            {
                var implicitMods = GetMods(0x6C, 0x70);
                var explicitMods = GetMods(0x7C, 0x80);
                return implicitMods.Concat(explicitMods).ToList();
            }
        }

        private List<ItemMod> GetMods(int startOffset, int endOffset)
        {
            var list = new List<ItemMod>();
            if (Address == 0)
                return list;

            int begin = M.ReadInt(Address + startOffset);
            int end = M.ReadInt(Address + endOffset);
            int count = (end - begin) / 24;
            if (count > 12)
                return list;

            for (int i = begin; i < end; i += 24)
                list.Add(GetObject<ItemMod>(i));

            return list;
        }
    }
}