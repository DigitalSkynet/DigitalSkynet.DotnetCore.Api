namespace DigitalSkynet.DotnetCore.Api.Enums
{
    public enum ResponseCode
    {
        NotImplemented = 501,
        Success = 200,
        NewResourceHasBeenCreated = 201,
        ResourceWasSuccesfullyDeleted = 204,
        NotModified = 304,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        UnexpectedError = 500
    }
}
