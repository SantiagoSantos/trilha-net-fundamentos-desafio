using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {    
            Console.WriteLine("Digite a placa do veículo para estacionar:");

            string placa = Console.ReadLine();
            
            if (!string.IsNullOrEmpty(placa) && ValidaPlaca(placa))
            {
                veiculos.Add(FormataPlaca(placa));
                Console.WriteLine("Veículo adicionado com sucesso.");
            }
            else
            {
                Console.WriteLine("Placa inválida. Por favor, digite uma placa válida.");
            }
        }

        public void RemoverVeiculo()
        {
            if (veiculos.Count == 0)
            {
                Console.WriteLine("Não há veículos para remover.");
                return;
            }

            Console.WriteLine("Digite a placa do veículo para remover:");
            
            string placa = Console.ReadLine();
            if (string.IsNullOrEmpty(placa) || !ValidaPlaca(placa))
            {
                Console.WriteLine("Placa inválida. Por favor, digite uma placa válida.");
                return;
            }

            string placaFormatada = FormataPlaca(placa);

            if (veiculos.Any(x => x.ToUpper() == placaFormatada.ToUpper()))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                int horas = 0;
                decimal valorTotal = 0; 
                string horasInput = Console.ReadLine();
                while (!int.TryParse(horasInput, out horas) || horas < 0)
                {
                    Console.WriteLine("Por favor, digite um número válido de horas:");
                    horasInput = Console.ReadLine();
                }

                valorTotal = precoInicial + (precoPorHora * horas);

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: {FormataValorMonetario(valorTotal)}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (var veiculo in veiculos)
                {
                    Console.WriteLine(veiculo);
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }

        private bool ValidaPlaca(string placa)
        {
            string placaLimpa = Regex.Replace(placa, @"[^a-zA-Z0-9]", "").ToUpper();
            string padraoAntigo = @"^[A-Z]{3}[0-9]{4}$";
            string padraoNovo = @"^[A-Z]{3}[0-9][A-Z][0-9]{2}$";
            return Regex.IsMatch(placaLimpa, padraoAntigo) || Regex.IsMatch(placaLimpa, padraoNovo);                        
        }

        private string FormataPlaca(string placa)
        {
            string placaLimpa = Regex.Replace(placa, @"[^a-zA-Z0-9]", "").ToUpper();
            if (placaLimpa.Length == 7)
            {
                return placaLimpa.Insert(3, "-");
            }
            else if (placaLimpa.Length == 8)
            {
                return placaLimpa.Insert(3, "-").Insert(5, "-");
            }
            return placa;
        }

        private int GetVeiculoCount()
        {
            return veiculos.Count;
        }

        private string FormataValorMonetario(decimal valor)
        {
            return valor.ToString("C2");
        }
    }
}
