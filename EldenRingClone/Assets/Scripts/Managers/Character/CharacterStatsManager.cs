using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MR
{
    public class CharacterStatsManager : MonoBehaviour
    {
    public int CalculateStaminaBasedOnEnduranceLevel(int endurance)
    {
      float stamina = 0;

      // CREATE AN EQUATION FOR HOW YOU WANT YOUR STAMINA TO BE CALCULATED
      stamina = endurance * 10;

      return Mathf.RoundToInt(stamina);
    }
  }
}
