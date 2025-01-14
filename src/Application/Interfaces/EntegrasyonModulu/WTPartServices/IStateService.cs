using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.EntegrasyonModulu.WTPartServices;

public interface IStateService
{
	Task RELEASED(CancellationToken token);
	Task CANCELLED(CancellationToken token);
	Task INWORK(CancellationToken token);

}
