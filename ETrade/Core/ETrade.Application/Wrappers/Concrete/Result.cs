
using ETrade.Application.Wrappers.Abstract;
using ETrade.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Application.Wrappers.Concrete;

public class Result : IResult
    {
        public Result(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
        }
        public Result(ResultStatus resultStatus, string message)
        {
            ResultStatus = resultStatus;
            Message = message;
        }
        public Result(ResultStatus resultStatus, string message, Exception exception)
        {
            ResultStatus = resultStatus;
            Message = message;
            Exception = exception;
        }
        public Result(ResultStatus resultStatus, string message, List<IdentityError> identityErrorList)
        {
            ResultStatus = resultStatus;
            Message = message;
            IdentityErrorList = identityErrorList;
        }
        public ResultStatus ResultStatus { get; }

        public string Message { get; }

        public Exception Exception { get; }
        
        public List<IdentityError> IdentityErrorList { get; }
    }
