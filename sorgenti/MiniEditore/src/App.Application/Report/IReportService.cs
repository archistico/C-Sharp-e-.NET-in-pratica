namespace App.Application.Report;

public interface IReportService
{
    Task<IReadOnlyList<VenditePerLibroDto>> GetVenditePerLibroAsync(ReportVenditeRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<VenditePerClienteDto>> GetVenditePerClienteAsync(ReportVenditeRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RiepilogoMensileDto>> GetRiepilogoMensileAsync(ReportVenditeRequest request, CancellationToken cancellationToken = default);
}
