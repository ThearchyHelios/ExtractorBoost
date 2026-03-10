using Game;
using Game.Economy;
using Game.Prefabs;
using Unity.Collections;
using Unity.Entities;
using System.Collections.Generic;

namespace ExtractorBoost.Systems
{
    public partial class ExtractorBoostSystem : GameSystemBase
    {
        private EntityQuery m_ProcessDataQuery;
        private Dictionary<Entity, int> m_OriginalOutputAmounts;
        private int m_LastSettingsHash;
        private bool m_Initialized;

        // All raw extractable resources
        private static readonly Resource kExtractableResources =
            Resource.Grain | Resource.Vegetables | Resource.Cotton | Resource.Livestock |
            Resource.Oil | Resource.Ore | Resource.Coal | Resource.Stone |
            Resource.Wood;

        protected override void OnCreate()
        {
            base.OnCreate();
            m_OriginalOutputAmounts = new Dictionary<Entity, int>();
            m_LastSettingsHash = 0;
            m_Initialized = false;

            // Query all entities with IndustrialProcessData (no ExtractorParameterData filter)
            m_ProcessDataQuery = GetEntityQuery(
                ComponentType.ReadWrite<IndustrialProcessData>()
            );

            RequireForUpdate(m_ProcessDataQuery);

            Mod.log.Info("ExtractorBoostSystem created");
        }

        protected override void OnUpdate()
        {
            var setting = Mod.m_Setting;
            if (setting == null)
                return;

            int currentHash = ComputeSettingsHash(setting);

            // Only run on first update or when settings change
            if (m_Initialized && currentHash == m_LastSettingsHash)
                return;

            m_LastSettingsHash = currentHash;

            var entities = m_ProcessDataQuery.ToEntityArray(Allocator.Temp);

            if (!m_Initialized)
            {
                Mod.log.Info($"ExtractorBoostSystem: Found {entities.Length} entities with IndustrialProcessData");
            }

            int updatedCount = 0;

            for (int i = 0; i < entities.Length; i++)
            {
                var entity = entities[i];
                var processData = EntityManager.GetComponentData<IndustrialProcessData>(entity);
                var outputResource = processData.m_Output.m_Resource;

                // Only modify extractors (entities that output raw extractable resources)
                if ((outputResource & kExtractableResources) == 0)
                    continue;

                if (!m_OriginalOutputAmounts.ContainsKey(entity))
                {
                    m_OriginalOutputAmounts[entity] = processData.m_Output.m_Amount;

                    if (!m_Initialized)
                    {
                        Mod.log.Info($"  Extractor prefab: entity={entity.Index}, resource={outputResource}, originalAmount={processData.m_Output.m_Amount}");
                    }
                }

                int original = m_OriginalOutputAmounts[entity];
                int multiplier = setting.GetResourceMultiplier(outputResource);
                int newAmount = original * multiplier;

                if (processData.m_Output.m_Amount != newAmount)
                {
                    processData.m_Output.m_Amount = newAmount;
                    EntityManager.SetComponentData(entity, processData);
                    updatedCount++;
                }
            }

            entities.Dispose();

            if (updatedCount > 0)
            {
                Mod.log.Info($"ExtractorBoostSystem: Updated {updatedCount} extractor prefabs (settings hash: {currentHash})");
            }

            m_Initialized = true;
        }

        private static int ComputeSettingsHash(Setting s)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + s.GlobalMultiplier;
                hash = hash * 31 + s.GrainMultiplier;
                hash = hash * 31 + s.VegetableMultiplier;
                hash = hash * 31 + s.CottonMultiplier;
                hash = hash * 31 + s.LivestockMultiplier;
                hash = hash * 31 + s.OilMultiplier;
                hash = hash * 31 + s.OreMultiplier;
                hash = hash * 31 + s.CoalMultiplier;
                hash = hash * 31 + s.StoneMultiplier;
                hash = hash * 31 + s.WoodMultiplier;
                return hash;
            }
        }
    }
}
