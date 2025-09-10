using System;
using System.Collections.Generic;
using System.IO;
using ENTITY;

namespace DAL
{
    public class ReservaRepositoryTxt : IReservaRepository
    {
        private readonly string _archivoPath = "reservas.txt";

        public void GuardarReserva(Reserva reserva)
        {
            try
            {
                string linea = $"{reserva.Id}|{reserva.Solicitante}|{reserva.Sala}|{reserva.Fecha:dd/MM/yyyy}";
                File.AppendAllText(_archivoPath, linea + Environment.NewLine);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al guardar la reserva: {ex.Message}");
            }
        }

        public List<Reserva> ObtenerTodasLasReservas()
        {
            List<Reserva> reservas = new List<Reserva>();
            
            try
            {
                if (!File.Exists(_archivoPath))
                    return reservas;

                string[] lineas = File.ReadAllLines(_archivoPath);
                
                foreach (string linea in lineas)
                {
                    if (!string.IsNullOrWhiteSpace(linea))
                    {
                        string[] partes = linea.Split('|');
                        if (partes.Length == 4)
                        {
                            Reserva reserva = new Reserva
                            {
                                Id = int.Parse(partes[0]),
                                Solicitante = partes[1],
                                Sala = partes[2],
                                Fecha = DateTime.ParseExact(partes[3], "dd/MM/yyyy", null)
                            };
                            reservas.Add(reserva);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al leer las reservas: {ex.Message}");
            }

            return reservas;
        }

        public int ObtenerUltimoId()
        {
            try
            {
                List<Reserva> reservas = ObtenerTodasLasReservas();
                if (reservas.Count == 0)
                    return 0;

                int maxId = 0;
                foreach (Reserva reserva in reservas)
                {
                    if (reserva.Id > maxId)
                        maxId = reserva.Id;
                }
                return maxId;
            }
            catch
            {
                return 0;
            }
        }
    }
}
