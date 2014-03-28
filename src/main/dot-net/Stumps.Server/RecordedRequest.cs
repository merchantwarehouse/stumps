﻿namespace Stumps.Server
{

    using System.Net;

    /// <summary>
    ///     A class that represents a recorded HTTP request.
    /// </summary>
    public sealed class RecordedRequest : RecordedContextPartBase, IStumpsHttpRequest
    {

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Stumps.Server.RecordedRequest" /> class.
        /// </summary>
        /// <param name="request">The <see cref="T:Stumps.IStumpsHttpRequest"/> used to initialize the instance.</param>
        public RecordedRequest(IStumpsHttpRequest request) : base(request)
        {
            this.HttpMethod = request.HttpMethod;
            this.LocalEndPoint = request.LocalEndPoint;
            this.ProtocolVersion = request.ProtocolVersion;
            this.RawUrl = request.RawUrl;
            this.RemoteEndPoint = request.RemoteEndPoint;
        }

        /// <summary>
        ///     Gets the HTTP data transfer method used by the client.
        /// </summary>
        /// <value>
        ///     The HTTP data transfer method used by the client.
        /// </value>
        public string HttpMethod
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the local end point where the HTTP request was received on.
        /// </summary>
        /// <value>
        ///     The local end point where the HTTP request was received on.
        /// </value>
        public IPEndPoint LocalEndPoint
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the HTTP protocol version.
        /// </summary>
        /// <value>
        ///     The HTTP protocol version.
        /// </value>
        public string ProtocolVersion
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the raw URL of the current request.
        /// </summary>
        /// <value>
        ///     The raw URL of the current request.
        /// </value>
        public string RawUrl
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the remote end point the HTTP request came from.
        /// </summary>
        /// <value>
        ///     The remote end point where the HTTP request came from.
        /// </value>
        public IPEndPoint RemoteEndPoint
        {
            get;
            private set;
        }

    }

}