using Game;
using Game.Prefabs;
using System;
using System.Collections.Generic;
using TransitCapacityMultiplier;
using Unity.Collections;
using Unity.Entities;
using Colossal.Serialization.Entities;

namespace TransitCapacityMultiplier
{
    public partial class TransitMultiplierSystem : GameSystemBase
    {
        private Dictionary<Entity, PublicTransportVehicleData> _transportToData = new Dictionary<Entity, PublicTransportVehicleData>();

        private EntityQuery _query;

        protected override void OnCreate()
        {
            base.OnCreate();

            _query = GetEntityQuery(new EntityQueryDesc()
            {
                All = new[] {
                    ComponentType.ReadWrite<PublicTransportVehicleData>()
                }
            });

            RequireForUpdate(_query);
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            var transports = _query.ToEntityArray(Allocator.Temp);

            foreach (var trans in transports)
            {
                PublicTransportVehicleData data;

                if (!_transportToData.TryGetValue(trans, out data))
                {
                    data = EntityManager.GetComponentData<PublicTransportVehicleData>(trans);
                    _transportToData.Add(trans, data);
                }

                float factor = data.m_TransportType switch
                {
                    TransportType.Bus => Mod.m_Setting.BusSlider,
                    TransportType.Taxi => Mod.m_Setting.TaxiSlider,
                    TransportType.Subway => Mod.m_Setting.SubwaySlider,
                    TransportType.Tram => Mod.m_Setting.TramSlider,
                    TransportType.Train => Mod.m_Setting.TrainSlider,
                    TransportType.Airplane => Mod.m_Setting.AirplaneSlider,
                    TransportType.Ship => Mod.m_Setting.ShipSlider,
                    _ => 1f
                };

                Mod.log.Info($"Factor {factor},{Math.Floor(factor * data.m_PassengerCapacity)}");

                data.m_PassengerCapacity = Math.Max((int)(Math.Floor(factor * data.m_PassengerCapacity)), 1);
                EntityManager.SetComponentData<PublicTransportVehicleData>(trans, data);
            }

            Enabled = false;
        }

        protected override void OnUpdate()
        {
            
        }
    }
}