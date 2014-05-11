﻿/*
 *   Coinium - Crypto Currency Pool Software - https://github.com/CoiniumServ/coinium
 *   Copyright (C) 2013 - 2014, Coinium Project - http://www.coinium.org
 *
 *   This program is free software: you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation, either version 3 of the License, or
 *   (at your option) any later version.
 *
 *   This program is distributed in the hope that it will be useful,
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *   GNU General Public License for more details.
 *
 *   You should have received a copy of the GNU General Public License
 *   along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using Coinium.Core.Mining;
using Coinium.Net.Sockets;
using Serilog;

namespace Coinium.Core.Miner
{
    /// <summary>
    /// Miner manager that manages all connected miners over different ports.
    /// </summary>
    public class MinerManager
    {
        private int _counter; // counter for assigining unique id's to miners.

        private readonly Dictionary<int, IMiner> _miners = new Dictionary<int, IMiner>(); // Dictionary that holds id <-> miner pairs. 

        public MinerManager()
        {
            Log.Verbose("MinerManager() init..");
        }

        /// <summary>
        /// Creates a new instance of IMiner type.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public T Create<T>(IConnection connection) where T : IMiner
        {
            var instance = Activator.CreateInstance(typeof (T), new object[] {this._counter++, connection});
            var miner = (IMiner) instance;
            this._miners.Add(miner.Id, miner);

            return (T)miner;
        }

        /// <summary>
        /// Creates a new instance of IMiner type.
        /// </summary>
        /// <returns></returns>
        public T Create<T>() where T : IMiner
        {
            var instance = Activator.CreateInstance(typeof(T), new object[] { this._counter++ });
            var miner = (IMiner)instance;
            this._miners.Add(miner.Id, miner);

            return (T)miner;
        }

        private static readonly MinerManager _instance = new MinerManager(); // memory instance of the MinerManager.

        /// <summary>
        /// Singleton instance of WalletManager.
        /// </summary>
        public static MinerManager Instance { get { return _instance; } }
    }
}