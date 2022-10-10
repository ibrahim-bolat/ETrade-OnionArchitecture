
using ETrade.Domain.Enums;

namespace ETrade.Application.DTOs.Common;
public abstract class BaseDto
    {
        public virtual ResultStatus ResultStatus { get; set; }
        public virtual string Message { get; set; }
    }
