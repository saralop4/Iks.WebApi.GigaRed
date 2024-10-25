namespace Iks.WebApi.Transversal.Modelos;

public class ResponsePagination<T> : ResponseGeneric<T>
{
    public int NumeroDePagina { get; set; }
    public int CantidadTotal { get; set; }
    public int TotalPaginas { get; set; }
    public bool HasPreviousPage => NumeroDePagina > 1; //indica si tiene pagina previa
    public bool HasNextPage => NumeroDePagina < TotalPaginas;   //indica si existe una pagina siguiente


}
