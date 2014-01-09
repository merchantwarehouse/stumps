﻿namespace Stumps.Proxy {

    using System;
    using System.Collections.Generic;
    using Stumps.Http;
    using Stumps.Logging;
    using Stumps.Utility;

    internal class StumpsHandler : IHttpHandler {

        private readonly ProxyEnvironment _environment;
        private readonly ILogger _logger;

        public StumpsHandler(ProxyEnvironment environment, ILogger logger) {

            if ( environment == null ) {
                throw new ArgumentNullException("environment");
            }

            if ( logger == null ) {
                throw new ArgumentNullException("logger");
            }

            _environment = environment;
            _logger = logger;

        }

        public ProcessHandlerResult ProcessRequest(IStumpsHttpContext context) {

            if ( context == null ) {
                throw new ArgumentNullException("context");
            }

            if ( _environment.RecordTraffic ) {
                return ProcessHandlerResult.Continue;
            }

            var result = processRequest(context);

            return result;

        }

        private void populateResponse(IStumpsHttpContext context, RecordedResponse recordedResponse) {

            context.Response.StatusCode = recordedResponse.StatusCode;
            context.Response.StatusDescription = recordedResponse.StatusDescription;

            writeHeadersFromResponse(context, recordedResponse);
            writeContextBodyFromResponse(context, recordedResponse);

        }

        private ProcessHandlerResult processRequest(IStumpsHttpContext context) {

            var result = ProcessHandlerResult.Continue;
            var stump = _environment.Stumps.FindStump(context);

            if ( stump != null ) {

                populateResponse(context, stump.Contract.Response);
                _environment.IncrementStumpsServed();

                result = ProcessHandlerResult.Terminate;

            }

            return result;

        }

        private void writeContextBodyFromResponse(IStumpsHttpContext incommingHttpContext, RecordedResponse recordedResponse) {

            var buffer = recordedResponse.Body;
            var header = recordedResponse.FindHeader("Content-Encoding");

            if ( header != null ) {
                var encoder = new ContentEncoding(header.Value);
                buffer = encoder.Encode(buffer);
            }

            incommingHttpContext.Response.OutputStream.Write(buffer, 0, buffer.Length);

        }

        private void writeHeadersFromResponse(IStumpsHttpContext incommingHttpContext, RecordedResponse recordedResponse) {

            var headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach ( var header in recordedResponse.Headers ) {
                headers.Add(header.Name, header.Value);
            }

            incommingHttpContext.Response.Headers.Clear();

            if ( headers.ContainsKey("content-type") ) {
                incommingHttpContext.Response.ContentType = headers["content-type"];
            }

            if ( headers.ContainsKey("transfer-encoding") && headers["transfer-encoding"].Equals("chunked", StringComparison.OrdinalIgnoreCase) ) {
                incommingHttpContext.Response.SendChunked = true;
            }

            headers.Remove("content-length");
            headers.Remove("content-type");

            // The following headers should not be necessary - re-enable them if we see
            // a need in the future.

            //headers.Remove("accept");
            //headers.Remove("connection");
            //headers.Remove("expect");
            //headers.Remove("date");
            //headers.Remove("host");
            //headers.Remove("if-modified-since");
            //headers.Remove("range");
            //headers.Remove("referer");
            headers.Remove("transfer-encoding");
            headers.Remove("keep-alive");
            //headers.Remove("user-agent");

            foreach ( var key in headers.Keys ) {
                incommingHttpContext.Response.Headers.Add(key, headers[key]);
            }

        }

    }

}