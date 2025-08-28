namespace API.model
{
    public class Moni
    {
        public int IdMonitor { get; set; } // Chave primária

        public string RA { get; set; } = default!;
        public string Nome { get; set; } = default!;
        public string Apelido { get; set; } = default!;
    }
}
