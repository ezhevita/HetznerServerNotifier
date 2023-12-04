using HetznerServerNotifier.Models;

namespace HetznerServerNotifier;

public interface IFilter
{
	public bool Execute(ServerInfo serverInfo);
}
