﻿
namespace net.paxialabs.mabe.serviplus.security.ManagerExceptions
{
    /// <summary>
    /// Enumerador que contiene los codigos de error HTML 
    /// Ref: http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html
    /// J. Carlos Escobar H.
    /// Septiembre de 2015
    /// </summary>
    public enum HtmlStatusCode
    {
        // Informational 1xx
        Continue = 100,
        Switching_Protocols = 101,

        // Successful 2xx
        OK = 200,
        Created = 201,
        Accepted = 202,
        Non_Authoritative_Information = 203,
        No_Content = 204,
        Reset_Content = 205,
        Partial_Content = 206,

        // Redirection 3xx
        Multiple_Choices = 300,
        Moved_Permanently = 301,
        Found = 302,
        See_Other = 303,
        Not_Modified = 304,
        Use_Proxy = 305,
        Unused = 306,
        Temporary_Redirect = 307,

        // Client Error 4xx
        Bad_Request = 400,
        Unauthorized = 401,
        Payment_Required = 402,
        Forbidden = 403,
        Not_Found = 404,
        Method_Not_Allowed = 405,
        Not_Acceptable = 406,
        Proxy_Authentication_Required = 407,
        Request_Timeout = 408,
        Conflict = 409,
        Gone = 410,
        Length_Required = 411,
        Precondition_Failed = 412,
        Request_Entity_Too_Large = 413,
        Request_URI_Too_Long = 414,
        Unsupported_Media_Type = 415,
        Requested_Range_Not_Satisfiable = 416,
        Expectation_Failed = 417,

        // Server Error 5xx
        Internal_Server_Error = 500,
        Not_Implemented = 501,
        Bad_Gateway = 502,
        Service_Unavailable = 503,
        Gateway_Timeout = 504,
        HTTP_Version_Not_Supported = 505
    }
}
