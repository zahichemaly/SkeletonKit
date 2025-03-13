using System.Net;

namespace SkeletonKit.Common.Exceptions
{
    public abstract class ApiException : Exception
    {
        public int StatusCode { get; set; }
        public string ResponseCode { get; set; }

        public ApiException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
            ResponseCode = Constants.ErrorCode.GenericError;
        }

        public ApiException(int statusCode, string responseCode, string message) : base(message)
        {
            StatusCode = statusCode;
            ResponseCode = responseCode;
        }
    }

    public class InternalServerErrorException : ApiException
    {
        public InternalServerErrorException(string message) : base((int)HttpStatusCode.InternalServerError, message)
        {
        }

        public InternalServerErrorException(string responseCode, string message) : base((int)HttpStatusCode.InternalServerError, responseCode, message)
        {
        }
    }

    public class BadRequestException : ApiException
    {
        public BadRequestException(string message) : base((int)HttpStatusCode.BadRequest, message)
        {
        }

        public BadRequestException(string responseCode, string message) : base((int)HttpStatusCode.BadRequest, responseCode, message)
        {
        }
    }

    public class NotFoundException : ApiException
    {
        public NotFoundException(string message) : base((int)HttpStatusCode.NotFound, message)
        {
        }

        public NotFoundException(string responseCode, string message) : base((int)HttpStatusCode.NotFound, responseCode, message)
        {
        }
    }

    public class ForbiddenException : ApiException
    {
        public ForbiddenException(string message) : base((int)HttpStatusCode.Forbidden, message)
        {
        }

        public ForbiddenException(string responseCode, string message) : base((int)HttpStatusCode.Forbidden, responseCode, message)
        {
        }
    }
}
