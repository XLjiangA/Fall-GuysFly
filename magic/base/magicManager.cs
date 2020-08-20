using MelonLoader;
using System.Collections.Generic;
using System.Reflection;

namespace FGFly.magic
{
    public class magicManager
    {
        private List<magic> magics = new List<magic>();

        private static magicManager _instance = null;

        public static magicManager instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new magicManager();
                    foreach(var type in Assembly.GetExecutingAssembly().GetTypes())
                    {
                        if(type.Namespace== "FGFly.magic"&&type.BaseType == typeof(magic))
                        {
                            object magicObj = Assembly.GetExecutingAssembly().CreateInstance(type.FullName,false);
                            _instance.magics.Add(magicObj as magic);
                        }
                    }
                    MelonLogger.Log($"loaded Func count:{_instance.magics.Count}");
                }
                return _instance;
            }
        }
        public void Hello()
        {
            //AACheat.Init();
            MelonLogger.Log("No Anti Cheat OK ?");
        }
        public void MagicInit(FallGuysCharacterController fg)
        {
            magics.ForEach(m=>m.Init(fg));
        }
        public void MagicUpdate()
        {
            magics.ForEach(m => m.Update());
        }
        public void MagicFixedUpdate()
        {
            magics.ForEach(m => m.FixedUpdate());
        }
        public void MagicGUI()
        {
            magics.ForEach(m => m.GUI());
        }
    }
}
