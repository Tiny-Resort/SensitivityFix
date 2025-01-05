namespace SensitivityFix;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;
using BepInEx;

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
        return Time.deltaTime / 2f;
    }

}
