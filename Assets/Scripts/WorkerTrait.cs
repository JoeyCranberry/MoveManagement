using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerTrait : MonoBehaviour
{
    public WorkerTraitType traitType;

    public double WorkerTraitObjectiveMultiplier(WorkerObjective obj, double curTick)
    {
        switch (traitType)
        {
            // Night owls do +20% work during the night
            case WorkerTraitType.NIGHT_OWL:
                if (Day.IsNightime(curTick))
                {
                    return 1.2d;
                }
                else
                {
                    return 1d;
                }
            // Morning peoiple do +20% work during the morning
            case WorkerTraitType.MORNING_PERSON:
                if (Day.IsMorning(curTick))
                {
                    return 1.2d;
                }
                else
                {
                    return 1d;
                }
            // Workers with a bad back cannot haul or construct
            case WorkerTraitType.BAD_BACK:
                if (obj.abilityRequirement == WorkerAbility.HAUL || obj.abilityRequirement == WorkerAbility.CONSTRUCT )
                {
                    return 0d;
                }
                else
                {
                    return 1d;
                }
            case WorkerTraitType.HEAVY_SLEEPER:
                if (Day.IsNightime(curTick))
                {
                    return .8d;
                }
                else
                {
                    return 1.2d;
                }
            default:
                return 1d;
        }
    }

    public enum WorkerTraitType
    { 
        NIGHT_OWL,
        MORNING_PERSON,
        BAD_BACK,
        HEAVY_SLEEPER
    }
}
