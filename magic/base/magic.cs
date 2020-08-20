using FGClient;
using UnityEngine;

namespace FGFly.magic
{
    public class magic
    {
        public FallGuysCharacterController fg { get; set; }

        public Transform FgT => fg.gameObject.transform;

        public ClientGameManager cGM => GlobalGameStateClient.Instance.GameStateView.GetLiveClientGameManager();

        public TagManager TaGm => cGM._tagManager;

        public virtual void Init(FallGuysCharacterController _fg) { fg = _fg; }
        public virtual void Update() { }
        public virtual void GUI() { }
        public virtual void FixedUpdate() { }
    }
}
