﻿using Harmony;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Reflection;
using Verse;

namespace ImpassableMapMaker
{
    [StaticConstructorOnStartup]
    public class Main
    {
        static Main()
        {
            var harmony = HarmonyInstance.Create("com.impassablemapmaker.rimworld.mod");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            Log.Message("ImpassableMapMaker: Adding Harmony Postfix to GenStep_ElevationFertility.Generate(Map)");
        }

        [HarmonyPatch(typeof(GenStep_ElevationFertility), "Generate")]
        static class Patch_GenStep_Terrain
        {
            static void Postfix(Map map)
            {
                Random r = new Random(map.uniqueID);
                int basePatch = 50 + r.Next(150);
                MapGenFloatGrid elevation = MapGenerator.FloatGridNamed("Elevation", map);
                foreach (IntVec3 current in map.AllCells)
                {
                    //Random r = new Random(map.uniqueID);
                    if (map.TileInfo.hilliness == Hilliness.Impassable)
                    {
                        float f = 0;
                        if ((current.x > 6 &&
                            current.x < map.Size.x - 7 &&
                            current.z > 6 &&
                            current.z < map.Size.z - 7))
                        {
                            f = 3.40282347E+38f;
                        }
                        else if (r.Next(6) < 2)
                        {
                            f = 0.75f;
                        }
                        else
                        {
                            f = 0.6f;
                        }

                        int i = r.Next(10);
                        if (current.x > basePatch + i && current.x < basePatch + 50 + i &&
                            current.z > basePatch + i && current.z < basePatch + 50 + i)
                        {
                            f = 0;
                        }

                        elevation[current] = f;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(WorldPathGrid), "CalculatedCostAt")]
        static class Patch_CompLaunchable
        {
            static void Postfix(ref int __result)
            {
                if (__result == 1000000)
                    __result -= 1;
            }
        }

        [HarmonyPatch(typeof(Page_SelectLandingSite), "CanDoNext")]
        static class Patch_Page_SelectLandingSite
        {
            static bool Prefix(ref bool __result)
            {
                int selectedTile = Find.World.UI.SelectedTile;
                if (selectedTile >= 0)
                {
                    Tile tile = Find.WorldGrid[selectedTile];
                    Faction faction = Find.World.factionManager.FactionAtTile(selectedTile);
                    if (faction != null)
                    {
                        Messages.Message("BaseAlreadyThere".Translate(new object[]
                        {
                            faction.Name
                        }), MessageSound.RejectInput);
                        __result = false;
                        return false;
                    }

                    if (tile.hilliness == Hilliness.Impassable)
                    {
                        Find.GameInitData.startingTile = selectedTile;
                        __result = true;
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
