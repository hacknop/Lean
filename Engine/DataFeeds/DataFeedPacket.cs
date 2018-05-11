/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
*/

using System.Collections.Generic;
using QuantConnect.Data;
using QuantConnect.Securities;
using QuantConnect.Util;

namespace QuantConnect.Lean.Engine.DataFeeds
{
    /// <summary>
    /// Defines a container type to hold data produced by a data feed subscription
    /// </summary>
    public class DataFeedPacket
    {
        private readonly IReadOnlyRef<bool> _isDisposed;

        /// <summary>
        /// The security
        /// </summary>
        public Security Security
        {
            get; private set;
        }

        /// <summary>
        /// The subscription configuration that produced this data
        /// </summary>
        public SubscriptionDataConfig Configuration
        {
            get; private set;
        }

        /// <summary>
        /// Gets the number of data points held within this packet
        /// </summary>
        public int Count => Data.Count;

        /// <summary>
        /// The data for the security
        /// </summary>
        public List<BaseData> Data { get; }

        /// <summary>
        /// Gets whether or not this packet should be disposed due to the subscription being removed
        /// </summary>
        public bool IsDisposed => _isDisposed.Value;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataFeedPacket"/> class
        /// </summary>
        /// <param name="security">The security whose data is held in this packet</param>
        /// <param name="configuration">The subscription configuration that produced this data</param>
        /// <param name="isDisposed">Reference to whether or not the subscription has since been disposed, defaults to false</param>
        public DataFeedPacket(Security security, SubscriptionDataConfig configuration, IReadOnlyRef<bool> isDisposed = null)
            : this(security, configuration, new List<BaseData>(), isDisposed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataFeedPacket"/> class
        /// </summary>
        /// <param name="security">The security whose data is held in this packet</param>
        /// <param name="configuration">The subscription configuration that produced this data</param>
        /// <param name="data">The data to add to this packet. The list reference is reused
        /// internally and NOT copied.</param>
        /// <param name="isDisposed">Reference to whether or not the subscription has since been disposed, defaults to false</param>
        public DataFeedPacket(Security security, SubscriptionDataConfig configuration, List<BaseData> data, IReadOnlyRef<bool> isDisposed = null)
        {
            Security = security;
            Configuration = configuration;
            Data = data;
            _isDisposed = isDisposed ?? Ref.Create(false);
        }

        /// <summary>
        /// Adds the specified data to this packet
        /// </summary>
        /// <param name="data">The data to be added to this packet</param>
        public void Add(BaseData data)
        {
            Data.Add(data);
        }
    }
}