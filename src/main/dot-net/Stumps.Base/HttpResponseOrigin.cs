namespace Stumps
{
    /// <summary>
    ///     An enumeration the origin of the HTTP response.
    /// </summary>
    public enum HttpResponseOrigin
    {
        /// <summary>
        ///     The request has not been processed, and the HTTP response does not have an origin.
        /// </summary>
        Unprocessed = 0,

        /// <summary>
        ///     The response was generated by a remote HTTP server.
        /// </summary>
        RemoteServer = 1,

        /// <summary>
        ///     The response was generated using a Stump.
        /// </summary>
        Stump = 2,

        /// <summary>
        ///     The request was not processed, and an HTTP 404 Not Found response was returned.
        /// </summary>
        NotFoundResponse = 3,

        /// <summary>
        ///     The request was not processed, and an HTTP 503 Service Unavailable response was returned.
        /// </summary>
        ServiceUnavailable = 4
    }
}
