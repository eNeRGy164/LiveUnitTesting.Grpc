﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LiveUnitTesting.GrpcClient
{
    using System;
    using System.Net.Http;
    using System.Text.Json;
    using Grpc.Net.Client;
    using Autogenerated = Autogen.Grpc.v1;

    /// <summary>
    /// Builder for building <see cref="DaprClient"/>
    /// </summary>
    public sealed class DaprClientBuilder
    {
        internal string GrpcEndpoint { get; private set; }

        // property exposed for testing purposes
        internal string HttpEndpoint { get; private set; }

        private Func<HttpClient> HttpClientFactory { get; set; }

        // property exposed for testing purposes
        internal JsonSerializerOptions JsonSerializerOptions { get; private set; }

        // property exposed for testing purposes
        internal GrpcChannelOptions GrpcChannelOptions { get; private set; }
        internal string DaprApiToken { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpEndpoint"></param>
        /// <returns></returns>
        public DaprClientBuilder UseHttpEndpoint(string httpEndpoint)
        {
            ArgumentVerifier.ThrowIfNullOrEmpty(httpEndpoint, nameof(httpEndpoint));
            this.HttpEndpoint = httpEndpoint;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="factory"></param>
        /// <returns></returns>
        internal DaprClientBuilder UseHttpClientFactory(Func<HttpClient> factory)
        {
            this.HttpClientFactory = factory;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grpcChannelOptions"></param>
        /// <returns></returns>
        public DaprClientBuilder UseGrpcChannelOptions(GrpcChannelOptions grpcChannelOptions)
        {
            this.GrpcChannelOptions = grpcChannelOptions;
            return this;
        }

        /// <summary>
        /// Builds a <see cref="DaprClient" /> instance from the properties of the builder.
        /// </summary>
        /// <returns>The <see cref="DaprClient" />.</returns>
        public DaprClient Build()
        {
            var grpcEndpoint = new Uri(this.GrpcEndpoint);
            if (grpcEndpoint.Scheme != "http" && grpcEndpoint.Scheme != "https")
            {
                throw new InvalidOperationException("The gRPC endpoint must use http or https.");
            }

            if (grpcEndpoint.Scheme.Equals(Uri.UriSchemeHttp))
            {
                // Set correct switch to maksecure gRPC service calls. This switch must be set before creating the GrpcChannel.
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            }

            var httpEndpoint = new Uri(this.HttpEndpoint);
            if (httpEndpoint.Scheme != "http" && httpEndpoint.Scheme != "https")
            {
                throw new InvalidOperationException("The HTTP endpoint must use http or https.");
            }

            var channel = GrpcChannel.ForAddress(this.GrpcEndpoint, this.GrpcChannelOptions);
            var client = new Autogenerated.Dapr.DaprClient(channel);

            //var apiTokenHeader = DaprClient.GetDaprApiTokenHeader(this.DaprApiToken);
            //var httpClient = HttpClientFactory is object ? HttpClientFactory() : new HttpClient();
            return new DaprClientGrpc();//channel, client, httpClient, httpEndpoint, this.JsonSerializerOptions, apiTokenHeader);
        }
    }
}