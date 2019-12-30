using System;
using System.Collections.Generic;
using System.Text;

namespace MiniMongler
{
    public class MiniMonglerApp
    {
        public static MiniMonglerApp App => app.Value;
        public static Lazy<MiniMonglerApp> app = new Lazy<MiniMonglerApp>(() => new MiniMonglerApp());

        public Action<object> SwitchContext { get; set; } = c => { };
    }
}
