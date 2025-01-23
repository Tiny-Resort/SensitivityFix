namespace SensitivityFix;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using BepInEx;
using UnityEngine.InputSystem;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class SensitivityFix : BaseUnityPlugin
{
    void Awake()
    {
        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!"); 
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
    }
}

[HarmonyPatch(typeof(GameNetcodeStuff.PlayerControllerB), "PlayerLookInput")]
public class PlayerLookInputPatch
{
    static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {

        MethodInfo GetModifierMethod = typeof(PlayerLookInputPatch).GetMethod(nameof(GetModifier), BindingFlags.Static | BindingFlags.NonPublic);
        var codes = new List<CodeInstruction>(instructions);

        // Replace the 0.008 modifier with own time-based value
        for (int i = 0; i < codes.Count; i++) 
            if (codes[i].opcode == OpCodes.Ldc_R4 && (float) codes[i].operand == 0.008f)
                codes[i] = new CodeInstruction(OpCodes.Call, GetModifierMethod);

        return codes;
    }

    // Calling this function makes it easy to adjust the value without a more complicated code instruction in the transpiler
    private static float GetModifier() {

        if (Keyboard.current.leftShiftKey.isPressed && Keyboard.current.leftAltKey.isPressed && Keyboard.current.leftCtrlKey.isPressed)
            return 0.008f;
        
        var deltaTime = Time.deltaTime;

        /*if (Keyboard.current.leftAltKey.isPressed) { 
            if (Random.Range(0.0f, 1.0f) < 0.02)
                Fibonacci(Random.RandomRangeInt(32, 36));
            else
                Fibonacci(31);
        }
        if (Mathf.Abs(deltaTime - 0.016f) >= 0.001f)
            Debug.Log("SENSITIVITY FIX: Delta Time is " + deltaTime);*/
        
        return Mathf.Max(deltaTime, 0.0083f) / 2f;
    }

    /*private static int Fibonacci(int n)
    {
        if (n <= 1) return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }*/

}
