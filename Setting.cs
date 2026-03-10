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
    [SettingsUIGroupOrder(kGlobalGroup, kAgricultureGroup, kMiningGroup, kForestryGroup)]
    [SettingsUIShowGroupName(kGlobalGroup, kAgricultureGroup, kMiningGroup, kForestryGroup)]
    public class Setting : ModSetting
    {
        public const string kSection = "Main";
        public const string kGlobalGroup = "Global";
        public const string kAgricultureGroup = "Agriculture";
        public const string kMiningGroup = "Mining";
        public const string kForestryGroup = "Forestry";

        public Setting(IMod mod) : base(mod) { }

        // Global
        [SettingsUISlider(min = 1, max = 10, step = 1, scalarMultiplier = 1, unit = Unit.kInteger)]
        [SettingsUISection(kSection, kGlobalGroup)]
        public int GlobalMultiplier { get; set; } = 1;

        [SettingsUISection(kSection, kGlobalGroup)]
        public bool ResetToDefault
        {
            set
            {
                SetDefaults();
                ApplyAndSave();
            }
        }

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

        public override void SetDefaults()
        {
            GlobalMultiplier = 1;
            GrainMultiplier = 1;
            VegetableMultiplier = 1;
            CottonMultiplier = 1;
            LivestockMultiplier = 1;
            OilMultiplier = 1;
            OreMultiplier = 1;
            CoalMultiplier = 1;
            StoneMultiplier = 1;
            WoodMultiplier = 1;
        }

        public int GetResourceMultiplier(Resource resource)
        {
            int specific = 1;
            if ((resource & Resource.Grain) != 0) specific = GrainMultiplier;
            else if ((resource & Resource.Vegetables) != 0) specific = VegetableMultiplier;
            else if ((resource & Resource.Cotton) != 0) specific = CottonMultiplier;
            else if ((resource & Resource.Livestock) != 0) specific = LivestockMultiplier;
            else if ((resource & Resource.Oil) != 0) specific = OilMultiplier;
            else if ((resource & Resource.Ore) != 0) specific = OreMultiplier;
            else if ((resource & Resource.Coal) != 0) specific = CoalMultiplier;
            else if ((resource & Resource.Stone) != 0) specific = StoneMultiplier;
            else if ((resource & Resource.Wood) != 0) specific = WoodMultiplier;
            return specific * GlobalMultiplier;
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

                { m_Setting.GetOptionGroupLocaleID(Setting.kGlobalGroup), "Global" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kAgricultureGroup), "Agriculture" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kMiningGroup), "Mining" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kForestryGroup), "Forestry" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.GlobalMultiplier)), "Global Multiplier" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.GlobalMultiplier)), "Multiplied on top of each resource's individual multiplier (e.g. 2x global × 3x grain = 6x grain output)" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.ResetToDefault)), "Reset to Default" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.ResetToDefault)), "Reset all multipliers back to 1x" },

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
            };
        }

        public void Unload()
        {
        }
    }
}
