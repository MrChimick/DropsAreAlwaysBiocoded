using HarmonyLib;
using RimWorld;
using Verse;

namespace DropsAreAlwaysBiocoded
{
    [StaticConstructorOnStartup]
    static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            new Harmony("DropsAreAlwaysBiocoded").PatchAll();
        }
    }

    [HarmonyPatch(typeof(PawnGenerator))]
    [HarmonyPatch("GeneratePawn", typeof(PawnGenerationRequest))]
    static class GeneratePawnPatch
    {
        static void Prefix(ref PawnGenerationRequest request)
        {
            if (request.Faction == null) return;
            
            if (request.Faction.HostileTo(Faction.OfPlayerSilentFail))
            {
                request.BiocodeApparelChance = 1f;
                request.BiocodeWeaponChance = 1f;
            }
            else if (request.Faction.IsPlayer)
            {
                request.BiocodeApparelChance = 0f;
                request.BiocodeWeaponChance = 0f;
            }
        }
    }
}