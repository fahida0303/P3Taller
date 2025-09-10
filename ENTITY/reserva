using System;

namespace ENTITY
{
    public class Reserva
    {
        public int Id { get; set; }
        public string Solicitante { get; set; }
        public string Sala { get; set; }
        public DateTime Fecha { get; set; }

        public Reserva()
        {
        }

        public Reserva(int id, string solicitante, string sala, DateTime fecha)
        {
            Id = id;
            Solicitante = solicitante;
            Sala = sala;
            Fecha = fecha;
        }

        public override string ToString()
        {
            return $"{Id} | {Solicitante} | {Sala} | {Fecha:dd/MM/yyyy}";
        }
    }
}
