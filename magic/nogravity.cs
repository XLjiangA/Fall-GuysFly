using FGFly.view;
using UnityEngine;

namespace FGFly.magic
{
    public class nogravity : magic
    {
        private bool NoGravity { get; set; }

        private float Axis { get; set; }

        private float H { get; set; }

        private float V { get; set; }

        private bool Q { get; set; }

        private float old_Y { get; set; }

        private float offset_Y { get; set; }

        private float moveSpeed = 6;

        public override void Init(FallGuysCharacterController _fg)
        {
            base.Init(_fg);
            NoGravity = false;
            //test data
            fg.Data.diveForce = 20;
            fg.Data.airDiveForce = 30;
            fg.Data.staggerForce = 0;
            fg.Data.bracePushForce = 300;
            fg.Data.armBoundsForce = 300;
            fg.Data.playerGrabCheckDistance = 30000;
            //test -----
        }
        public override void Update()
        {
            Axis = Input.GetAxis("Mouse ScrollWheel");
            H = Input.GetAxis("Horizontal");
            V = Input.GetAxis("Vertical");
            Q = Input.GetKeyDown(KeyCode.Q);
            moveSpeed = Input.GetKey(KeyCode.LeftShift) ? 16 : 6;
        }
        public override void FixedUpdate()
        {
            if (H != 0 || V != 0)
            {
                Vector3 targetDirection = new Vector3(H, 0, V);
                float y = Camera.main.transform.rotation.eulerAngles.y;
                targetDirection = Quaternion.Euler(0, y, 0) * targetDirection;
                FgT.Translate(targetDirection * Time.deltaTime * moveSpeed, Space.World);
            }
            if (Q)
                GameLog.write((NoGravity = !NoGravity) ? "[飞行模式]开启" : "[飞行模式]关闭");
            fg.ApplyGravity = !NoGravity;
            fg.DebugZeroGravity = NoGravity;
            if (NoGravity)
            {
                FgT.position = new Vector3(FgT.position.x, old_Y + offset_Y, FgT.position.z);
                offset_Y += Axis > 0 ? 1f : -1f;
            }
            else
            {
                offset_Y = 0;
                old_Y = FgT.position.y;
            }
        }



    }
}
