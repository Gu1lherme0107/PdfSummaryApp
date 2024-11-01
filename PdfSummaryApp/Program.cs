using RestSharp;
using UglyToad.PdfPig;
using System;
using System.IO;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Inserir caminho do arquivo PDF");
            Console.WriteLine("2. Ver dúvidas frequentes");
            Console.WriteLine("3. Sair");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await ProcessPdfAsync();
                    break;
                case "2":
                    ShowFaq();
                    break;
                case "3":
                    Console.WriteLine("Saindo...");
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    static async Task ProcessPdfAsync()
    {
        Console.WriteLine("Por favor, insira o caminho completo do arquivo PDF:");
        string pdfPath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(pdfPath))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("O caminho do arquivo PDF não pode estar vazio.");
            Console.ResetColor();
            return;
        }

        if (!File.Exists(pdfPath))
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("O arquivo especificado não foi encontrado.");
            Console.ResetColor();
            return;
        }

        string extractedText = ExtractTextFromPdf(pdfPath);
        Console.WriteLine("\nTexto Extraído:\n" + extractedText);

        string summary = await SummarizeTextAsync(extractedText);
        Console.WriteLine("\nResumo do Texto (JSON):\n" + summary);
    }

    static void ShowFaq()
    {
        Console.WriteLine("Dúvidas Frequentes:");
        Console.WriteLine("1. Como inserir o caminho do arquivo PDF?");
        Console.WriteLine("2. O que fazer se o arquivo não for encontrado?");
        Console.WriteLine("3. O que fazer com o texto extraído?");
        Console.WriteLine("4. Voltar ao menu principal");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Console.WriteLine("Para inserir o caminho do arquivo PDF, forneça o caminho completo. Exemplo: C:/users/seu_usuario/Downloads/arquivo.pdf.");
                break;
            case "2":
                Console.WriteLine("Se o arquivo não for encontrado, verifique se o caminho está correto e se o arquivo realmente existe.");
                break;
            case "3":
                Console.WriteLine("Com o texto extraído, você pode copiá-lo e usá-lo em um assistente como o ChatGPT para ajustes ou resumos.");
                break;
            case "4":
                return;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Opção inválida. Tente novamente.");
                Console.ResetColor();
                break;
        }
    }

    static string ExtractTextFromPdf(string pdfPath)
    {
        using (var pdf = PdfDocument.Open(pdfPath))
        {
            string text = "";
            foreach (var page in pdf.GetPages())
            {
                text += page.Text;
            }
            return text;
        }
    }

    static async Task<string> SummarizeTextAsync(string text)
    {
        var client = new RestClient("https://api-inference.huggingface.co/models/facebook/bart-large-cnn");
        var request = new RestRequest("/", Method.Post);
        request.AddHeader("Authorization", "Bearer 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");
        request.AddHeader("Content-Type", "application/json");

        var requestBody = new
        {
            inputs = text,
            parameters = new
            {
                max_length = 200
            }
        };

        request.AddJsonBody(requestBody);

        RestResponse response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return response.Content;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Erro na requisição POST:");
            Console.WriteLine("Status Code: " + response.StatusCode);
            Console.WriteLine("Content: " + response.Content);
            Console.WriteLine("Error Message: " + response.ErrorMessage);
            Console.ResetColor();
            return "Erro ao resumir o texto.";
        }
    }
}

