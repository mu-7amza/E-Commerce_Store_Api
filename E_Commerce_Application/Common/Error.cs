namespace E_Commerce_Application.Common
{
    public record Error(string StatusCode, string Desciption, ErrorType ErrorType)
    {
       public static Error Failure(string code = "General.Failure",string description = "A general failure has occured.")
            => new(code, description,ErrorType.Failure);
        public static Error Validation(string code = "General.Validation", string description = "A validation error has occured.")
            => new(code, description, ErrorType.Validation);
        public static Error NotFound(string code = "General.NotFound", string description = "The request resource was not found !.")
            => new(code, description, ErrorType.NotFound);
        public static Error Conflict(string code = "General.Conflict", string description = "A conflict happended with current state.")
            => new(code, description, ErrorType.Conflict);
        public static Error UnAuthorized(string code = "General.UnAuthorized", string description = "Access is denied due to lack of authorization.")
            => new(code, description, ErrorType.UnAuthorized);
        public static Error Forbidden(string code = "General.Forbidden", string description = "The operation is forbidden.")
            => new(code, description, ErrorType.Forbidden);
        public static Error InvalidCredintials(string code = "General.InvalidCredintials", string description = "The provided credintials are not valid !.")
            => new(code, description, ErrorType.InvalidCredintials);

    }

    public enum ErrorType
    {
        Failure = 0,
        Validation,
        NotFound,
        Conflict,
        UnAuthorized,
        Forbidden,
        InvalidCredintials
    }
}