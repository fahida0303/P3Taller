using System;
using System.Collections.Generic;
using BLL;
using DAL;
using ENTITY;

namespace taller
{
    internal class Program
    {
        private static ReservaService _reservaService;

        static void Main(string[] args)
        {
            
            IReservaRepository repositorio = new ReservaRepositoryTxt();
            _reservaService = new ReservaService(repositorio);

            Console.WriteLine("=== SISTEMA DE RESERVAS DE SALAS ===");
            Console.WriteLine();

            bool continuar = true;
            while (continuar)
            {
                MostrarMenu();
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarReserva();
                        break;
                    case "2":
                        ListarReservas();
                        break;
                    case "3":
                        continuar = false;
                        Console.WriteLine("¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("MENÚ PRINCIPAL");
            Console.WriteLine("1. Registrar reserva");
            Console.WriteLine("2. Listar reservas");
            Console.WriteLine("3. Salir");
            Console.Write("\nSeleccione una opción: ");
        }

        private static void RegistrarReserva()
        {
            Console.WriteLine("\n=== REGISTRAR RESERVA ===");
            
            Console.Write("Ingrese el solicitante: ");
            string solicitante = Console.ReadLine();

            Console.Write("Ingrese la sala: ");
            string sala = Console.ReadLine();

            string resultado = _reservaService.RegistrarReserva(solicitante, sala);
            Console.WriteLine($"\n{resultado}");
        }

        private static void ListarReservas()
        {
            Console.WriteLine("\n=== LISTADO DE RESERVAS ===");
            
            try
            {
                List<Reserva> reservas = _reservaService.ObtenerTodasLasReservas();
                
                if (reservas.Count == 0)
                {
                    Console.WriteLine("(No hay reservas aún)");
                }
                else
                {
                    Console.WriteLine("Id | Solicitante | Sala | Fecha");
                    Console.WriteLine("".PadRight(50, '-'));
                    
                    foreach (Reserva reserva in reservas)
                    {
                        Console.WriteLine(reserva.ToString());
                    }
                    
                    Console.WriteLine($"\nTotal de reservas: {reservas.Count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al listar reservas: {ex.Message}");
            }
        }
    }
}
