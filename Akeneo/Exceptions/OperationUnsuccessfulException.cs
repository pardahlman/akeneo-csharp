using Akeneo.Client;

namespace Akeneo.Exceptions
{
	public class OperationUnsuccessfulException : System.Exception
	{
		public AkeneoResponse Response { get; }

		public OperationUnsuccessfulException(string message, AkeneoResponse response) :base(message)
		{
			Response = response;
		}
	}
}
