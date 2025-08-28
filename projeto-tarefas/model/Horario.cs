namespace API.model
{
    public class Horario
    {
        public int IdHorario { get; set; }
        public int DiaSemana { get; set; }
        public string horario { get; set; } = default!;

        public int IdMonitor { get; set; }
        public Moni Monitor { get; set; } = default!;
    }
}
