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
            fg.Data.diveForce = 20;
            fg.Data.airDiveForce = 30;
            fg.Data.staggerForce = 0;
            fg.Data.bracePushForce = 300;
            fg.Data.armBoundsForce = 300;
            fg.Data.playerGrabCheckDistance = 30000;
        }
        public override void Update()
        {
            Axis = Input.GetAxis("Mouse ScrollWheel");
            H = Input.GetAxis("Horizontal");
            V = Input.GetAxis("Vertical");
            Q = Input.GetKeyDown(KeyCode.Q);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 14;
            }
            else
            {
                moveSpeed = 6;
            }
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
            {
                NoGravity = !NoGravity;
                if (NoGravity)
                {
                    GameLog.write("[飞行模式]开启");
                }
                else
                {
                    GameLog.write("[飞行模式]关闭");
                }
            }
            if (NoGravity)
            {
                fg.ApplyGravity = false;
                fg.DebugZeroGravity = true;
                FgT.position = new Vector3(FgT.position.x, old_Y + offset_Y, FgT.position.z);
                if (Axis > 0)
                {
                    //MelonLogger.Log("Fall Guys Up");
                    offset_Y += 1f;
                }
                if (Axis < 0)
                {
                    offset_Y -= 1f;
                }

            }
            else
            {
                fg.ApplyGravity = true;
                fg.DebugZeroGravity = false;
                offset_Y = 0;
                old_Y = FgT.position.y;
            }
        }
    }
}
