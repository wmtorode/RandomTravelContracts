﻿using BattleTech;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace RandomTravelContracts {
    public class Helper {

        public static Settings LoadSettings() {
            try {
                using (StreamReader r = new StreamReader($"{RandomTravelContracts.ModDirectory}/settings.json")) {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<Settings>(json);
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return null;
            }
        }

        public static bool IsBorder(StarSystem system, SimGameState Sim) {
            try {
                bool result = false;
                if (Sim.Starmap != null ) {
                    foreach (StarSystem neigbourSystem in Sim.Starmap.GetAvailableNeighborSystem(system)) {
                        if (system.OwnerDef.ID != neigbourSystem.OwnerDef.ID) {
                            result = true;
                            break;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return false;
            }
        }

        public static bool IsWarBorder(StarSystem system, SimGameState Sim) {
            try {
                bool result = false;
                if (Sim.Starmap != null) {
                    if (system.OwnerValue.Name != FactionEnumeration.GetNoFactionValue().Name) {
                        foreach (StarSystem neigbourSystem in Sim.Starmap.GetAvailableNeighborSystem(system)) {
                            if (system.OwnerDef.Enemies.Contains(neigbourSystem.OwnerValue.Name) && neigbourSystem.OwnerValue.Name != FactionEnumeration.GetNoFactionValue().Name) {
                                result = true;
                                break;
                            }
                        }
                    }
                }
                return result;
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return false;
            }
        }

    }
}