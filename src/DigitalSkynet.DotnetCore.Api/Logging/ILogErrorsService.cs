using System;
using System.Threading.Tasks;

namespace DigitalSkynet.DotnetCore.Api.Logging
{
    public interface ILogErrorsService
    {
        Task<bool> CreateLog(Exception ex);
    }
}
