namespace GestionDeEstadosDeUnSistema
{
    internal class Program
    {
        static GestorSolicitudes gestor = new GestorSolicitudes();

        static void Main(string[] args)
        {
            Console.WriteLine("Gestor de estados de un sistema");

            int opcion;

            do
            {
                Console.WriteLine("\n---MENU---");
                Console.WriteLine("1. Registrar solicitud");
                Console.WriteLine("2. Mostrar solicitudes");
                Console.WriteLine("3. Cambiar estado");
                Console.WriteLine("4. Buscar por ID");
                Console.WriteLine("0. Salir");
                Console.Write("Opcion: ");

                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        RegistrarSolicitud();
                        break;
                    case 2:
                        MostrarSolicitudes();
                        break;
                    case 3:
                        CambiarEstado();
                        break;
                    case 4:
                        BuscarPorId();
                        break;
                    case 0:
                        Console.WriteLine("¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción inválida");
                        break;
                }

            } while (opcion != 0);
        }

        static void RegistrarSolicitud()
        {
            Console.Write("Nombre del cliente: ");
            string nombre = Console.ReadLine();
            Console.Write("Descripción de la solicitud: ");
            string descripcion = Console.ReadLine();

            gestor.RegistrarSolicitud(nombre, descripcion);
            Console.WriteLine("Solicitud registrada exitosamente\n");
        }

        static void MostrarSolicitudes()
        {
            Console.WriteLine("\n");
            gestor.MostrarSolicitudes();
        }

        static void CambiarEstado()
        {
            Console.Write("ID de la solicitud: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("\nEstados disponibles:");
            Console.WriteLine("0. Pendiente");
            Console.WriteLine("1. En_proceso");
            Console.WriteLine("2. Completada");
            Console.WriteLine("3. Cancelada");
            Console.Write("Selecciona el nuevo estado: ");
            int estadoOpcion = int.Parse(Console.ReadLine());

            EstadoSolicitud nuevoEstado = (EstadoSolicitud)estadoOpcion;
            gestor.CambiarEstado(id, nuevoEstado);
            Console.WriteLine("✓ Estado actualizado exitosamente\n");
        }

        static void BuscarPorId()
        {
            Console.Write("ID de la solicitud a buscar: ");
            int id = int.Parse(Console.ReadLine());

            Solicitud solicitud = gestor.BuscarPorId(id);
            if (solicitud != null)
            {
                Console.WriteLine("\n");
                solicitud.MostrarInfo();
            }
            else
            {
                Console.WriteLine("Solicitud no encontrada\n");
            }
        }
    }

    public enum EstadoSolicitud
    {
        Pendiente,
        En_proceso,
        Completada,
        Cancelada
    }

    public class Solicitud
    {
        public int IdSolicitud { get; set; }
        public string NombreCliente { get; set; }
        public string DescripcionSolicitud { get; set; }
        public EstadoSolicitud Estado { get; set; }

        public void MostrarInfo()
        {
            Console.WriteLine("---INFORMACION---");
            Console.WriteLine($"ID: {IdSolicitud}");
            Console.WriteLine($"Nombre Cliente: {NombreCliente}");
            Console.WriteLine($"Descripción: {DescripcionSolicitud}");
            Console.WriteLine($"Estado: {Estado}");
        }
    }

    public class GestorSolicitudes
    {
        private List<Solicitud> solicitudes = new List<Solicitud>();
        private int proximoId = 1;

        public void RegistrarSolicitud(string nombreCliente, string descripcion)
        {
            solicitudes.Add(new Solicitud
            {
                IdSolicitud = proximoId++,
                NombreCliente = nombreCliente,
                DescripcionSolicitud = descripcion,
                Estado = EstadoSolicitud.Pendiente
            });
        }

        public void MostrarSolicitudes()
        {
            if (solicitudes.Count == 0)
            {
                Console.WriteLine("No hay solicitudes registradas");
                return;
            }

            foreach (var solicitud in solicitudes)
            {
                solicitud.MostrarInfo();
                Console.WriteLine();
            }
        }

        public Solicitud BuscarPorId(int id)
        {
            return solicitudes.FirstOrDefault(s => s.IdSolicitud == id);
        }

        public void CambiarEstado(int id, EstadoSolicitud nuevoEstado)
        {
            Solicitud solicitud = BuscarPorId(id);
            if (solicitud != null)
            {
                solicitud.Estado = nuevoEstado;
            }
            else
            {
                Console.WriteLine("Solicitud no encontrada\n");
            }
        }
    }
}
