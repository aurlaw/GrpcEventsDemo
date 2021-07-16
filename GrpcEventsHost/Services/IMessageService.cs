using System;
using System.Threading.Tasks;

namespace GrpcEventsHost.Services
{
	public interface IMessageService
	{
		Task Process();
	}
}
