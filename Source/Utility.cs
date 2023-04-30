using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace ImpassableMapMaker
{
    public static class Utility
    {
        public static bool IsQuestArea(this GenStepDef genStep)
        {
            if (genStep == null) return false;
            return genStep.linkWithSite != null;
            //return genStep.linkWithSite == SitePartDefOf.AncientAltar
            //    || genStep.linkWithSite == SitePartDefOf.AncientComplex
            //    || genStep.linkWithSite == SitePartDefOf.AncientComplex_Mechanitor
            //    || genStep.linkWithSite == SitePartDefOf.Archonexus
            //    || genStep.linkWithSite == SitePartDefOf.BanditCamp
            //    || genStep.linkWithSite == SitePartDefOf.Manhunters
            //    || genStep.linkWithSite == SitePartDefOf.Outpost
            //    || genStep.linkWithSite == SitePartDefOf.PossibleUnknownThreatMarker
            //    || genStep.linkWithSite == SitePartDefOf.PreciousLump
            //    || genStep.linkWithSite == SitePartDefOf.SleepingMechanoids
            //    || genStep.linkWithSite == SitePartDefOf.Turrets
            //    || genStep.linkWithSite == SitePartDefOf.WorshippedTerminal;
        }
    }
}
