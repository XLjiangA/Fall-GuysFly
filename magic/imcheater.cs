using FG.Common;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FGFly.magic
{
    public static class imcheater
    {
        static MethodInfo EliminateParticipant { get; set; }

        public static void Init()
        {
            EliminateParticipant = typeof(IGameStateServerActions).GetMethod("EliminateParticipant", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            var target = typeof(imcheater).GetMethod("HookTarget", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            HookMethod(EliminateParticipant, target);

        }
        private static void HookTarget(this IGameStateServerActions IGS, MPGNetObject playerNetObject, bool isCheater)
        {
            EliminateParticipant.Invoke(IGS, new object[] { playerNetObject, false });
        }
        private static void HookMethod(MethodInfo sourceMethod, MethodInfo destinationMethod)
        {
            RuntimeHelpers.PrepareMethod(sourceMethod.MethodHandle);
            RuntimeHelpers.PrepareMethod(destinationMethod.MethodHandle);
            unsafe
            {
                int* inj = (int*)destinationMethod.MethodHandle.Value.ToPointer() + 2;
                int* tar = (int*)sourceMethod.MethodHandle.Value.ToPointer() + 2;
                *tar = *inj;
            }
        }



    }
}
