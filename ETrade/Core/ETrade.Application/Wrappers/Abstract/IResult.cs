using ETrade.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Wrappers.Abstract;

public interface IResult
    {
        public ResultStatus ResultStatus { get; }
        public string Message { get; }
        public Exception Exception { get; }
        public List<IdentityError> IdentityErrorList{ get; }
    }
