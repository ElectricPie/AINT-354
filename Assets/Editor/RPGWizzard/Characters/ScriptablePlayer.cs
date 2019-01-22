using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player", menuName = "RPGWizzard/Characters/New Player")]
public class ScriptablePlayer : ScriptableCharacter {
    public int maxLevel;

    public AnimationCurve experanceCurve;
}
