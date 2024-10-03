namespace Tiknas.Tracing;

public class TiknasCorrelationIdOptions
{
    public string HttpHeaderName { get; set; } = "X-Correlation-Id";

    public bool SetResponseHeader { get; set; } = true;
}
