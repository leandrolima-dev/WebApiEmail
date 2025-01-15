# WebApiEmail 📧 - Enviando emails com Mailkit

Este projeto é uma API Web desenvolvida em .NET 5.0 🚀 que permite o envio de e-mails utilizando o protocolo SMTP. 
A API é configurada para utilizar o servidor de e-mails do Gmail.

## Funcionalidades

* Envio de e-mails com anexos em formato PDF 📄
* Configuração de servidor de e-mails via arquivo de configuração (`appsettings.json`) 📁
* Utilização de bibliotecas de terceiros, como MailKit e MimeKit, para lidar com o envio de e-mails 📨

## Configuração

A configuração da API é feita via arquivo `appsettings.json`, que contém as seguintes configurações:

* `SmtpSettings`: configurações do servidor de e-mails, incluindo servidor, porta, nome do remetente, e-mail do remetente, usuário e senha. 🔒
* `PdfFilePath`: caminho do arquivo PDF que será anexado ao e-mail. 📄

## Endpoints

A API possui um único endpoint, `SendEmail`, que recebe como parâmetros o e-mail do destinatário, o assunto e o corpo do e-mail. 
O endpoint utiliza as configurações do arquivo `appsettings.json` para enviar o e-mail.

## Tecnologias utilizadas

* [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0) 🚀
* [MailKit](https://github.com/jstedfast/MailKit) 📨
* [MimeKit](https://github.com/jstedfast/MimeKit) 📄
* [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) 📚

## Pré-requisitos

* .NET 5.0 instalado no sistema 📦
* Servidor de e-mails configurado (no caso, Gmail) 📧

## Instalação

1. Clone o repositório do projeto 📁
2. Instale as dependências necessárias via NuGet 📦
3. Configure o arquivo `appsettings.json` com as informações do servidor de e-mails 📁
4. Execute a API via comando `dotnet run` 🚀

## Documentação

A documentação da API é gerada automaticamente via Swashbuckle.AspNetCore e pode ser acessada via endpoint `/swagger`. 📚

