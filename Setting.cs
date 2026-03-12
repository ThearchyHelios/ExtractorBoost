using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI;
using Game.Economy;
using System.Collections.Generic;

namespace ExtractorBoost
{
    [FileLocation(nameof(ExtractorBoost))]
    [SettingsUIGroupOrder(kAgricultureGroup, kMiningGroup, kForestryGroup, kFishingGroup)]
    [SettingsUIShowGroupName(kAgricultureGroup, kMiningGroup, kForestryGroup, kFishingGroup)]
    public class Setting : ModSetting
    {
        public const string kSection = "Main";
        public const string kAgricultureGroup = "Agriculture";
        public const string kMiningGroup = "Mining";
        public const string kForestryGroup = "Forestry";
        public const string kFishingGroup = "Fishing";

        public Setting(IMod mod) : base(mod) { }

        // Agriculture
        [SettingsUISlider(min = 1, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kAgricultureGroup)]
        public int GrainMultiplier { get; set; } = 1;

        [SettingsUISlider(min = 1, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kAgricultureGroup)]
        public int VegetableMultiplier { get; set; } = 1;

        [SettingsUISlider(min = 1, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kAgricultureGroup)]
        public int CottonMultiplier { get; set; } = 1;

        [SettingsUISlider(min = 1, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kAgricultureGroup)]
        public int LivestockMultiplier { get; set; } = 1;

        // Mining
        [SettingsUISlider(min = 1, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kMiningGroup)]
        public int OilMultiplier { get; set; } = 1;

        [SettingsUISlider(min = 1, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kMiningGroup)]
        public int OreMultiplier { get; set; } = 1;

        [SettingsUISlider(min = 1, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kMiningGroup)]
        public int CoalMultiplier { get; set; } = 1;

        [SettingsUISlider(min = 1, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kMiningGroup)]
        public int StoneMultiplier { get; set; } = 1;

        // Forestry
        [SettingsUISlider(min = 1, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kForestryGroup)]
        public int WoodMultiplier { get; set; } = 1;

        // Fishing
        [SettingsUISlider(min = 1, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kFishingGroup)]
        public int FishMultiplier { get; set; } = 1;

        [SettingsUISection(kSection, kFishingGroup)]
        public bool ResetToDefault
        {
            set
            {
                SetDefaults();
                ApplyAndSave();
            }
        }

        public override void SetDefaults()
        {
            GrainMultiplier = 1;
            VegetableMultiplier = 1;
            CottonMultiplier = 1;
            LivestockMultiplier = 1;
            OilMultiplier = 1;
            OreMultiplier = 1;
            CoalMultiplier = 1;
            StoneMultiplier = 1;
            WoodMultiplier = 1;
            FishMultiplier = 1;
        }

        public int GetResourceMultiplier(Resource resource)
        {
            if ((resource & Resource.Grain) != 0) return GrainMultiplier;
            if ((resource & Resource.Vegetables) != 0) return VegetableMultiplier;
            if ((resource & Resource.Cotton) != 0) return CottonMultiplier;
            if ((resource & Resource.Livestock) != 0) return LivestockMultiplier;
            if ((resource & Resource.Oil) != 0) return OilMultiplier;
            if ((resource & Resource.Ore) != 0) return OreMultiplier;
            if ((resource & Resource.Coal) != 0) return CoalMultiplier;
            if ((resource & Resource.Stone) != 0) return StoneMultiplier;
            if ((resource & Resource.Wood) != 0) return WoodMultiplier;
            if ((resource & Resource.Fish) != 0) return FishMultiplier;
            return 1;
        }
    }

    public class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleEN(Setting setting)
        {
            m_Setting = setting;
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "Extractor Boost" },
                { m_Setting.GetOptionTabLocaleID(Setting.kSection), "Main" },

                { m_Setting.GetOptionGroupLocaleID(Setting.kAgricultureGroup), "Agriculture" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kMiningGroup), "Mining" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kForestryGroup), "Forestry" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kFishingGroup), "Fishing" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.GrainMultiplier)), "Grain" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.GrainMultiplier)), "Production multiplier for grain extractors" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.VegetableMultiplier)), "Vegetables" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.VegetableMultiplier)), "Production multiplier for vegetable extractors" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CottonMultiplier)), "Cotton" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CottonMultiplier)), "Production multiplier for cotton extractors" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LivestockMultiplier)), "Livestock" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LivestockMultiplier)), "Production multiplier for livestock extractors" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OilMultiplier)), "Oil" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OilMultiplier)), "Production multiplier for oil extractors" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OreMultiplier)), "Ore" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OreMultiplier)), "Production multiplier for ore extractors" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CoalMultiplier)), "Coal" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CoalMultiplier)), "Production multiplier for coal extractors" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StoneMultiplier)), "Stone" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StoneMultiplier)), "Production multiplier for stone extractors" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.WoodMultiplier)), "Wood" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.WoodMultiplier)), "Production multiplier for wood extractors" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FishMultiplier)), "Fish" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FishMultiplier)), "Production multiplier for fishing extractors" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToDefault)), "Reset to Default" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToDefault)), "Reset all multipliers back to 1x" },
            };
        }

        public void Unload()
        {
        }
    }

    public class LocaleZH : IDictionarySource
    {
        private readonly Setting m_Setting;

        public LocaleZH(Setting setting)
        {
            m_Setting = setting;
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), "采集倍率增强" },
                { m_Setting.GetOptionTabLocaleID(Setting.kSection), "主设置" },

                { m_Setting.GetOptionGroupLocaleID(Setting.kAgricultureGroup), "农业" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kMiningGroup), "采矿" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kForestryGroup), "林业" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kFishingGroup), "渔业" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.GrainMultiplier)), "谷物" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.GrainMultiplier)), "谷物采集建筑的产量倍率" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.VegetableMultiplier)), "蔬菜" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.VegetableMultiplier)), "蔬菜采集建筑的产量倍率" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CottonMultiplier)), "棉花" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CottonMultiplier)), "棉花采集建筑的产量倍率" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LivestockMultiplier)), "畜牧" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LivestockMultiplier)), "畜牧采集建筑的产量倍率" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OilMultiplier)), "石油" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OilMultiplier)), "石油采集建筑的产量倍率" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OreMultiplier)), "矿石" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OreMultiplier)), "矿石采集建筑的产量倍率" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CoalMultiplier)), "煤炭" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CoalMultiplier)), "煤炭采集建筑的产量倍率" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.StoneMultiplier)), "石材" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.StoneMultiplier)), "石材采集建筑的产量倍率" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.WoodMultiplier)), "木材" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.WoodMultiplier)), "木材采集建筑的产量倍率" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.FishMultiplier)), "鱼类" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.FishMultiplier)), "渔业采集建筑的产量倍率" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToDefault)), "恢复默认设置" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToDefault)), "将所有倍率重置为1倍" },
            };
        }

        public void Unload()
        {
        }
    }
}
