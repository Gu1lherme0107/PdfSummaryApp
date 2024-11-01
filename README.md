# PdfSummaryApp

Este programa em C# é uma aplicação de console que permite ao usuário processar arquivos PDF e obter um resumo do texto contido neles. Abaixo está uma visão geral das principais funcionalidades.

## Funcionalidades

1. **Menu Interativo**  
   - O programa exibe um menu com três opções:
     1. Inserir caminho do arquivo PDF.
     2. Ver dúvidas frequentes.
     3. Sair.

2. **Processamento de PDF**  
   - Quando o usuário escolhe a opção de inserir o caminho do arquivo PDF, o programa solicita o caminho completo do arquivo.
   - Verifica se o caminho fornecido não está vazio e se o arquivo existe.
   - Se o arquivo for encontrado, o programa extrai o texto do PDF usando a biblioteca `UglyToad.PdfPig`.
   - O texto extraído é enviado para uma API de resumo de texto (Hugging Face) para gerar um resumo.

3. **Dúvidas Frequentes**  
   - O programa possui uma seção de dúvidas frequentes onde o usuário pode selecionar perguntas comuns e obter respostas sobre como usar o programa.

4. **Mensagens de Alerta**  
   - O programa exibe mensagens de alerta em cores diferentes (amarelo e vermelho) para indicar erros ou opções inválidas.

## Fluxo de Execução

- O usuário interage com o menu principal.
- Dependendo da escolha, o programa processa o PDF, exibe dúvidas frequentes ou encerra a execução.
- Durante o processamento do PDF, o texto é extraído e resumido, e os resultados são exibidos no console.

## Bibliotecas Utilizadas

- **RestSharp**: Para fazer requisições HTTP à API de resumo de texto.
- **UglyToad.PdfPig**: Para abrir e extrair texto de arquivos PDF.

## Objetivo

Este programa é útil para quem precisa extrair e resumir texto de arquivos PDF de forma rápida e interativa diretamente no console.
