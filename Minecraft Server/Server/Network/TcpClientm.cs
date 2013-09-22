﻿using Minecraft_Server.Framework.Network;
using Minecraft_Server.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server.Server.Network
{
    class TcpClientm:Framework.Network.TcpClientm
    {
        public byte[] token;
        public byte[] SharedKey;
        public Client.Client cli;

        new public static TcpClientm Get(TcpClient t, ushort i)
        {
            return new TcpClientm(t, i);
        }

        public TcpClientm(TcpClient t, ushort i)
            : base(t, i)
        {
            this.cli = new Client.Client(this);
            this.cli.onConnect();
        }
        public override void APacket(byte o)
        {
            if (encrypted)
                o = this.decryptCipher.ProcessByte(o)[0];
            Log.Info("Get packet: " + o);
            Network.GetPacket(o).Invoke(this);
        }
        public void Close()
        {
            base.Close();
            if (this.cli != null)
                this.cli.Close();
            this.cli = null;
        }
    }
}
