namespace SistemaSecretaria.Domain.Entities
{
    public class PaginacaoRequest
    {
        public int NumeroPagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 10;
    }

    public class PaginacaoResult<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public int TotalRegistros { get; set; }
        public int NumeroPagina { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalRegistros / TamanhoPagina);
    }

}
