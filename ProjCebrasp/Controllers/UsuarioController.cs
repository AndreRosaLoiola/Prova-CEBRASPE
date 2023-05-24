
using System.IO;
using System.Collections.Generic;
using ProjCebrasp.Models; 
using Microsoft.AspNetCore.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text;
using ProjCebrasp.Repositorio;


namespace ProjCebrasp.Controllers
{
    public class UsuarioController : Controller
    {


        private readonly RepositorioCebrasp _pessoaRepository;

        public UsuarioController(RepositorioCebrasp pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        // GET: Pessoa/Cadastrar
        public ActionResult Cadastrar()
        {
            return View();
        }

        // POST: Pessoa/Cadastrar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                _pessoaRepository.InserirUsuario(usuario);
                return RedirectToAction("Listar");
              
            }

            return View(usuario);
        }

        // GET: Pessoa/Listar
        public ActionResult Listar()
        {
            List<UsuarioModel> usuario = _pessoaRepository.ListarUsuario();
            return View(usuario);
        }

        // GET: Pessoa/Relatorio
        public ActionResult Relatorio()
        {
            List<UsuarioModel> usuarios = _pessoaRepository.ListarUsuario();

            // Criar um novo documento PDF
            Document document = new Document();

            // Definir o caminho do arquivo PDF para salvar ou retornar como download
            string filePath = "C:\\Users\\andre\\Documents\\relatorios\\relatorio.pdf";


            // Crie um escritor de PDF para gravar no arquivo
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

            // Abra o documento para escrever o conteúdo
            document.Open();

            // Adicione o conteúdo ao documento
            Paragraph paragraph = new Paragraph("Relatório de Usuários");
            document.Add(paragraph);

            // Adicione os dados dos usuários ao documento
            foreach (var usuario in usuarios)
            {
                // Adicione os detalhes do usuário ao document
                document.Add(new Paragraph($"CPF: {usuario.Cpf}"));
                document.Add(new Paragraph($"Nome: {usuario.Nome}"));
                document.Add(new Paragraph($"Identidade: {usuario.Identidade}"));
                document.Add(new Paragraph($"Email: {usuario.Email}"));
                document.Add(new Paragraph("--------------------------------------------------"));
            }

            document.Close();

            // Redirecione para a página "Listar"
            return RedirectToAction("Listar"); ;
        }
    }


}


