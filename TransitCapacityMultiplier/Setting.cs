using Colossal;
using Colossal.IO.AssetDatabase;
using Game.Modding;
using Game.Settings;
using Game.UI;
using Game.UI.Widgets;
using System.Collections.Generic;
using Unity.Entities;

namespace TransitCapacityMultiplier
{
    [FileLocation(nameof(TransitCapacityMultiplier))]
    [SettingsUIGroupOrder(CapacityGroup, CapacityGroup)]
    [SettingsUIShowGroupName(CapacityGroup)]
    public class Setting : ModSetting
    {
        public const string CapacitySection = "CapacitySection";
        public const string ResetSection = "ResetSection";
        public const string CapacityGroup = "CapacityGroup";

        public Setting(IMod mod) : base(mod)
        {
            if (BusSlider == 0) SetDefaults();
        }

        public override void SetDefaults()
        {
            BusSlider = 1f;
            TaxiSlider = 1f;
            SubwaySlider = 1f;
            TramSlider = 1f;
            TrainSlider = 1f;
        }

        public override void Apply()
        {
            base.Apply();
            var system = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<TransitMultiplierSystem>();
            system.Enabled = true;
        }

        [SettingsUISlider(min = 0.1f, max = 5f, step = 0.1f, scalarMultiplier = 1, unit = Unit.kFloatSingleFraction)]
        [SettingsUISection(CapacitySection, CapacityGroup)]
        public float BusSlider { get; set; }

        [SettingsUISlider(min = 0.1f, max = 5f, step = 0.1f, scalarMultiplier = 1, unit = Unit.kFloatSingleFraction)]
        [SettingsUISection(CapacitySection, CapacityGroup)]
        public float TaxiSlider { get; set; }

        [SettingsUISlider(min = 0.1f, max = 5f, step = 0.1f, scalarMultiplier = 1, unit = Unit.kFloatSingleFraction)]
        [SettingsUISection(CapacitySection, CapacityGroup)]
        public float TramSlider { get; set; }

        [SettingsUISlider(min = 0.1f, max = 5f, step = 0.1f, scalarMultiplier = 1, unit = Unit.kFloatSingleFraction)]
        [SettingsUISection(CapacitySection, CapacityGroup)]
        public float TrainSlider { get; set; }

        [SettingsUISlider(min = 0.1f, max = 5f, step = 0.1f, scalarMultiplier = 1, unit = Unit.kFloatSingleFraction)]
        [SettingsUISection(CapacitySection, CapacityGroup)]
        public float SubwaySlider { get; set; }
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
                { m_Setting.GetSettingsLocaleID(), "Transit Capacity Multiplier" },
                { m_Setting.GetOptionTabLocaleID(Setting.CapacitySection), "Main" },
                { m_Setting.GetOptionGroupLocaleID(Setting.CapacityGroup), "Transit Vehicle Capacity" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BusSlider)), "Bus" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TaxiSlider)), "Taxi" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.SubwaySlider)), "Subway" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TramSlider)), "Tram" },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TrainSlider)), "Train" },

                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BusSlider)), "Set the vehicle capacity of bus vehicles relative to their vanilla capacity." },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TaxiSlider)), "Set the vehicle capacity of taxi vehicles relative to their vanilla capacity." },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.SubwaySlider)), "Set the vehicle capacity of subway vehicles relative to their vanilla capacity." },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TramSlider)), "Set the vehicle capacity of tram vehicles relative to their vanilla capacity." },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TrainSlider)), "Set the vehicle capacity of train vehicles relative to their vanilla capacity." },
            };
        }

        public void Unload()
        {

        }

    }
}
