using System;
using System.Collections.Generic;
using UnityEngine;

namespace FGFly.view
{
    public static class GameLog
    {
        private static List<string> messages = new List<string>();

        public static void Clear() => messages.Clear();

        public static void write(string msg) => messages.Add(msg);

        private static float Timer = 2f;

        private static float delayTime = 2f;

        public static void listen()
        {
            if (messages.Count > 0)
            {
                if (Timer > 0) Timer -= Time.deltaTime;
                if (Timer < 0) Timer = 0;
                if (Timer == 0)
                {
                    messages.RemoveAt(0);
                    Timer = delayTime;
                }
            }
        }
        public static string[] getLogs()
        {
            var temp = messages.ToArray();
            Array.Reverse(temp);
            return temp;
        }



    }
}
