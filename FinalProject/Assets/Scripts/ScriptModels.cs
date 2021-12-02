using System;
using System.Collections.Generic;
using UnityEngine;

public static class ScriptModels
{
    #region - Player -

    [Serializable]
    public class PlayerSettingsModel
    {
        [Header("View Settings")]
        public float ViewXSensitivity;
        public float ViewYSensitivity;
        public float viewClampYMin = -80;
        public float viewClampYMax = 60;
        
        [Header("Movement -Walking")]
        public float MoveForwardSpeed;
        public float MoveBackSpeed;
        public float MoveStrafeSpeed;

        [Header("Jumping")]
        public float JumpHeight;
        public float JumpFalloff;
    }

    #endregion
}
