using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rararainer.API;

namespace Rararainer.MemoryWriter
{
    internal partial class WriterConfig
    {
        public String autoPotsAddress { get; set; }
        public String atkSpeedAddress { get; set; }
        public String droneViewAddress { get; set; }
        public String wallNameAddress { get; set; }
        public String longRangeAddress { get; set; }
    }

    internal class Writer
    {
        public WriterConfig config;
        Client cl;
        String server;
        String hwid;
        
        public Writer(String server, String hwid)
        {
            this.cl = new Client();
            this.server = server;
            this.hwid = hwid;
            this.config = cl.getConfig(server, hwid);
        }

        public bool changeServer(String server)
        {
            this.server = server;
            this.config = cl.getConfig(this.server, this.hwid);
            if (this.config == null) return false;
            return true;
        }
    }
}
