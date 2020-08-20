using FGFly.view;
using System.Collections.Generic;
using UnityEngine;

namespace FGFly.magic
{
    public class stopplayers : magic
    {
        private bool R { get; set; }

        private bool StopPlayers { get; set; }

        private Dictionary<string, Vector3> old_PosList = new Dictionary<string, Vector3>();

        private Dictionary<string, Quaternion> old_QuaList = new Dictionary<string, Quaternion>();

        public override void Init(FallGuysCharacterController _fg)
        {
            base.Init(_fg);
            StopPlayers = false;
            old_PosList.Clear();
            old_QuaList.Clear();
        }
        public override void Update()
        {
            R = Input.GetKeyDown(KeyCode.R);
        }
        public override void FixedUpdate()
        {
            if (R)
                GameLog.write((StopPlayers = !StopPlayers) ? "[时间停止]开启" : "[时间停止]关闭");
            frozenPos(StopPlayers);
        }
        private void frozenPos(bool value = true)
        {
            foreach (var _fg in GameObject.FindObjectsOfType<FallGuysCharacterController>())
            {
                if (!_fg.IsLocalPlayer)
                {
                    if (value)
                    {
                        old_PosList[_fg.name] = _fg.transform.position;
                        old_QuaList[_fg.name] = _fg.transform.rotation;
                    }
                    _fg.transform.position = old_PosList[_fg.name];
                    _fg.transform.rotation = old_QuaList[_fg.name];
                }
            }
        }



    }
}
