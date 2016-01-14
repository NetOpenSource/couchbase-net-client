using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching.Memcached;

namespace Enyim.Caching.Configuration
{
    public class SocketPoolConfiguration : ISocketPoolConfiguration
    {
        private int minPoolSize = 10;
        private int maxPoolSize = 20;
        private TimeSpan connectionTimeout = new TimeSpan(0, 0, 10);
        private TimeSpan receiveTimeout = new TimeSpan(0, 0, 10);
        private TimeSpan deadTimeout = new TimeSpan(0, 0, 2);
        private TimeSpan queueTimeout = new TimeSpan(0, 0, 0, 2, 500);
        private INodeFailurePolicyFactory policyFactory = new FailImmediatelyPolicyFactory();
        private bool _lingerEnabled = false;
        private TimeSpan _lingerTime = new TimeSpan(0, 0, 10);

        public SocketPoolConfiguration()
        {
            EnableTcpKeepAlives = true;
            TcpKeepAliveTime = (uint) 2*60*60*1000;
            TcpKeepAliveInterval = ((uint) 1000);
        }

        int ISocketPoolConfiguration.MinPoolSize
        {
            get { return this.minPoolSize; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value", "MinPoolSize must be >= 0!");

                if (value > this.maxPoolSize)
                    throw new ArgumentOutOfRangeException("value", "MinPoolSize must be <= MaxPoolSize!");

                this.minPoolSize = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the maximum amount of sockets per server in the socket pool.
        /// </summary>
        /// <returns>The maximum amount of sockets per server in the socket pool. The default is 20.</returns>
        /// <remarks>It should be 0.75 * (number of threads) for optimal performance.</remarks>
        int ISocketPoolConfiguration.MaxPoolSize
        {
            get { return this.maxPoolSize; }
            set
            {
                if (value < this.minPoolSize)
                    throw new ArgumentOutOfRangeException("value", "MaxPoolSize must be >= MinPoolSize!");

                this.maxPoolSize = value;
            }
        }

        TimeSpan ISocketPoolConfiguration.ConnectionTimeout
        {
            get { return this.connectionTimeout; }
            set
            {
                if (value < TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException("value", "value must be positive");

                this.connectionTimeout = value;
            }
        }

        TimeSpan ISocketPoolConfiguration.ReceiveTimeout
        {
            get { return this.receiveTimeout; }
            set
            {
                if (value < TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException("value", "value must be positive");

                this.receiveTimeout = value;
            }
        }

        TimeSpan ISocketPoolConfiguration.QueueTimeout
        {
            get { return this.queueTimeout; }
            set
            {
                if (value < TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException("value", "value must be positive");

                this.queueTimeout = value;
            }
        }

        TimeSpan ISocketPoolConfiguration.DeadTimeout
        {
            get { return this.deadTimeout; }
            set
            {
                if (value < TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException("value", "value must be positive");

                this.deadTimeout = value;
            }
        }

        INodeFailurePolicyFactory ISocketPoolConfiguration.FailurePolicyFactory
        {
            get { return this.policyFactory; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                this.policyFactory = value;
            }
        }

        public TimeSpan LingerTime
        {
            get { return _lingerTime; }
            set
            {
                if (value < TimeSpan.Zero)
                {
                    throw new ArgumentOutOfRangeException("value", "value must be positive");
                }
                _lingerTime = value;
            }
        }

        public bool LingerEnabled
        {
            get { return _lingerEnabled; }
            set { _lingerEnabled = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether enable TCP keep alives.
        /// </summary>
        /// <value>
        /// <c>true</c> to enable TCP keep alives; otherwise, <c>false</c>.
        /// </value>
        public bool EnableTcpKeepAlives { get; set; }

        /// <summary>
        /// Specifies the timeout, in milliseconds, with no activity until the first keep-alive packet is sent.
        /// </summary>
        /// <value>
        /// The TCP keep alive time in milliseconds.
        /// </value>
        /// <remarks>The default is 2hrs.</remarks>
        public uint TcpKeepAliveTime { get; set; }

        /// <summary>
        /// Specifies the interval, in milliseconds, between when successive keep-alive packets are sent if no acknowledgement is received.
        /// </summary>
        /// <value>
        /// The TCP keep alive interval in milliseconds..
        /// </value>
        /// <remarks>The default is 1 second.</remarks>
        public uint TcpKeepAliveInterval { get; set; }
    }
}

#region [ License information          ]

/* ************************************************************
 *
 *    Copyright (c) 2010 Attila Kisk�, enyim.com
 *
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *
 *        http://www.apache.org/licenses/LICENSE-2.0
 *
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *
 * ************************************************************/

#endregion