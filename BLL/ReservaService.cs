using System;
using System.Collections.Generic;
using DAL;
using ENTITY;

namespace BLL
{
    public class ReservaService
    {
        private readonly IReservaRepository _reservaRepository;

   
        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public string RegistrarReserva(string solicitante, string sala)
        {
            try
            {
             
                if (string.IsNullOrWhiteSpace(solicitante))
                {
                    return "Error: El solicitante no puede estar vacío.";
                }

                if (string.IsNullOrWhiteSpace(sala))
                {
                    return "Error: La sala no puede estar vacía.";
                }

            
                int nuevoId = _reservaRepository.ObtenerUltimoId() + 1;

              
                DateTime fechaActual = DateTime.Now;

                
                Reserva nuevaReserva = new Reserva(nuevoId, solicitante.Trim(), sala.Trim(), fechaActual);

               
                _reservaRepository.GuardarReserva(nuevaReserva);

                return $"Reserva registrada exitosamente con ID: {nuevoId}";
            }
            catch (Exception ex)
            {
                return $"Error al registrar la reserva: {ex.Message}";
            }
        }

        public List<Reserva> ObtenerTodasLasReservas()
        {
            try
            {
                return _reservaRepository.ObtenerTodasLasReservas();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las reservas: {ex.Message}");
            }
        }
    }
}
