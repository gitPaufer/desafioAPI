namespace ImoService.Models
{
    public class Imovel_Foto
    {
        public Guid Id { get; set; }
        public Int64 IdImovel { get; set; }
        public string ContentType { get; set; }
        public string DescricaoFoto { get; set; }
        public string PathFoto { get; set; }
        public DateTime? Date { get; set; }
        public int? IdUser { get; set; }
    }
}
