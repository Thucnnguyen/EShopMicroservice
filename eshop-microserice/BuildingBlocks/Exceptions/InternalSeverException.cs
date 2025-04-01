
namespace BuildingBlocks.Exceptions;

public class InternalSeverException : Exception
{
    public string Details { get; set; }

	public InternalSeverException(string message) : base(message)
	{
	}
    public InternalSeverException(string message, string details) : base(message)
    {
        Details = details;  
    }
}
