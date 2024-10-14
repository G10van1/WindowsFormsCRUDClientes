using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using WindowsFormsApp1.Model;

namespace WindowsFormsApp1.Repositories
{
    public class ClienteRepository
    {
        private string filePath = "clientes.json";

        public List<Cliente> CarregarClientes()
        {
            if (!File.Exists(filePath))
            {
                return new List<Cliente>();
            }

            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<Cliente>>(json);
        }

        public void SalvarClientes(List<Cliente> clientes)
        {
            string json = JsonConvert.SerializeObject(clientes, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

        public void AdicionarCliente(Cliente cliente)
        {
            var clientes = CarregarClientes();
            cliente.Id = clientes.Any() ? clientes.Max(c => c.Id) + 1 : 1;
            clientes.Add(cliente);
            SalvarClientes(clientes);
        }

        public void EditarCliente(Cliente clienteAtualizado)
        {
            var clientes = CarregarClientes();
            var clienteExistente = clientes.FirstOrDefault(c => c.Id == clienteAtualizado.Id);

            if (clienteExistente != null)
            {
                clienteExistente.Nome = clienteAtualizado.Nome;
                clienteExistente.Endereço = clienteAtualizado.Endereço;
                clienteExistente.Telefone = clienteAtualizado.Telefone;
                clienteExistente.Email = clienteAtualizado.Email;
                SalvarClientes(clientes);
            }
        }

        public void RemoverCliente(int id)
        {
            var clientes = CarregarClientes();
            var clienteARemover = clientes.FirstOrDefault(c => c.Id == id);

            if (clienteARemover != null)
            {
                clientes.Remove(clienteARemover);
                SalvarClientes(clientes);
            }
        }        
    }

}
