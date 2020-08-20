using FGFly.view;
using UnityEngine;

namespace FGFly.magic
{
    public class SelfRotate : magic
    {
        private bool E { get; set; }

        private bool IsRotate { get; set; }

        public override void Init(FallGuysCharacterController _fg)
        {
            base.Init(_fg);
            IsRotate = false;
        }
        public override void Update()
        {
            E = Input.GetKeyDown(KeyCode.E);
        }
        public override void FixedUpdate()
        {
            if (E)
                GameLog.write((IsRotate = !IsRotate) ? "[自我旋转]开启" : "[自我旋转]关闭");
            if (IsRotate)
            {
                fg.Animator.enabled = false;
                FgT.Rotate(Vector3.up * Time.deltaTime * 6000f, Space.World);
            }
            else fg.Animator.enabled = true;
        }



    }
}
