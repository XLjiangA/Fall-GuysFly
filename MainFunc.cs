using FGFly.magic;
using FGFly.view;
using MelonLoader;
using UnityEngine;
namespace FGFly
{
    public class MainFunc : MelonMod
    {
        private FallGuysCharacterController fg { get; set; }

        public override void OnApplicationStart()
        {
            magicManager.instance.Hello();
        }
        public override void OnUpdate()
        {
            if (fg == null)
            {
                GameLog.Clear();
                foreach (var _fg in GameObject.FindObjectsOfType<FallGuysCharacterController>())
                {
                    if (_fg.IsLocalPlayer)
                    {
                        fg = _fg;
                        magicManager.instance.MagicInit(fg);
                        MelonLogger.Log("GetSelfPlayer....OK");
                        MelonLogger.Log($"Self Layer:{ LayerMask.LayerToName(fg.gameObject.layer)}");
                        MelonLogger.Log($"Player Layer:{ LayerMask.NameToLayer("Player")}");
                        MelonLogger.Log($"UI Layer:{ LayerMask.NameToLayer("UI")}");
                        MelonLogger.Log("eggtag:{0} ,balltag:{1} ,tailtag:{2} ", ScenesConsts.FALL_GUYEGG_GRAB, ScenesConsts.FALL_GUYFALL_BALL2, ScenesConsts.TAIL_TAG);
                    }
                }
            }
            else
            {
                magicManager.instance.MagicUpdate();
                GameLog.listen();
            }
        }
        public override void OnFixedUpdate()
        {
            if (fg == null) return;
            magicManager.instance.MagicFixedUpdate();
        }
        public override void OnGUI()
        {
            magicManager.instance.MagicGUI();
            GUI.skin.label.fontSize = 20;
            GUI.skin.label.fontStyle = FontStyle.Bold;
            var msgs = GameLog.getLogs();
            for (int i = 0; i < msgs.Length; i++)
            {

                GUI.skin.label.normal.textColor = msgs[i].Contains("开启") ? Color.yellow : Color.red;
                if (i > 1)
                {
                    GUI.skin.label.normal.textColor = Color.white;
                }
                GUI.Label(new Rect(25, Screen.height - (100 + 30 * i), 200, 30), msgs[i]);

            }
        }



    }
}
