using UnityEngine;
using System.Collections;

sealed public class PasrEasing 
{
    public static float GetValue(float value, float pauseTime, float attackTime, float sustainTime, float releaseTime)
    {
        float duration2 = attackTime + pauseTime;
        float duration3 = duration2 + sustainTime;
        float duration4 = duration3 + releaseTime;
        value %= duration4;

        if (value <= pauseTime)
        {
            return 0f; //hold at 0 during pause time
        }
        else if (value <= duration2)
        {
            //move up from 0f to 1f during the duration of attack time
            value -= pauseTime;

            return Mathf.Clamp01(value / attackTime);
        }
        else if (value <= duration3)
        {
            return 1f; //hold at 1 during sustain time
        }
        else if (value <= duration4)
        {
            //move down from 1f to 0f during the duration of release time
            value -= duration3;
            return Mathf.Clamp01(1f - (value / releaseTime));
        }
        else
        {
            //we're completed so return to 0f
            //this shouldn't really happen because we mod value by the total duration
            //but we might get here from float inaccuracy
            return 0f;
        }
    }
}
